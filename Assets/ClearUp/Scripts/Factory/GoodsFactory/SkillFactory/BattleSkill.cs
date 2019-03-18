using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSkill : BaseSkill,IBattleSkill
{
    public BattleSkillType BattleSkillType
    {
        get { return mBattleSkillType; }
    }
    private BattleSkillType mBattleSkillType;
    public BattleSkill(int price) : base(price) { }

    public BattleSkillType SetBattleSkill(BattleSkillType battleSkillType)
    {
        mBattleSkillType = battleSkillType;
        return mBattleSkillType;
    }

    public override void SetName()
    {
        _Name = mBattleSkillType.ToString();
    }

    public override void SetSprite()
    {
        _Sprite = FactoryManager.AssetFactory.LoadSprite(GameControl.GetGameControl.SpritePathDict.TryGet(_Name));
    }

    public override void SetDescription()
    {
        _Description = string.Format("描述：{0}\n 价格：{1}\n", PropertiesUtils.GetDescByProperties(mBattleSkillType), _Price);
    }

    public bool ReleaseSkill(ref RelaseSkill relaseSkill)
    {
        if (mBattleSkillType == BattleSkillType.Parry)
        {
            relaseSkill.DamagePower = relaseSkill.DamagePower > 0 ? -relaseSkill.DamagePower : relaseSkill.DamagePower;//负数轻击，对方按标准最小退后 
            relaseSkill.DamageDrate = relaseSkill.DamageDrate > 0 ? -relaseSkill.DamageDrate : relaseSkill.DamageDrate;//负数按标准退后
            relaseSkill.FuryGainRate *= Mathf.Pow(2f, _SkillLevel);//怒气获取按1.5倍比例增加
        }
        else if (mBattleSkillType == BattleSkillType.Block)
        {
            relaseSkill.DamagePower *= 0;//为0没有攻击动作，对方不退后
            relaseSkill.DamageDrate *= Mathf.Pow(0.5f, _SkillLevel);//减免退后
            relaseSkill.FuryGainRate *= Mathf.Pow(2f, _SkillLevel);//怒气获取按1.5倍比例增加
        }
        else if (mBattleSkillType == BattleSkillType.Swipe)
        {
            relaseSkill.DamagePower *= Mathf.Pow(2f, _SkillLevel);
            relaseSkill.DamageDrate = 1;
            relaseSkill.FuryGainRate = 1;
        }
        else if (mBattleSkillType == BattleSkillType.Impact)
        {
            relaseSkill.DamagePower = relaseSkill.DamagePower > 0 ? -relaseSkill.DamagePower : relaseSkill.DamagePower;//负数轻击，对方按标准最小退后 
            relaseSkill.DamageDrate *= Mathf.Pow(0.5f, _SkillLevel);//减免退后
            relaseSkill.FuryGainRate = 1;
        }
        else { return false; }        
        return true;
    }
}
