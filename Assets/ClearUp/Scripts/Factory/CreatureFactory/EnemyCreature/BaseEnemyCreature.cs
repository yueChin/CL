
using UnityEngine;

public class BaseEnemyCreature : BaseCreature,IEnemyCreature,IBattleCreatureAction
{
    protected EnemyType _EnemyType;
    protected GearsInventory _GearsInventory;
    protected SkillsInventory _SkillsInventory;
    protected int _LevelOfDanger;
    protected int _ValueOfDamage;
    protected int _ValueOfDefence;
    protected int _NumberOfGears;
    protected int _NumberOfSkill;
    //装备系统
    //武备值
    public BaseEnemyCreature(EnemyType enemyType)
    {
        _EnemyType = enemyType;
        EventCenter.AddListener(EventType.LevelBattle,Hide);
    }

    ~BaseEnemyCreature() {
        EventCenter.RemoveListener(EventType.LevelBattle, Hide);
    }

    public virtual void AdaptEquipment()
    {
        _ValueOfDamage = 0;
        _ValueOfDefence = 0;
        _NumberOfGears = 0;
        int Capacity = 0;
        float maxValue = _LevelOfDanger > 2 ? _LevelOfDanger * Random.Range(30, 60): _LevelOfDanger * Random.Range(10, 30);
        _GearsInventory = InventoryManager.GetInventoryManager.GetGearsInventory(this);
        _GearsInventory.ClearInventory();
        Capacity = _GearsInventory.GetMaxCapacity();        
        //去InventoryMgr里拿个装备栏（就是角色栏）对象池，没有activeself 就新add一个
        //去weaponMgr里拿武器填到装备栏,武备值小于武备上限就继续增加装备，直到下一件装备会突破
        //去protectorMgr里拿防具填到装备栏
        GearsInventoryAddGear(GoodsManager.GetGoodsManager.GetRandomWeapon(_LevelOfDanger));
        //即使装满装备栏还到武备上限，但是装备栏未满（涉及到好装备更换差装备要这么做？），也跳出，玩家装备栏不能这么干
        
        for (int i =0; i < _LevelOfDanger * 10 && _ValueOfDamage + _ValueOfDefence < maxValue && _NumberOfGears < Capacity;i++)
        {
            GearsInventoryAddGear(GoodsManager.GetGoodsManager.GetRandomProtection(_LevelOfDanger));
        }
    }

    protected virtual void GearsInventoryAddGear(BaseGears baseGears)
    {
        //Debug.Log(baseGears.Name);
        if (_GearsInventory.PutGearIn(baseGears))
        {           
            if (baseGears is IWeaponAction) { _ValueOfDamage = (baseGears as IWeaponAction).GetDamageValue(); }
            if (baseGears is IProtectionAction) { _ValueOfDefence += (baseGears as IProtectionAction).GetDefenseValue(); }
            _NumberOfGears++;
        }       
    }
    public override void AdaptName() { _Name = PropertiesUtils.GetNameByProperties(_EnemyType); }
    public override void AdaptGameObject()
    {
        _CreatureGO = FactoryManager.AssetFactory.LoadGameObject(GameControl.GetGameControl.GOPathDict.TryGet(_EnemyType.ToString()));
    }//默认敌人
    public override void AdaptIcon()
    {
        _CreatureIcon = FactoryManager.AssetFactory.LoadSprite(GameControl.GetGameControl.SpritePathDict.TryGet(_EnemyType.ToString()));
    }//默认敌人头像
    public override void AdaptCoins() { _Coins = _LevelOfDanger * Random.Range(3 * _LevelOfDanger, 5 * _LevelOfDanger); }
    public virtual void AdaptLevel() { _LevelOfDanger = 1; }
    public virtual void AdjuestBattleSkill()
    {
        _NumberOfSkill = _LevelOfDanger * Random.Range(1, _LevelOfDanger);
        _SkillsInventory = InventoryManager.GetInventoryManager.GetSkillInventory(this);
        _SkillsInventory.ClearInventory();
        for (int i = 0;i< _NumberOfSkill;i++ )
        {
            //.Log(_NumberOfSkill+"调整技能");
            _SkillsInventory.LearnSkill(GoodsManager.GetGoodsManager.GetRandomBattleSkill(_LevelOfDanger));
        }
    }
    public override void HideGameObject()
    {
        ObjectsPoolManager.DestroyActiveObject(GameControl.GetGameControl.GOPathDict.TryGet(_EnemyType.ToString()), _CreatureGO);
    }
    public virtual int GetArmamentValue() { return _ValueOfDamage + _ValueOfDefence; }
    public virtual int GetDamageValue() { return _ValueOfDamage; }
    public virtual int GetDefenceValue() { return _ValueOfDefence; }
    public GameObject GetGameObject() { return _CreatureGO; }
    public virtual int BattleFailLoseCoins()
    {
        int coins = Random.Range(0, _Coins);
        _Coins -= coins;
        return coins;
    }
    public virtual int BattleWinGainCoins(int coins)
    {
        _Coins += coins;
        return _Coins;
    }
    public virtual BaseGears BattleFailLoseGear()
    {
        return _GearsInventory.GainOutGear();
    }
    public virtual bool BattleWinGainGear(BaseGears baseGears)
    {
        return _GearsInventory.PutGearIn(baseGears);
    }
    public override void ActionAfterShow()
    {
        UIManager.GetUIManager.ShowMessage(string.Format("遇到了{0},进入战斗", _Name));
        GameControl.GetGameControl.TriggerBattle(this);
    }
    public string GetName()
    {
        return _EnemyType.ToString();
    }
}
