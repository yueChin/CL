using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodTraveller : BaseGoodCreature
{
    public GoodTraveller(OccupationType occupationType) : base(occupationType) { }

    public override void AdaptCoins()
    {
        _Coins = Random.Range(1, 6);
    }

    public override void ActionAfterShow()
    {
        //通知gamcontrol去增加玩家的金币
        GameControl.GetGameControl.GainCoins(_Coins);
        UIManager.GetUIManager.ShowMessage(string.Format("遇到了{0},得到了{1}金币",_Name,_Coins));
        Hide();
    }
}