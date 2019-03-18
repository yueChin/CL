using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInventory {
    public virtual void PutGoodIn(BaseGoods baseGoods) { }
    public abstract void ClearInventory();
}
