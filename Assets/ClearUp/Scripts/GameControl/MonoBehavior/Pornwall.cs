using UnityEngine;
using DG.Tweening;

public class Pornwall : MonoBehaviour {
    private bool mIsTimeCheck = false;
    private bool mIsShaking = false;
    private float mIntervalTime;
    private float mBoomTime;
    private float mCountDownTime;
    private Material mShareMaterial;
    private bool mIsCheck;
    public delegate void OnRigidbodySleep();
    public event OnRigidbodySleep OnRigidbodySleepEventHandle;

    private void Awake()
    {
        mIsCheck = false;
        mShareMaterial = this.GetComponent<Renderer>().sharedMaterial;
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

    private void Start()
    {
        mBoomTime = Time.time;
        float maxCube = GameControl.GetGameControl.PlayNumber;
        float doneCube = GameControl.GetGameControl.DoneNumber;
        int maxTime = 90 + (int)(100 * Mathf.Pow((250 / maxCube),4)) + (int)(150 * Mathf.Pow((maxCube / (doneCube + maxCube)),4));
        mCountDownTime = Random.Range(30, maxTime);        
        mShareMaterial.DOFade(1, 1);
        this.transform.DOLocalMoveY(0, 3).SetEase(Ease.InOutBounce);
    }

    private void Update()
    {
        if (Time.time - mBoomTime > mCountDownTime)
        {
            this.gameObject.transform.DOShakeScale(0.8f, 0.2f);
            this.gameObject.transform.DOLocalMoveY(-15, 3)
                .OnComplete(()=> mShareMaterial.DOFade(0,0.8f)
                .OnComplete(()=> { this.gameObject.SetActive(false); }));
        }
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
            if (Time.time - mIntervalTime > 0.4f && mIsShaking)
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
                this.gameObject.SetActive(false);
            }
        }
    }
}
