using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MessageUI : MonoBehaviour {
    Text mMessage;
    // Use this for initialization
    void Awake () {
        mMessage = this.GetComponent<Text>();
    }
	
    public void SetMessage(string message,Transform parent,float delayTime = 0)
    {
        //Debug.Log("显示消息"+message + "父对象"+ parent);
        this.transform.SetParent(parent,true);
        this.transform.SetSiblingIndex(parent.childCount - 1);
        this.transform.localPosition = new Vector3(0, 500, 0);
        mMessage.text = message;
        this.transform.DOLocalMoveY(600, 2f).SetDelay(delayTime).OnComplete(() => {
            mMessage.DOFade(0, 1f).SetDelay(5f).OnComplete(() =>
            {
                ObjectsPoolManager.DestroyActiveObject("UI/Bars/MessageBar", this.gameObject);
            });
        });
    }
}
