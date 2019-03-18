using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCreation {
    public Transform PlayerScene { get { return mPlayScene; }}
    private int mResumeNumber;
    private GameData mGameData;
    private Transform mPlayScene;
    public GameCreation(GameData gameData)
    {
        mGameData = gameData;
        mResumeNumber = 0;      
    }

    /// <summary>
    /// 创世！！！
    /// </summary>
    public void Creation()
    {
        mPlayScene = FactoryManager.AssetFactory.LoadGameObject("UI/UIGO/PlayScene").transform;
        InitMap();
        InitCamera();
        InitPlayer();
    }

    /// <summary>
    /// 脱水！！！
    /// </summary>
    /// <param name="gameObject"></param>
    public void FreshTerrien(GameObject gameObject)
    {
        mResumeNumber++;
        if (mGameData.PlayNumber > 250)
        {
            mGameData.DifficultCubeNumber++;
            mGameData.Postions.Remove(gameObject.transform.position);
            while (mResumeNumber!= 0)
            {
                if (CreateMapCube()) mResumeNumber--;
            }
        }
    }

    #region 生成人物
    /// <summary>
    /// 初始化人物
    /// </summary>
    private void InitPlayer()
    {
        FactoryManager.PlayerFactory.InitPlayer(mGameData.GamePlayer, 
            mGameData.Postions[UnityEngine.Random.Range(0, mGameData.Postions.Count)] + new Vector3(0, 12, 0), mPlayScene);
        //GameControl.GetGameControl.Addheart(3);
    }
    #endregion

    #region 生成地图,天气，摄像机初始位置
    /// <summary>
    /// 初始化摄像机位置
    /// </summary>
    private void InitCamera()
    {
        Vector3 mCameraPos = Camera.main.transform.position;
        mCameraPos = Camera.main.transform.position;
        mCameraPos.x = ((int)Mathf.Sqrt(mGameData.PlayNumber) + (int)Mathf.Sqrt(mGameData.PlayNumber) / 2) / 2;
        mCameraPos.y = 15;
        mCameraPos.z = -((int)Mathf.Sqrt(mGameData.PlayNumber) + (int)Mathf.Sqrt(mGameData.PlayNumber) / 2) / 4;
        Camera.main.transform.position = mCameraPos;
        Camera.main.transform.rotation = Quaternion.Euler(60, 0, 0);
    }

    /// <summary>
    /// 初始化地图
    /// </summary>
    private void InitMap()
    {
        mGameData.MapCubeDict = new Dictionary<int, MapCube>();
        mGameData.Postions = new List<Vector3>();//已有的位置
        mGameData.Border = (int)Mathf.Sqrt(mGameData.PlayNumber) + (int)Mathf.Sqrt(mGameData.PlayNumber) / 2;
        while (mGameData.PlayNumber > mGameData.DoneNumber && mGameData.DoneNumber <= 300) //一次最多生成300个块
        {
            if (CreateMapCube())
            {
                mGameData.DoneNumber++;
            }
        }
        mGameData.DoneNumber = 0;
        //生成天气
        string mapPath = mGameData.GameObjectPathDict.TryGet(((MapType)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(MapType)).Length) - 1).ToString());
        if (mapPath != null)
        {
            FactoryManager.MapFactory.LoadMap(mapPath, mGameData.Postions[UnityEngine.Random.Range(0, mGameData.Postions.Count)]);
        }

    }

    /// <summary>
    /// 生成地图方块
    /// </summary>
    private bool CreateMapCube()
    {
        Vector3 Pos = RandomPos((int)mGameData.Border);
        if (!mGameData.Postions.Contains(Pos))
        {
            CubeType cubeType = (CubeType)RandomByGuidToCube();
            BuildType buildType = (BuildType)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(BuildType)).Length - 1);
            MapCube cube;
            if (mGameData.PlayNumber > 250)
            {
                if (mGameData.DifficultCubeNumber > 250) //游戏中生成的块
                {
                    cube = FactoryManager.MapCubeFactory.RespawnCube<MapCube>(cubeType, mGameData.GameObjectPathDict.TryGet(cubeType.ToString()),
                        buildType, mGameData.GameObjectPathDict.TryGet(buildType.ToString()), Pos);
                    cube.CubeGO.transform.position = cube.CubeGO.transform.position + new Vector3(0, -30, 0);
                }
                else //困难模式一开始生成的块
                {
                    cube = FactoryManager.MapCubeFactory.DifficultCube<MapCube>(cubeType, mGameData.GameObjectPathDict.TryGet(cubeType.ToString()), 
                        buildType, mGameData.GameObjectPathDict.TryGet(buildType.ToString()), Pos);
                }
            }
            else
            {
                cube = FactoryManager.MapCubeFactory.GetCube<MapCube>(cubeType, mGameData.GameObjectPathDict.TryGet(cubeType.ToString()), 
                    buildType, mGameData.GameObjectPathDict.TryGet(buildType.ToString()), Pos);
            }
            mGameData.MapCubeDict.Add(cube.CubeGO.GetHashCode(), cube);
            mGameData.Postions.Add(Pos);
            cube.CubeGO.transform.SetParent(mPlayScene,false);
            if (cube.CubeScore > 0) { mGameData.MaxScore += cube.CubeScore; }
            return true;
        }
        return false;
    }

    /// <summary>
    /// 在一定范围内生成位置
    /// </summary>
    private Vector3 RandomPos(int num)
    {
        Vector3 tempPos;
        tempPos.x = Mathf.Round(UnityEngine.Random.Range(0, num));
        tempPos.y = 0;
        tempPos.z = Mathf.Round(UnityEngine.Random.Range(0, num));
        return tempPos;
    }

    /// <summary>
    /// 控制方块颜色数量
    /// </summary>
    /// <returns></returns>
    private int RandomByGuidToCube()
    {
        int i = UnityEngine.Random.Range(0, 1000);
        float j = 0;
        if (mGameData.PlayNumber > 250)
        {
            j = Mathf.Lerp(1, 5, (float)(mGameData.PlayNumber * 0.002));
        }
        if (i < (150 - j * j * j)) { i = 0; }
        else if (i < (850 - j * j * j * 4)) { i = 1; }
        else if (i < (925 - j * j * j * 1.5)) { i = 2; }
        else { i = 3; }
        return i;
    }
    #endregion
}
