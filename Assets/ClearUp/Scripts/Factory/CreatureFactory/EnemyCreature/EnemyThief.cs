using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThief : BaseEnemyCreature
{
    public EnemyThief(EnemyType enemyType) : base(enemyType) { }

    public override void AdaptLevel()
    {
        _LevelOfDanger = 1;
    }
}
