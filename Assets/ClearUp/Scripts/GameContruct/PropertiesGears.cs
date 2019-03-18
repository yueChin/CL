
[System.AttributeUsage(System.AttributeTargets.Field | System.AttributeTargets.Enum)]
public class PropertiesGear : System.Attribute
{
    public string Desc { get; private set; }
    public int ArmementValue { get; private set; }
    public PartOfBodyType PartOfBodyType { get; private set; }
    public string GearInfo { get; private set; }
    public PropertiesGear(int value, PartOfBodyType partOfBodyType, string v)//回传一个带|的string，然后分隔再设置？
    {
        Desc = v;
        ArmementValue = value;
        PartOfBodyType = partOfBodyType;
        GearInfo = string.Format("{0}|{1}|{2}", value.ToString(), partOfBodyType.ToString(), v);//这样的话获取的时候要拆分，增加cpu压力？
    }
}