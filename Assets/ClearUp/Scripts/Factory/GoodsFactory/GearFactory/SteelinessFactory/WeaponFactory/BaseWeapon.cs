using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon :BaseSteeliness,IWeapon,IWeaponAction{
    protected WeaponType _WeaponType;
    protected int _WeaponDamage;
    public BaseWeapon(int price):base(price) { }   
    public WeaponType SetWeaponType(WeaponType weaponType)
    {
        _WeaponType = weaponType;
        return _WeaponType;

    }

    public int SetDamage()
    {
        _WeaponDamage = PropertiesUtils.GetValueByProperties(_WeaponType);
        _WeaponDamage *= ((int)_DurabilityType * RareMultimes((int)_DurabilityType, System.Enum.GetValues(typeof(DurabilityType)).Length - 1)
           + (int)_CategoryType * RareMultimes((int)_CategoryType, System.Enum.GetValues(typeof(CategoryType)).Length - 1))
           * ((int)_WeaponType > 7 ? 2 : 1);
        _ArmamentValue = _WeaponDamage;
        return _WeaponDamage;
    }
    
    public override void SetName()
    {
        _Name = _WeaponType.ToString();
    }

    public override void SetDescription()
    {
        _Description = string.Format("部位：{0}\n {1}\n {2}\n {3}\n 价格：{4}\n 钢值：{5}", PropertiesUtils.GetDescByProperties(_PartOfBodyType),
            PropertiesUtils.GetDescByProperties(_WeaponType), PropertiesUtils.GetDescByProperties(_CategoryType), PropertiesUtils.GetDescByProperties(_DurabilityType),
            _Price, _SteelPrice);
    }

    public override void SetSprite()
    {
        _Sprite = FactoryManager.AssetFactory.LoadSprite(GameControl.GetGameControl.SpritePathDict.TryGet(_Name));
    }

    public override void SetPartOfBodyType()
    {
        _PartOfBodyType = PropertiesUtils.GetPartByProperties(_WeaponType);
    }

    public int GetDamageValue() { return _WeaponDamage; }

}
