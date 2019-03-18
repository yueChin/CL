using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattleSkill : ISkill {
    BattleSkillType SetBattleSkill(BattleSkillType battleSkillType);
    bool ReleaseSkill(ref RelaseSkill relaseSkill);
}
