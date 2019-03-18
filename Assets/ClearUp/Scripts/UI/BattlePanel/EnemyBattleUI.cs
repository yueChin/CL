using DG.Tweening;
using System;
using UnityEngine;

public class EnemyBattleUI : CreatureBattleUI {
    private delegate void EventHandler<TEventArgs>(object sender, TEventArgs e) where TEventArgs : EventArgs;
    

    protected override void Awake ()
    {
        base.Awake();
        EventCenter.AddListener<float>(EventType.EnemySkillPoweUpUI,SliderValueChange);
        EventCenter.AddListener<BattleSkill>(EventType.AddEnemtySkillUI,AddBattleSkillUI);
	}

    protected override void AddBattleSkillUI(BattleSkill battleSkill)
    {
        BattleSkillSlot battleSkillSlot = FactoryManager.SlotFactory.GetSlot<BattleSkillSlotTwo>(battleSkill);
        BattleSkillSlotAnimation(battleSkillSlot, new UnityEngine.Vector3(-1000,0,0));
    }

    protected override void BattleSkillSlotAnimation(BattleSkillSlot battleSkillSlot, Vector3 StartPos)
    {
        base.BattleSkillSlotAnimation(battleSkillSlot, StartPos);
        Vector3 vector3 = new Vector3(380 - 140 * _BattleSkills.childCount,0, 0);
        battleSkillSlot.transform.DOLocalMove(vector3, 1f).OnComplete(() =>
        {
            battleSkillSlot.transform.SetParent(_BattleSkills);
            battleSkillSlot.transform.SetAsFirstSibling();
            battleSkillSlot.transform.localScale = Vector3.one;
        });
    }

    private void OnDestroy()
    {        
        EventCenter.RemoveListener<float>(EventType.EnemySkillPoweUpUI, SliderValueChange);
        EventCenter.RemoveListener<BattleSkill>(EventType.AddEnemtySkillUI, AddBattleSkillUI);
    }
}
