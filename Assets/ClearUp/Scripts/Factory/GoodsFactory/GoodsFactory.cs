using UnityEngine;
using System.Diagnostics;

public class GoodsFactory{
    private WeaponFactory mWeaponFactory;
    private AmorFactory mAmorFactory;
    private EquipmentFacoty mEquipmentFactory;
    private SkillFactory mSkillFactory;
    private ItemFactory mItemFactory;

    private T SetGoods<T>(T t) where T : BaseGoods
    {
        t.SetName();
        t.SetSprite();
        t.SetDescription();
        return t;
    }

    private T SetSteeliness<T>(T t,int steel) where T : BaseSteeliness
    {
        t.SetSteel(steel);
        return t;
    }

    private T SetGears<T>(T t, DurabilityType durabilityType, CategoryType categoryType) where T : BaseGears
    {
        t.SetCategoryType(categoryType);
        t.SetDurabilityType(durabilityType);
        return t;
    }

    public BaseItem GetItem(int price,ItemType itemType)
    {
        if (itemType == ItemType.None) { return null; }
        if (mItemFactory == null) { mItemFactory = new ItemFactory(); }
        BaseItem baseItem = mItemFactory.GetItem(price);
        baseItem.SetItemType(itemType);
        baseItem = SetGoods(baseItem);
        return baseItem;
    }

    public BaseSkill GetSkill(int price,SkillType skillType)
    {
        if (mSkillFactory == null) { mSkillFactory = new SkillFactory(); }
        BaseSkill baseSkill = mSkillFactory.GetSkill(price);
        baseSkill.SetNormalSkill(skillType);
        baseSkill = SetGoods(baseSkill);
        return baseSkill;
    }

    public BattleSkill GetBattleSkill(int price,BattleSkillType battleSkillType)
    {
        if (mSkillFactory == null) { mSkillFactory = new SkillFactory(); }
        BattleSkill battleSkill = mSkillFactory.GetBattleSkill(price);
        battleSkill.SetBattleSkill(battleSkillType);
        battleSkill = SetGoods(battleSkill);
        return battleSkill;
    }

    public BaseWeapon GetWeapon(int price,DurabilityType durabilityType, CategoryType categoryType,int steel,WeaponType weaponType)
    {
        if (mWeaponFactory == null) { mWeaponFactory = new WeaponFactory(); }
        BaseWeapon baseWeapon = mWeaponFactory.GetWeapon(price);
        baseWeapon.SetWeaponType(weaponType);
        baseWeapon.SetPartOfBodyType();
        baseWeapon = SetGears(baseWeapon, durabilityType, categoryType);
        baseWeapon = SetSteeliness(baseWeapon, steel);
        baseWeapon = SetGoods(baseWeapon);
        baseWeapon.SetDamage();
        return baseWeapon;
    }

    public BaseEquipment GetEquipment(int price, DurabilityType durabilityType, CategoryType categoryType, EquipmentType equipmentType)
    {
        if (mEquipmentFactory == null) { mEquipmentFactory = new EquipmentFacoty(); }
        BaseEquipment baseEquipment = mEquipmentFactory.GetEquipment(price);
        baseEquipment.SetEquipment(equipmentType);
        baseEquipment.SetPartOfBodyType();
        baseEquipment = SetGears(baseEquipment, durabilityType, categoryType);
        baseEquipment = SetGoods(baseEquipment);
        baseEquipment.SetDefenseValue();
        return baseEquipment;
    }

    public BaseAmor GetAmor(int price, DurabilityType durabilityType, CategoryType categoryType, int steel, AmorType amorType)
    {
        if (mAmorFactory == null) { mAmorFactory = new AmorFactory(); }
        BaseAmor baseAmor = mAmorFactory.GetAmor(price);
        baseAmor.SetAmorType(amorType);
        baseAmor.SetPartOfBodyType();
        baseAmor = SetSteeliness(baseAmor,steel);
        baseAmor = SetGears(baseAmor, durabilityType, categoryType);
        baseAmor = SetGoods(baseAmor);
        baseAmor.SetDefenseValue();
        return baseAmor;
    }
}
