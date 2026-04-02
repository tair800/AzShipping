using General.Application.DTOs.Task;

using General.Domain.AggregatesModel.TaskAggregate;



namespace General.Application.Features.Tasks;



internal static class TaskInternalRelation

{

    /// <summary>

    /// Applies <see cref="TaskRelatedModule"/> + record id for both <see cref="TaskType.Internal"/> and <see cref="TaskType.Client"/>.

    /// <see cref="GeneralTask.ProjectId"/> stays null (no project FK until the product adds it). <see cref="GeneralTask.RelatedRecordId"/> still holds the project guid when module is Projects.

    /// </summary>

    public static void ApplyForCreate(GeneralTask entity, TaskType taskType, int? relatedModuleDto, Guid? relatedRecordIdDto, Guid? legacyOperationId, Guid? legacyProjectId, Guid? legacyClientId)

    {

        entity.RelatedModule = TaskRelatedModule.None;

        entity.RelatedRecordId = null;

        entity.OperationId = null;

        entity.ProjectId = null;

        entity.ClientId = legacyClientId;



        var module = (TaskRelatedModule)(relatedModuleDto ?? 0);

        var recId = relatedRecordIdDto;



        if (module == TaskRelatedModule.None && legacyOperationId.HasValue)

        {

            module = TaskRelatedModule.Operations;

            recId = legacyOperationId;

        }

        else if (module == TaskRelatedModule.None && legacyProjectId.HasValue)

        {

            module = TaskRelatedModule.Projects;

            recId = legacyProjectId;

        }



        entity.RelatedModule = module;

        entity.RelatedRecordId = recId;



        switch (module)

        {

            case TaskRelatedModule.Operations:

                entity.OperationId = recId;

                break;

            case TaskRelatedModule.Projects:

                break;

            case TaskRelatedModule.Clients:

                if (taskType == TaskType.Internal)

                    entity.ClientId = recId;

                break;

            case TaskRelatedModule.Quotes:

            case TaskRelatedModule.Requests:

            case TaskRelatedModule.Carriers:

            case TaskRelatedModule.None:

            default:

                break;

        }

    }



    public static void ApplyOnUpdate(GeneralTask entity, UpdateTaskDto dto)

    {

        var isInternal = entity.TaskType == TaskType.Internal;



        entity.OperationId = null;

        entity.ProjectId = null;

        entity.ClientId = dto.ClientId;



        var module = dto.RelatedModule.HasValue ? (TaskRelatedModule)dto.RelatedModule.Value : entity.RelatedModule;

        var recId = dto.RelatedRecordId ?? entity.RelatedRecordId;



        if (!dto.RelatedModule.HasValue && !dto.RelatedRecordId.HasValue && dto.OperationId.HasValue)

        {

            module = TaskRelatedModule.Operations;

            recId = dto.OperationId;

        }

        else if (!dto.RelatedModule.HasValue && !dto.RelatedRecordId.HasValue && dto.ProjectId.HasValue)

        {

            module = TaskRelatedModule.Projects;

            recId = dto.ProjectId;

        }



        entity.RelatedModule = module;

        entity.RelatedRecordId = recId;



        switch (module)

        {

            case TaskRelatedModule.Operations:

                entity.OperationId = recId;

                break;

            case TaskRelatedModule.Projects:

                break;

            case TaskRelatedModule.Clients:

                if (isInternal)

                    entity.ClientId = recId;

                break;

            case TaskRelatedModule.Quotes:

            case TaskRelatedModule.Requests:

            case TaskRelatedModule.Carriers:

            case TaskRelatedModule.None:

            default:

                break;

        }

    }



    public static string Label(TaskRelatedModule m) => m switch

    {

        TaskRelatedModule.Operations => "Operations",

        TaskRelatedModule.Quotes => "Quotes",

        TaskRelatedModule.Requests => "Requests",

        TaskRelatedModule.Projects => "Projects",

        TaskRelatedModule.Clients => "Clients",

        TaskRelatedModule.Carriers => "Carriers",

        _ => "—"

    };

}

