using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class QuestBarUI : MonoBehaviour {
    private Image mDoneFlag;
    private Text mQuestDescription;
    private AudioSource mAudioSource;
    private Quest mQuest;
    private Button mButton;
    // Use this for initialization
    void Awake()
    {
        mDoneFlag = this.transform.GetComponentInChildren<Image>();
        mDoneFlag.fillAmount = 0;
        mQuestDescription = this.transform.GetComponentInChildren<Text>();
        mAudioSource = this.transform.GetComponent<AudioSource>();
        mButton = this.GetComponent<Button>();
        mButton.onClick.AddListener(ShowDesc);
        //事件
        EventCenter.AddListener<Quest,BaseGoods>(EventType.QuestDone,QuestDone);
        EventCenter.AddListener(EventType.OverGaming,Destroy);
    }

    private void OnEnable()
    {
        if (mDoneFlag != null) { mDoneFlag.fillAmount = 0; }
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventType.OverGaming, Destroy);
        EventCenter.RemoveListener<Quest, BaseGoods>(EventType.QuestDone, QuestDone);
    }

    private void Destroy()
    {
        ObjectsPoolManager.DestroyActiveObject("UI/Bars/QuestBar", this.gameObject);
    }

    private void ShowDesc()
    {
        UIManager.GetUIManager.ShowItemSlotInfo(this.transform.position,mQuest.Description);
    }

    private void QuestDone(Quest quest,BaseGoods baseGoods)
    {
        if (mQuest.Equals(quest))
        {
            mDoneFlag.DOFillAmount(1, 0.3f).OnComplete(() => { mAudioSource.Play(); SlotGain(baseGoods);GameObject.Destroy(this.gameObject); });
        }       
    }

    public void SetQuest(Quest quest)
    {
        mQuest = quest;
        mQuestDescription.text = PropertiesUtils.GetNameByProperties(quest.QuestType) ;
    }

    public void SlotGain(BaseGoods baseGoods)
    {
        if (baseGoods == null)
        {
            GameControl.GetGameControl.GainSteel(1);
        }
        else
        {
            PackageSlot packageSlot = FactoryManager.SlotFactory.GetSlot<PackageSlot>(baseGoods);
            packageSlot.transform.position = this.transform.position;
            packageSlot.SlotAsBootyMove();
        }
    }
}
