using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyCreature:ICreature {
    void AdaptLevel();
    void AdaptEquipment();
    void AdjuestBattleSkill();
}
