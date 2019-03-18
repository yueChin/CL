using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSystem
{
    private GameControl mGameControl;
    public QuestSystem(GameControl gameControl)
    {
        mGameControl = gameControl;
    }
    //任务完成判定在没个quest里，但是奖励的发放要由system控制
    private List<Quest> lQuests;
    public Quest AddQuest(QuestType questType)
    {
        if (lQuests == null) lQuests = new List<Quest>();
        Quest quest = CreateQuest(questType);
        lQuests.Add(quest);
        return quest;

    }

    public void CheckQuest(string str,BaseCreature player)
    {
        if (lQuests == null || lQuests.Count < 1) return;
        for (int i = 0; i<lQuests.Count;i++)
        {
            if (lQuests[i].Check(str))
            {
                QuestComplete(lQuests[i], player);
                return;
            }
        }
    }

    public void QuestComplete(Quest quest, BaseCreature player)
    {
        lQuests.Remove(quest);
        mGameControl.QuestDone(quest, CreateReword(quest.RewordType, player));
    }

    private Quest CreateQuest(QuestType questType)
    {
        Quest quest = null;
        switch (questType)
        {
            case QuestType.DispreseCateran:
                quest = new Quest(questType, (RewordType)(Random.Range(0, 3)));
                break;
            case QuestType.DisperseThief:
                quest = new Quest(questType, (RewordType)(Random.Range(0, 2)));
                break;
            case QuestType.DisperseRobber:
                quest = new Quest(questType, (RewordType)(Random.Range(0, 2)));
                break;
            case QuestType.DispersePirate:
                quest = new Quest(questType, (RewordType)(Random.Range(0, 4)));
                break;
            case QuestType.DispreseBandit:
                quest = new Quest(questType, (RewordType)(Random.Range(0, 3)));
                break;
            case QuestType.DispreseBrigand:
                quest = new Quest(questType, (RewordType)(Random.Range(0, 4)));
                break;
        }
        return quest;
    }

    private BaseGoods CreateReword(RewordType rewordType, BaseCreature player)
    {
        BaseGoods baseGoods = null;
        switch (rewordType)
        {
            case RewordType.GivenAmor:
                baseGoods = GoodsManager.GetGoodsManager.GetRandomAmor(Random.Range(1,5)); 
                break;
            case RewordType.GivenEquipment:
                baseGoods = GoodsManager.GetGoodsManager.GetRandomEquipment(Random.Range(1, 5));
                break;
            case RewordType.GivenWeapon:
                baseGoods = GoodsManager.GetGoodsManager.GetRandomWeapon(Random.Range(1, 10));
                break;
            case RewordType.GivenSteel:                
                break;
        }
        if (baseGoods != null)
        {
            InventoryManager.GetInventoryManager.GetPackageInventory(player).PutGoodIn(baseGoods);
        }
        return baseGoods;
    }
}
