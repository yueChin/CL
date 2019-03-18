using UnityEngine;

public class MapCubeBuilder : IMapCubeBuilder {
    private MapCube mMapCube;
    private GameObject mCube;
    private GameObject mBuild;
    private CubeType mCubeType;
    private string mCubePath;
    private BuildType mBuildType;
    private string mBuildPath;
    private Vector3 mSpawnPos;

    public MapCubeBuilder(CubeType cubeType, string cubePath, BuildType buildType, string buildPath, Vector3 spawnPos ,MapCube cube)
    {
        mCubeType = cubeType;
        mCubePath = cubePath;
        mBuildType = buildType;
        mBuildPath = buildPath;
        mSpawnPos = spawnPos;
        mMapCube = cube;
    }

    public void SetMapCubeType()
    {
        mMapCube.SetMapType(mBuildType,mCubeType);
    }

    public void AddBuild()
    {
        mBuild = FactoryManager.AssetFactory.LoadGameObject(mBuildPath);       
        mBuild.transform.SetParent(mCube.transform,false);
        mBuild.transform.position = mSpawnPos + new Vector3(0, 0.5f, 0);
        mBuild.transform.localEulerAngles = new Vector3(0, UnityEngine.Random.Range(0, 4) * 90f, 0); //随机朝向
        mMapCube.SetBuild = mBuild;        
    }

    public void AddCube()
    {
        //Debug.Log(mBuildType.ToString());
        mCube = FactoryManager.AssetFactory.LoadGameObject(mCubePath);
        mCube.transform.position = mSpawnPos;
        mCube.transform.localEulerAngles = Vector3.zero;
        mMapCube.CubeGO = mCube;   
    }

    public void SetScore()
    {
        int i;
        if (mCubeType == CubeType.Blue) { i = 1; }
        else if (mCubeType == CubeType.Green) { i = 0; }
        else if (mCubeType == CubeType.Yellow) { i = 5; }
        else { i = -2; }
        mMapCube.CubeScore = i;
    }

    public void ReAddBuild()
    {
        mBuild = ObjectsPoolManager.GetObject(mBuildType.ToString());
        if (mBuild == null)
        {
            mBuild = FactoryManager.AssetFactory.LoadGameObject(mBuildPath);
            ObjectsPoolManager.PushObject(mBuildType.ToString(), mBuild);
        }      
        mBuild.transform.SetParent(mCube.transform,false);
        mBuild.transform.position = mSpawnPos + new Vector3(0, 1f, 0);
        mBuild.transform.localEulerAngles = new Vector3(0, UnityEngine.Random.Range(0, 4) * 90f, 0); //随机朝向
        mMapCube.SetBuild = mBuild;
    }

    public void ReAddCube()
    {
        mCube = ObjectsPoolManager.GetObject(mCubeType.ToString());
        if (mCube == null)
        {
            mCube = FactoryManager.AssetFactory.LoadGameObject(mCubePath);
            ObjectsPoolManager.PushObject(mCubeType.ToString(), mCube);
        }
        mCube.transform.position = mSpawnPos;
        mCube.transform.localEulerAngles = Vector3.zero;
        mMapCube.CubeGO = mCube;
    }

    public MapCube SpawnMapCube()
    {
        return mMapCube;
    }

    public void AddMono()
    {
        mCube.AddComponent<Fuckwall>();
        mBuild.AddComponent<ShitGum>();
    }

    public void ReAddMono()
    {
        mCube.AddComponent<Pornwall>();
        mBuild.AddComponent<DickGum>();
    }
}
