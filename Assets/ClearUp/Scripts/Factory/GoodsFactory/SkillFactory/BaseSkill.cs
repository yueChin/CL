using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSkill : BaseGoods,ISkill,INormalSkill
{
    public int SkillLevel { get { return _SkillLevel; } }
    protected int _SkillLevel;
    private SkillType mSkillType;
    public BaseSkill(int price) : base(price)
    {
        _SkillLevel = 0;
    }

    public int LevelUp()
    {
        _SkillLevel++;
        return _SkillLevel;
    }

    public SkillType SetNormalSkill(SkillType skillType)
    {
        mSkillType = skillType;
        return mSkillType;
    }

    public override void SetName()
    {
        _Name = mSkillType.ToString();
    }

    public override void SetSprite()
    {
        _Sprite = FactoryManager.AssetFactory.LoadSprite(GameControl.GetGameControl.SpritePathDict.TryGet(_Name));
    }

    public override void SetDescription()
    {
        _Description = string.Format("描述：{0}\n 价格：{1}\n", PropertiesUtils.GetDescByProperties(mSkillType),_Price);
    }

    /// <summary>
    /// 释放技能？
    /// </summary>
    public float ReleaseSkill() 
    {
        return _SkillLevel;
    }

    
}
