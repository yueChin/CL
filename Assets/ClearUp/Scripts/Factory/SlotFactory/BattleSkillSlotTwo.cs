using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSkillSlotTwo : BattleSkillSlot {
    private float mTriggerTime;
    private float mTime;
    private bool mIsTrigger;
    protected override void OnEnable()
    {
        base.OnEnable();
        mTime = Time.time;
        mTriggerTime = Random.Range(3, 20);
        mIsTrigger = false;
    }

    protected override void Update()
    {
        base.Update();
        if (Time.time - mTime > mTriggerTime && !mIsTrigger)
        {
            GameControl.GetGameControl.ReleaseEnemySkill(_BaseGood as BattleSkill);
            _AudioSource.Play();
            mIsTrigger = true;
        }
    }
}
