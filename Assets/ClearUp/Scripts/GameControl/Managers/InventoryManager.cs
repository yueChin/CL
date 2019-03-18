using System.Collections.Generic;
using UnityEngine;
public class InventoryManager {

    private static InventoryManager mInventoryManager;
    public static InventoryManager GetInventoryManager
    {
        get
        {
            if (mInventoryManager == null)
            {
                mInventoryManager = new InventoryManager();
            }
            return mInventoryManager;
        }
    }

    private static Dictionary<BaseCreature, BussinessInventory> dBussinessInventorys; //同一个生物有多个背包 如何解决？
    private static Dictionary<BaseCreature, GearsInventory> dCharacterInventorys;
    private static Dictionary<BaseCreature, PackageInventory> dPackageInventory;
    private static Dictionary<BaseCreature, SkillsInventory> dSkillInvenoty;
    private static Dictionary<BaseCreature, BaseInventory> dBaseInventoryDict;
    private InventoryManager()
    {
        dBussinessInventorys = new Dictionary<BaseCreature, BussinessInventory>();
        dCharacterInventorys = new Dictionary<BaseCreature, GearsInventory>();
        dPackageInventory = new Dictionary<BaseCreature, PackageInventory>();
        dSkillInvenoty = new Dictionary<BaseCreature, SkillsInventory>();
    }

    public BussinessInventory GetBussinessInventory(BaseCreature baseCreature)
    {
        if (baseCreature == null) { Debug.Log("请求拿取交易栏的角色为空，请检查"); }
        if (baseCreature is BaseGoodCreature) { baseCreature = baseCreature as BaseGoodCreature; }//背包复用？
        BussinessInventory bussinessInventory = null;
        if (dBussinessInventorys.Count > 0)
        {
            bussinessInventory = dBussinessInventorys.TryGet(baseCreature);
        }
        if (bussinessInventory == null)
        {
            bussinessInventory = FactoryManager.InventoryFactory.GetBussinessInventory();
            dBussinessInventorys.Add(baseCreature, bussinessInventory);
        }
        return bussinessInventory;
    }

    public GearsInventory GetGearsInventory(BaseCreature baseCreature)
    {
        if (baseCreature == null) { Debug.Log("请求拿取装备栏的角色为空，请检查"); }
        if (baseCreature is EnemyCateran) { baseCreature = baseCreature as BaseEnemyCreature; }
        GearsInventory gearsInventory = null;
        if (dCharacterInventorys.Count > 0)
        {
            gearsInventory = dCharacterInventorys.TryGet(baseCreature);
        }
        if (gearsInventory == null)
        {
            gearsInventory = FactoryManager.InventoryFactory.GetCharacterInventory();
            dCharacterInventorys.Add(baseCreature, gearsInventory);
        }
        return gearsInventory;
    }

    public SkillsInventory GetSkillInventory(BaseCreature baseCreature)
    {
        if (baseCreature == null) { Debug.Log("请求拿取技能栏的角色为空，请检查"); }
        if (baseCreature is EnemyCateran) { baseCreature = baseCreature as BaseEnemyCreature; }
        else if (baseCreature is BaseGoodCreature) { baseCreature = baseCreature as BaseGoodCreature; }
        SkillsInventory skillsInventory = null;
        if (dSkillInvenoty.Count > 0)
        {
            skillsInventory = dSkillInvenoty.TryGet(baseCreature);
        }
        if (skillsInventory == null)
        {
            skillsInventory = FactoryManager.InventoryFactory.GetSkillsInventory();
            dSkillInvenoty.Add(baseCreature, skillsInventory);
        }
        return skillsInventory;
    }

    public PackageInventory GetPackageInventory(BaseCreature baseCreature)
    {
        if (baseCreature == null) { Debug.Log("请求拿取背包栏的角色为空，请检查"); }
        if (baseCreature is BaseGoodCreature) { baseCreature = baseCreature as BaseGoodCreature; }
        else if (baseCreature is EnemyCateran) { baseCreature = baseCreature as BaseEnemyCreature; }
        PackageInventory packageInventory = null;
        if (dPackageInventory.Count > 0)
        {
            packageInventory = dPackageInventory.TryGet(baseCreature);
        }
        if (packageInventory == null)
        {
            packageInventory = FactoryManager.InventoryFactory.GetPackageInventory();
            dPackageInventory.Add(baseCreature, packageInventory);
        }
        return packageInventory;
    }

