using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class PackageSlot : BaseSlot{

    protected override void OnEnable()
    {
        base.OnEnable();
        Destroy(this.GetComponent<GearSlot>());
        Destroy(this.GetComponent<BattleSkillSlot>());
    }

    protected override void OnSingleClick()
    {
        UIManager.GetUIManager.ShowGearSlotInfo(this.transform.position,_BaseGood.Description);      
    }

    protected override void OnDoubleClick()
    {
        //inventory物品更换
        if (_BaseGood is BaseItem)
        {
            //通知Inventory 删除物品
            if (GameControl.GetGameControl.RemovePlayerItem(_BaseGood))
            {
                BaseItem baseItem = _BaseGood as BaseItem;
                baseItem.TriggerItemEffect();
                this.gameObject.SetActive(false);
            }
        }
        if (_BaseGood is BaseGears)
        {
            //通知Inventory 更换装备
            UIManager.GetUIManager.SlotMove(PackageSlotMoveAnimation);//这个格子移到装备栏
            BaseGears baseGears = GameControl.GetGameControl.EquitPlayerGear(_BaseGood as BaseGears);
            if(baseGears != null)
            {
                //装备栏的返回
                GameControl.GetGameControl.GearReturnPack(baseGears);
            }            
        }              
    }

    /// <summary>
    /// 长按之后生成一个交易的格子，自己消失
    /// </summary>
    protected override void OnLongClick()
    {
        BussinessSlot bussinessSlot = FactoryManager.SlotFactory.GetSlot<BussinessSlot>(_BaseGood);
        bussinessSlot.PickSlot();
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// 得到物品
    /// </summary>
    /// <param name="playerPanel"></param>
    public override void OnSlotMoveAnimation(PlayerPanel playerPanel)
    {
        base.OnSlotMoveAnimation(playerPanel);
        this.transform.DOMove(playerPanel.PlayerItemPos, 1.5f).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            playerPanel.PushItemSlotIn(this);
            ObjectsPoolManager.DestroyActiveObject("UI/Slots/Slot", this.gameObject);
        });
    }

    /// <summary>
    /// 装备物品
    /// </summary>
    /// <param name="playerPanel"></param>
    public void PackageSlotMoveAnimation(PlayerPanel playerPanel)
    {
        this.transform.SetParent(UIManager.GetUIManager.GetCanvasTransform);
        this.transform.position = UIManager.GetUIManager.PlayerPanel.PlayerItemPos;
        this.transform.DOMove(playerPanel.PlayerGearsPos, 1.5f).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            playerPanel.PushGearSlotIn(FactoryManager.SlotFactory.GetSlot<GearSlot>(_BaseGood));
            ObjectsPoolManager.DestroyActiveObject("UI/Slots/Slot", this.gameObject);
        });
    }

    public void SlotAsBootyMove()
    {
        UIManager.GetUIManager.SlotMove(OnSlotMoveAnimation);
    }

    protected override void Destroy()
    {
        ObjectsPoolManager.DestroyActiveObject("UI/Slots/Slot", this.gameObject);
    }
}
