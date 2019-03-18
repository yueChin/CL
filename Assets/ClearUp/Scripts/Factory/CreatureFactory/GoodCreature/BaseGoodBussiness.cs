using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBussinessGoodCreature : BaseGoodCreature ,IBussinessCreature,IBussinessCreatureAction
{
    protected int _GoodsLevel;
    protected int _GoodsNumber;
    protected int _NumberOfItem;
    protected BaseGoods _BaseGoods;
    protected BussinessInventory _BussinessInventory;
    protected Sprite _DialogPortrait;//商人立绘
    protected string _DialogTalk;//商店对话

    public BaseBussinessGoodCreature(OccupationType occupationType) : base(occupationType)
    {
        EventCenter.AddListener(EventType.ExitDialog,Hide);
    }

    ~BaseBussinessGoodCreature()
    {
        EventCenter.RemoveListener(EventType.ExitDialog, Hide);
    }
    public virtual void AdjuestItemLevel() { _GoodsLevel = 1; }
    public virtual void AdjuestItemNumber() { _GoodsNumber = 1; }
    public virtual void ShowStore()
    {
        _BussinessInventory = InventoryManager.GetInventoryManager.GetBussinessInventory(this);//获取一个空的背包
        _BussinessInventory.ClearInventory();//清理背包
        _NumberOfItem = 0;
    }
    protected virtual void StoreFreshItemAndUI(BaseGoods baseGoods)
    {
        _BussinessInventory.PutGoodIn(baseGoods);
        GameControl.GetGameControl.ShowShopItem(baseGoods);
        _NumberOfItem++;
    }
    public override void ActionAfterShow()
    {
        base.ActionAfterShow();
        //Debug.Log("对话出来吧");
        GameControl.GetGameControl.ShowShopDialog(this);
    }
}
