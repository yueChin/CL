using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemsUI : MonoBehaviour {
    private RectTransform mGoodsPackage;

    private void Awake()
    {
        mGoodsPackage = UITool.FindChild<RectTransform>(this.transform,"Goods");
    }

    public void PushItemIn(PackageSlot packageSlot)
    {
        packageSlot.transform.SetParent(mGoodsPackage);
        packageSlot.transform.localScale = Vector3.one;
    }
}
