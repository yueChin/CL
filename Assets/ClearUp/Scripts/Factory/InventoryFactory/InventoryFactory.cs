using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryFactory  {

    //public T GetBaseInventory<T>() where T : BaseInventory, new()
    //{
    //    T t = new T();
    //    return t;
    //}

    public BussinessInventory GetBussinessInventory()
    {
        BussinessInventory bussinessInventory = new BussinessInventory();
        return bussinessInventory;
    }

    public GearsInventory GetCharacterInventory()
    {
        GearsInventory gearsInventory = new GearsInventory();
        return gearsInventory;
    }

    public SkillsInventory GetSkillsInventory()
    {
        SkillsInventory skillsInventory = new SkillsInventory();
        return skillsInventory;
    }

    public PackageInventory GetPackageInventory()
    {
        PackageInventory packageInventory = new PackageInventory();
        return packageInventory;
    }
}
