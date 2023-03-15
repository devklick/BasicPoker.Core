using BasicPoker.Core.Players;

namespace BasicPoker.Core.Helpers;

public class PlayerButtonHelper
{
    private static readonly Dictionary<PlayerButtonType, PlayerButtonTypeMetadataAttribute> HandMetadata =
        EnumHelper.GetEnumMembersAndAttributes<PlayerButtonType, PlayerButtonTypeMetadataAttribute>();

    public static PlayerButtonMetadata GetMetadata(PlayerButtonType button)
    {
        var attr = HandMetadata[button];
        return new PlayerButtonMetadata(attr.Sprite, attr.MinBetMultiplier);
    }
}