using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GoodVendor : BaseBussinessGoodCreature
{

    public GoodVendor(OccupationType occupationType) : base(occupationType) { }

    public override void AdaptCoins()
    {
        _Coins = Random.Range(3, 10);
    }

    public override void AdjuestItemLevel()
    {
        _GoodsLevel = Random.Range(1, 3);
    }

    public override void AdjuestItemNumber()
    {
        _GoodsNumber = Random.Range(0, 3);
    }

    public override void ShowStore()
    {
        base.ShowStore();
        //随机数量的物品，
        //Debug.Log(_NumberOfItem + "最大数" + _GoodsNumber);
        while (_NumberOfItem < _GoodsNumber)
        {
            _BaseGoods = GoodsManager.GetGoodsManager.GetRandomProtection(_GoodsLevel);
            StoreFreshItemAndUI(_BaseGoods);
        }        
    }
}
