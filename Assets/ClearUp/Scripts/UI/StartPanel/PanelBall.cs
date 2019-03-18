using UnityEngine;

public class PanelBall : MonoBehaviour {

    private bool mReady = false;
    private bool mLeftRight = false;
    private bool mLRDone = false;
    private float y = 0;
    private float mAxisY = 0;
    private AudioSource mAudioSource;
    private float mTimeInterval;
    private Vector3 mOriginPos;
    private void Awake()
    {
        mAudioSource = this.transform.GetComponent<AudioSource>();
        mOriginPos = this.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate() {
        //Debug.Log(mReady);       
        if (mReady)
        {
            if (mLRDone)
            {
                if (Time.time - mTimeInterval > 0.5)
                {
                    mLRDone = false;
                }
            }
            mAxisY = Mathf.Sin(Mathf.PI * y) > 0 ? Mathf.Sin(Mathf.PI * y) : -Mathf.Sin(Mathf.PI * y);
            if (mLeftRight)
            {
                MoveLeft();
                y += Time.deltaTime;
            }
            else
            {
                MoveRight();
                y -= Time.deltaTime;
            }
        }
        else { Drop(); }

        if (gameObject.transform.localPosition.x < -5 || gameObject.transform.localPosition.x > 5)
        {
            this.gameObject.SetActive(false);
            this.transform.position = mOriginPos;
        }

        if (gameObject.transform.localPosition.y <= 1.2f)
        {
            mReady = true;
            if (!mLRDone)
            {
                mAudioSource.Stop();
                int j = UnityEngine.Random.Range(0, 1000);
                if (j >= 500)
                {
                    mLeftRight = true;
                }
                else
                {
                    mLeftRight = false;
                }                                 
                mTimeInterval = Time.time;
                mAudioSource.Play();
                mLRDone = true;//下次弹跳方向判断完毕  
            }
        }
    }

    private void MoveRight()
    {
        this.transform.localPosition = new Vector3(this.transform.localPosition.x + 3f * Time.deltaTime, 1f+2 * mAxisY, 0);
    }

    private void MoveLeft()
    {
        this.transform.localPosition = new Vector3(this.transform.localPosition.x - 3f * Time.deltaTime, 1f+2 * mAxisY, 0);
    }

    private void Drop()
    {
        this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y - 9.8f * Time.deltaTime, 0);
    }
}
