using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : BaseSlot
{
    private Text mSkillLevel;
    private Image mMaskImage;
    // Use this for initialization
    protected override void Awake()
    {
        _AudioSource = this.transform.GetComponent<AudioSource>();
        _SlotButton = this.transform.GetComponent<Button>();
        _Image = this.transform.GetComponent<Image>();
    }

    protected override void Start()
    {
        base.Start();
        mSkillLevel = this.gameObject.GetComponentInChildren<Text>();
        mMaskImage = this.gameObject.GetComponentInChildren<Image>();
        _AudioSource.clip = FactoryManager.AssetFactory.LoadAudioClip("Audios/SkillUp");
        //技能事件
        EventCenter.AddListener<int,string>(EventType.SkillLevelUp,SkillUp);
        EventCenter.AddListener(EventType.StartGaming,SkillInit);
        EventCenter.AddListener(EventType.OverGaming,Destroy);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        EventCenter.RemoveListener<int, string>(EventType.SkillLevelUp, SkillUp);
        EventCenter.RemoveListener(EventType.StartGaming, SkillInit);
        EventCenter.RemoveListener(EventType.OverGaming, Destroy);
    }

    private void SkillInit()
    {
        mSkillLevel.text = string.Empty;
        mSkillLevel.gameObject.SetActive(false);
        mMaskImage.gameObject.SetActive(true);
    }

    /// <summary>
    /// 技能升级
    /// </summary>
    /// <param name="skillLevel">技能等级</param>
    /// <param name="skillname">技能名称</param>
    private void SkillUp(int skillLevel, string skillname)
    {
        if (skillname == _BaseGood.Name)
        {
            if (skillLevel > 0)
            {
                mMaskImage.gameObject.SetActive(false);
                mSkillLevel.gameObject.SetActive(true);
                mSkillLevel.text = skillLevel.ToString();
            }
            _AudioSource.Play();
        }
    }

    //当鼠标在技能格子上的时候，通知uimgr显示该技能介绍，要把技能介绍传过去
    protected override void OnSingleClick()
    {
        base.OnSingleClick();
        UIManager.GetUIManager.ShowSkillSlotInfo(this.transform.position, _BaseGood.Description);//通知uimgr显示技能信息
    }

    protected override void Destroy()
    {
        ObjectsPoolManager.DestroyActiveObject("UI/Slots/SmallSlot", this.gameObject);
    }
}
