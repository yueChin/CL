using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRobber : BaseEnemyCreature
{
    public EnemyRobber(EnemyType enemyType) : base(enemyType) { }

    public override void AdaptLevel()
    {
        _LevelOfDanger = 2;
    }
}
