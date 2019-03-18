using UnityEngine;

public class EffectFactory {

    private const string PrefixPath = "Effect/";

    public GameObject LoadEffect(string path)
    {
        string surePath = PrefixPath + path;
        GameObject gameObject = FactoryManager.AssetFactory.LoadGameObject(surePath);
        return gameObject;
    }
}
