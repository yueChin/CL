using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 商店栏
/// </summary>
public class BussinessInventory : BaseInventory,IBussinessInventoryAction
{
    private Dictionary<int, BaseGoods> dGoodsDict;
    private List<BaseGoods> lbaseGoods;

    /// <summary>
    /// 把某个物品放入，初始化用
    /// </summary>
    /// <param name="baseGoods"></param>
    public override void PutGoodIn(BaseGoods baseGoods) 
    {
        if (dGoodsDict == null)
        {
            dGoodsDict = new Dictionary<int, BaseGoods>();
        }
        dGoodsDict.Add(baseGoods.GetHashCode(), baseGoods);
    }

    /// <summary>
    /// 清理背包
    /// </summary>
    public override void ClearInventory()
    {
        if (dGoodsDict != null)
        {
            dGoodsDict.Clear();
        }      
    }

    /// <summary>
    /// 卖出某个物品，即删除
    /// </summary>
    /// <param name="baseGoods">物品</param>
    public void SellGoods(BaseGoods baseGoods)
    {
        if (dGoodsDict == null)
        {
            UnityEngine.Debug.Log("商店栏为空");
            return;
        }
        if (dGoodsDict.ContainsKey(baseGoods.GetHashCode()))
        {
            dGoodsDict.Remove(baseGoods.GetHashCode());
        }
        else
        {
            UnityEngine.Debug.Log("请求购买的物品不存在");
        }
    }

    /// <summary>
    /// 回购主角某个物品，即增加
    /// </summary>
    /// <param name="baseGoods"></param>
    public void BuyGoods(BaseGoods baseGoods)
    {
        if (baseGoods == null)
        {
            UnityEngine.Debug.Log("物品栏为空");
            return;
        }
        if (dGoodsDict == null)
        {
            dGoodsDict = new Dictionary<int, BaseGoods>();
        }
        dGoodsDict.Add(baseGoods.GetHashCode(), baseGoods);
    }
}