    /// <summary>
    /// 购买物品
    /// </summary>
    /// <param name="baseGoods">建议的物品</param>
    /// <param name="shopCreature">商人，出售方</param>
    /// <param name="playerCreature">顾客，收购方</param>
    public void BuyGood(BaseGoods baseGoods,BaseCreature shopCreature,BaseCreature playerCreature)
    {
        if (baseGoods == null) { Debug.Log("请求出售的物品为空，请检查");return; }
        if (shopCreature == null) { Debug.Log("接受交易（出售）的角色为空，请检查");return; }
        if (playerCreature == null) { Debug.Log("请求交易（购买）的角色为空，请检查"); return; }
        BussinessInventory ShopInventory = GetBussinessInventory(shopCreature);
        ShopInventory.SellGoods(baseGoods);
        GainItem(baseGoods, playerCreature);      
    }

    /// <summary>
    /// 出售物品
    /// </summary>
    /// <param name="baseGoods"></param>
    /// <param name="shopCreature"></param>
    /// <param name="playerCreature"></param>
    public void SellGood(BaseGoods baseGoods, BaseCreature shopCreature, BaseCreature playerCreature)
    {
        if (baseGoods == null) { Debug.Log("请求出售的物品为空，请检查"); return; }
        if (shopCreature == null) { Debug.Log("接受交易（回购）的角色为空，请检查"); return; }
        if (playerCreature == null) { Debug.Log("请求交易（出售）的角色为空，请检查");return; }
        BussinessInventory ShopInventory = GetBussinessInventory(shopCreature);
        ShopInventory.BuyGoods(baseGoods);
        RemoveItem(baseGoods,playerCreature);
    }

    /// <summary>
    /// 交换装备
    /// </summary>
    /// <param name="baseGoods"></param>
    /// <param name="playerCreature"></param>
    /// <returns></returns>
    public BaseGears ExChangeGear(BaseGears baseGears, BaseCreature playerCreature)
    {
        if (baseGears == null) { Debug.Log("请求交换的装备为空，请检查"); return null; }
        if (playerCreature == null) { Debug.Log("请求交换装备的角色为空，请检查"); return null; }
        GearsInventory gearsInventory = GetGearsInventory(playerCreature);
        BaseGears gears = gearsInventory.PickGear(baseGears.PartOfBodyType);
        if (gears == null) { gearsInventory.PushGear(baseGears); }
        else
        {
            PackageInventory packageInventory = GetPackageInventory(playerCreature);
            packageInventory.GainGoods(gears);
        }
        return gears;
    }

    /// <summary>
    /// 删除人物的某个装备
    /// </summary>
    /// <param name="baseGears"></param>
    /// <param name="playerCreature"></param>
    public bool DisChargeGear(BaseGears baseGears, BaseCreature playerCreature)
    {
        if (baseGears == null) { Debug.Log("请求删除的装备为空，请检查"); return false; }
        if (playerCreature == null) { Debug.Log("请求删除装备的角色为空，请检查"); return false; }
        GearsInventory gearsInventory = GetGearsInventory(playerCreature);
        return gearsInventory.DisChargeGear(baseGears);
    }

    /// <summary>
    /// 人物获得物品，直接获得好还是先放到背包再使用好？商店页面会暂停游戏，有充分的时间确认信息，而在背包里确认信息再使用/换装是要时间的，所以直接替换
    /// </summary>
    /// <param name="baseGoods">获得的物品</param>
    /// <param name="playerCreature">暂定为主角</param>
    public void GainItem(BaseGoods baseGoods, BaseCreature playerCreature)
    {
        if (baseGoods == null) { Debug.Log("请求获取的物品为空，请检查"); return; }
        if (playerCreature == null) { Debug.Log("请求获取物品的人物为空，请检查");return; }
        if (baseGoods is BaseItem)
        {
            PackageInventory packageInventory = GetPackageInventory(playerCreature);
            packageInventory.GainGoods(baseGoods as BaseItem);
        }
        else if (baseGoods is BaseSkill)
        {
            SkillsInventory skillsInventory = GetSkillInventory(playerCreature);
            skillsInventory.LearnSkill(baseGoods as BaseSkill);
        }
        else if (baseGoods is BaseGears)
        {
            GearsInventory gearsInventory = GetGearsInventory(playerCreature);
            gearsInventory.PushGear(baseGoods as BaseGears);
        }
    }

    /// <summary>
    /// 删除人物的某个物品
    /// </summary>
    /// <param name="baseItem">删除物品</param>
    /// <param name="playerCreature">暂定为主角</param>
    /// <returns></returns>
    public bool RemoveItem(BaseGoods baseGoods, BaseCreature playerCreature)
    {
        if (baseGoods == null) { Debug.Log("请求删除物品为空，请检查"); return false; }
        if (playerCreature == null) { Debug.Log("请求删除物品的角色，请检查"); return false; }
        PackageInventory packageInventory = GetPackageInventory(playerCreature);
        return packageInventory.RemoveGoods(baseGoods);
    }
}
