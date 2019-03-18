using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodCitizen :BaseGoodCreature{

    private QuestType mQuestType;
    public GoodCitizen(OccupationType occupationType) :base(occupationType){ }

    public override void AdaptCoins()
    {
        _Coins = Random.Range(0,6);
    }

    public override void ActionAfterShow()
    {
        mQuestType = (QuestType)Random.Range(0, System.Enum.GetValues(typeof(QuestType)).Length);
        UIManager.GetUIManager.ShowMessage(string.Format("遇到了{0},获得新任务：\n {1},\n 并得到了预付金{2}", _Name, PropertiesUtils.GetNameByProperties(mQuestType),_Coins));
        GameControl.GetGameControl.TriggerQuest(_CreatureGO.transform.position, mQuestType);
        //通过gamecontrol去通知dialogsystem显示任务动画，并让其通知任务系统
        Hide();
    }
}
