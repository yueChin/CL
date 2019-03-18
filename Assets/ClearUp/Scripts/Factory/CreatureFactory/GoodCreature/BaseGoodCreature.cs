
public class BaseGoodCreature : BaseCreature, IGoodCreature
{
    public string[] Dialogs { get { return _Dialogs; } }
    protected string[] _Dialogs;
    protected OccupationType _OccupationType;
    public BaseGoodCreature(OccupationType occupationType) { _OccupationType = occupationType; }
    public override void AdaptName() { _Name = PropertiesUtils.GetNameByProperties(_OccupationType); }
    public override void AdaptGameObject()
    {
        _CreatureGO = FactoryManager.AssetFactory.LoadGameObject(GameControl.GetGameControl.GOPathDict.TryGet(_OccupationType.ToString()));
    }//默认的好人模型和头像
    public override void AdaptIcon()
    {
        _CreatureIcon = FactoryManager.AssetFactory.LoadSprite(GameControl.GetGameControl.SpritePathDict.TryGet(_OccupationType.ToString()));
    }
    public virtual void AdaptContentOfDialogue()
    {
        string str = PropertiesUtils.GetDescByProperties(_OccupationType);
        _Dialogs = str.Split('|');
    }
    public override void HideGameObject()
    {
        ObjectsPoolManager.DestroyActiveObject(GameControl.GetGameControl.GOPathDict.TryGet(_OccupationType.ToString()), _CreatureGO);
    }
}
