
public enum SkillType {
    [PropertiesDescription("[速降]：\n 在空中再次点击悬停，然后下滑快速下落")]
    DashDown,//快速下落，升级增加判断时间
    [PropertiesDescription("[双连跳]：\n 在空中是再次点击悬停，然后点击跳第二次")]
    DoubleJump,//双连跳，升级增加判断时间
    [PropertiesDescription("[钢筋铁骨]：\n 防御永久增加")]
    IronBody,//钢筋铁骨，防御按一定倍率增加
    [PropertiesDescription("[巨人之力]：\n 永久增加出力")]
    TitanForce,//巨人之力，攻击按一定倍率增加
}
