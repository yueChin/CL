using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureEventFactory  {

    public IAdventureEventAction GetAdventureEvent(AdventureEventType adventureEventType,Vector3 vector3)
    {
        IAdventureEventAction adventureEventAction = null ;
        //GoodsGain(GoodsManager.GetGoodsManager.GetRandomEquipment(Random.Range(1, 2)), vector3);
        switch (adventureEventType)
        {
            case AdventureEventType.ReduceTime:
                //ui触发特效？
                //gamecontrol修改数值？
                UIManager.GetUIManager.ShowMessage("增加时间");//介绍反射上来？
                GameControl.GetGameControl.ReduceTime(Random.Range(10, 20));
                break;
            case AdventureEventType.IncreaseTime:
                //ui触发特效？
                //gamecontrol修改数值？
                UIManager.GetUIManager.ShowMessage("减少时间");
                GameControl.GetGameControl.IncreaseTime(Random.Range(10, 20));
                break;
            case AdventureEventType.GainScore:
                UIManager.GetUIManager.ShowMessage("获得分数");
                GameControl.GetGameControl.GainScore(Random.Range(10, 100));
                break;
            case AdventureEventType.GainCoins:
                UIManager.GetUIManager.ShowMessage("捡到金币");
                GameControl.GetGameControl.GainCoins(Random.Range(1, 5));
                break;
            case AdventureEventType.LoseCoins:
                UIManager.GetUIManager.ShowMessage("丢失金币");
                GameControl.GetGameControl.LoseCoins(Random.Range(1, 5));
                break;
            case AdventureEventType.GainEquitment:
                UIManager.GetUIManager.ShowMessage("捡到了装备");
                GoodsGain(GoodsManager.GetGoodsManager.GetRandomEquipment(Random.Range(1,2)),vector3);
                break;
            case AdventureEventType.GainWeapon:
                UIManager.GetUIManager.ShowMessage("捡到了武器");
                GoodsGain(GoodsManager.GetGoodsManager.GetRandomWeapon(Random.Range(1, 5)), vector3);
                break;
        }
        FactoryManager.EventFactory.TriggerAdventureComplete();
        return adventureEventAction;
    }

    public void GoodsGain(BaseGoods baseGoods,Vector3 pos)
    {
        PackageSlot packageSlot = FactoryManager.SlotFactory.GetSlot<PackageSlot>(baseGoods);
        packageSlot.transform.position = Camera.main.ViewportToScreenPoint(Camera.main.WorldToViewportPoint(pos)); ;
        packageSlot.SlotAsBootyMove();
    }
}
