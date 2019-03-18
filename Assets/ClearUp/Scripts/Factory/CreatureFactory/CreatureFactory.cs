using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureFactory{

    public GameObject GetBattleCreatureOne(string name)
    {
        GameObject gameObject = FactoryManager.AssetFactory.LoadGameObject(string.Format("Creature/BattlePlayer/Prefabs/{0}",name));       
        return gameObject;
    }

    public GameObject GetBattleCreatureTwo(string name)
    {
        GameObject gameObject = FactoryManager.AssetFactory.LoadGameObject(GameControl.GetGameControl.GOPathDict.TryGet(name));
        gameObject.GetComponent<BoxCollider>().enabled = true;
        return gameObject;
    }
    public ICreatureAction GetGoodCreature(OccupationType occupationType,Vector3 pos) 
    {
        IGoodCreature goodCreature = GoodCreature(occupationType);        
        if (goodCreature == null)
        {
            UnityEngine.Debug.Log("未找到该生物事件 ："+ goodCreature);return null;
        }
        AdaptCreature(goodCreature);
        goodCreature.AdaptCoins();
        goodCreature.AdaptContentOfDialogue();
        goodCreature.AdaptPostion(pos);
        if (goodCreature is IBussinessCreature)
        {
            IBussinessCreature bussinessCreature = goodCreature as IBussinessCreature;
            bussinessCreature.AdjuestItemLevel();
            bussinessCreature.AdjuestItemNumber();
            return bussinessCreature as ICreatureAction;
        }
        return goodCreature as ICreatureAction;
    }

    public ICreatureAction GetEnemyCreature(EnemyType enemyType,Vector3 pos)
    {
        IEnemyCreature enemyCreature = EnemyCreature(enemyType);        
        if (enemyCreature == null)
        {
            UnityEngine.Debug.Log("未找到该生物事件 ：" + enemyCreature); return null;
        }
        AdaptCreature(enemyCreature);
        enemyCreature.AdaptLevel();
        enemyCreature.AdaptEquipment();
        enemyCreature.AdjuestBattleSkill();
        enemyCreature.AdaptPostion(pos);
        return enemyCreature as ICreatureAction;
    }

    private void AdaptCreature(ICreature creature)
    {
        creature.AdaptName();
        creature.AdaptGameObject();
        creature.AdaptIcon();
    }

    private static Dictionary<OccupationType, IGoodCreature> mGoodCreatureDict;
    private static Dictionary<EnemyType, IEnemyCreature> mEnemyCreatureDict;

    public static IGoodCreature GoodCreature(OccupationType occupationType)
    {
        //switch (occupationType)
        //{
        //    case OccupationType.Blacksmith:
        //        return new GoodBlacksmith(OccupationType.Blacksmith);                
        //    case OccupationType.Citizen:
        //        return new GoodCitizen(OccupationType.Citizen);               
        //    case OccupationType.Trader:
        //        return new GoodTrader(OccupationType.Trader);               ;
        //    case OccupationType.Traveller:
        //        return new GoodTraveller(OccupationType.Traveller);               
        //    case OccupationType.Vendor:
        //        return new GoodVendor(OccupationType.Vendor);                
        //    case OccupationType.Wizard:
        //        return new GoodWizard(OccupationType.Wizard);               
        //    case OccupationType.Warrior:
        //        return new GoodWarrior(OccupationType.Warrior);
        //    default:
        //        return null;
        //}
        if (mGoodCreatureDict == null)
        {
            mGoodCreatureDict = new Dictionary<OccupationType, IGoodCreature>();
        }
        if (!mGoodCreatureDict.ContainsKey(occupationType))
        {
            switch (occupationType)
            {
                case OccupationType.Blacksmith:
                    mGoodCreatureDict.Add(OccupationType.Blacksmith, new GoodBlacksmith(OccupationType.Blacksmith));
                    break;
                case OccupationType.Citizen:
                    mGoodCreatureDict.Add(OccupationType.Citizen, new GoodCitizen(OccupationType.Citizen));
                    break;
                case OccupationType.Trader:
                    mGoodCreatureDict.Add(OccupationType.Trader, new GoodTrader(OccupationType.Trader));
                    break;
                case OccupationType.Traveller:
                    mGoodCreatureDict.Add(OccupationType.Traveller, new GoodTraveller(OccupationType.Traveller));
                    break;
                case OccupationType.Vendor:
                    mGoodCreatureDict.Add(OccupationType.Vendor, new GoodVendor(OccupationType.Vendor));
                    break;
                case OccupationType.Wizard:
                    mGoodCreatureDict.Add(OccupationType.Wizard, new GoodWizard(OccupationType.Wizard));
                    break;
                case OccupationType.Warrior:
                    mGoodCreatureDict.Add(OccupationType.Warrior, new GoodWarrior(OccupationType.Warrior));
                    break;
            }
        }
        return mGoodCreatureDict.TryGet(occupationType);
    }

    public static IEnemyCreature EnemyCreature(EnemyType enemyType)
    {
        //switch (enemyType)
        //{
        //    case EnemyType.Thief:
        //        return new EnemyThief(EnemyType.Thief);
        //    case EnemyType.Robber:
        //        return new EnemyRobber(EnemyType.Robber);
        //    case EnemyType.Bandit:
        //        return new EnemyBandit(EnemyType.Bandit);
        //    case EnemyType.Cateran:
        //        return new EnemyCateran(EnemyType.Cateran);
        //    case EnemyType.Brigand:
        //        return new EnemyBrigand(EnemyType.Brigand);
        //    case EnemyType.Private:
        //        return new EnemyPrivate(EnemyType.Private);
        //    default:
        //        return null;
        //}
        if (mEnemyCreatureDict == null)
        {
            mEnemyCreatureDict = new Dictionary<EnemyType, IEnemyCreature>();
        }
        if (!mEnemyCreatureDict.ContainsKey(enemyType))
        {
            switch (enemyType)
            {
                case EnemyType.Thief:
                    mEnemyCreatureDict.Add(EnemyType.Thief, new EnemyThief(EnemyType.Thief));
                    break;
                case EnemyType.Robber:
                    mEnemyCreatureDict.Add(EnemyType.Robber, new EnemyRobber(EnemyType.Robber));
                    break;
                case EnemyType.Bandit:
                    mEnemyCreatureDict.Add(EnemyType.Bandit, new EnemyBandit(EnemyType.Bandit));
                    break;
                case EnemyType.Cateran:
                    mEnemyCreatureDict.Add(EnemyType.Cateran, new EnemyCateran(EnemyType.Cateran));
                    break;
                case EnemyType.Brigand:
                    mEnemyCreatureDict.Add(EnemyType.Brigand, new EnemyBrigand(EnemyType.Brigand));
                    break;
                case EnemyType.Private:
                    mEnemyCreatureDict.Add(EnemyType.Private, new EnemyPrivate(EnemyType.Private));
                    break;
            }
        }
        return mEnemyCreatureDict.TryGet(enemyType);
    }
}
