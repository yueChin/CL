using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public enum GameState
{
    None,
    Playing,
    Pause,
}

public class GameControl {

    private static GameControl mGameControl;
    public static GameControl GetGameControl
    {
        get
        {
            if (mGameControl == null)
            {
                mGameControl = new GameControl();
            }
            return mGameControl;
        }
    }
    public GameBaseState GameBaseState { set { mGameBaseState = value; } }
    public float Border { get { return mGameData.Border; } }
    public float PlayNumber { get { return mGameData.PlayNumber; } set { mGameData.PlayNumber = value; } }
    public float DoneNumber { get { return mGameData.DoneNumber; } }
    public bool IsCameraFollow { get { return mGameData.IsCameraFollow; } set { mGameData.IsCameraFollow = value; } }
    public GameObject Player { get { return mGameData.Player; } }
    public GameState GameState { get { return mGameData.GameState; } }
    public PlayerType PlayerType { get { return mGameData.PlayerType; } set { mGameData.PlayerType = value; } }
    public float Record { get { return mGameData.Record; } }
    public float GameTime { get { return mGameData.Time; } }
    public float CurrentScore { get { return mGameData.CurrentScore; } }
    public int Heart { get { return mGameData.PlayerHeart; } }
    public Transform PlayScene { get { return mGameCreation.PlayerScene; } }
    public Dictionary<string, string> GOPathDict { get { return mGameData.GameObjectPathDict; } }
    public Dictionary<string, string> SpritePathDict { get { return mGameData.SpritePathDict; } }
    private AudioSource mAudioSource;
    private GameBaseState mGameBaseState;
    private GameCreation mGameCreation;
    private SaveAndLoad mSaveAndLoad;
    private GameData mGameData;
    private QuestSystem mQuestSystem;
    private BattleSystem mBattleSystem;
    private DialogSystem mDialogSystem;
    private ActionFSMSystem mActionSystem;//主角是否能作出各种动作判断
    private float mPoolClearTime;

    public void Init()
    {
        mAudioSource =  UIManager.GetUIManager.GetCanvasTransform.GetComponent<AudioSource>();
        mGameData = new GameData(new GamePlayer());
        mGameCreation = new GameCreation(mGameData);
        mSaveAndLoad = new SaveAndLoad(mGameData);
        mDialogSystem = new DialogSystem(this);
        mQuestSystem = new QuestSystem(this);
        mBattleSystem = new BattleSystem(this);
        mActionSystem = new ActionFSMSystem(this);
        ActionFSMIdleState idleState = new ActionFSMIdleState(mActionSystem);
        idleState.AddTransition(FSMTransition.JumpClick, FSMStateID.JumpingFSMStateID);
        ActionFSMJumpState jumpState = new ActionFSMJumpState(mActionSystem, mGameData.GamePlayer);
        jumpState.AddTransition(FSMTransition.SkillClick, FSMStateID.ReleaseSkillFsmStateID);
        jumpState.AddTransition(FSMTransition.HitMapCube, FSMStateID.IdleFSMStateID);
        ActionFSMSkillState skillState = new ActionFSMSkillState(mActionSystem);
        skillState.AddTransition(FSMTransition.HitMapCube, FSMStateID.IdleFSMStateID);
        mActionSystem.AddFSMSate(idleState);
        mActionSystem.AddFSMSate(jumpState);
        mActionSystem.AddFSMSate(skillState);
    }

    public void Update()
    {
        if (mGameData.GameState == GameState.Playing)//是否需要为这个增加一个fsm系统？
        {
            mGameData.Time += Time.deltaTime;
            ShowPreScoreTime();
            if (mGameData.PlayNumber > 250)
            {
                if (Time.time - mPoolClearTime > ObjectsPoolManager.Alive_Time)
                {
                    ObjectsPoolManager.BeyondTimeObject();
                    mPoolClearTime = Time.time;
                    UnityEngine.Debug.Log("时间：" + mPoolClearTime);
                }
            }
        }
        EixtDetection();
        mActionSystem.UpdateSystem();
        mBattleSystem.Update();
    }

