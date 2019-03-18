using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ShareButtonUI : MonoBehaviour {
    private AudioSource mShareBtnAudioSource;
    private Button mShareButton;
    private GameObject CanvasShareObj;
    // Use this for initialization
    void Start () {
        mShareButton = this.GetComponent<Button>();
        mShareBtnAudioSource = this.GetComponent<AudioSource>();
        CanvasShareObj = UITool.FindChild<Transform>(this.transform, "SharePanel").gameObject;        
        mShareButton.onClick.AddListener(OnShareButtonPress);
    }

    
    private bool isProcessing = false;
    private bool isFocus = false;
    private void OnShareButtonPress()
    {
        mShareBtnAudioSource.Play();
        if (!isProcessing)
        {
            CanvasShareObj.SetActive(true);
            StartCoroutine(ShareScreenshot());
        }
    }

    private IEnumerator ShareScreenshot()
    {
        isProcessing = true;
        yield return new WaitForEndOfFrame();
        ScreenCapture.CaptureScreenshot("screenshot.png", 2);
        string destination = Path.Combine(Application.persistentDataPath, "screenshot.png");//截图
        yield return new WaitForSeconds(0.3f); //WaitForSecondsRealtime(0.3f);
        if (!Application.isEditor)
        {
            AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
            AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
            intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
            AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
            AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + destination);//分享的图片
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"),
            uriObject);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"),
            "Can you beat my score?");//文本
            intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");
            AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject chooser = intentClass.CallStatic<AndroidJavaObject>("createChooser",
            intentObject, "Share your new score");
            currentActivity.Call("startActivity", chooser);
            yield return new WaitForSeconds(1f); //WaitForSecondsRealtime(1f);
        }

        UnityEngine.Debug.Log("????");
        yield return new WaitUntil(() => isFocus);
        CanvasShareObj.SetActive(false);
        isProcessing = false;
    }

    private void OnApplicationFocus(bool focus)
    {
        isFocus = focus;
    }
}
