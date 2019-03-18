using UnityEngine;

public class Effect :MonoBehaviour
{
    /// <summary>
    /// 获取的粒子
    /// </summary>
    private ParticleSystem mPs;
    public ParticleSystem PS { get { return mPs; } }

    /// <summary>
    /// “销毁”的时间
    /// </summary>
    private float mPlayTime;

    /// <summary>
    /// 跟随物体，如果此物体不为空，特效会跟随父物体移动，直到父物体变为null
    /// </summary>
    public GameObject Parent;

    ///<summary>
    /// 销毁状态
    /// </summary>
    public bool DestroyStatus;

    ///<summary>
    /// 存取时间
    /// </summary>
    public float AliveTime;   

    public void Awake()
    {
        mPs = this.gameObject.GetComponent<ParticleSystem>();
    }

    /// <summary>
    /// 基本的播放，播放完成自动“销毁”
    /// </summary>
    /// <param name="scale"></param>
    public void Play(float scale)
    {
        mPlayTime = 0;
        this.gameObject.SetActive(true);
        mPs.Play();
        SetScale(this.transform, 0.8f*scale);
        DestroyStatus = false;
    }

    /// <summary>
    /// 自动跟随和销毁用的计时
    /// </summary>
    void Update()
    {
        if (DestroyStatus == true) return;
        mPlayTime += Time.deltaTime;
        if (Parent != null)
        {
            transform.position = Parent.transform.position;
            return;
        }
        if (mPlayTime > mPs.main.duration)
        {
            Die();
        }
    }    

    /// <summary>
    /// 设置
    /// </summary>
    /// <param name="t"></param>
    /// <param name="scale"></param>
    public void SetScale(Transform t, float scale)
    {
        for (int i = 0; i < t.childCount; i++)
        {
            SetScale(t.GetChild(i), scale);
        }
        t.localScale = new Vector3(scale,scale, scale);
    }

    /// <summary>
    /// 如果需要显示拖尾效果，需要调用设置父物体
    /// </summary>
    /// <param name="obj"></param>
    public void SetParent(GameObject obj)
    {
        this.Parent = obj;
    }

    /// <summary>
    /// 物体“销毁”
    /// </summary>
    public void Die()
    {
        DestroyStatus = true;
        gameObject.SetActive(false);        
    }

    ///<summary>
    /// 检测是否超时，返回true或false，没有其他的操作
    /// </summary>
    public bool IsBeyondAliveTime()
    {
        if (!this.gameObject.activeSelf)
            return false;
        if (Time.time - this.AliveTime >= EffectsPoolManager.Alive_Time)
        {
            UnityEngine.Debug.Log("已超时!!!!!!");
            return true;
        }
        return false;
    }
}