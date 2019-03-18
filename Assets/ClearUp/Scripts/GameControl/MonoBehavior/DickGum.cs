using DG.Tweening;
using UnityEngine;

public class DickGum : MonoBehaviour {

    private Pornwall mPornwall;
    private bool mIsTrigger;
    private void Start()
    {
        mPornwall = gameObject.transform.parent.GetComponent<Pornwall>();
        mPornwall.OnRigidbodySleepEventHandle += new Pornwall.OnRigidbodySleep(Result);
    }

    private void OnDestroy()
    {
        mPornwall.OnRigidbodySleepEventHandle -= new Pornwall.OnRigidbodySleep(Result);
    }

    /// <summary>
    /// 确认是否连击
    /// </summary>
    private void Result()
    {
        this.transform.DOShakePosition(0.3f, 0.1f);
        if (mIsTrigger && mIsTrigger)
        {
            GameControl.GetGameControl.ShowCombo(true);
            TriggerEvent();          
        }
        else { GameControl.GetGameControl.ShowCombo(false); }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            mIsTrigger = true;
        }
    }

    private void TriggerEvent()
    {
        FactoryManager.EventFactory.TriggerAdventureEvent(this.tag, this.transform.parent.tag, this.transform.position);
    }

}
