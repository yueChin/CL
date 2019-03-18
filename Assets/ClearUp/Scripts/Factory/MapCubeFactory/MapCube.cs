using UnityEngine;

public class MapCube
{
    private int mCubeScore;
    private GameObject mCube;
    private GameObject mBuild;
    private BuildType mBuildType;
    private CubeType mCubeType;

    public int CubeScore
    {
        get { return mCubeScore; }
        set { mCubeScore = value; }
    }

    public GameObject CubeGO
    {
        set { mCube = value; }
        get { return mCube; }
    }

    public GameObject SetBuild
    {
        set { mBuild = value; }
    }

    public void SetMapType(BuildType buildType, CubeType cubeType)
    {
        mBuildType = buildType;
        mCubeType = cubeType;
    }
}
