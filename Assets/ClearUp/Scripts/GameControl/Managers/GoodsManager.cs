using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsManager {

    private static GoodsManager mGoodsManager;
    public static GoodsManager GetGoodsManager
    {
        get
        {
            if (mGoodsManager == null)
            {
                mGoodsManager = new GoodsManager();
            }
            return mGoodsManager;
        }
    }

    /// <summary>
    /// 获取随机物品
    /// </summary>
    /// <param name="itemLevel">货物品质</param>
    /// <returns></returns>
    public BaseItem GetRandomItem(int itemLevel)
    {
        //Debug.Log("物品");
        return FactoryManager.GoodsFactory.GetItem(CalculatePrice(itemLevel), RandomItemType());
    }

    /// <summary>
    /// 获取随机技能
    /// </summary>
    /// <param name="itemLevel">技能品质</param>
    /// <returns></returns>
    public BaseSkill GetRandomSkill(int itemLevel)
    {
        //Debug.Log("地图技能");
        return FactoryManager.GoodsFactory.GetSkill(CalculatePrice(itemLevel), RandomSkillType());
    }

    /// <summary>
    /// 获取随机战斗技能
    /// </summary>
    /// <param name="itemLevel">技能品质</param>
    /// <returns></returns>
    public BattleSkill GetRandomBattleSkill(int itemLevel)
    {
        //Debug.Log("战斗技能");
        return FactoryManager.GoodsFactory.GetBattleSkill(CalculatePrice(itemLevel), RandomBattleSkillType());
    }

    /// <summary>
    /// 获取随机武器
    /// </summary>
    /// <param name="armmentLevel">武备等级</param>
    /// <returns></returns>
    public BaseWeapon GetRandomWeapon(int armmentLevel)
    {
        //Debug.Log("武器");
        return FactoryManager.GoodsFactory.GetWeapon(CalculatePrice(armmentLevel), 
            RandomDurability(armmentLevel), RandomCategory(armmentLevel), (armmentLevel > 7 ? Random.Range(1, 3) : 0), RandomWeaponType(armmentLevel));
    }

    /// <summary>
    /// 获取随机防具
    /// </summary>
    /// <param name="armmentLevel">武备等级</param>
    /// <returns></returns>
    public BaseGears GetRandomProtection(int armmentLevel)
    {
        if (armmentLevel > 2) { return GetRandomAmor(armmentLevel); }
        else { return GetRandomEquipment(armmentLevel); }
    }

    /// <summary>
    /// 获取随机装备
    /// </summary>
    /// <param name="armmentLevel"></param>
    /// <returns></returns>
    public BaseEquipment GetRandomEquipment(int armmentLevel)
    {
        //Debug.Log("装备");
        return FactoryManager.GoodsFactory.GetEquipment(CalculatePrice(armmentLevel), 
            RandomDurability(armmentLevel), RandomCategory(armmentLevel), RandomEquipmentType());
    }

    /// <summary>
    /// 获取随机装甲
    /// </summary>
    /// <param name="armmentLevel">武备等级</param>
    /// <returns></returns>
    public BaseAmor GetRandomAmor(int armmentLevel)
    {
        //Debug.Log("装甲");
        return FactoryManager.GoodsFactory.GetAmor(CalculatePrice(armmentLevel), 
            RandomDurability(armmentLevel), RandomCategory(armmentLevel), Random.Range(1,3), RandomAmorType());
    }

    /// <summary>
    /// 随机耐久
    /// </summary>
    /// <param name="armmentLevel">武备等级</param>
    /// <returns></returns>
    private DurabilityType RandomDurability(int armmentLevel)
    {
        DurabilityType durabilityType = (DurabilityType)TypeRandom.RandomProportion(armmentLevel, 2 * armmentLevel - 1);
        return durabilityType;
    }
    
    /// <summary>
    /// 随机耐久
    /// </summary>
    /// <param name="armmentLevel">武备等级</param>
    /// <returns></returns>
    private CategoryType RandomCategory(int armmentLevel)
    {
        CategoryType durabilityType = (CategoryType)TypeRandom.RandomProportion(armmentLevel, 2 * armmentLevel - 1);
        return durabilityType;
    }

    /// <summary>
    /// 随机武器
    /// </summary>
    /// <param name="armmentLevel">armmentLevel</param>
    /// <returns></returns>
    private WeaponType RandomWeaponType(int armmentLevel)
    {
        return (WeaponType)Random.Range(0, 7 + 10 * (armmentLevel > 3 ? 1 : 0));
    }

    /// <summary>
    /// 随机装备类型
    /// </summary>
    /// <returns></returns>
    private EquipmentType RandomEquipmentType()
    {
        return (EquipmentType)Random.Range(0, System.Enum.GetValues(typeof(EquipmentType)).Length);
    }

    /// <summary>
    /// 随机装甲类型
    /// </summary>
    /// <returns></returns>
    private AmorType RandomAmorType()
    {
        return (AmorType)Random.Range(0, System.Enum.GetValues(typeof(AmorType)).Length);
    }

    private BattleSkillType RandomBattleSkillType()
    {
        return (BattleSkillType)Random.Range(0, System.Enum.GetValues(typeof(BattleSkillType)).Length);
    }

    private SkillType RandomSkillType()
    {
        return (SkillType)Random.Range(0, System.Enum.GetValues(typeof(SkillType)).Length);
    }

    private ItemType RandomItemType()
    {
        return (ItemType)Random.Range(1, System.Enum.GetValues(typeof(ItemType)).Length);
    }

    /// <summary>
    /// 计算价格
    /// </summary>
    /// <param name="maxLevel"></param>
    /// <returns></returns>
    private int CalculatePrice(int maxLevel)
    {
        return maxLevel * 2;
    }

}
