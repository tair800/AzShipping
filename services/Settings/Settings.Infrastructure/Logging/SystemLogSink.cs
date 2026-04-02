using System.Collections.Concurrent;
using System.IO;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Core;
using Serilog.Events;
using Settings.Domain.AggregatesModel.SystemLogAggregate;

namespace Settings.Infrastructure.Logging;

/// <summary>Serilog sink that writes log events to the SystemLogs table.</summary>
public sealed class SystemLogSink : ILogEventSink, IDisposable
{
    private static readonly string[] LevelMap = ["Verbose", "Debug", "Information", "Warning", "Error", "Fatal"];
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ConcurrentQueue<SystemLog> _queue = new();
    private readonly Timer _flushTimer;
    private bool _disposed;

    public SystemLogSink(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
        _flushTimer = new Timer(FlushCallback, null, TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(2));
    }

    public void Emit(LogEvent logEvent)
    {
        if (logEvent == null) return;
        var name = logEvent.Properties.TryGetValue("SourceContext", out var ctx) ? ctx.ToString().Trim('"') : "Application";
        if (name.Length > 200) name = name[..200];
        var level = MapLevel(logEvent.Level);
        var body = FormatMessage(logEvent);
        if (body.Length > 10000) body = body[..10000];
        _queue.Enqueue(new SystemLog
        {
            CreatedAt = logEvent.Timestamp.UtcDateTime,
            Name = name,
            Level = level,
            Body = body
        });
    }

    private static string MapLevel(LogEventLevel level)
    {
        var i = (int)level;
        return i >= 0 && i < LevelMap.Length ? LevelMap[i] : level.ToString();
    }

    private void FlushCallback(object? _) => _ = FlushAsync();

    private static string FormatMessage(LogEvent logEvent)
    {
        using var sw = new StringWriter();
        logEvent.RenderMessage(sw);
        if (logEvent.Exception != null)
            sw.Write("\nException: " + logEvent.Exception);
        return sw.ToString();
    }

    private async Task FlushAsync()
    {
        if (_queue.IsEmpty) return;
        var batch = new List<SystemLog>();
        while (_queue.TryDequeue(out var log) && batch.Count < 100)
            batch.Add(log);
        if (batch.Count == 0) return;
        try
        {
            using var scope = _scopeFactory.CreateScope();
            var repo = scope.ServiceProvider.GetService<ISystemLogRepository>();
            if (repo == null) return;
            foreach (var log in batch)
                await repo.AddAsync(log);
        }
        catch (Exception ex)
        {
            try { Console.Error.WriteLine("SystemLogSink flush failed: " + ex.Message); } catch { }
        }
    }

    public void Dispose() => Dispose(true);
    private void Dispose(bool disposing)
    {
        if (_disposed) return;
        if (disposing) _flushTimer.Dispose();
        _disposed = true;
    }
}
