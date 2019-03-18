using System.Collections.Generic;
using UnityEngine;

public class AssetFactory
{
    private Dictionary<string, AudioClip> mAudioClips = new Dictionary<string, AudioClip>();
    private Dictionary<string, Sprite> mSprites = new Dictionary<string, Sprite>();

    public GameObject LoadGONotPool(string path)
    {
        GameObject gameObject = ObjectsPoolManager.GetObject(path);
        if (gameObject == null)
        {
            gameObject = InstantiateGameObject(path);
        }
        return gameObject;
    }

    public GameObject LoadGameObject(string path)
    {       
        GameObject gameObject = ObjectsPoolManager.GetObject(path);
        if (gameObject == null)
        {
            gameObject = InstantiateGameObject(path);
            ObjectsPoolManager.PushObject(path, gameObject);
        }
        return gameObject;
    }

    public AudioClip LoadAudioClip(string path)
    {
        if (mAudioClips.ContainsKey(path)) { return mAudioClips[path]; }
        else
        {
            AudioClip audioClip = LoadAsset<AudioClip>(path) as AudioClip;
            mAudioClips.Add(path, audioClip);
            return audioClip;
        }
    }

    public Sprite LoadSprite(string path)
    {
        if (mSprites.ContainsKey(path)) { return mSprites[path]; }
        else
        {
            Sprite sprite = LoadAsset<Sprite>(path) as Sprite ;
            if (sprite != null) { mSprites.Add(path, sprite); }
            return sprite;
        }
    }

    private GameObject InstantiateGameObject(string path)
    {
        UnityEngine.Object o = Resources.Load(path);
        if (o == null)
        {
            UnityEngine.Debug.LogError("无法加载资源，路径:" + path); return null;
        }
        return UnityEngine.GameObject.Instantiate(o) as GameObject;
    }

    public UnityEngine.Object LoadAsset<T>(string path) where T: UnityEngine.Object
    {
        UnityEngine.Object o = Resources.Load(path, typeof(T));
        if (o == null)
        {
            Debug.LogError("无法加载资源，路径:" + path); return null;
        }
        return o;
    }
}
