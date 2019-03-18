using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CreatureBattleUI : MonoBehaviour {
    protected Transform _BattleSkills;
    protected Slider _Slider;

    private Transform mTransform;
    //protected Transform _Armament;
    //protected Text _ArmaValue;
    //protected Image _ArmaImage;
    // Use this for initialization   

    protected virtual void Awake()
    {
        _Slider = UITool.FindChild<Slider>(this.transform, "Slider");
        _BattleSkills = UITool.FindChild<Transform>(this.transform,"Skills");
        mTransform = UITool.FindChild<Transform>(this.transform, "BattleSkillsFrame");
        //_Armament = UITool.FindChild<Transform>(this.transform, "Text");
        // _ArmaValue = _Armament.GetComponentInChildren<Text>();
        //_ArmaImage = _Armament.GetComponentInChildren<Image>();
    }

    protected virtual void OnEnable()
    {
        if (_Slider != null) { _Slider.value = 0; }
    }

    protected virtual void SliderValueChange(float value)
    {
        _Slider.value = value;
    }

    protected virtual void AddBattleSkillUI(BattleSkill battleSkill)
    {
        BattleSkillSlotAnimation(FactoryManager.SlotFactory.GetSlot<BattleSkillSlot>(battleSkill), Vector3.zero);
    }

    protected virtual void BattleSkillSlotAnimation(BattleSkillSlot battleSkillSlot,Vector3 StartPos)
    {
        battleSkillSlot.transform.SetParent(mTransform);
        battleSkillSlot.transform.SetSiblingIndex(this.transform.childCount - 1);
        battleSkillSlot.transform.localPosition = StartPos;
        //前面有块了怎么解决？每次都到底吗？               
    }
}
