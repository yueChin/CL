using UnityEngine;

public class MapCubeFactory  {

    public MapCube GetCube<T>(CubeType cubeType,string cubePath,BuildType buildType, string buildPath,Vector3 spawnPos ) where T:MapCube,new()
    {
        T cube = new T();
        IMapCubeBuilder mapCube = new MapCubeBuilder(cubeType, cubePath, buildType, buildPath, spawnPos,cube);
        return MapCubeDirector.MapCubeConstruct(mapCube);
    }

    public MapCube RespawnCube<T>(CubeType cubeType, string cubePath, BuildType buildType, string buildPath, Vector3 spawnPos) where T : MapCube, new()
    {
        T cube = new T();
        IMapCubeBuilder mapCube = new MapCubeBuilder(cubeType, cubePath, buildType, buildPath, spawnPos, cube);
        return MapCubeDirector.MapCubeRespawn(mapCube);
    }

    public MapCube DifficultCube<T>(CubeType cubeType, string cubePath, BuildType buildType, string buildPath, Vector3 spawnPos) where T : MapCube, new()
    {
        T cube = new T();
        IMapCubeBuilder mapCube = new MapCubeBuilder(cubeType, cubePath, buildType, buildPath, spawnPos, cube);
        return MapCubeDirector.MapCubeDifficult(mapCube);
    }
}
