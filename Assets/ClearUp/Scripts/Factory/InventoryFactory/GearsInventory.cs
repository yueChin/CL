using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 装备栏
/// </summary>
public class GearsInventory : BaseInventory,IGearsInventoryAction
{
    private int mDef;
    private int mAtk;
    public GearsInventory()
    {
        mDef = 0;
        mAtk = 0;
    }
    private Dictionary<PartOfBodyType,BaseGears> mGearsInventoryDict;
    /// <summary>
    /// 把装备放入，栏位已有的话返回
    /// </summary>
    /// <param name="baseGears"></param>
    public bool PutGearIn(BaseGears baseGears)
    {
        if (baseGears == null)
        {
            Debug.Log("放入装备栏的装备为空");
            return false;
        }
        PartOfBodyType partOfBodyType = baseGears.PartOfBodyType;
        if (mGearsInventoryDict == null) { mGearsInventoryDict = new Dictionary<PartOfBodyType, BaseGears>(); }
        if (!mGearsInventoryDict.ContainsKey(partOfBodyType))
        {
            mGearsInventoryDict.Add(partOfBodyType, baseGears);
            AddValue(baseGears);
            return true;
        }
        else
        {
            //判断装备战力值大小？
            //if (baseGears.GetArmamentValue() > mCharacterInventoryDict[partOfBodyType].GetGear().GetArmamentValue())
            //mCharacterInventoryDict[partOfBodyType].ExchangeGood(baseGears);
            return false;//npc随机完成不更改
        }
    }

    /// <summary>
    /// 把装备放入，栏位已有的话，替换掉栏位的装备
    /// </summary>
    /// <param name="baseGears"></param>
    public bool PushGear(BaseGears baseGears)
    {
        if (baseGears == null)
        {
            Debug.Log("放入装备栏的装备为空");
            return false;
        }
        PartOfBodyType partOfBodyType = baseGears.PartOfBodyType;
        if (mGearsInventoryDict == null) { mGearsInventoryDict = new Dictionary<PartOfBodyType, BaseGears>(); }
        if (!mGearsInventoryDict.ContainsKey(partOfBodyType))
        {
            mGearsInventoryDict.Add(partOfBodyType, baseGears);
            AddValue(baseGears);
            return true;
        }
        else
        {
            RemoveValue(mGearsInventoryDict[partOfBodyType]);
            AddValue(baseGears);
            mGearsInventoryDict[partOfBodyType] = baseGears;
            return false;
        }
    }

    /// <summary>
    /// 获取某个部位的装备
    /// </summary>
    /// <param name="baseGears"></param>
    /// <returns></returns>
    public BaseGears PickGear(PartOfBodyType partOfBodyType)
    {
        if (mGearsInventoryDict == null || mGearsInventoryDict.Keys.Count < 1) { UnityEngine.Debug.Log("装备栏为空"); return null; }
        if (!mGearsInventoryDict.ContainsKey(partOfBodyType)) { UnityEngine.Debug.Log("该部位的装备不存在"); return null; }
        else
        {
            BaseGears gears = mGearsInventoryDict.TryGet(partOfBodyType);
            RemoveValue(gears);
            mGearsInventoryDict.Remove(partOfBodyType);
            return gears;
        }
    }

    /// <summary>
    /// 获取一件已有的装备
    /// </summary>
    /// <returns></returns>
    public BaseGears GainOutGear()
    {
        if (mGearsInventoryDict == null || mGearsInventoryDict.Keys.Count == 0 || mGearsInventoryDict.Values.Count ==0)
        {
            UnityEngine.Debug.Log("装备栏为空");
            return null;
        }
        BaseGears[] baseGears = new BaseGears[] { };
        mGearsInventoryDict.Values.CopyTo(baseGears,0);
        if (baseGears.Length > 0)
        {
            BaseGears bg = baseGears[Random.Range(0, baseGears.Length)];
            mGearsInventoryDict.Remove(bg.PartOfBodyType);
            RemoveValue(bg);
            return bg;
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// 卸下装备
    /// </summary>
    /// <param name="baseGears">卸下的装备</param>
    /// <returns></returns>
    public bool DisChargeGear(BaseGears baseGears)
    {
        if (mGearsInventoryDict == null)
        {
            UnityEngine.Debug.Log("装备栏为空");
            return false;
        }
        if (mGearsInventoryDict.ContainsValue(baseGears))
        {
            RemoveValue(baseGears);
            return mGearsInventoryDict.Remove(baseGears.PartOfBodyType);
        }
        else
        {
            UnityEngine.Debug.Log("请求删除的装备不存在"); return false;
        }
    }

    /// <summary>
    /// 获取已有的装备个数
    /// </summary>
    /// <returns></returns>
    public int GetNumberOfGears()
    {       
        return mGearsInventoryDict == null? 0:mGearsInventoryDict.Values.Count;
    }

    /// <summary>
    /// 获取栏位的最大容量
    /// </summary>
    /// <returns></returns>
    public int GetMaxCapacity()
    {
        return System.Enum.GetValues(typeof(PartOfBodyType)).Length;
    }

    /// <summary>
    /// 清理背包
    /// </summary>
    public override void ClearInventory()
    {
        mDef = 0;
        mAtk = 0;
        if (mGearsInventoryDict != null)
        {
            mGearsInventoryDict.Clear();
        }      
    }

    public int GetDefValue() { return mDef; }
    public int GetDamageValue() { return mAtk; }
    public int GetArmamentValue() { return mAtk+mDef; }

    private void RemoveValue(BaseGears baseGears)
    {
        if (baseGears is IWeapon) { mAtk -= baseGears.GetArmamentValue(); }
        else if (baseGears is IProtection) { mAtk -= baseGears.GetArmamentValue(); }
    }

    private void AddValue(BaseGears baseGears)
    {
        if (baseGears is IWeapon) { mAtk += baseGears.GetArmamentValue(); }
        else if (baseGears is IProtection) { mAtk += baseGears.GetArmamentValue(); }
    }
}
