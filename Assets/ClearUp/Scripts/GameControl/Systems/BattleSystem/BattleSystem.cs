using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct RelaseSkill
{
    public RelaseSkill(string name, float one = 1,float two = 1,float three =1)
    {
        Name = name;
        DamagePower = one;
        DamageDrate = two;
        FuryGainRate = three;
    }
    public string Name { get; private set; }
    public float DamagePower { get; set; }
    public float DamageDrate { get; set; }
    public float FuryGainRate { get; set; }
}

public class BattleSystem {

    private GameControl mGameControl;
    public BattleSystem(GameControl gameControl)
    {
        mGameControl = gameControl;
    }
    //战斗系统除了控制是否进入战斗界面，也控制战斗ai，还控制战斗是否触发，逃跑是否增加？增加的话任务会比较难做
    //敌人ai暂时只会消第一个块
    private IBattleCreatureAction mBattleCreatureOne;
    private IBattleCreatureAction mBattleCreatureTwo;
    private List<BattleSkill> lPlayerSkills;//对战双方所有的战斗技能
    private List<BattleSkill> lEnemySkills;
    private ComboSkillList<BattleSkill> lPlayerBattleSkills;//对战双方搓出来的技能
    private ComboSkillList<BattleSkill> lEnemyBattleSkills;
    //释放的技能，获取下一次碰撞的攻击和碰撞怒气的获取
    
    private List<RelaseSkill> lPlayerReleaseSkillsCache;
    private List<RelaseSkill> lEnemyReleaseSkillsCache;
    private float mDeltaTime;
    private float mPlayerEnergyPower;
    private float mEnemyEnergyPower;
    private float mPlayerDamage;
    private float mEnemyDamage;
    private float mPlayerDefence;
    private float mEnemyDefence;
    private bool mIsInBattle;

    //通过搓技能或者自然增长的怒气增加一个技能，每次增加一个块ui就增加一个块，在system里的连击list里增加一个节点，判断是否是连击块
    //消块后在连击list里判断消块，通知ui删除1-3个块，然后把技能放到释放的缓存中，下次碰撞从缓存list拿第一个释放 
    public void EnterBattle(IBattleCreatureAction enemyCreature, IBattleCreatureAction playerCreature)
    {
        //.Log("进入战斗系统!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        mGameControl.GameFrezze();
        mBattleCreatureOne = playerCreature;
        mBattleCreatureTwo = enemyCreature;     
        if (lPlayerSkills == null) { lPlayerSkills = new List<BattleSkill>(); }
        if (lEnemySkills == null) { lEnemySkills = new List<BattleSkill>(); }
        if(lPlayerSkills.Count > 0)
            lPlayerSkills.Clear();
        if (lEnemySkills.Count > 0)
            lEnemySkills.Clear();
        List<BattleSkill> battleSkillsP = InventoryManager.GetInventoryManager.GetSkillInventory(playerCreature as BaseCreature).GetBattleSkills();
        List<BattleSkill> battleSkillsE = InventoryManager.GetInventoryManager.GetSkillInventory(enemyCreature as BaseCreature).GetBattleSkills();
        if(battleSkillsP != null)
            lPlayerSkills = battleSkillsP;
        if (battleSkillsE != null)
            lEnemySkills = battleSkillsE;
        if (lPlayerBattleSkills == null) { lPlayerBattleSkills = new ComboSkillList<BattleSkill>(this); }
        if (lEnemyBattleSkills == null) { lEnemyBattleSkills = new ComboSkillList<BattleSkill>(this); }
        if (lPlayerBattleSkills.Count > 0)
            lPlayerBattleSkills.Clear();
        if (lEnemyBattleSkills.Count > 0)
            lEnemyBattleSkills.Clear();
        if (lPlayerReleaseSkillsCache == null) { lPlayerReleaseSkillsCache = new List<RelaseSkill>(); }
        if (lEnemyReleaseSkillsCache == null) { lEnemyReleaseSkillsCache = new List<RelaseSkill>(); }
        if (lPlayerReleaseSkillsCache.Count > 0)
            lPlayerReleaseSkillsCache.Clear() ;
        if (lEnemyReleaseSkillsCache.Count > 0)
            lEnemyReleaseSkillsCache.Clear();       
        mPlayerDamage = 10 + mBattleCreatureOne.GetDamageValue() *
            (1 + 0.2f * (InventoryManager.GetInventoryManager.GetSkillInventory(playerCreature as GamePlayer).GetSkillLevel(SkillType.TitanForce.ToString())));
        mPlayerDefence = 10 + mBattleCreatureOne.GetDefenceValue() *
            (1 + 0.2f * (InventoryManager.GetInventoryManager.GetSkillInventory(playerCreature as GamePlayer).GetSkillLevel(SkillType.IronBody.ToString())));
        mEnemyDamage = mBattleCreatureTwo.GetDamageValue();
        mEnemyDefence = mBattleCreatureTwo.GetDefenceValue();
        //GC.Collect();
        //从BattleCreature工厂里拿?
        mIsInBattle = false;
        //Debug.Log("战斗检测进入");
        UIManager.GetUIManager.PushPanel(UIPanelType.Battle);//所有战斗部分UI通过gamecotrol来控制/触发？不通过uimgr吗？
        mGameControl.EnterBattle(mPlayerDamage + mPlayerDefence , mEnemyDamage + mEnemyDefence, //敌人咩有地图技能加持
            FactoryManager.CreatureFactory.GetBattleCreatureOne(playerCreature.GetName()), 
            FactoryManager.CreatureFactory.GetBattleCreatureTwo(mBattleCreatureTwo.GetName()),
            FactoryManager.AssetFactory.LoadSprite(GameControl.GetGameControl.SpritePathDict.TryGet(playerCreature.GetName())), 
            FactoryManager.AssetFactory.LoadSprite(GameControl.GetGameControl.SpritePathDict.TryGet(mBattleCreatureTwo.GetName())));//初始化战斗，ui部分
    }

