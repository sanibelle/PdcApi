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
        services.AddScoped<ICreateTodoUseCase, CreateTodoUseCase>();
        services.AddScoped<IDeleteTodoUseCase, DeleteTodoUseCase>();
        services.AddScoped<IGetAllTodosUseCase, GetAllTodosUseCase>();
        services.AddScoped<IToggleTodoCompleteStatusUseCase, ToggleTodoCompleteStatusUseCase>();

        return services;
    }
}