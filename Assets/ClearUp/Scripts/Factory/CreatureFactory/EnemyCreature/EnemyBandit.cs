using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBandit : BaseEnemyCreature
{
    public EnemyBandit(EnemyType enemyType) : base(enemyType) { }

    public override void AdaptLevel()
    {
        _LevelOfDanger = Random.Range(1, 4);
    }
}
