
using UnityEngine;
using DG.Tweening;

public class BattleCameraFollow : MonoBehaviour {
    private Camera mCamera;
    private Renderer mBattlePlayerGO;
    private float mOffset;
    private float mValue;
    private Vector3 mNowPos;
    private Vector3 mOriginPos;
    private bool mIsStart;
    private bool mIsFollow;
    private void Awake()
    {
        mCamera = this.GetComponent<Camera>();
        //注册方法
        EventCenter.AddListener<GameObject>(EventType.SetBattleCamera,SetPlayer);
        EventCenter.AddListener<Sprite,Sprite,float,float>(EventType.EnterBattleUI,EnterBattle);
        EventCenter.AddListener<float,float>(EventType.UnderAttackUI,Shake);
        mOriginPos = this.transform.position;
        mValue = mOriginPos.x;
    }
    public bool IsVisibleFrom()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(mCamera);
        return GeometryUtility.TestPlanesAABB(planes, mBattlePlayerGO.bounds);
    }

    private void Update()
    {
        if (mBattlePlayerGO == null) return;
        Debug.Log("???");
        if (!IsVisibleFrom())
        {
            mIsFollow = true;

        }
        if (mIsFollow)
        {
            mOffset = this.transform.position.x - mBattlePlayerGO.transform.position.x;
            mOffset = mOffset > 0 ? mOffset : -mOffset;

            if (mOffset < 1)
            {
                //mValue = Mathf.Lerp(this.transform.position.x, mBattlePlayerGO.transform.position.x + 2, Time.deltaTime * 2);
                mIsFollow = false;
            }
            else
            {
                mNowPos = this.transform.position;
                mNowPos.x = mValue;
                this.transform.position = mNowPos;
            }

            mValue = Mathf.Lerp(this.transform.position.x, mBattlePlayerGO.transform.position.x + 2,
                Time.deltaTime * 2);
        }

    }

    private void EnterBattle(Sprite sprite1,Sprite sprite2,float f1,float f2)
    {
        this.transform.position = mOriginPos;
        //Vector3 pos = this.transform.position;
        Vector3 rota = this.transform.localRotation.eulerAngles;
        this.transform.position = new Vector3(mOriginPos.x, mOriginPos.y,0);
        this.transform.DOMove(mOriginPos, 1).SetEase(Ease.InExpo);
    }

    private void Shake(float p1,float p2)
    {
        mCamera.DOShakePosition(p1 * 0.1f,0.01f * p1);
        mCamera.DOShakeRotation(p1 * 0.1f,0.1f);
    }

    private void SetPlayer(GameObject gameObject)
    {
        //Debug.Log("设置摄像机"+ gameObject);
        mBattlePlayerGO = gameObject.GetComponent<Renderer>();
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener<GameObject>(EventType.SetBattleCamera, SetPlayer);
        EventCenter.RemoveListener<Sprite, Sprite, float, float>(EventType.EnterBattleUI, EnterBattle);
        EventCenter.RemoveListener<float, float>(EventType.UnderAttackUI, Shake);
    }
}
