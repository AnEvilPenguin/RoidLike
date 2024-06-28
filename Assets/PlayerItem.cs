using System;
using System.Text;

public enum PlayerItemType
{
    Acceleration,
    Speed,
    Rotation,
}

public class PlayerItem
{
    public string Name;
    public string Description;

    public float Value;
    public float MinValue = 1;
    public float MaxValue = 5;

    public PlayerItemType Type;

    // TODO consider rarity enum, Luck, etc.
    // TODO consider corporations and other fluff like that?

    internal PlayerItem()
    {
        var types = Enum.GetValues(typeof(PlayerItemType));
        var choice = UnityEngine.Random.Range(0, types.Length);

        Type = (PlayerItemType)types.GetValue(choice);

        // TODO bring rarity into the mix
        Value = UnityEngine.Random.Range(MinValue, MaxValue);

        // TODO build names based on attributes somehow?
        Name = $"{Type} booster";

        var description = new StringBuilder();

        description.Append($"Using the latest in {Type} technology this booster from the Spaceship company provides modest upgrades.");
        description.AppendLine();
        description.AppendLine("Zip around in style, blow up the roids. You do you.");
        description.AppendLine();
        description.AppendLine($"Provides an increase of {Value}.");

        Description = description.ToString();
    }
}