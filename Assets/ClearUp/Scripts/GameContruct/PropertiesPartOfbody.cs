
[System.AttributeUsage(System.AttributeTargets.Field | System.AttributeTargets.Enum)]
public class PropertiesPartOfbody : System.Attribute
{
    public PartOfBodyType PartOfBodyType { get; private set; }
    public PropertiesPartOfbody(PartOfBodyType partOfBodyType)
    {
        PartOfBodyType = partOfBodyType;
    }
}