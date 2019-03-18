using UnityEngine;

public class Object{

    ///<summary>
    /// 对象
    /// </summary>
    public GameObject GameObject;

    ///<summary>
    /// 存取时间
    /// </summary>
    public float AliveTime;

    ///<summary>
    /// 销毁状态
    /// </summary>
    public bool DestoryStatus;

    public Object(GameObject gameObject)
    {
        this.GameObject = gameObject;
        this.DestoryStatus = false;
    }

    ///<summary>
    /// 激活对象，将对象显示
    /// </summary>
    public GameObject Active()
    {
        this.GameObject.SetActive(true);
        this.DestoryStatus = false;
        AliveTime = 0;
        return this.GameObject;

    }

    ///<summary>
    /// 销毁对象，不是真正的销毁
    /// </summary>
    public void Destroy()
    {///重置对象状态
        this.GameObject.SetActive(false);
        this.DestoryStatus = true;
        this.AliveTime = Time.time;
    }

    ///<summary>
    /// 检测是否超时，返回true或false，没有其他的操作
    /// </summary>
    public bool IsBeyondAliveTime()
    {
        if (!this.DestoryStatus)
            return false;
        if (Time.time - this.AliveTime >= ObjectsPoolManager.Alive_Time)
        {
            UnityEngine.Debug.Log("已超时!!!!!!");
            return true;
        }
        return false;
    }
}
