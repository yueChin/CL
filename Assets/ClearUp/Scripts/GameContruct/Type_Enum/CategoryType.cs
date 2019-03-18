
public enum CategoryType:int  {
    [PropertiesDescription("[<Color=#BEBEBE>农用</Color>]:一般作为农具")]
    Farm,//农具
    [PropertiesDescription("[<Color=#F5F5DC>猎具</Color>]:其实是可以用来打猎的")]
    Hunter,//猎具
    [PropertiesDescription("[<Color=#FFFFFF>防具</Color>]:不仅可以抵御猛兽的扑击，一般的武器也防的住")]
    Protection,//防具
    [PropertiesDescription("[<Color=#00FF00>战器</Color>]:快来和我打一架")]
    Battle,//战器
    [PropertiesDescription("[<Color=#0000FF>御用</Color>]:皇家用品！大概可以用来打架，大概=.=")]
    Royal,//御用
    [PropertiesDescription("[<Color=#A020F0>圣器</Color>]:精品中的精品，完美中的完美")]
    Hallows,//圣器
    [PropertiesDescription("[<Color=#FF00FF>史诗</Color>]:那些来自英雄传说的···")]
    Epic,//史诗
    [PropertiesDescription("[<Color=#FF0000>远古</Color>]:制造工艺已无法考据")]
    Antiquity,//远古
    [PropertiesDescription("[<Color=#FFA500>神赐</Color>]:天上的XX向你吐了口口水，并随手丢了个垃圾砸了你")]
    Divine,//神赐
    [PropertiesDescription("[<Color=#FFFF00>神器</Color>]:“哎，我的东西呢？”(摸摸口袋)“不可能啊，刚刚还在这的？”（摸摸裤袋）")]
    Artifact,//神器
}