    /// <summary>
    /// 通知游戏状态改变，不是暂停这种
    /// </summary>
    public void ChangeState()
    {
        //Debug.Log(mGameBaseState.StateName);
        mGameBaseState.ChangeState();
    }

    /// <summary>
    /// 更换背景音乐
    /// </summary>
    public void ChangeMusic()
    {       
        mAudioSource.clip = FactoryManager.AssetFactory.LoadAudioClip(string.Format("Audios/BGM{0}",
            UnityEngine.Random.Range(1, mGameData.MusicAchievement()).ToString()));
        mAudioSource.Play();
    }

    public void ChangeBattleMusic(string path)
    {
        mAudioSource.clip = FactoryManager.AssetFactory.LoadAudioClip(path);
        mAudioSource.Play();
    }

    /// <summary>
    /// 更新地图
    /// </summary>
    public void UpdateMap(GameObject gameObject)
    {
        mGameCreation.FreshTerrien(gameObject);
    }

    public bool RemovePostion(Vector3 vector3)
    {
        return mGameData.RemovePos(vector3);
    }

    /// <summary>
    /// 确认主角的丢失生命后的生命状态
    /// </summary>
    public bool CheckPlayerHealth()
    {
        mActionSystem.TransitionFSMState(FSMTransition.HitMapCube);
        if (mGameData.LoseHeart())
        {
            return false;
        }            
        else
        {
            GameOver();
            return true;
        }   
    }

    /// <summary>
    /// 重生，随机位置？还是在上一个位置？
    /// </summary>
    public bool ReBorn()
    {
        //Debug.Log("重生调用减生命");
        if (mGameData.GameState == GameState.Playing)
            mGameData.Player.transform.position = mGameData.Postions[UnityEngine.Random.Range(0, mGameData.Postions.Count)] + new Vector3(0, 6, 0);
        return CheckPlayerHealth();
    }

    /// <summary>
    /// 计算倍率
    /// </summary>
    /// <param name="scoreMulType"></param>
    /// <returns></returns>
    public void GetScoreResultBar_Mul(ScoreMulType scoreMulType, out float mulNumber, out float niceTimes)
    {
        mGameData.GetScoreResultBar_Mul(scoreMulType, out mulNumber, out niceTimes);
    }

    /// <summary>
    /// 减少时间
    /// </summary>
    /// <param name="time"></param>
    public void ReduceTime(float time)
    {
        mGameData.ReduceTime(time);
    }

    /// <summary>
    /// 增加时间
    /// </summary>
    /// <param name="time"></param>
    public void IncreaseTime(float time)
    {
        mGameData.IncreaseTime(time);
    }

    /// <summary>
    /// 增加分数
    /// </summary>
    /// <param name="score"></param>
    public void GainScore(float score)
    {
        mGameData.GainScore(score);
    }

    /// <summary>
    /// 增加金币
    /// </summary>
    /// <param name="coins"></param>
    public void GainCoins(int coins)
    {
        mGameData.GainCoins(coins);
    }

    /// <summary>
    /// 丢失金币
    /// </summary>
    /// <param name="coins"></param>
    public void LoseCoins(int coins)
    {
        mGameData.LoseCoins(coins);
    }

    /// <summary>
    /// 获取钢块
    /// </summary>
    /// <param name="steel"></param>
    public void GainSteel(int steel)
    {
        mGameData.GainSteel(steel);
    }

    public void Addheart(int hearts =1)
    {
        //Debug.Log("增加生命"+ hearts);
        for (int i = 0; i < hearts; i++)
        {
            //Debug.Log("增加生命" + hearts);
            mGameData.AddHeart();
        }       
    }

    #region 按键退出
    private float mCountDown;
    private bool mIsTiming;

