using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEquipment :BaseGears ,IProtection,IEquipment,IProtectionAction{
    protected EquipmentType _EquipmentType;
    protected int _EquipmentDef;

    public BaseEquipment(int price) : base(price) { }

    public int GetDefenseValue()
    {
        return _EquipmentDef;
    }

    public int SetDefenseValue()
    {
        _EquipmentDef = PropertiesUtils.GetValueByProperties(_EquipmentType);
        _EquipmentDef *= ((int)_DurabilityType * RareMultimes((int)_DurabilityType, System.Enum.GetValues(typeof(DurabilityType)).Length - 1)
           + (int)_CategoryType * RareMultimes((int)_CategoryType, System.Enum.GetValues(typeof(CategoryType)).Length - 1));
        return _EquipmentDef;
    }

    public EquipmentType SetEquipment(EquipmentType equipmentType)
    {
        _EquipmentType = equipmentType;
        return _EquipmentType;
    }

    public override void SetName()
    {
        _Name = _EquipmentType.ToString();
    }

    public override void SetDescription()
    {
        _Description = string.Format("部位：{0}\n {1}\n {2}\n {3}\n 价格：{4}\n", PropertiesUtils.GetDescByProperties(_PartOfBodyType), 
            PropertiesUtils.GetDescByProperties(_EquipmentType), PropertiesUtils.GetDescByProperties(_CategoryType), PropertiesUtils.GetDescByProperties(_DurabilityType),
            _Price);
        //string newText = string.Format("{0}\n\n<color=blue>武器类型：{1}\n攻击力：{2}</color>", text, wpTypeText, Damage);
    }

    public override void SetPartOfBodyType()
    {
        _PartOfBodyType = PropertiesUtils.GetPartByProperties(_EquipmentType);
    }

    public override void SetSprite()
    {
        _Sprite = FactoryManager.AssetFactory.LoadSprite(GameControl.GetGameControl.SpritePathDict.TryGet(_Name));
    }
}
