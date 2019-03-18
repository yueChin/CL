using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodWarrior : BaseBussinessGoodCreature
{
    public GoodWarrior(OccupationType occupationType) : base(occupationType) { }

    public override void AdaptCoins()
    {
        _Coins = Random.Range(5, 16);
    }

    public override void AdjuestItemLevel()
    {
        _GoodsLevel = 5;//技能等级都是5
    }

    public override void AdjuestItemNumber()
    {
        _GoodsNumber = Random.Range(1, 5);
    }

    public override void ShowStore()
    {
        base.ShowStore();
        //随机数量的物品，
        //Debug.Log(_NumberOfItem + "最大数" + _GoodsNumber);
        while (_NumberOfItem < _GoodsNumber)
        {
            _BaseGoods = GoodsManager.GetGoodsManager.GetRandomBattleSkill(_GoodsLevel);
            StoreFreshItemAndUI(_BaseGoods);
        }
    }
}
