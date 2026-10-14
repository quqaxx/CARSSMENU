using System.Reflection;
using AssettoServer.Server;
using AssettoServer.Server.Plugin;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace CarMenuPlugin;

public class CarMenuService : BackgroundService
{
    private readonly EntryCarManager _entryCars;
    private readonly CarMenuConfiguration _config;

    public CarMenuService(
        EntryCarManager entryCars,
        CarMenuConfiguration config,
        CSPServerScriptProvider scriptProvider)
    {
        _entryCars = entryCars;
        _config = config;

        using var stream = Assembly.GetExecutingAssembly()
            .GetManifestResourceStream("CarMenuPlugin.lua.car_menu.lua")!;
        scriptProvider.AddScript(stream, "car_menu.lua");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try { BroadcastState(); }
            catch (Exception ex) { Log.Error(ex, "CarMenu: durum yayini hatasi"); }
            await Task.Delay(1000, stoppingToken);
        }
    }

    // Trafik slotlari (AI=fixed) veya adinda yasakli kelime gecen modeller menude gorunmez
    private bool IsTraffic(EntryCar car)
    {
        if (car.AiMode.ToString() == "Fixed") return true;
        foreach (var word in _config.ExcludeNameContains)
        {
            if (!string.IsNullOrWhiteSpace(word) &&
                car.Model.Contains(word, StringComparison.OrdinalIgnoreCase)) return true;
        }
        return false;
    }

    private void BroadcastState()
    {
        foreach (var car in _entryCars.EntryCars)
        {
            if (_config.ExcludeModels.Contains(car.Model)) continue;
            if (IsTraffic(car)) continue;

            _entryCars.BroadcastPacket(new CarMenuStatePacket
            {
                Slot = car.SessionId,
                Model = car.Model,
                Skin = car.Skin,
                Taken = (byte)(car.Client != null ? 1 : 0)
            });
        }
    }
}
