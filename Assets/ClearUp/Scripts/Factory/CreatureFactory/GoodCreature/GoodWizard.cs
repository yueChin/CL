using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodWizard : BaseBussinessGoodCreature
{
    public GoodWizard(OccupationType occupationType) : base(occupationType) { }

    public override void AdaptCoins()
    {
        _Coins = Random.Range(0, 21);
    }

    public override void AdjuestItemLevel()
    {
        _GoodsLevel = 5;
    }

    public override void AdjuestItemNumber()
    {
        _GoodsNumber = Random.Range(0, 2);
    }

    public override void ShowStore()
    {
        base.ShowStore();
        //随机数量的物品，
        //Debug.Log(_NumberOfItem + "最大数" + _GoodsNumber);
        while (_NumberOfItem < _GoodsNumber)
        {
            _BaseGoods = GoodsManager.GetGoodsManager.GetRandomSkill(_GoodsLevel);
            StoreFreshItemAndUI(_BaseGoods);
        }
    }
}
