using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 背包栏
/// </summary>
public class PackageInventory : BaseInventory
{
    private Dictionary<int, BaseGoods> dGoodsDict;
    public override void ClearInventory()
    {
        if(dGoodsDict != null)
            dGoodsDict.Clear();
    }

    public void GainGoods(BaseGoods baseGoods)
    {
        if (dGoodsDict == null) { dGoodsDict = new Dictionary<int, BaseGoods>(); }
        dGoodsDict.Add(baseGoods.GetHashCode(), baseGoods);
    }

    /// <summary>
    /// 删除物品
    /// </summary>
    /// <param name="baseItem"></param>
    /// <returns></returns>
    public bool RemoveGoods(BaseGoods baseGoods)
    {
        if (dGoodsDict == null)
        {
            UnityEngine.Debug.Log("物品栏为空");
            return false;
        }
        if (dGoodsDict.ContainsKey(baseGoods.GetHashCode()))
        {
            return dGoodsDict.Remove(baseGoods.GetHashCode());
        }
        else
        {
            UnityEngine.Debug.Log("请求删除的装备不存在"); return false;
        }
    }
}
