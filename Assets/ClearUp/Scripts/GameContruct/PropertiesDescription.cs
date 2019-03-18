
[System.AttributeUsage(System.AttributeTargets.Field | System.AttributeTargets.Enum)]
public class PropertiesDescription : System.Attribute
{
    public string Desc { get; private set; }
    public PropertiesDescription(string v)
    {
        Desc = v;
    }
}

[System.AttributeUsage(System.AttributeTargets.Field | System.AttributeTargets.Enum)]
public class PropertiesChinaName : System.Attribute
{
    public string NameDesc { get; private set; }
    public PropertiesChinaName(string v)
    {
        NameDesc = v;
    }
}