using JetBrains.Annotations;

namespace CarMenuPlugin;

[UsedImplicitly(ImplicitUseKindFlags.Assign, ImplicitUseTargetFlags.WithMembers)]
public class CarMenuConfiguration
{
    // Menu basligi (gorsel tasarim sonra eklenecek)
    public string Title { get; init; } = "ARAC MENUSU";

    // Oyuncu secti ama yeniden baglanmadiysa slot kac saniye sonra serbest kalsin
    public int ReservationSeconds { get; init; } = 20;

    // Menude gosterilmeyecek arac modelleri (ornek: ["traffic_car", "bus"])
    public List<string> ExcludeModels { get; init; } = new();

    // Adinda bu kelimelerden biri gecen modeller menude gosterilmez (trafik araclari icin)
    public List<string> ExcludeNameContains { get; init; } = new() { "traffic" };
}
