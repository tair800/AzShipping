using System.Text.RegularExpressions;
using Settings.Application.DTOs.Numeration;
using Settings.Domain.AggregatesModel.NumerationAggregate;

namespace Settings.Application.Features.Numerations;

internal static partial class NumerationGenerationEngine
{
    private static readonly StringComparer Cmp = StringComparer.OrdinalIgnoreCase;

    public static (Numeration Rule, int Score)? ResolveRule(
        IEnumerable<Numeration> candidates,
        NumerationGenerateRequestDto request)
    {
        var compatible = candidates
            .Select(x => (Rule: x, Score: Score(x, request)))
            .Where(x => x.Score >= 0)
            .OrderByDescending(x => x.Score)
            .ThenBy(x => x.Rule.IsSystemic)
            .ThenByDescending(x => x.Rule.CreatedAt)
            .FirstOrDefault();

        return compatible.Rule == null ? null : compatible;
    }

    private static int Score(Numeration rule, NumerationGenerateRequestDto request)
    {
        var score = 0;
        if (!MatchGuid(rule.CompanyId, request.CompanyId, ref score)) return -1;
        if (!MatchGuid(rule.DepartmentId, request.DepartmentId, ref score)) return -1;
        if (!MatchGuid(rule.ClientId, request.ClientId, ref score)) return -1;
        if (!MatchGuid(rule.EmployeeId, request.EmployeeId, ref score)) return -1;
        if (!MatchStr(rule.ElementCode, request.ElementCode, ref score)) return -1;
        if (!MatchStr(rule.DocumentTypeCode, request.DocumentTypeCode, ref score)) return -1;
        return score;
    }

    private static bool MatchGuid(Guid? ruleValue, Guid? requestValue, ref int score)
    {
        if (!ruleValue.HasValue) return true;
        if (!requestValue.HasValue || requestValue.Value != ruleValue.Value) return false;
        score += 10;
        return true;
    }

    private static bool MatchStr(string? ruleValue, string? requestValue, ref int score)
    {
        if (string.IsNullOrWhiteSpace(ruleValue)) return true;
        if (string.IsNullOrWhiteSpace(requestValue) || !Cmp.Equals(ruleValue.Trim(), requestValue.Trim())) return false;
        score += 10;
        return true;
    }

    public static string Render(Numeration rule, NumerationGenerateRequestDto request, int index)
    {
        var date = request.Date ?? DateTime.UtcNow;
        var tokens = new Dictionary<string, string>(Cmp)
        {
            ["year"] = date.ToString("yy"),
            ["yy"] = date.ToString("yy"),
            ["yyyy"] = date.ToString("yyyy"),
            ["month"] = date.ToString("MM"),
            ["mm"] = date.ToString("MM"),
            ["day"] = date.ToString("dd"),
            ["dd"] = date.ToString("dd"),
            ["index"] = index.ToString().PadLeft(rule.NumberOfDigits, '0'),
            ["seq"] = index.ToString().PadLeft(rule.NumberOfDigits, '0'),
            ["numerationForCode"] = request.NumerationForCode ?? string.Empty,
            ["userCode"] = request.EmployeeCode ?? string.Empty,
            ["employeeCode"] = request.EmployeeCode ?? string.Empty,
            ["clientCode"] = request.ClientCode ?? string.Empty,
            ["companyCode"] = request.CompanyCode ?? string.Empty,
            ["companyPrefix"] = request.CompanyPrefix ?? string.Empty,
            ["departmentCode"] = request.DepartmentCode ?? string.Empty,
            ["departmentPrefix"] = request.DepartmentPrefix ?? string.Empty,
            ["elementCode"] = request.ElementCode ?? string.Empty,
            ["documentTypeCode"] = request.DocumentTypeCode ?? string.Empty,
        };

        foreach (var kv in request.Tokens ?? [])
            tokens[kv.Key] = kv.Value;

        var formula = rule.Formula ?? string.Empty;
        return FormulaTokenRegex().Replace(formula, m =>
        {
            var key = m.Groups[1].Value;
            var fmt = m.Groups[2].Success ? m.Groups[2].Value : null;

            if (Cmp.Equals(key, "index") || Cmp.Equals(key, "seq"))
            {
                if (!string.IsNullOrWhiteSpace(fmt) && int.TryParse(fmt, out var width) && width > 0)
                    return index.ToString().PadLeft(width, '0');
            }

            return tokens.TryGetValue(key, out var v) ? v : string.Empty;
        });
    }

    [GeneratedRegex(@"\{\{\s*([a-zA-Z0-9_]+)(?::([0-9]+))?\s*\}\}", RegexOptions.Compiled)]
    private static partial Regex FormulaTokenRegex();
}
