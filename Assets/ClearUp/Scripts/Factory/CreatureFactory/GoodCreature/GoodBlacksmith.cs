using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodBlacksmith : BaseBussinessGoodCreature
{
    public GoodBlacksmith(OccupationType occupationType) : base(occupationType) { }

    public override void AdaptCoins()
    {
        _Coins = Random.Range(10, 21);
    }

    public override void AdjuestItemLevel()
    {
        _GoodsLevel = Random.Range(2,6);
    }

    public override void AdjuestItemNumber()
    {
        _GoodsNumber = Random.Range(2,6);
    }

    public override void ShowStore()
    {
        base.ShowStore();
        //随机数量的物品，
        Debug.Log(_NumberOfItem + "最大数" + _GoodsNumber);
        while (_NumberOfItem < _GoodsNumber)
        {
            if (_NumberOfItem < _GoodsLevel * 0.5f)
            {
                _BaseGoods = GoodsManager.GetGoodsManager.GetRandomWeapon(_GoodsLevel);//一半武器
            }
            else
            {
                _BaseGoods = GoodsManager.GetGoodsManager.GetRandomAmor(_GoodsLevel);//其余护甲
            }            
            StoreFreshItemAndUI(_BaseGoods);
        }
    }
}
