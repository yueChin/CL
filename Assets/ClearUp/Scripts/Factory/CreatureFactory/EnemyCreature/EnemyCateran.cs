using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCateran : BaseEnemyCreature
{
    public EnemyCateran(EnemyType enemyType) : base(enemyType) { }

    public override void AdaptLevel()
    {
        _LevelOfDanger = Random.Range(2, 5);
    }
}
