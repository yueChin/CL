using UnityEngine;


public class GameLoop : MonoBehaviour {

    private GameStateControl controller = null;

    private void Awake()
    {
        Screen.SetResolution(1080,1920,false);
    }
    // Use this for initialization
    void Start()
    {
        controller = new GameStateControl();
        controller.SetState(new GameStart(controller), false);
        
    }

    void Update()
    {
        controller.StateUpdate();
    }
    #region 分辨率设定
    private float f_UpdateInterval = 1F;
    private float f_LastInterval = 0;
    private int i_Frames = 0;
    private float f_Fps;
    private const int fpsArrayLen = 8;
    private float[] fpsArray = new float[fpsArrayLen];
    private int fpsIndex = 0;
    private const int standardFPS = 26;

    /* 再update 中执行，每隔1秒计算一次FPS*/
    void FPSUpdate()
    {
        ++i_Frames;
        if (Time.realtimeSinceStartup > f_LastInterval + f_UpdateInterval)
        {
            f_Fps = i_Frames / (Time.realtimeSinceStartup - f_LastInterval);
            i_Frames = 0;
            f_LastInterval = Time.realtimeSinceStartup;
            fpsArray[fpsIndex] = f_Fps;
            fpsIndex = (++fpsIndex) % fpsArrayLen;
            //if (!IsStandFPS())
            //{
            //    DoScreen();
            //}
        }
    } 

    /*判断时候达到了标准的FPS*/
    private bool IsStandFPS()
    {
        float totalFPS = 0;
        for (int i = 0; i < fpsArray.Length; ++i)
        {
            totalFPS += fpsArray[i];
        }
        float averageFPS = totalFPS / fpsArray.Length;
        if (averageFPS < standardFPS)
        {
            return false;
        }
        return true;
    }

    private void DoScreen()
    {
        int scWidth = Screen.width;
        int scHeight = Screen.height;
        int designWidth = scWidth /2; //这个是设计分辨率 
        int designHeight = scHeight /2;
        Screen.SetResolution(designWidth, designHeight, true);
    }
    #endregion
}
