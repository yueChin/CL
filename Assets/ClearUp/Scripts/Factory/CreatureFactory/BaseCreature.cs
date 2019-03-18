using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCreature:AdventureEvent,ICreature,ICreatureAction,ITradeAcion {
    public string Name { get { return _Name; } }
    public Sprite CreatureIcon { get { return _CreatureIcon; } }
    protected Sprite _CreatureIcon;
    protected GameObject _CreatureGO;
    protected int _Coins;
    protected string _Name;
    public virtual void BuyGoodLoseCoin(int price) { _Coins -= price; }
    public virtual void SellGoodGainCoin(int price) { _Coins += price; }
    public virtual bool IsEnoughCoin(int price) 
    {
        if (_Coins >= price) { return true; }
        else { return false; }
    }
    public abstract void AdaptGameObject();//默认游戏物体和头像?basecreature 无法被生成，所以不需要
    public abstract void AdaptIcon();
    /// <summary>
    /// 跳出来镜头拉近，如果不是修改以上动作，请去重写ActionAfterShow
    /// </summary>
    public override void Show()
    {
        //Debug.Log("生物出现");
        _CreatureGO.gameObject.SetActive(true);
        GameControl.GetGameControl.CloseView(_CreatureGO.transform.position);
        _CreatureGO.transform.localScale = Vector3.zero;
        _CreatureGO.transform.DOScale(1, 1f);
        _CreatureGO.transform.DOLocalJump(_CreatureGO.transform.position+Vector3.up, 3, 1, 1f).OnComplete(() => { ActionAfterShow(); });
        _CreatureGO.transform.DOLocalRotate(new Vector3(0, 540, 0), 1f,RotateMode.LocalAxisAdd).SetLoops(-1);
        //通知摄像头拉近，战斗或者对话退出后要返回原来的摄像位置       
    }
    /// <summary>
    /// 直接隐藏，如果不是修改隐藏动作，请去重写ActionBeforeHide
    /// </summary>
    public override void Hide()
    {
        //Debug.Log("生物影藏");
        ActionBeforeHide();
        _CreatureGO.transform.DOScale(0, 1f);
        _CreatureGO.transform.DOLocalJump(_CreatureGO.transform.position - new Vector3(0, 1, 0), 3, 1, 1f).OnComplete(() => { HideGameObject(); base.Hide(); });
        GameControl.GetGameControl.ReturnView();      
    }
    public abstract void HideGameObject();
    public virtual void ActionAfterShow() {
        UIManager.GetUIManager.ShowMessage(string.Format("遇到了{0}", _Name));
    }
    public virtual void ActionBeforeHide() { }
    public virtual void AdaptName() { _Name = "黑衣人"; }
    public virtual void AdaptCoins() { _Coins = 0; }//默认是0
    public virtual void AdaptPostion(Vector3 vector3)
    {
        _CreatureGO.transform.localPosition = vector3;
    }
}
