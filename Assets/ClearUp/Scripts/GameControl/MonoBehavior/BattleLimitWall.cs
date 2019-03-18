
using UnityEngine;
using DG.Tweening;

public class BattleLimitWall : MonoBehaviour {

    private AudioSource mAudioSource;
    private bool mIsIng;
	// Use this for initialization
	void Start () {
        mAudioSource = this.gameObject.GetComponent<AudioSource>();        
	}

    private void OnEnable()
    {
        //Debug.Log("战斗刚起");
        mIsIng = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.transform.tag + "结束战斗人物");
        if (other.transform.CompareTag("Enemy") || other.transform.CompareTag("Player"))
        {
            if (!mIsIng)
            {
                //Debug.Log("战斗结束");
                mIsIng = true;
                other.transform.GetComponent<Rigidbody>().Sleep();
                GameControl.GetGameControl.BattleEnd(other.transform.tag);
                mAudioSource.Play();
            }
        }
    }
}
