using System.Reflection;
using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using OA.Data.Entities;
using OA.Repository.Context;

namespace OA.Web.ConfigAutofac;
public static class AutofacExtensions
{
    public static void AutofacConfig(this ContainerBuilder builder)
    {

        builder.RegisterAssemblyTypes(Assembly.Load("OA.Repository"))
        .Where(t => t.Name.EndsWith("Repository"))
        .AsImplementedInterfaces()
        .InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(Assembly.Load("OA.Service"))
        .Where(t => t.Name.EndsWith("Service"))
        .AsImplementedInterfaces()
        .InstancePerLifetimeScope();

        builder.RegisterType<DataContext>().InstancePerLifetimeScope();
        builder.Register(c => new UserStore<ApplicationUser>(c.Resolve<DataContext>())).As<IUserStore<ApplicationUser>>().InstancePerRequest()
        .OnRelease(x =>
        {
            x.Dispose();
        }).InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(Assembly.Load("OA.Service"))
        .Where(t => t.Name.EndsWith("Mapper"))
        .AsImplementedInterfaces()
        .InstancePerLifetimeScope();
    }
}