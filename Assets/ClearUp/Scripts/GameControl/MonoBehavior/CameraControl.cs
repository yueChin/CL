using UnityEngine;
using UnityEngine.EventSystems;

public class CameraControl : MonoBehaviour {
    
#if UNITY_ANDROID

    private Touch mOldTouch1;  //上次触摸点1(手指1)
    private Touch mOldTouch2;  //上次触摸点2(手指2)

    private enum slideVector { nullVector, up, down, left, right };
    private Vector2 mTouchFirst = Vector2.zero; //手指开始按下的位置
    private Vector2 mTouchSecond = Vector2.zero; //手指拖动的位置
    private slideVector currentVector = slideVector.nullVector;//当前滑动方向
    private float mTimer;//时间计数器  
    private float mOffsetTime = 0.1f;//判断的时间间隔 
    private Vector3 mTempPos;
    //private float mAngleX;
    //private float mAngleY;
    //private float mAngleZ;
    private float mDeltaMove = 0.3f;

    void LateUpdate()   
    {
        if (Input.touchCount == 2)
        {
            if (Input.touches[0].phase == TouchPhase.Stationary && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) //如果第一只手指按下不动，第二只移动
            {
                if (Input.touches[1].phase == TouchPhase.Began)//记录第二只手指按下的位置
                {
                    mTouchFirst = Input.touches[1].position;
                }
                if (Input.touches[1].phase == TouchPhase.Moved)//第二只手指移动的方向
                {
                    mTouchSecond = Input.touches[1].position;
                    mTimer += Time.deltaTime;  //计时器
                    if (mTimer > mOffsetTime)
                    {
                        mTouchSecond = Input.touches[1].position; //记录结束下的位置
                        Vector2 slideDirection = mTouchSecond - mTouchFirst;
                        float x = slideDirection.x;
                        float y = slideDirection.y;
                        //mAngleX = this.transform.eulerAngles.x;
                        //mAngleY = this.transform.eulerAngles.y;
                        //mAngleZ = this.transform.eulerAngles.z;
                        if ((x = (x > 0 ? x : -x)) > (y = (y > 0 ? y : -y)))//左右移动
                        {
                            if (slideDirection.x > 0 && this.transform.position.x < GameControl.GetGameControl.Border * 2)
                            {
                                mTempPos = this.transform.position;
                                mTempPos += new Vector3(this.transform.right.x, 0, this.transform.right.z);
                                this.transform.position = mTempPos;
                            }
                            else if (slideDirection.x <= 0 && this.transform.position.x > -GameControl.GetGameControl.Border)
                            {
                                mTempPos = this.transform.position;
                                mTempPos -= new Vector3(this.transform.right.x, 0, this.transform.right.z);
                                this.transform.position = mTempPos;
                            }
                        }
                        else //上下移动
                        {
                            if (slideDirection.y > 0 && this.transform.position.z < GameControl.GetGameControl.Border * 2)
                            {
                                mTempPos = this.transform.position;
                                mTempPos += new Vector3(this.transform.forward.x, 0, this.transform.forward.z);
                                this.transform.position = mTempPos;
                            }
                            else if (slideDirection.y <= 0 && this.transform.position.z > -3 -GameControl.GetGameControl.Border)
                            {
                                mTempPos = this.transform.position;
                                mTempPos -= new Vector3(this.transform.forward.x, 0, this.transform.forward.z);
                                this.transform.position = mTempPos;
                            }
                        }                      
                    }                    
                }
            }
            if (Input.touches[1].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Ended)
            {//滑动结束     
                mTimer = 0;
                mTouchFirst = mTouchSecond;
            }
        }
        if (Input.touchCount == 3)
        {
            if (Input.touches[0].phase == TouchPhase.Stationary && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                Touch newTouch1 = Input.GetTouch(1);
                Touch newTouch2 = Input.GetTouch(2);
                //第2点刚开始接触屏幕, 只记录，不做处理
                if (newTouch2.phase == TouchPhase.Began)
                {
                    mOldTouch2 = newTouch2;
                    mOldTouch1 = newTouch1;
                }
                if (Input.touches[1].phase == TouchPhase.Moved || Input.touches[2].phase == TouchPhase.Moved)
                {
                    //计算老的两点距离和新的两点间距离，变大要放大模型，变小要缩放模型
                    float oldDistance = Vector2.Distance(mOldTouch1.position, mOldTouch2.position);
                    float newDistance = Vector2.Distance(newTouch1.position, newTouch2.position);
                    //两个距离之差，为正表示放大手势， 为负表示缩小手势                    
                    float offset = newDistance - oldDistance;
                    if (offset > 0 && this.transform.position.y < 20)
                    {
                        mTempPos = this.transform.position;
                        mTempPos.y += mDeltaMove;
                        this.transform.position = mTempPos;
                    }
                    else if(offset < 0 && this.transform.position.y > 8)
                    {
                        mTempPos = this.transform.position;
                        mTempPos.y -= mDeltaMove;
                        this.transform.position = mTempPos;
                    }                  
                }
                else
                {
                    //记住最新的触摸点，下次使用
                    mOldTouch1 = newTouch1;
                    mOldTouch2 = newTouch2;
                }
            }
        }
    }
#endif

#if UNITY_STANDALONE_WIN

    public float scaleSpeed = 5.0f;
    private float minScale = 1.0f;
    private float maxScale = 150.0f;
    private float currentScale;
    private Vector3 mCameraPos;

    // Use this for initialization
    void Start()
    {
        //根据当前摄像机是正交还是透视进行对应赋值
        if (Camera.main.orthographic == true)
        {
            currentScale = Camera.main.orthographicSize;
        }
        else
        {
            currentScale = Camera.main.fieldOfView;
        }
    }

    void LateUpdate()
    {
        //获取鼠标滚轮的值，向前大于0，向后小于0，并设置放大缩小范围值
        currentScale -= Input.GetAxis("Mouse ScrollWheel") * scaleSpeed;
        currentScale = Mathf.Clamp(currentScale, minScale, maxScale);

        //Debug.Log(currentScale);
        //根据当前摄像机是正交还是透视进行对应赋值，放大缩小
        if (Camera.main.orthographic == true)
        {
            Camera.main.orthographicSize = currentScale;
        }
        else
        {
            Camera.main.fieldOfView = currentScale;
        }
    }
#endif
}



