using AssettoServer.Network.ClientMessages;

namespace CarMenuPlugin;

// Sunucu -> istemci: tek slotun durumu (arac secimi istemcide ac.reconnectTo ile yapilir, sunucuya paket gitmez)
[OnlineEvent(Key = "carMenuState")]
public class CarMenuStatePacket : OnlineEvent<CarMenuStatePacket>
{
    [OnlineEventField(Name = "slot")] public byte Slot = 0;
    [OnlineEventField(Name = "model", Size = 64)] public string Model = "";
    [OnlineEventField(Name = "skin", Size = 64)] public string Skin = "";
    [OnlineEventField(Name = "taken")] public byte Taken = 0; // 0 = musait, 1 = dolu
}
