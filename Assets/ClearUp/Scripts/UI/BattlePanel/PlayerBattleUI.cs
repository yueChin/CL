using DG.Tweening;
using UnityEngine;

public class PlayerBattleUI : CreatureBattleUI {
    protected override void Awake()
    {
        base.Awake();
        EventCenter.AddListener<float>(EventType.PlayerSkillPowerUPUI,SliderValueChange);
        EventCenter.AddListener<BattleSkill>(EventType.AddPlayerSkillUI, AddBattleSkillUI);
    }

    protected override void AddBattleSkillUI(BattleSkill battleSkill)
    {
        BattleSkillSlot battleSkillSlot = FactoryManager.SlotFactory.GetSlot<BattleSkillSlotOne>(battleSkill);
        BattleSkillSlotAnimation(battleSkillSlot, new UnityEngine.Vector3(-1000, 0, 0));
    }

    protected override void BattleSkillSlotAnimation(BattleSkillSlot battleSkillSlot, Vector3 StartPos)
    {
        base.BattleSkillSlotAnimation(battleSkillSlot, StartPos);
        Vector3 vector3 = new Vector3(530 + 140 * _BattleSkills.childCount, 0, 0); 
        battleSkillSlot.transform.DOLocalMove(vector3, 1f).OnComplete(() =>
        {
            battleSkillSlot.transform.SetParent(_BattleSkills);
            battleSkillSlot.transform.localScale = Vector3.one;
        });
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener<float>(EventType.PlayerSkillPowerUPUI, SliderValueChange);
        EventCenter.RemoveListener<BattleSkill>(EventType.AddPlayerSkillUI, AddBattleSkillUI);
    }
}
