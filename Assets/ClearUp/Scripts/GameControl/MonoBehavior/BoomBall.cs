using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class BoomBall : MonoBehaviour {

    private Rigidbody mRigidbody;
    [SerializeField]
    private GameObject mDirection;
    private float mDirectionDelayTime;
    private bool mArrowRotate;
    private bool mIsChecked;
    private bool mIsSleep;
    void Awake() {
        mRigidbody = this.GetComponent<Rigidbody>();
        mDirection = GameObject.Instantiate(mDirection);
        mDirection.SetActive(false);      
    }

    private void OnEnable()
    {
        mRigidbody.Sleep();
    }

    private void OnDisable()
    {
        mIsSleep = mRigidbody.IsSleeping();
    }

    void Update()
    {
        if (transform.position.y < -15 
            || transform.position.x < -15 || transform.position.x > GameControl.GetGameControl.Border + 5 
            || transform.position.z < -15 || transform.position.z > GameControl.GetGameControl.Border + 5) 
        {
            if (GameControl.GetGameControl.GameState != GameState.Playing) return;
            if (!mIsChecked)
            {
                mIsChecked = true;
                //Debug.Log(this.transform.name + this.transform.tag + this.transform.position);
                //这次要先去扣除生命，然后重生
                if (GameControl.GetGameControl.ReBorn())
                {
                    Destroy(mDirection);
                }
                else
                {
                    mIsChecked = false;
                }                
            }          
        }       
    }

    /// <summary>
    /// 更新箭头
    /// </summary>
    private void LateUpdate()
    {
        if (!IsInView(this.transform.position))
        {
            mDirectionDelayTime = Time.time;
            ShowDirectionInView(this.transform.position, mDirection.transform);
        }
        else
        {
            if (mRigidbody.IsSleeping())
            {
                if (!IsBeNotOcclusion(this.transform.position))
                {
                    mDirectionDelayTime = Time.time;
                    mArrowRotate = true;
                }
            }
            else
            {
                if (IsBeNotOcclusion(this.transform.position))
                {
                    if (Time.time - mDirectionDelayTime > 1f)
                    {
                        mArrowRotate = false;
                        mDirection.SetActive(false);
                    }
                }
            }
        }
        if (mArrowRotate) { ArrowNeedToRotate(); }
    }

    /// <summary>
    /// 判断是否在主摄像机视野内
    /// </summary>
    /// <param name="targetWorldPos"></param>
    /// <returns></returns>
    private bool IsInView(Vector3 targetWorldPos)
    {
        Transform camTransform = Camera.main.transform;
        Vector2 viewPos = Camera.main.WorldToViewportPoint(targetWorldPos); //目标位置相对摄像机的位置
        Vector3 dir = (targetWorldPos - camTransform.position).normalized; //摄像机看向目标的方向
        float dot = Vector3.Dot(camTransform.forward, dir);     //判断物体是否在相机前面        
        if (dot > 0 && viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 是否未被遮挡
    /// </summary>
    /// <param name="targetWorldPos"></param>
    /// <returns></returns>
    private bool IsBeNotOcclusion(Vector3 targetWorldPos)
    {
        Transform camTransform = Camera.main.transform;
        RaycastHit raycastHit;
        Physics.Raycast(camTransform.position, this.transform.position - camTransform.position, out raycastHit,100f);
        if (raycastHit.transform.CompareTag("Player"))
            return true;
        else
            return false;
    }

    /// <summary>
    /// 旋转箭头
    /// </summary>
    private void ArrowNeedToRotate()
    {
        if (!mDirection.activeSelf) { mDirection.SetActive(true); }
        mDirection.transform.position = this.transform.position + new Vector3(0, 3, 0);
        if (mDirection.transform.up != Vector3.up) { mDirection.transform.up = Vector3.up; }        
        mDirection.transform.Rotate(Vector3.up * 10);
    }

    /// <summary>
    /// 在边界显示箭头
    /// </summary>
    private void ShowDirectionInView(Vector3 targetWorldPos,Transform arrow)
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(targetWorldPos); //目标位置相对摄像机的位置       
        if (viewPos.x > 1) { viewPos.x = 0.9f; }
        else if (viewPos.x < 0) { viewPos.x = 0.1f; }
        if (viewPos.y > 1) { viewPos.y = 0.9f; }
        else if (viewPos.y < 0) { viewPos.y = 0.1f; }
        if (viewPos.z < 0) { viewPos.z = 2.5f; }
        if (!arrow.gameObject.activeSelf) { arrow.gameObject.SetActive(true); }
        arrow.position = Camera.main.ViewportToWorldPoint(viewPos);
        arrow.up = (this.transform.position - arrow.transform.position).normalized;
        arrow.Rotate(arrow.up * 10f);
    }
}
