using UnityEngine;
using DG.Tweening;

public class Fuckwall : MonoBehaviour {
    private bool mIsTimeCheck = false;
    private bool mIsShaking = false;
    private float mIntervalTime;
    private bool mIsCheck;
    public delegate void OnRigidbodySleep();
    public event OnRigidbodySleep OnRigidbodySleepEventHandle;
    private Material mShareMaterial;

    private void Awake()
    {
        mIsCheck = false;
        mShareMaterial = this.GetComponentInChildren<Renderer>().sharedMaterial;
        EventCenter.AddListener(EventType.OverGaming,OnGameOver);
    }

    private void OnGameOver()
    {
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventType.OverGaming, OnGameOver);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            //Debug.Log("进入碰撞");
            if (!mIsCheck)
            {
                GameControl.GetGameControl.HitCubeOrDead();
                collision.transform.GetComponent<Rigidbody>().Sleep();
                collision.transform.GetComponent<AudioSource>().Stop();               
                GameControl.GetGameControl.ShowScore(this.gameObject.GetHashCode());
                GameControl.GetGameControl.RemovePostion(this.transform.position);
                OnRigidbodySleepEventHandle();
                mIsCheck = true;
            }
        }   
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            //Debug.Log("离开碰撞");
            //显示特效
            Destroy(this.GetComponent<Collider>());
            mIntervalTime = Time.time;            
            mIsTimeCheck = true;
        }        
    }

    private void FixedUpdate()
    {
        if (mIsTimeCheck)
        {
            if (Time.time - mIntervalTime > 0.4f && !mIsShaking)
            {
                mIsShaking = true;
                gameObject.transform.DOShakePosition(1f, new Vector3(0.1f, 0, 0.1f)).OnComplete(()=> { mIsShaking = false; });
            }            
            if (Time.time - mIntervalTime > 1.2f) 
            {
                Effect effect = EffectsPoolManager.PointFixedEffect("DestroyEffect", this.transform.position, 1f);
                effect.GetComponent<Renderer>().sharedMaterial = mShareMaterial;
                effect.GetComponent<AudioSource>().Play();
                GameControl.GetGameControl.UpdateMap(this.gameObject);
                Destroy(this.gameObject);
            }
        }
    }
}
