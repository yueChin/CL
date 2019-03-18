using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UniversalWhell : MonoBehaviour
{
    private Transform mControledCamera;
    private float mSpeed = 1f;
    private float mCountDown;
    private bool mIsTiming;
    private Button mRetoPlayerBtn;

    private void Start()
    {
        mControledCamera = Camera.main.transform;
        mRetoPlayerBtn = UITool.FindChild<Button>(this.transform.parent, "Button");
        mRetoPlayerBtn.onClick.AddListener(LooAtDetection);
    }

    void Update()
    {
#if UNITY_STANDALONE_WIN
        if (Input.GetKey(KeyCode.Mouse0))
        {
#endif
#if UNITY_ANDROID
        if (Input.touchCount == 1)
        { 
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
#endif
                if (!IsHereUW()) return;
                float fMouseX = Input.GetAxis("Mouse X");
                float fMouseY = Input.GetAxis("Mouse Y");
                this.transform.Rotate(Vector3.up, -fMouseX * mSpeed * 5f, Space.World);
                this.transform.Rotate(Vector3.right, fMouseY * mSpeed * 5f, Space.World);
                float x = fMouseX > 0 ? fMouseX : -fMouseX;
                float y = fMouseY > 0 ? fMouseY : -fMouseY;
                if (x > y && x - y > 0.1)//绕主角移动?绕地图中心移动比较好
                {
                    if (fMouseX > 0)
                    {
                        mControledCamera.RotateAround(new Vector3(GameControl.GetGameControl.Border/2,0, GameControl.GetGameControl.Border/2),
                            Vector3.up, mSpeed * 10f);
                    }
                    else
                    {
                        mControledCamera.RotateAround(new Vector3(GameControl.GetGameControl.Border / 2, 0, GameControl.GetGameControl.Border / 2), Vector3.up, -mSpeed * 10f);
                    }
                }
                else if (x < y && y - x > 0.1)//摄像头上下移动，最好可以过90度然后反转回来
                {
                    if (mControledCamera.rotation.eulerAngles.x > 15 && mControledCamera.rotation.eulerAngles.x < 85)
                    {
                        Vector3 mainCameraRotation = mControledCamera.rotation.eulerAngles;
                        if (fMouseY > 0)
                        {
                            mainCameraRotation.x += mSpeed;
                        }
                        else
                        {
                            mainCameraRotation.x -= mSpeed;
                        }
                        mControledCamera.rotation = Quaternion.Euler(mainCameraRotation);
                    }
                }
#if UNITY_ANDROID
            }
#endif
        }
    }  

    /// <summary> 
    /// 万向轮检测
    /// </summary> 
    private void LooAtDetection()
    {
        if (mCountDown == 0) //当倒计时时间等于0的时候 
        {
            mCountDown = Time.time;
            mIsTiming = true; //开始计时                
        }
        else
        {
            mControledCamera.LookAt(GameControl.GetGameControl.Player.transform.position);
        }
        if (mIsTiming) //如果 IsTiming 为 true 
        {
            if ((Time.time - mCountDown) > 2.0) //如果 两次点击时间间隔大于2秒 
            {
                mCountDown = 0; //倒计时时间归零 
                mIsTiming = false; //关闭倒计时 
            }
        }
    }

    /// <summary>
    /// 是否碰到万向轮
    /// </summary>
    /// <param name="targetWorldPos"></param>
    /// <returns></returns>
    private bool IsHereUW()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();        
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        for (int i =0;i< results.Count;i++)
        {
            if (results[i].gameObject.CompareTag("RawImage")) return true;
        }
        return false ;
    }
}