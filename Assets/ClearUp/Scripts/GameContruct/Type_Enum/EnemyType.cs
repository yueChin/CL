
public enum EnemyType  {
    [PropertiesChinaName("小偷")]
    Thief,//小偷      房子              战备L1
    [PropertiesChinaName("劫匪")]
    Robber,//劫匪     房子              战备L2
    [PropertiesChinaName("强盗")]
    Bandit,//强盗     树                战备L2 - L4
    [PropertiesChinaName("山贼")]
    Cateran,//山贼    山                战备L2 - L4
    [PropertiesChinaName("土匪")]
    Brigand,//土匪    绿块+黄块 + 树    战备L3 - L5    根据块数加强    线性 
    [PropertiesChinaName("海盗")]
    Private,//海盗    蓝块 + 山         战备L1 - L5    根据块数加强    平方
}
