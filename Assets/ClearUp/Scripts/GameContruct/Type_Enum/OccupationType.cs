
public enum OccupationType {
    [PropertiesDescription("你好")]
    [PropertiesChinaName("路人")]
    Traveller = 0, //路人     房子 + 生物
    [PropertiesDescription("我需要帮助|···帮助|····助|·····")]
    [PropertiesChinaName("市民")]
    Citizen = 1, //市民       房子
    [PropertiesDescription("来点什么？|你需要什么？|不来点？|你这是？···找茬？|可以")]
    [PropertiesChinaName("铁匠")]
    Blacksmith = 2,//铁匠      房子          出售中-高等装备
    [PropertiesDescription("欢迎！|要点什么吗？|有什么需要帮忙的吗？|点的爽吗？|爽吗点点？")]
    [PropertiesChinaName("商人")]
    Trader = 3,//商人         房子 + 生物   出售中等装备 
    [PropertiesDescription("价廉物美啦|再买点吧！|还要吗？|这是作甚？！！|哼！")]
    [PropertiesChinaName("小贩")]
    Vendor = 4,//小贩         所有          出售低等装备 
    [PropertiesDescription("无上神力！无可匹敌！|再点信不信我灭了你|找死！")]
    [PropertiesChinaName("巫师")]
    Wizard = 5,//巫师         生物 山 树    学习地图技能
    [PropertiesDescription("啊哈！|力量！|攻击！|莫挨老子！")]
    [PropertiesChinaName("勇士")]
    Warrior = 6,//勇士        生物 山 树    学习战斗技能
}
