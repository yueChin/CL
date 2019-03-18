using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotFactory {

    private string mSlotName = string.Empty;
    private const string mSlotPath = "UI/Slots/";

    public T GetSlot<T>(BaseGoods baseGoods) where T : BaseSlot
    {
        T t;       
        if (typeof(T).IsAssignableFrom(typeof(BussinessSlot)))
        {
            t = LoadBusinessSlot<T>();
        }
        else if (typeof(T).IsSubclassOf(typeof(BattleSkillSlot)))
        {
            t = LoadBattleSkillSlot<T>();
        }
        else if (typeof(T).IsAssignableFrom(typeof(SkillSlot)))
        {
            t = LoadSlot<T>("SmallSlot");
        }
        else if(typeof(T).IsAssignableFrom(typeof(PackageSlot)))
        {
            t = LoadPackageSlot<T>();
        }
        else if(typeof(T).IsAssignableFrom(typeof(GearSlot)))
        {
            t = LoadGearSlot<T>();
        }
        else
        {
            t = LoadSlot<T>("Slot");
        }
        t.SetSlotGood(baseGoods);
        return t;
    }

    //可以的话重写一下,slot复用的话，脚本会有重合
    private T LoadSlot<T>(string name) where T : BaseSlot
    {
        mSlotName = name;
        GameObject gameObject = FactoryManager.AssetFactory.LoadGameObject(mSlotPath + mSlotName);      
        return SetSlot<T>(gameObject);
    }

    private T LoadBusinessSlot<T>() where T : BaseSlot
    {
        mSlotName = "BigSlot";
        GameObject gameObject = ObjectsPoolManager.GetObject("BusinessSlot");
        if (gameObject == null)
        {
            gameObject = FactoryManager.AssetFactory.LoadGONotPool(mSlotPath + mSlotName);
            ObjectsPoolManager.PushObject("BusinessSlot", gameObject);
        }
        return SetSlot<T>(gameObject);
    }

    private T LoadBattleSkillSlot<T>() where T : BaseSlot
    {
        mSlotName = "BigSlot";
        GameObject gameObject = ObjectsPoolManager.GetObject("BattleSlot");
        if (gameObject == null)
        {
            gameObject = FactoryManager.AssetFactory.LoadGONotPool(mSlotPath + mSlotName);
            ObjectsPoolManager.PushObject("BattleSlot", gameObject);
        }
        return SetSlot<T>(gameObject);
    }

    private T LoadGearSlot<T>() where T : BaseSlot
    {
        mSlotName = "Slot";
        GameObject gameObject = ObjectsPoolManager.GetObject("GearSlot");
        if (gameObject == null)
        {
            gameObject = FactoryManager.AssetFactory.LoadGONotPool(mSlotPath + mSlotName);
            ObjectsPoolManager.PushObject("GearSlot", gameObject);
        }
        return SetSlot<T>(gameObject);
    }

    private T LoadPackageSlot<T>() where T : BaseSlot
    {
        mSlotName = "Slot";
        GameObject gameObject = ObjectsPoolManager.GetObject("PackageSlot");
        if (gameObject == null)
        {
            gameObject = FactoryManager.AssetFactory.LoadGONotPool(mSlotPath + mSlotName);
            ObjectsPoolManager.PushObject("PackageSlot", gameObject);
        }
        return SetSlot<T>(gameObject);
    }

    private T SetSlot<T>(GameObject gameObject) where T:BaseSlot
    {
        gameObject.transform.SetParent(UIManager.GetUIManager.GetCanvasTransform);
        T t = gameObject.GetComponent<T>();
        if (t == null) { return gameObject.AddComponent<T>(); }
        return t;
    }
}
