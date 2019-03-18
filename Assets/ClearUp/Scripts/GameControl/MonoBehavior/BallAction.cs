using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAction : MonoBehaviour {
    private Vector3 mForceVector;
    private Vector3 mScreenPos;
    private Vector3 mMousePosInWorld;
    private Vector3 mCameraFollowOffset;
    private Rigidbody mRigidbody;
    private AudioSource mFly;
    private Vector3 mVelocity;
    // Use this for initialization
    void Start () {
        mFly = this.GetComponent<AudioSource>();
        mRigidbody = this.GetComponent<Rigidbody>();
        EventCenter.AddListener<float,Vector3>(EventType.BallJump,Jump);
        EventCenter.AddListener(EventType.BallFrezze,Frezze);
        EventCenter.AddListener(EventType.BallDash,Dash);
        EventCenter.AddListener(EventType.BallRelease,Release);
        EventCenter.AddListener(EventType.OverGaming,OnGameOver);
        EventCenter.AddListener(EventType.Sleep, OnSleep);
        EventCenter.AddListener(EventType.Awake, OnWakeUp);
    }

    private void Update()
    {
        if (!mRigidbody.IsSleeping())
        {
            if (GameControl.GetGameControl.IsCameraFollow)
            {
                Camera.main.transform.position = this.transform.position + mCameraFollowOffset;
            }
        }
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener<float, Vector3>(EventType.BallJump, Jump);
        EventCenter.RemoveListener(EventType.BallFrezze, Frezze);
        EventCenter.RemoveListener(EventType.BallDash, Dash);
        EventCenter.RemoveListener(EventType.BallRelease, Release);
        EventCenter.RemoveListener(EventType.OverGaming, OnGameOver);
        EventCenter.RemoveListener(EventType.Sleep, OnSleep);
        EventCenter.RemoveListener(EventType.Awake, OnWakeUp);
    }

    private void OnWakeUp()
    {
        mRigidbody.WakeUp();
    }

    private void OnSleep()
    {
        mRigidbody.Sleep();
    }

    private void OnGameOver()
    {
        Destroy(this.gameObject);
    }

    private void Jump(float force, Vector3 pointPos)
    {
        mScreenPos = Camera.main.WorldToScreenPoint(transform.position);
        pointPos.z = mScreenPos.z;
        mMousePosInWorld = Camera.main.ScreenToWorldPoint(pointPos);
        mForceVector.x = (-this.transform.position + mMousePosInWorld).x;
        mForceVector.z = (-this.transform.position + mMousePosInWorld).z;
        mForceVector.y = Mathf.Sqrt(Mathf.Pow(mForceVector.x, 2) + Mathf.Pow(mForceVector.z, 2));
        mForceVector = mForceVector.normalized;
        if (GameControl.GetGameControl.IsCameraFollow)//在跳跃过程中点击跟随
        {
            mCameraFollowOffset = Camera.main.transform.position - this.transform.position;
        }
        mRigidbody.AddForce(mForceVector * force, ForceMode.VelocityChange);
        mFly.Play();
    }

    private void Frezze()
    {
        mVelocity = mRigidbody.velocity;//记录当前速度
        mRigidbody.Sleep();
    }

    private void Release()
    {
        mRigidbody.velocity = mVelocity;
    }

    private void Dash()
    {
        mRigidbody.AddForce(Vector3.down, ForceMode.VelocityChange);
    }
}
