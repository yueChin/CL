using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPrivate : BaseEnemyCreature
{
    public EnemyPrivate(EnemyType enemyType) : base(enemyType) { }

    public override void AdaptLevel()
    {
        _LevelOfDanger = Random.Range(1, 6);
    }
}
