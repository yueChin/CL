
[System.AttributeUsage(System.AttributeTargets.Field | System.AttributeTargets.Enum)]
public class PropertiesArmament : System.Attribute
{
    public int ArmementValue { get; private set; }
    public PropertiesArmament(int value)
    {
        ArmementValue = value;
    }
}