    public void BattleEnd(string tag)
    {
        //Debug.Log("战斗检测退出");
        mIsInBattle = false;
        UIManager.GetUIManager.PushPanel(UIPanelType.Play);
        if (tag.Equals("Player"))
        {                      
            //玩家失败，丢失金钱，丢失生命,如果是玩家失败 应该直接退出，然后再去通知heart动画，heart动画是在playerPanel的，还是说把heartPanel拿到根目录下？
            mBattleCreatureOne.BattleFailLoseCoins();
            mGameControl.LevelBattle(false, string.Empty);
        }
        else
        {
            //敌人失败，增加金钱，通知任务系统，爆出装备？爆出后直接收取？还是选择性收取？背包大小是否限制？
            mGameControl.LevelBattle(true, mBattleCreatureTwo.GetName());
            mBattleCreatureOne.BattleWinGainCoins(mBattleCreatureTwo.BattleFailLoseCoins());
            BaseGears baseGears = mBattleCreatureTwo.BattleFailLoseGear();
            if(baseGears != null)
            {
                mBattleCreatureOne.BattleWinGainGear(baseGears);
                //在这里通知ui
                //战斗界面,战利品获取，在战斗界面显示还是在play界面显示？
                PackageSlot packageSlot = FactoryManager.SlotFactory.GetSlot<PackageSlot>(baseGears);
                packageSlot.transform.localPosition = Camera.main.WorldToScreenPoint(mBattleCreatureTwo.GetGameObject().transform.position);
                packageSlot.SlotAsBootyMove();
            }                          
        }       
        mGameControl.GameResume();
    }

    public void Update()
    {
        if (mIsInBattle)
        {
            if (Time.time - mDeltaTime > 0.5)
            {
                EnemySkillPowerChange(0.05f);
                PlayerSkillPowerChange(0.05f);
                mDeltaTime = Time.time;
            }
        }
    }   

    public void BattleStartFight()
    {
        mIsInBattle = true;
        mPlayerEnergyPower = 0;
        mEnemyEnergyPower = 0;
    }

    /// <summary>
    /// 释放技能，注册？玩家一
    /// </summary>
    public void ReleaseBattleSkillOne(BattleSkill battleSkill)
    {
        RelaseSkill relaseSkill = new RelaseSkill(battleSkill.Name);
        lPlayerBattleSkills.ReleaseSkill(battleSkill,ref relaseSkill);
        lPlayerReleaseSkillsCache.Add(relaseSkill);
    }

    /// <summary>
    /// 玩家二
    /// </summary>
    /// <param name="battleSkill"></param>
    public void ReleaseBattleSkillTwo(BattleSkill battleSkill)
    {
        RelaseSkill relaseSkill = new RelaseSkill(battleSkill.Name);
        lEnemyBattleSkills.ReleaseSkill(battleSkill, ref relaseSkill);
        //Debug.Log(relaseSkill.Name + ":技能名字"+ relaseSkill);
        lEnemyReleaseSkillsCache.Add(relaseSkill);
    }    

    /// <summary>
    /// 碰撞后各自回退一段距离，然后slider增加能量，能量满了增加技能
    /// </summary>
    public void CreatureKnockOther(Vector3 vector3)
    {
        float damageRateOne = 1;
        float damageRateTwo = 1;
        float distanceDrateOne = 1;
        float distanceDrateTwo = 1;
        float energyRateOne = 1;
        float energyRatetwo = 1;
        if (lPlayerReleaseSkillsCache.Count > 0)
        {
            damageRateOne *= lPlayerReleaseSkillsCache[0].DamagePower;
            distanceDrateOne *= lPlayerReleaseSkillsCache[0].DamageDrate;
            energyRateOne *= lPlayerReleaseSkillsCache[0].FuryGainRate;
            mGameControl.PlayerTalkUI(lPlayerReleaseSkillsCache[0].Name);
            lPlayerReleaseSkillsCache.RemoveAt(0);                      
        }
        if (lEnemyReleaseSkillsCache.Count > 0)
        {
            damageRateTwo *= lEnemyReleaseSkillsCache[0].DamagePower;
            distanceDrateTwo *= lEnemyReleaseSkillsCache[0].DamageDrate;
            energyRatetwo *= lEnemyReleaseSkillsCache[0].FuryGainRate;
            mGameControl.NPCTalkUI(lEnemyReleaseSkillsCache[0].Name);
            lEnemyReleaseSkillsCache.RemoveAt(0);
        }
        //通知ui向后移动多少距离
        //通知获取了多少能量-》
        //Debug.Log(mPlayerDamage);
        //Debug.Log(mEnemyDamage);
        AudioSource.PlayClipAtPoint(FactoryManager.AssetFactory.LoadAudioClip("Audios/Bench"), vector3+ new Vector3(1000,960,0));
        mGameControl.UnderAttackUI(
            CreatureTwoKnockOne(mEnemyDamage * damageRateTwo * distanceDrateOne, energyRateOne * damageRateTwo * mEnemyDamage),
            CreatureOneKnockTwo(mPlayerDamage * damageRateOne * distanceDrateTwo, energyRatetwo * damageRateOne * mPlayerDamage));
    }

