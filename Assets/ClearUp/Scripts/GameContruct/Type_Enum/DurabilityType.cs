
public enum DurabilityType :int{
    [PropertiesDescription("[<color=#BEBEBE>破烂</color>]：该锈的地方···锈没了")]
    Scrap, //破烂的      0  L1
    [PropertiesDescription("[<Color=#F5F5DC>遗弃</color>]：谁扔的炮仗？")]
    Abandoned,//遗弃的   1  L1
    [PropertiesDescription("[<color=#FFFFFF>陈旧</color>]：干干巴巴的，麻麻赖赖的，盘它！")]
    Obsolete, //陈旧的   2  L2
    [PropertiesDescription("[<color=#00FF00>普通</color>]：看起来或许有点特别？")]
    Normal, //普通的     3  L2
    [PropertiesDescription("[<color=#0000FF>崭新</color>]:会反光喔")]
    New, //崭新的        4  L3
    [PropertiesDescription("[<color=#A020F0>制式</color>]:统一出品，必属精品")]
    War, //制式的        5  L3
    [PropertiesDescription("[<color=#FF00FF>稀有</color>]:我和你说，这可是家传之宝")]
    Rare,//稀有的        6  L4 
    [PropertiesDescription("[<color=#FF0000>精良</color>]:磨了又磨，打了又打")]
    Excellent, //精良的  7  L4
    [PropertiesDescription("[<color=#FFA500>附魔</color>]:我的能量，无穷无尽")]
    Enchanted, //附魔的  8  L5
    [PropertiesDescription("[<color=#FFFF00>传说</color>]:···金色！！！！传说！！！")]
    Legendary, //传说的  9  L5
}
