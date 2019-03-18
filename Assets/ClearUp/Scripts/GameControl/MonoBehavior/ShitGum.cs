using UnityEngine;
using DG.Tweening;

public class ShitGum : MonoBehaviour {

    private Fuckwall mFuckwall;
    private bool mIsTrigger;
    private void Start()
    {
        mFuckwall = gameObject.transform.parent.GetComponent<Fuckwall>();
        mFuckwall.OnRigidbodySleepEventHandle += new Fuckwall.OnRigidbodySleep(Result);
    }

    private void OnDestroy()
    {
        mFuckwall.OnRigidbodySleepEventHandle -= new Fuckwall.OnRigidbodySleep(Result);
    }

    /// <summary>
    /// 确认是否连击
    /// </summary>
    private void Result()
    {
        this.transform.DOShakePosition(0.3f, 0.1f);
        if (mIsTrigger)
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