    /// <summary>
    /// A击退B
    /// </summary>
    /// <param name="backDistance">B退后的距离</param>
    /// <param name="gainEnergy">B获得的能量</param>
    public float CreatureOneKnockTwo(float backDistance,float gainEnergy)
    {
        //Debug.Log("一击退二的距离" + backDistance);
        gainEnergy = gainEnergy > 0 ? gainEnergy : -gainEnergy;
        EnemySkillPowerChange(0.01f * gainEnergy);
        return (backDistance = backDistance > 0 ? backDistance : 10) * (1 - 0.005f * mEnemyDefence);
    }

    /// <summary>
    /// 敌人攻击玩家
    /// </summary>
    /// <param name="backDistance">玩家退后的距离</param>
    /// <param name="gainEnergy">玩家获得的能量</param>
    public float CreatureTwoKnockOne(float backDistance, float gainEnergy)
    {
        //Debug.Log("二击退一的距离" + backDistance);
        gainEnergy = gainEnergy > 0 ? gainEnergy : -gainEnergy; 
        PlayerSkillPowerChange(0.01f * gainEnergy);
        return (backDistance = backDistance > 0 ? backDistance : 10) * (1 - 0.005f * mPlayerDefence);
    }

    /// <summary>
    /// 玩家点击增加能量
    /// </summary>
    public void PlayerTap()
    {
        PlayerSkillPowerChange(0.01f);
    }

    /// <summary>
    /// 控制能量的显示，玩家二
    /// </summary>
    /// <param name="power">本次增加的能量</param>
    private void EnemySkillPowerChange(float power)
    {
        mEnemyEnergyPower += power;
        while (mEnemyEnergyPower >= 1)
        {
            mEnemyEnergyPower = mEnemyEnergyPower - 1;
            //增加技能
            AddRandomBattleSkillTwo();
        }
        mGameControl.EnemySkillPowerUpUI(mEnemyEnergyPower);
    }

    /// <summary>
    /// 控制能量的显示，玩家一
    /// </summary>
    /// <param name="power">本次增加的能量</param>
    private void PlayerSkillPowerChange(float power)
    {
        mPlayerEnergyPower += power;
        while (mPlayerEnergyPower >= 1)
        {
            mPlayerEnergyPower = mPlayerEnergyPower - 1;
            //增加技能
            AddRandomBattleSkillOne();
        }
        mGameControl.PlayerSkillPowerUpUI(mPlayerEnergyPower);
    }

    /// <summary>
    /// 玩家一搓出来的技能,增加到战斗列表，连击的另一个方案是在生成的时候就确定是否是连击块，消块后的也更新的时候也要确认一下
    /// </summary>
    /// <param name="baseCreature"></param>
    private void AddRandomBattleSkillOne()
    {
        //Debug.Log(lPlayerSkills == null);
        //Debug.Log(lPlayerSkills.Count + "玩家技能");
        if (lPlayerSkills.Count > 0)
        {
            BattleSkill battleSkill = lPlayerSkills[UnityEngine.Random.Range(0, lPlayerSkills.Count)];//随机增加  
            BattleSkill bs = FactoryManager.GoodsFactory.GetBattleSkill(0, battleSkill.BattleSkillType);
            for (int i = 0; i < bs.SkillLevel; i++)
            {
                bs.LevelUp();
            }
            lPlayerBattleSkills.AddLast(bs);
            //通知ui更新战斗技能列表
            mGameControl.AddPlayerSkillUI(bs);
        }
    }

    /// <summary>
    /// 玩家二搓技能
    /// </summary>
    private void AddRandomBattleSkillTwo()
    {
        if (lEnemySkills.Count > 0)
        {
            BattleSkill battleSkill = lEnemySkills[UnityEngine.Random.Range(0, lEnemySkills.Count)]; //随机增加
            BattleSkill bs = FactoryManager.GoodsFactory.GetBattleSkill(0, battleSkill.BattleSkillType);
            for (int i = 0; i < bs.SkillLevel; i++)
            {
                bs.LevelUp();
            }

            lEnemyBattleSkills.AddLast(bs);
            //通知ui更新战斗技能列表
            mGameControl.AddEnemySkillUI(bs);
        }
    }
}
