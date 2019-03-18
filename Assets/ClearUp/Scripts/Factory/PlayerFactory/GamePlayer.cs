using System;
using UnityEngine;

public class GamePlayer:BaseCreature,IPlayer,IBattleCreatureAction,ISmeltAction{

    public int Heart { get { return mHeart; } }
    private int mHeart = 0;//生命数
    private int mSteel;
    public GamePlayer(int heartNum = 0)
    {
        mHeart += heartNum;
        EventCenter.AddListener<Action<Sprite>>(EventType.SetIcon,SetPlayerIcon);
    }

    ~GamePlayer()
    {
        EventCenter.RemoveListener<Action<Sprite>>(EventType.SetIcon, SetPlayerIcon);
    }

    public override void Hide() { return; }
    public override void Show() { return; }

    public override void AdaptName()
    {
        _Name = GameControl.GetGameControl.PlayerType.ToString();
    }

    public override void AdaptGameObject()
    {
        _CreatureGO = FactoryManager.AssetFactory.LoadGameObject(GameControl.GetGameControl.GOPathDict.TryGet(_Name));
        //_CreatureGO.AddComponent<BallAction>();
    }

    public override void AdaptIcon()
    {
        _CreatureIcon = FactoryManager.AssetFactory.LoadSprite(GameControl.GetGameControl.SpritePathDict.TryGet(_Name));
    }

    private void SetPlayerIcon(Action<Sprite> action)
    {
        action(_CreatureIcon);
    }

    public override void BuyGoodLoseCoin(int price)
    {
        base.BuyGoodLoseCoin(price);
        GameControl.GetGameControl.CoinChangeUI(_Coins);
        //通过gamecontrol时间通知uimgr更新ui资源
    }

    public override void SellGoodGainCoin(int price)
    {
        base.SellGoodGainCoin(price);
        GameControl.GetGameControl.CoinChangeUI(_Coins);
        //通过gamecontrol时间通知uimgr更新ui资源
    }

    public void GainSteel(int steel)
    {
        mSteel += steel;
        GameControl.GetGameControl.SteelChangeUI(mSteel);
    }

    public void CaseSteel(int steel)
    {
        mSteel -= steel;
        GameControl.GetGameControl.SteelChangeUI(mSteel);
    }

    public int GetDamageValue()
    {
        return InventoryManager.GetInventoryManager.GetGearsInventory(this).GetDamageValue();
    }

    public int GetDefenceValue()
    {
        return InventoryManager.GetInventoryManager.GetGearsInventory(this).GetDefValue();
    }

    public int GetArmamentValue()
    {
        return InventoryManager.GetInventoryManager.GetGearsInventory(this).GetArmamentValue();
    }

    public GameObject GetGameObject()
    {
        return _CreatureGO;
    }

    public virtual int BattleFailLoseCoins()
    {
        int coins = UnityEngine.Random.Range(0, _Coins);
        BuyGoodLoseCoin(coins);
        return coins;
    }
    public virtual int BattleWinGainCoins(int coins)
    {
        SellGoodGainCoin(coins);
        return _Coins;
    }
    public virtual BaseGears BattleFailLoseGear()
    {        
        return InventoryManager.GetInventoryManager.GetGearsInventory(this).GainOutGear();
    }
    public virtual bool BattleWinGainGear(BaseGears baseGears)
    {
        InventoryManager.GetInventoryManager.GetPackageInventory(this).GainGoods(baseGears);
        return true; 
    }

    public string GetName()
    {
        return GameControl.GetGameControl.PlayerType.ToString();
    }

    public void AddHeart(int heart = 1)
    {
        mHeart += heart;
        GameControl.GetGameControl.AddheartUI();
    }

    public void LoseHeart(int heart = 1)
    {
        mHeart -= heart ;
        GameControl.GetGameControl.LoseHeartUI(mHeart);
    }

    public void SetParent(Transform transform)
    {
        _CreatureGO.transform.SetParent(transform);
    }

    public override void HideGameObject() { }
}
