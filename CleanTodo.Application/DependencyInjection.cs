using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Pdc.Application.Service.Todo;
using Pdc.Application.UseCase;
using System.Reflection;

namespace Pdc.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddScoped<ITodoService, TodoService>();
        services.AddScoped<ICreateTodoUseCase, CreateProgramOfSudy>();
        services.AddScoped<IDeleteTodoUseCase, DeleteProgramOfSudy>();
        services.AddScoped<IGetAllProgramOfSudyUseCase, GetAllProgramOfSudy>();
        services.AddScoped<IToggleTodoCompleteStatusUseCase, ToggleTodoCompleteStatusUseCase>();

        return services;
    }
}