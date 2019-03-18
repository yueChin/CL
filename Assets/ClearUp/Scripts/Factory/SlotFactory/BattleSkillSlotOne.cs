using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSkillSlotOne : BattleSkillSlot {

    protected override void OnDoubleClick()
    {
        GameControl.GetGameControl.ReleasePlayerSkill(_BaseGood as BattleSkill);
        _AudioSource.Play();
    }

}
