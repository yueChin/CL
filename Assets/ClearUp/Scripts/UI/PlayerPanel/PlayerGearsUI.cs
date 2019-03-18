using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGearsUI : MonoBehaviour {

    private RectTransform mRectTransform;
    private void Awake()
    {
        mRectTransform = UITool.FindChild<RectTransform>(this.transform, "Gears");
    }

    public void PushGearIn(GearSlot gearSlot)
    {
        gearSlot.transform.SetParent(mRectTransform);
        gearSlot.transform.localScale = Vector3.one;
    }
}
