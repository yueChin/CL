using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveOfViewControl : MonoBehaviour {
    private float mTime;
    private bool mIsView;
    private void OnEnable()
    {
        mIsView = true;
    }

    private void LateUpdate()
    {
        if (!mIsView)
        {
            if (Time.time - mTime > 10)//如果超过10秒不可见，就下能
            {
                this.gameObject.SetActive(false);
            }
        }
    }

    private void OnBecameVisible()
    {        
        this.gameObject.SetActive(true);   
    }

    private void OnBecameInvisible()
    {
        mIsView = false;
        mTime = Time.time;   
    }
}