    /// <summary> 
    /// 退出检测 
    /// </summary> 
    private void EixtDetection()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //如果按下退出键 
        {
            if (mCountDown == 0) //当倒计时时间等于0的时候 
            {
                mCountDown = Time.time; //把游戏开始时间，赋值给 CountDown
                mIsTiming = true; //开始计时                
            }
            else
            {
                UIManager.GetUIManager.ShowExitPanel(UIPanelType.Exit);
            }
        }
        if (mIsTiming) //如果 IsTiming 为 true 
        {
            if ((Time.time - mCountDown) > 2.0) //如果 两次点击时间间隔大于2秒 
            {
                mCountDown = 0; //倒计时时间归零 
                mIsTiming = false; //关闭倒计时 
            }
        }
    }
    #endregion
    #region 游戏状态
    /// <summary>
    /// 等待玩家开始,数值清零
    /// </summary>
    public void DoBeforePlay()
    {
        mSaveAndLoad.ParseRecordFormJson();
        mGameData.ClearDataBeforeGameGoOn();
        mPoolClearTime = 0;
    }

    /// <summary>
    /// 游戏结束，清除列表信息，但是不能清除记录的数值，会影响结算的显示
    /// </summary>
    public void DoAfterPlay()
    {
        mGameData.ClearDataAfterGameGoOn();
    }    

    /// <summary>
    /// 游戏暂停
    /// </summary>
    public void GameFrezze()
    {
        mGameData.GameState = GameState.Pause;
        EventCenter.Broadcast(EventType.Sleep);
    }

    /// <summary>
    /// 游戏恢复
    /// </summary>
    public void GameResume()
    {
        mGameData.GameState = GameState.Playing;
        EventCenter.Broadcast(EventType.Awake);
    }

    /// <summary>
    /// 退出游戏
    /// </summary>
    public void GameExit()
    {
        Application.Quit();
    }
    #endregion
    #region 事件和委托相关
    #region 地图技能释放相关
    public void Jump(float force, Vector3 pos)
    {
        EventCenter.Broadcast(EventType.BallJump,force,pos);
    }

    public void Frezze()
    {
        EventCenter.Broadcast(EventType.BallFrezze);
    }

    public void Release()
    {
        EventCenter.Broadcast(EventType.BallRelease);
    }

    public void Dash()
    {
        EventCenter.Broadcast(EventType.BallDash);
    }

    public void ShowRemainSkillTime(float remainTime)
    {
        EventCenter.Broadcast(EventType.ShowRemainTime,remainTime);
    }

    public void HitCubeOrDead()
    {
        EventCenter.Broadcast(EventType.HitCubeOrDead);
    }
    #endregion
    #region GameData相关事件
    /// <summary>
    /// 消块显示最新分数
    /// </summary>
    /// <param name="gameObjectHashCode"></param>
    public void ShowScore(int gameObjectHashCode)
    {
        mGameData.DoneNumber++;
        MapCube cube = mGameData.MapCubeDict.TryGet(gameObjectHashCode);      
        if (cube != default(MapCube))
        {
            mGameData.CurrentScore += cube.CubeScore;
            mGameData.MapCubeDict.Remove(gameObjectHashCode);
        }
        EventCenter.Broadcast(EventType.SetScore, mGameData.CurrentScore, mGameData.MaxScore, cube.CubeScore *1.0f, cube.CubeGO.gameObject.GetComponentInChildren<MeshRenderer>().sharedMaterial.color);
    }

    /// <summary>
    /// 显示连击数
    /// </summary>
    /// <param name="isCombo"></param>
    public void ShowCombo(bool isCombo)
    {
        if (isCombo)
        {
            mGameData.CurrentCombo++;
            if (mGameData.CurrentCombo > mGameData.MaxCombo)
            {
                mGameData.MaxCombo = mGameData.CurrentCombo;
            }
        }
        else
        {
            mGameData.CurrentCombo = 0;
        }
        EventCenter.Broadcast(EventType.SetCombo, mGameData.CurrentCombo,isCombo);
    }

    /// <summary>
    /// 显示每分时间
    /// </summary>
    public void ShowPreScoreTime()
    {
        if (mGameData.GameState == GameState.Playing)
        {
            if (mGameData.CurrentScore == 0) return;
            EventCenter.Broadcast(EventType.PreScoreTime, mGameData.Time / mGameData.CurrentScore);
        }
    }

    /// <summary>
    /// 游戏开始
    /// </summary>
    public void GameStart()
    {
        //Debug.Log("游戏开始");
        mGameData.GameState = GameState.Playing;
        mGameCreation.Creation();
        EventCenter.Broadcast(EventType.StartGaming);
    }

    /// <summary>
    /// 游戏结束，调试困难，应该需要传个系统进来，然后是继承自默认基类系统
    /// </summary>
    public void GameOver()
    {
        mGameData.GameState = GameState.None;
        EventCenter.Broadcast(EventType.OverGaming);
        ChangeState();
    }
    #endregion
    #region 主角相关事件
    /// <summary>
    /// 设置玩家头像
    /// </summary>
    /// <param name="sprite"></param>
    public void SetPlayerIcon(Action<Sprite> action)
    {
        EventCenter.Broadcast(EventType.SetIcon,action);
    }

    /// <summary>
    /// 改变的金币数量，通知UI
    /// </summary>
    /// <param name="coin">正加负减</param>
    public void CoinChangeUI(int coin)
    {
        EventCenter.Broadcast(EventType.ChangeCoin,coin);
    }

    /// <summary>
    /// 改变的钢块数量，通知UI
    /// </summary>
    /// <param name="steel">正加，负减</param>
    public void SteelChangeUI(int steel)
    {
        EventCenter.Broadcast(EventType.ChangeSteel, steel);
    }
    /// <summary>
    /// 主角增加生命
    /// </summary>
    public void AddheartUI()
    {
        EventCenter.Broadcast(EventType.AddHeart);
    }

    /// <summary>
    /// 主角丢失生命
    /// </summary>
    public void LoseHeartUI(int remainHearts)
    {
        EventCenter.Broadcast(EventType.LoseHeart, remainHearts);
    }

    #endregion
    #region 任务系统相关事件
    public void TriggerQuest(Vector3 vector3, QuestType quest)
    {       
        EventCenter.Broadcast(EventType.AddQuest,vector3, mQuestSystem.AddQuest(quest));
    }

    public void QuestDone(Quest quest, BaseGoods baseGoods)
    {
        EventCenter.Broadcast(EventType.QuestDone,quest,baseGoods);
    }

    #endregion
    #region 摄像头控制相关事件
    public void CloseView(Vector3 vector3)
    {
        EventCenter.Broadcast(EventType.CloseView,vector3);
    }

    public void ReturnView()
    {
        EventCenter.Broadcast(EventType.ReturnView);
    }

    #endregion
    #region 对话系统相关事件
    //public delegate void DoEnterDialog(Sprite spriteOne, Sprite spriteTwo);
    //public delegate void DoShowShopGood(BaseGoods baseGoods);
    //public delegate void DoSkillLevelUp(int skillLevel, string skillname);
    //public delegate void DoExitDialog();
    //public delegate void DoPlayerSay(string context);
    //public delegate void DoNPCSay(string context);
    ///// <summary>
    /// 显示商店的对话面板
    /// </summary>
    /// <param name="spriteOne"></param>
    /// <param name="spriteTwo"></param>
    public void ShowShopDialog(Sprite spriteOne, Sprite spriteTwo)
    {
        EventCenter.Broadcast(EventType.EnterDialog, spriteOne, spriteTwo);
    }

    /// <summary>
    /// 商店增加物品
    /// </summary>
    /// <param name="baseGoods"></param>
    public void ShowShopItem(BaseGoods baseGoods)
    {
        EventCenter.Broadcast(EventType.ShowShopGood,baseGoods);
    }

    /// <summary>
    /// 点击npc头像npc作出的反应
    /// 如果这里直接把对话内容string返回的话，那么战斗系统那里说话就要单独写
    /// </summary>
    public void NPCRequestSayAndDo()
    {
        mDialogSystem.NPCSaySomething();
        EventCenter.Broadcast(EventType.NPCSay);
    }

    /// <summary>
    /// 玩家请求离开对话
    /// </summary>
    public void PlayerRequestExitDialog()
    {
        mDialogSystem.PlayerExit();
    }

    /// <summary>
    /// 离开对话
    /// </summary>
    public void ExitDialogUI()
    {
        EventCenter.Broadcast(EventType.ExitDialog);
    }

    /// <summary>
    /// 购买物品
    /// </summary>
    /// <param name="baseGoods"></param>
    public bool BuyGood(BaseGoods baseGoods)
    {
        return mDialogSystem.BuyGood(mGameData.GamePlayer,baseGoods);
    }

    /// <summary>
    /// 出售物品
    /// </summary>
    /// <param name="baseGoods"></param>
    /// <returns></returns>
    public bool SellGood(BaseGoods baseGoods)
    {
        return mDialogSystem.SellGood(mGameData.GamePlayer, baseGoods);
    }

    /// <summary>
    /// 技能更新
    /// </summary>
    /// <param name="skillLevel"></param>
    public void PlayerSkillLevelUp(int skillLevel, string skillname)
    {
        EventCenter.Broadcast(EventType.SkillLevelUp, skillLevel, skillname);
    }

    /// <summary>
    /// 主角说话
    /// </summary>
    public void PlayerTalkUI(string context)
    {
        EventCenter.Broadcast(EventType.PlayerSay,context);
    }

    /// <summary>
    /// NPC说话
    /// </summary>
    public void NPCTalkUI(string context)
    {
        EventCenter.Broadcast(EventType.NPCSay,context);
    }
    #endregion
    #region 战斗相关
    /// <summary>
    /// 互相碰撞后更新UI
    /// </summary>
    /// <param name="palyer">玩家退后的距离</param>
    /// <param name="enemy">敌人退后的距离</param>
    public void UnderAttackUI(float palyerBack,float enemyBack)
    {
        //Debug.Log("退后的距离："+palyerBack + "111"+ enemyBack);        
        EventCenter.Broadcast(EventType.UnderAttackUI, palyerBack,enemyBack);
    }

    /// <summary>
    /// 触发战斗
    /// </summary>
    /// <param name="battleCreatureAction"></param>
    public void TriggerBattle(IBattleCreatureAction battleCreatureAction)
    {
        mBattleSystem.EnterBattle(battleCreatureAction,mGameData.GamePlayer);
    }

    /// <summary>
    /// 进入战斗，初始化血量和游戏物体
    /// </summary>
    /// <param name="oneHealth"></param>
    /// <param name="twoHealth"></param>
    public void EnterBattle(float oneHealth, float twoHealth, GameObject oneGO, GameObject twoGO,Sprite spriteOne,Sprite spriteTwo)
    {
        //DoEnterBattleUIEventHandle(spriteOne, spriteTwo, oneHealth, twoHealth);
        EventCenter.Broadcast(EventType.EnterBattleUI, spriteOne, spriteTwo, oneHealth, twoHealth);
        //Debug.Log(spriteOne + "战斗头像" + spriteTwo);
        //DoEnterBattleEventHandle(oneHealth, twoHealth, oneGO, twoGO);
        EventCenter.Broadcast(EventType.EnterBattle, oneHealth, twoHealth, oneGO, twoGO);
        //Debug.Log(oneGO + "战斗物体" + twoGO);       
        //DoSetBattleCameraEventHandle(oneGO);
        EventCenter.Broadcast(EventType.SetBattleCamera, oneGO);
    }

    /// <summary>
    /// 开始攻击/战斗
    /// </summary>
    public void BattleFight()
    {
        //Debug.Log("开始战斗");
        EventCenter.Broadcast(EventType.AttackUI);
        mBattleSystem.BattleStartFight();
    }

    /// <summary>
    /// ui通知战斗更新slider数值
    /// </summary>
    public void BattlePlayerTap()
    {
        mBattleSystem.PlayerTap();
    }

    /// <summary>
    /// 通知ui更新slider
    /// </summary>
    /// <param name="power"></param>
    public void PlayerSkillPowerUpUI(float power)
    {
        //DoPlayerSkillPowerUPUIEventHandle(power);
        EventCenter.Broadcast(EventType.PlayerSkillPowerUPUI, power);
    }

    /// <summary>
    /// 通知ui更新slider
    /// </summary>
    /// <param name="power"></param>
    public void EnemySkillPowerUpUI(float power)
    {
        //DoEnemySkillPoweUpUIEventHandle(power);
        EventCenter.Broadcast(EventType.EnemySkillPoweUpUI,power);
    }

    /// <summary>
    /// 通知ui更新主角技能块
    /// </summary>
    public void AddPlayerSkillUI(BattleSkill battleSkill)
    {
        //DoAddPlayerSkillUIEventHandle(battleSkill);
        EventCenter.Broadcast(EventType.AddPlayerSkillUI, battleSkill);
    }

    /// <summary>
    /// 通知UI更新敌方技能块
    /// </summary>
    public void AddEnemySkillUI(BattleSkill battleSkill)
    {
        //DoAddEnemtySkillUIEventHandle(battleSkill);
        EventCenter.Broadcast(EventType.AddEnemtySkillUI, battleSkill);
        
    }

    /// <summary>
    /// player消除了方块
    /// </summary>
    public void ReleasePlayerSkill(BattleSkill battleSkill)
    {
        mBattleSystem.ReleaseBattleSkillOne(battleSkill);
    }

    /// <summary>
    /// Enemy消除了方块
    /// </summary>
    /// <param name="battleSkill"></param>
    public void ReleaseEnemySkill(BattleSkill battleSkill)
    {
        mBattleSystem.ReleaseBattleSkillTwo(battleSkill);
    }

    /// <summary>
    /// 通知UI更新技能块的释放 
    /// </summary>
    /// <param name="battleSkill"></param>
    public void ReleaseSkillUI(BattleSkill battleSkill)
    {
        EventCenter.Broadcast(EventType.DoneSkillUI, battleSkill);
    }

    /// <summary>
    /// 互相碰撞到了
    /// </summary>
    public void BattlePunch(Vector3 vector3)
    {
        //Debug.Log("碰撞");
        mBattleSystem.CreatureKnockOther(vector3);
    }

    /// <summary>
    /// 战斗结束离开战斗
    /// </summary>
    public void LevelBattle(bool winOrLose,string name)
    {
        ///切回ui
        //Debug.Log("离开战斗");        
        EventCenter.Broadcast(EventType.LevelBattle);
        if (winOrLose)
        {            
            mQuestSystem.CheckQuest(name, mGameData.GamePlayer);
        }
        else
        {
            CheckPlayerHealth();
        }
    }

    /// <summary>
    /// 通知战斗结束，给出败方
    /// </summary>
    /// <param name="tag"></param>
    public void BattleEnd(string tag)
    {
        mBattleSystem.BattleEnd(tag);
    }
    #endregion
    #region 背包交互
    public delegate void DoReturnPackageUI(BaseGears baseGears);
    public event DoReturnPackageUI DoReturnPackageUIEventHandle;
    /// <summary>
    /// 卸下主角装备
    /// </summary>
    /// <param name="baseGears"></param>
    public bool DisChargePlayerGear(BaseGears baseGears)
    {
        return InventoryManager.GetInventoryManager.DisChargeGear(baseGears, mGameData.GamePlayer);
    }

    /// <summary>
    /// 主角使用物品，删除物品
    /// </summary>
    /// <param name="baseGoods"></param>
    /// <returns></returns>
    public bool RemovePlayerItem(BaseGoods baseGoods)
    {        
        return InventoryManager.GetInventoryManager.RemoveItem(baseGoods, mGameData.GamePlayer);
    }

    /// <summary>
    /// 主角穿戴装备
    /// </summary>
    /// <param name="baseGears"></param>
    /// <returns></returns>
    public BaseGears EquitPlayerGear(BaseGears baseGears)
    {
        return InventoryManager.GetInventoryManager.ExChangeGear(baseGears, mGameData.GamePlayer);
    }

    public void GearReturnPack(BaseGears baseGears)
    {
        DoReturnPackageUIEventHandle(baseGears);
    }
    #endregion
    #endregion
    /// <summary>
    /// 显示商店对话
    /// </summary>
    /// <param name="bussinessCreatureAction"></param>
    /// <param name="baseGoodCreature"></param>
    public void ShowShopDialog(BaseBussinessGoodCreature bussinessGoodCreature)
    {
        mDialogSystem.ShowShopDialog(bussinessGoodCreature, mGameData.GamePlayer);
    }    

    /// <summary>
    /// 保存记录
    /// </summary>
    /// <param name="score"></param>
    public void SaveToJson(float score)
    {
        mSaveAndLoad.SaveToJson(score);
    }
}
