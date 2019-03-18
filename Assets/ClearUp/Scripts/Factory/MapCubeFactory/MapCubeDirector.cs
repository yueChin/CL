
public class MapCubeDirector  {

    public static MapCube MapCubeConstruct(IMapCubeBuilder mapCubeBuilder)
    {
        mapCubeBuilder.AddCube();
        mapCubeBuilder.AddBuild();
        mapCubeBuilder.AddMono();
        mapCubeBuilder.SetScore();        
        return mapCubeBuilder.SpawnMapCube();
    }

    public static MapCube MapCubeRespawn(IMapCubeBuilder mapCubeBuilder)
    {
        mapCubeBuilder.ReAddCube();
        mapCubeBuilder.ReAddBuild();
        mapCubeBuilder.ReAddMono();
        mapCubeBuilder.SetScore();       
        return mapCubeBuilder.SpawnMapCube();
    }

    public static MapCube MapCubeDifficult(IMapCubeBuilder mapCubeBuilder)
    {
        mapCubeBuilder.ReAddCube();
        mapCubeBuilder.ReAddBuild();
        mapCubeBuilder.AddMono();
        mapCubeBuilder.SetScore();
        return mapCubeBuilder.SpawnMapCube();
    }
}
