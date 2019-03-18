using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData {

    #region 获取MapContruct的prefab路径
    
    public Dictionary<string, string> SpritePathDict { get; private set; }
    public Dictionary<string, string> GameObjectPathDict { get; private set; }

    //各种路径

    [Serializable]
    class GameObjectInfoJson
    {
        public List<GameObjectInfo> GameObjectInfoList;
    }

    /// <summary>
    /// 获取各个asset的具体枚举名称和对应的路径
    /// </summary>
    private void ParseGameObjectJson()
    {
        GameObjectPathDict = new Dictionary<string, string>();
        TextAsset ta = Resources.Load<TextAsset>("GameObjectInfo");
        //Debug.Log(ta.text);
        GameObjectInfoJson jsonObject = JsonUtility.FromJson<GameObjectInfoJson>(ta.text);
        //Debug.Log(jsonObject.gameContructInfoList);
        for (int j = 0; j < jsonObject.GameObjectInfoList.Count; j++)
        {
            for (int i = 0; i < jsonObject.GameObjectInfoList[j].BuildGOList.Count; i++)
            {
                //Debug.Log(infos.PlayerType + " + " + infos.playerPath);
                GameObjectPathDict.Add(jsonObject.GameObjectInfoList[j].BuildGOList[i].GameObjectString, 
                    jsonObject.GameObjectInfoList[j].BuildGOList[i].GameObjectPath);
            }
            for (int i = 0; i < jsonObject.GameObjectInfoList[j].MapGOList.Count; i++)
            {
                //Debug.Log(infos.PlayerType + " + " + infos.playerPath);
                GameObjectPathDict.Add(jsonObject.GameObjectInfoList[j].MapGOList[i].GameObjectString, 
                    jsonObject.GameObjectInfoList[j].MapGOList[i].GameObjectPath);
            }
            for (int i = 0; i < jsonObject.GameObjectInfoList[j].PlayerGOList.Count; i++)
            {
                //Debug.Log(infos.PlayerType + " + " + infos.playerPath);
                GameObjectPathDict.Add(jsonObject.GameObjectInfoList[j].PlayerGOList[i].GameObjectString, 
                    jsonObject.GameObjectInfoList[j].PlayerGOList[i].GameObjectPath);
            }
            for (int i = 0; i < jsonObject.GameObjectInfoList[j].CubeGOList.Count; i++)
            {
                //Debug.Log(infos.PlayerType + " + " + infos.playerPath);
                GameObjectPathDict.Add(jsonObject.GameObjectInfoList[j].CubeGOList[i].GameObjectString, 
                    jsonObject.GameObjectInfoList[j].CubeGOList[i].GameObjectPath);
            }
            for (int i = 0; i < jsonObject.GameObjectInfoList[j].EnemyGOList.Count; i++)
            {
                //Debug.Log(infos.PlayerType + " + " + infos.playerPath);
                GameObjectPathDict.Add(jsonObject.GameObjectInfoList[j].EnemyGOList[i].GameObjectString, 
                    jsonObject.GameObjectInfoList[j].EnemyGOList[i].GameObjectPath);
            }
            for (int i = 0; i < jsonObject.GameObjectInfoList[j].NPCGOList.Count; i++)
            {
                //Debug.Log(infos.PlayerType + " + " + infos.playerPath);
                GameObjectPathDict.Add(jsonObject.GameObjectInfoList[j].NPCGOList[i].GameObjectString, 
                    jsonObject.GameObjectInfoList[j].NPCGOList[i].GameObjectPath);
            }
        }
    }

    [Serializable]
    class GameSpriteInfoJson
    {
        public List<SpriteInfo> SpriteInfoList;
    }

    private void ParseSpriteJson()
    {
        SpritePathDict = new Dictionary<string, string>();
        TextAsset ta = Resources.Load<TextAsset>("SpriteInfo");
        GameSpriteInfoJson jsonObject = JsonUtility.FromJson<GameSpriteInfoJson>(ta.text);
        for (int j = 0; j < jsonObject.SpriteInfoList.Count; j++)
        {

            for (int i = 0; i < jsonObject.SpriteInfoList[j].PlayerSpriteList.Count; i++)
            {
                //Debug.Log(infos.PlayerType + " + " + infos.playerPath);
                SpritePathDict.Add(jsonObject.SpriteInfoList[j].PlayerSpriteList[i].SpriteString, 
                    jsonObject.SpriteInfoList[j].PlayerSpriteList[i].SpritePath);
            }
            for (int i = 0; i < jsonObject.SpriteInfoList[j].NPCSpriteList.Count; i++)
            {
                //Debug.Log(infos.PlayerType + " + " + infos.playerPath);
                SpritePathDict.Add(jsonObject.SpriteInfoList[j].NPCSpriteList[i].SpriteString, 
                    jsonObject.SpriteInfoList[j].NPCSpriteList[i].SpritePath);
            }
            for (int i = 0; i < jsonObject.SpriteInfoList[j].EnemySpriteList.Count; i++)
            {
                //Debug.Log(infos.PlayerType + " + " + infos.playerPath);
                SpritePathDict.Add(jsonObject.SpriteInfoList[j].EnemySpriteList[i].SpriteString, 
                    jsonObject.SpriteInfoList[j].EnemySpriteList[i].SpritePath);
            }
            for (int i = 0; i < jsonObject.SpriteInfoList[j].WeaponSpriteList.Count; i++)
            {
                //Debug.Log(infos.PlayerType + " + " + infos.playerPath);
                SpritePathDict.Add(jsonObject.SpriteInfoList[j].WeaponSpriteList[i].SpriteString, 
                    jsonObject.SpriteInfoList[j].WeaponSpriteList[i].SpritePath);
            }
            for (int i = 0; i < jsonObject.SpriteInfoList[j].AmorSpriteList.Count; i++)
            {
                //Debug.Log(infos.PlayerType + " + " + infos.playerPath);
                SpritePathDict.Add(jsonObject.SpriteInfoList[j].AmorSpriteList[i].SpriteString, 
                    jsonObject.SpriteInfoList[j].AmorSpriteList[i].SpritePath);
            }
            for (int i = 0; i < jsonObject.SpriteInfoList[j].EquipmentSpriteList.Count; i++)
            {
                //Debug.Log(infos.PlayerType + " + " + infos.playerPath);
                SpritePathDict.Add(jsonObject.SpriteInfoList[j].EquipmentSpriteList[i].SpriteString, 
                    jsonObject.SpriteInfoList[j].EquipmentSpriteList[i].SpritePath);
            }
            for (int i = 0; i < jsonObject.SpriteInfoList[j].NormalSkillSpriteList.Count; i++)
            {
                //Debug.Log(infos.PlayerType + " + " + infos.playerPath);
                SpritePathDict.Add(jsonObject.SpriteInfoList[j].NormalSkillSpriteList[i].SpriteString, 
                    jsonObject.SpriteInfoList[j].NormalSkillSpriteList[i].SpritePath);
            }
            for (int i = 0; i < jsonObject.SpriteInfoList[j].BattleSkillSpriteList.Count; i++)
            {
                //Debug.Log(infos.PlayerType + " + " + infos.playerPath);
                SpritePathDict.Add(jsonObject.SpriteInfoList[j].BattleSkillSpriteList[i].SpriteString, 
                    jsonObject.SpriteInfoList[j].BattleSkillSpriteList[i].SpritePath);
            }
            for (int i = 0; i < jsonObject.SpriteInfoList[j].ItemSpriteList.Count; i++)
            {
                //Debug.Log(infos.PlayerType + " + " + infos.playerPath);
                SpritePathDict.Add(jsonObject.SpriteInfoList[j].ItemSpriteList[i].SpriteString,
                    jsonObject.SpriteInfoList[j].ItemSpriteList[i].SpritePath);
            }
        }
    }
    
    #endregion   
    public GamePlayer GamePlayer { get { return mGamePlayer; } }
    public int PlayerHeart { get { return mGamePlayer.Heart; } }
    public GameState GameState { get { return mGameState; }set { mGameState = value; } }
    public bool IsCameraFollow { get { return mIsCameraFollow; } set { mIsCameraFollow = value; } }
    public float PlayNumber { set { mPlayNumber = value; } get { return mPlayNumber; } }
    public float CurrentCombo { get { return mCurrentCombo; } set { mCurrentCombo = value; } } 
    public float MaxCombo { get { return mMaxCombo; } set{ mMaxCombo = value; } } 
    public float CurrentScore { get { return mCurrentScore; } set { mCurrentScore = value; } }
    public float Time { get { return mTime; }set { mTime = value; } }
    public float MaxScore { get { return mMaxScore; } set { mMaxScore = value; } }
    public PlayerType PlayerType { get { return mPlayerType; } set { mPlayerType = value; } }
    public float DoneNumber { get { return mDoneNumber; } set { mDoneNumber = value; } }
    public float DifficultCubeNumber { get { return mDifficultCubeNumber; } set { mDifficultCubeNumber = value; } }
    public float Record { get { return mRecord; } set { mRecord = value; } }    
    public float Border { get { return mBorder; }set { mBorder = value; } }
    public GameObject Player { get { return mGamePlayer.GetGameObject(); } }
    public List<Vector3> Postions { get { return lPostions; }set { lPostions = value; } }
    public Dictionary<int, MapCube> MapCubeDict { get { return dMapCubeDict; }set { dMapCubeDict = value; } }

    private GamePlayer mGamePlayer;
    private GameState mGameState;
    private bool mIsCameraFollow;
    private float mPlayNumber; //玩家输入的最大块数    
    private float mCurrentCombo; //当前连击
    private float mMaxCombo; //最高连击
    private float mCurrentScore; //当前分
    private float mTime;
    private float mMaxScore; //除去减分以外的最高分
    private PlayerType mPlayerType;
    private float mDoneNumber; //消块数/生成的块数
    private float mDifficultCubeNumber;
    private float mRecord;
    private float mBorder;
    private List<Vector3> lPostions;
    private Dictionary<int, MapCube> dMapCubeDict;

    public GameData(GamePlayer gamePlayer)
    {
        mGamePlayer = gamePlayer;       
        ParseGameObjectJson();
        ParseSpriteJson();  
    }

    public bool RemovePos(Vector3 vector3)
    {
        return lPostions.Remove(vector3);
    }

    /// <summary>
    /// 主角丢失生命后是否存活(每次重置的时候主角生命为1，然后在加2即可，那么就只会增加两次)
    /// </summary>
    /// <returns></returns>
    public bool LoseHeart()
    {
        mGamePlayer.LoseHeart();
        //Debug.Log(mGamePlayer.Heart);
        if (mGamePlayer.Heart > 0) return true;
        else return false;
    }

    public void AddHeart()
    {
        //Debug.Log("增加生命");
        mGamePlayer.AddHeart() ;        
    }

    /// <summary>
    /// 获取最后的倍率
    /// </summary>
    /// <param name="scoreMulType"></param>
    /// <param name="mulNumber"></param>
    /// <param name="niceTimes"></param>
    public void GetScoreResultBar_Mul(ScoreMulType scoreMulType, out float mulNumber, out float niceTimes)
    {
        mulNumber = 1;
        niceTimes = 1;
        //Debug.Log("消块数" + mDoneNumber + "最大块数" + mPlayNumber + "连击数" + mCombo);
        if (mDoneNumber == 0) return;
        if (scoreMulType == ScoreMulType.DoneCubeMul)
        {
            mulNumber = Mathf.Pow(((mDoneNumber + mPlayNumber) / mPlayNumber), 3); //最大8倍
            niceTimes = 7;
        }
        else if (scoreMulType == ScoreMulType.MaxCUbeMul)
        {
            mulNumber = Mathf.Pow(mPlayNumber / 100f, 3);//300为最大块数，则27倍?
            niceTimes = 2.5f;
        }
        else if (scoreMulType == ScoreMulType.ComboAdd)
        {
            mulNumber = Mathf.Pow(((mMaxCombo + mDoneNumber) / (mDoneNumber)) * ((mDoneNumber + mPlayNumber) / mPlayNumber), 4) * (mMaxCombo / mPlayNumber);//最大64倍
            niceTimes = 48f;
        }
        else if (scoreMulType == ScoreMulType.PreTimeMul)
        {
            float preScoreTime = mTime / mCurrentScore;
            if (preScoreTime < 0) preScoreTime = 100;
            mulNumber = Mathf.Pow((Mathf.Pow(preScoreTime / 10, -0.1f) + 0.5f) * Mathf.Pow((mDoneNumber / 100f), 0.1f), 4) * (mCurrentScore / mMaxScore); //没块1s，每100块为1，大概是64倍
            niceTimes = 24f;
        }
        else { UnityEngine.Debug.Log("EndPanel类型参数为空"); }
    }

    /// <summary>
    /// 游戏开始前清除上次遗留的数据
    /// </summary>
    public void ClearDataBeforeGameGoOn()
    {
        mPlayNumber = 100;
        mPlayerType = PlayerType.Sphere;
        mDoneNumber = 0;
        mDifficultCubeNumber = 250;
        mMaxScore = 0;
        mCurrentScore = 0;
        mCurrentCombo = 0;
        mMaxCombo = 0;
        mTime = 0;
        mIsCameraFollow = false;
    }

    public void ClearDataAfterGameGoOn()
    {
        lPostions.Clear();//清理位置信息
        dMapCubeDict.Clear(); //清理cube字典
        EffectsPoolManager.BeyondTimeObject();//清理对象池
        ObjectsPoolManager.BeyondTimeObject();
    }

    /// <summary>
    /// 音乐成就
    /// </summary>
    /// <returns></returns>
    public int MusicAchievement()
    {
        if (mRecord < 100) { return 5; }
        else
        {
            int i = 5;
            if (mRecord > 200) { i += 5; }
            if (mRecord > 500) { i += 5; }
            if (mRecord > 1000) { i += 3; };
            if (mRecord > 2000) { i += 2; };
            if (mRecord > 5000) { i += 1; };
            return i;
        }
    }

    public void ReduceTime(float time)
    {
        mTime -= time;
    }

    public void IncreaseTime(float time)
    {
        mTime += time;
    }

    public void GainScore(float score)
    {
        mCurrentCombo += score;
    }

    public void GainCoins(int coins)
    {
        mGamePlayer.SellGoodGainCoin(coins);
    }

    public void LoseCoins(int coins)
    {
        mGamePlayer.BuyGoodLoseCoin(coins);
    }

    public void GainSteel(int steel)
    {
        mGamePlayer.GainSteel(steel);
    }
}
