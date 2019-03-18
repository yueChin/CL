using DG.Tweening;
using UnityEngine;

public class PlayerPanel : BasePanel {
    public Vector3 PlayerItemPos { get { return mPlayerItemsUI.transform.position; } }
    public Vector3 PlayerGearsPos { get { return mPlayerGearsUI.transform.position; } }
    public Vector3 PlayerSkillsPos { get { return mPlayerSkillsUI.transform.position; } }
    private PlayerItemsUI mPlayerItemsUI;
    private PlayerGearsUI mPlayerGearsUI;
    private PlayerSkillsUI mPlayerSkillsUI;
    private Tween mTween;
    // Use this for initialization
    void Awake() {
        mPlayerGearsUI = this.GetComponentInChildren<PlayerGearsUI>();
        mPlayerItemsUI = this.GetComponentInChildren<PlayerItemsUI>();
        mPlayerSkillsUI = this.GetComponentInChildren<PlayerSkillsUI>();
        //PlayerItemPos = UITool.FindChild<Transform>(mPlayerItemsUI.transform, "Goods").position;
        //PlayerGearsPos = UITool.FindChild<Transform>(mPlayerGearsUI.transform, "Gears").position;
        EventCenter.AddListener(EventType.StartGaming,Init);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventType.StartGaming, Init);
    }

    private void Init()
    {
        for (int i = 0; i < System.Enum.GetValues(typeof(BattleSkillType)).Length; i++)
        {
            //只有slot，没有skillinventroy
            PushSkillSlotIn(FactoryManager.SlotFactory.GetSlot<SkillSlot>(FactoryManager.GoodsFactory.GetSkill(1, ((SkillType)i))));
        }
    }

    public override void EnterPanel()
    {
        base.EnterPanel();
        //Debug.Log("主角面板进入");
        EnterAniamtion();
    }

    public override void ExitPanel()
    {
        base.ExitPanel();
        ExitAniamtion();
    }

    public void PushItemSlotIn(PackageSlot packageSlot)
    {
        mPlayerItemsUI.PushItemIn(packageSlot);
    }

    public void PushGearSlotIn(GearSlot gearSlot)
    {
        mPlayerGearsUI.PushGearIn(gearSlot);
    }

    public void PushSkillSlotIn(SkillSlot skillSlot)
    {
        mPlayerSkillsUI.PushSkillIn(skillSlot);
    }

    public Vector3 GetEndPackagePos(BaseGoods baseGoods)
    {
        if (baseGoods is BaseItem)
        {
            return PlayerItemPos;
        }
        else if (baseGoods is BaseGears)
        {
            return PlayerGearsPos;
        }
        else if (baseGoods is BaseSkill)
        {
            return PlayerSkillsPos; 
        }
        else
        {
            Debug.Log("商品类型不正确");
            return this.transform.position;
        }
    }

    #region 动画相关
    private void EnterAniamtion()
    {
        gameObject.SetActive(true);
        this.transform.SetSiblingIndex(this.transform.parent.childCount - 1);
        gameObject.transform.localScale = Vector3.zero;
        if (mTween != null) { mTween.Pause(); }
        mTween = gameObject.transform.DOScale(1, 0.3f);
    }

    private void ExitAniamtion()
    {
        mTween = gameObject.transform.DOScale(0, 0.3f).OnComplete(()=> { gameObject.SetActive(false); });
    }
    #endregion
}
