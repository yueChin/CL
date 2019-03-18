using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBussinessInventoryAction  {
    /// <summary>
    /// 卖出货物
    /// </summary>
    void SellGoods(BaseGoods baseGoods);
    /// <summary>
    /// 购入货物
    /// </summary>
    void BuyGoods(BaseGoods baseGoods);
    /// <summary>
    /// 移除货物
    /// </summary>
    /// <param name="baseGoods"></param>
}
