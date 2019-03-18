using UnityEngine;

public class MapFactory {

    public GameObject LoadMap(string path, Vector3 pos)
    {
        GameObject map = FactoryManager.AssetFactory.LoadGameObject(path);
        map.transform.position = pos + new Vector3(0,5,0);
        return map;
    }
}
