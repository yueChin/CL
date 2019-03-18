using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class BussinessSlot : BaseSlot {
    private bool mIsBePick;
    private Vector2 FingerPos;

    protected override void OnEnable()
    {
        base.OnEnable();
        mIsBePick = false;
    }

    protected override void Update()
    {
        base.Update();
        if (mIsBePick)
        {
            //如果我们捡起了物品，我们就要让物品跟随鼠标
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                UIManager.GetUIManager.GetCanvasTransform as RectTransform, Input.mousePosition, null, out FingerPos);
            this.transform.localPosition.Set(FingerPos.x + 10f, FingerPos.y - 10f, 0);
        }
    }

    private void OnDisable()
    {
        ObjectsPoolManager.DestroyActiveObject("BusinessSlot", this.gameObject);
    }

    protected override void OnSingleClick()
    {
        UIManager.GetUIManager.ShowItemSlotInfo(this.transform.position, _BaseGood.Description);//通知uimgr显示物品信息
    }
    #region 购买物品
    protected override void OnDoubleClick()
    {
        //当玩家的金币变化的时候，要有音效，铁块也是一样，脚本方法OnvalueChange应该在玩家的钱包上
        //通知背包啥的购买物品，扣钱，
        //玩家背包物品push，商店背包物品remove，玩家金钱减少
        if (GameControl.GetGameControl.BuyGood(_BaseGood)) //判断是否购买成功，成功后更新UI
        {
            UIManager.GetUIManager.SlotMove(OnSlotMoveAnimation);
        }
        else
        {
            _AudioSource.clip = FactoryManager.AssetFactory.LoadAudioClip("Audios/WalltEmpty");
            _AudioSource.Play();
        }
    }

    public override void OnSlotMoveAnimation(PlayerPanel playerPanel)
    {
        base.OnSlotMoveAnimation(playerPanel);
        this.transform.DOMove(playerPanel.GetEndPackagePos(_BaseGood), 0.5f).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            if (_BaseGood is BaseItem)
            {
                PackageSlot packageSlot = FactoryManager.SlotFactory.GetSlot<PackageSlot>(_BaseGood);
                playerPanel.PushItemSlotIn(packageSlot);
            }
            else if (_BaseGood is BaseGears)
            {
                //已有的部位如何移除？
                GearSlot gearSlot = FactoryManager.SlotFactory.GetSlot<GearSlot>(_BaseGood);
                playerPanel.PushGearSlotIn(gearSlot);
            }
            else if (_BaseGood is BaseSkill)
            {
                BaseSkill baseSkill = _BaseGood as BaseSkill;
                GameControl.GetGameControl.PlayerSkillLevelUp(baseSkill.SkillLevel,baseSkill.Name);
                //buygood已经把生物的技能栏弄好了，所以这里要完成的是显示部分，不在skillInventory通知uimgr更新是因为只有玩家有 PlayerPanel
            }               
            ObjectsPoolManager.DestroyActiveObject("BusinessSlot", this.gameObject);
        });
    }
    #endregion
    #region 出售物品
    public void PickSlot()
    {
        mIsBePick = true;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (!mIsBePick) return;
        base.OnPointerUp(eventData);
        if (IsHereShop())
        {
            if (!SellItem())//如果出售失败的话，显示退回的动画
            {
                UIManager.GetUIManager.SlotMove(SlotReturnAnimation);
                _AudioSource.clip = FactoryManager.AssetFactory.LoadAudioClip("Audios/WalltEmpty");
                _AudioSource.Play();
            }
        }
        else
        {
            ObjectsPoolManager.DestroyActiveObject("BusinessSlot", this.gameObject);
        }
    }

    /// <summary>
    /// 是否被拖拽到了商店上
    /// </summary>
    /// <returns></returns>
    private bool IsHereShop()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        foreach (RaycastResult rr in results)
        {
            if (rr.gameObject.transform.CompareTag("Shop"))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 出售物品
    /// </summary>
    private bool SellItem()
    {
        return GameControl.GetGameControl.SellGood(_BaseGood);
    }

    /// <summary>
    /// Slot退回动画
    /// </summary>
    /// <param name="playerPanel"></param>
    private void SlotReturnAnimation(PlayerPanel playerPanel)
    {
        this.transform.DOMove(playerPanel.PlayerItemPos, 0.5f).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            playerPanel.PushItemSlotIn(FactoryManager.SlotFactory.GetSlot<PackageSlot>(_BaseGood));
            ObjectsPoolManager.DestroyActiveObject("BusinessSlot", this.gameObject); 
        });
    }

    protected override void Destroy()
    {
        ObjectsPoolManager.DestroyActiveObject("BusinessSlot", this.gameObject);
    }
    #endregion
}
