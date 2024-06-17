using Mapster;
using Microsoft.AspNetCore.Identity;
using TaskManagement.Application.Features.Task.V1.Commands;
using TaskManagement.Domain.Entities.Dtos;

namespace TaskManagement.Infrastructure.Mapping.Mapste
{
    public class MapsterConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<TaskResponse, Domain.Entities.Persistence.Task>();

            config.NewConfig<CreateTaskRequest, Domain.Entities.Persistence.Task>();

            config.NewConfig<UpdateTask, UpdateTaskRequest>();

            config.NewConfig<UpdateTaskRequest, Domain.Entities.Persistence.Task>();

            config.NewConfig<IdentityUser, UserResponse>()
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.UserId, src => src.Id);
        }
    }
}
