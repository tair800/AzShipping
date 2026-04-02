namespace General.Infrastructure.Services;

public class TaskDocumentStorageOptions
{
    /// <summary>Directory under content root, e.g. <c>App_Data/task-documents</c>.</summary>
    public string RootRelativePath { get; set; } = "App_Data/task-documents";
}
