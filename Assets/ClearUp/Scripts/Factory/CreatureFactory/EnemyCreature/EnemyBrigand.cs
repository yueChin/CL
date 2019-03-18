using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrigand : BaseEnemyCreature
{
    public EnemyBrigand(EnemyType enemyType) : base(enemyType) { }

    public override void AdaptLevel()
    {
        _LevelOfDanger = Random.Range(3, 6);
    }
}
