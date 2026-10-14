using Autofac;
using AssettoServer.Server.Plugin;
using Microsoft.Extensions.Hosting;

namespace CarMenuPlugin;

public class CarMenuModule : AssettoServerModule<CarMenuConfiguration>
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<CarMenuService>().AsSelf().As<IHostedService>().SingleInstance();
    }
}
