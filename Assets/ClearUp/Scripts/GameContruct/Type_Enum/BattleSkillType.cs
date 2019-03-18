
public enum BattleSkillType{
    [PropertiesDescription("[招架]：\n 卸掉部分伤害并轻击，怒气获得增加")]
    Parry,//招架，标准退后，获得一定倍率的怒气，对方标准退后
    [PropertiesDescription("[格挡]：\n 卸掉大部分伤害但是不攻击，怒气获得增加")]
    Block,//格挡，减免退后，获得一定倍率的怒气，没有攻击动作
    [PropertiesDescription("[重击]：\n 加大输出")]
    Swipe,//重击，伤害倍率增加
    [PropertiesDescription("[冲撞]：\n 卸掉大部分伤害，然后轻击")]
    Impact,//冲撞，减免退后，对方标准退后
}
