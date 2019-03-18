using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodTrader : BaseBussinessGoodCreature
{
    public GoodTrader(OccupationType occupationType) : base(occupationType) { }

    public override void AdaptCoins()
    {
        _Coins = Random.Range(5, 16);
    }

    public override void AdjuestItemLevel()
    {
        _GoodsLevel = Random.Range(2, 4);
    }

    public override void AdjuestItemNumber()
    {
        _GoodsNumber = Random.Range(1, 6);
    }

    public override void ShowStore()
    {
        base.ShowStore();
        //随机数量的物品，
        //Debug.Log(_NumberOfItem + "最大数" + _GoodsNumber);
        while (_NumberOfItem < _GoodsNumber)
        {
            if (_GoodsNumber > 3 && _NumberOfItem < 1)//一把武器
            {
                _BaseGoods = GoodsManager.GetGoodsManager.GetRandomWeapon(_GoodsLevel);
            }
            if (_NumberOfItem < _GoodsNumber * 0.5f)//其余防具和物品平分
            {
                _BaseGoods = GoodsManager.GetGoodsManager.GetRandomProtection(_GoodsLevel);
            }
            else
            {
                _BaseGoods = GoodsManager.GetGoodsManager.GetRandomItem(5);
            }
            StoreFreshItemAndUI(_BaseGoods);
        }
    }
}
