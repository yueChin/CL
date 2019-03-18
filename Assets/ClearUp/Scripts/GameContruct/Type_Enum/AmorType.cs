
public enum AmorType  {
    [PropertiesDescription("[<Color=yellow>战靴</Color>]：重重的")]
    [PropertiesPartOfbody(PartOfBodyType.Foots)]
    [PropertiesArmament(4)]
    Caliga,//战靴 脚
    [PropertiesDescription("[<Color=yellow>膝甲</Color>]：膝盖中箭？过去式")]
    [PropertiesPartOfbody(PartOfBodyType.Knee)]
    [PropertiesArmament(3)]
    Poleyn,//膝甲 膝盖
    [PropertiesDescription("[<Color=yellow>腿甲</Color>]：“哐当”")]
    [PropertiesPartOfbody(PartOfBodyType.Leg)]
    [PropertiesArmament(5)]
    Cuisse,//腿甲 腿部
    [PropertiesDescription("[<Color=yellow>臀甲</Color>]：谁踹我的屁股！？！！")]
    [PropertiesPartOfbody(PartOfBodyType.Buttocks)]
    [PropertiesArmament(3)]
    Tasses,//臀甲 臀部
    [PropertiesDescription("[<Color=yellow>裙甲</Color>]：甩啊甩，甩不起来")]
    [PropertiesPartOfbody(PartOfBodyType.LowerPart)]
    [PropertiesArmament(4)]
    Skirt,//裙甲  下半身
    [PropertiesDescription("[<Color=yellow>腰甲</Color>]：上下连接！")]
    [PropertiesPartOfbody(PartOfBodyType.Waist)]
    [PropertiesArmament(4)]
    Brace, //腰甲 腰部
    [PropertiesDescription("[<Color=yellow>腹甲</Color>]：腹肌无用！")]
    [PropertiesPartOfbody(PartOfBodyType.Abdomen)]
    [PropertiesArmament(3)]
    Breastplate,//腹甲    腹部
    [PropertiesDescription("[<Color=yellow>胸甲</Color>]：我说铁匠，这两个尖尖的是什么鬼？")]
    [PropertiesPartOfbody(PartOfBodyType.Chest)]
    [PropertiesArmament(5)]
    Corselet, //胸甲  胸部
    [PropertiesDescription("[<Color=yellow>肩甲</Color>]：身体压低，重心放稳，撞！")]
    [PropertiesPartOfbody(PartOfBodyType.Torso)]
    [PropertiesArmament(4)]
    Pauldron,//肩甲   肩部
    [PropertiesDescription("[<Color=yellow>锁甲</Color>]：穿着不舒服")]
    [PropertiesPartOfbody(PartOfBodyType.Shoulder)]
    [PropertiesArmament(4)]
    Hauberk,//锁甲   躯干
    [PropertiesDescription("[<Color=yellow>臂甲</Color>]：手提不动了")]
    [PropertiesPartOfbody(PartOfBodyType.Arm)]
    [PropertiesArmament(4)]
    Gardebras,//臂甲  手臂
    [PropertiesDescription("[<Color=yellow>护手</Color>]：谁设计的馒头？？？？")]
    [PropertiesPartOfbody(PartOfBodyType.Hands)]
    [PropertiesArmament(3)]
    Armguard,//护手   手
    [PropertiesDescription("[<Color=yellow>头盔</Color>]：我···我看不见啦")]
    [PropertiesPartOfbody(PartOfBodyType.Head)]
    [PropertiesArmament(5)]
    Helmet,//头盔 头部
}
