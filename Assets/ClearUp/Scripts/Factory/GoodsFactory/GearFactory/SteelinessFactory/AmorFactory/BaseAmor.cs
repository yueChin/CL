using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAmor :BaseSteeliness,IAmor,IProtectionAction{
    protected AmorType _AmorType;
    protected int _AmorDef;
    public BaseAmor(int price) : base(price) { }    
    public virtual AmorType SetAmorType(AmorType amorType)
    {
        _AmorType = amorType;
        return _AmorType;
    }

    public int SetDefenseValue()
    {
        _AmorDef = PropertiesUtils.GetValueByProperties(_AmorType);
        _AmorDef = ((int)_DurabilityType * RareMultimes((int)_DurabilityType, System.Enum.GetValues(typeof(DurabilityType)).Length)
           + (int)_CategoryType * RareMultimes((int)_CategoryType, System.Enum.GetValues(typeof(CategoryType)).Length));
        _ArmamentValue = _AmorDef;
        return _AmorDef;
    }

    public int GetDefenseValue()
    {
        return _AmorDef;
    }

    public override void SetName()
    {
        _Name = _AmorType.ToString();
    }

    public override void SetDescription()
    {
        _Description = string.Format("部位：{0}\n {1}\n {2}\n {3}\n 价格：{4}\n 钢值：{5}", PropertiesUtils.GetDescByProperties(_PartOfBodyType),
            PropertiesUtils.GetDescByProperties(_AmorType), PropertiesUtils.GetDescByProperties(_CategoryType), PropertiesUtils.GetDescByProperties(_DurabilityType),
            _Price,_SteelPrice);
    }

    public override void SetSprite()
    {
        _Sprite = FactoryManager.AssetFactory.LoadSprite(GameControl.GetGameControl.SpritePathDict.TryGet(_Name));
    }

    public override void SetPartOfBodyType()
    {
        _PartOfBodyType = PropertiesUtils.GetPartByProperties(_AmorType);
    }
}
