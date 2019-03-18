using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest  {
    public string Description
    {
        get
        {
            return string.Format("{0}\n 任务完成后：{1}",PropertiesUtils.GetDescByProperties(mQuestType),PropertiesUtils.GetDescByProperties(mRewordType));
        }
    }//反射
    public RewordType RewordType { get { return mRewordType; } }
    public QuestType QuestType { get { return mQuestType; } }
    private QuestType mQuestType;
    private RewordType mRewordType;
    private EnemyType mEnemyType;
    public Quest(QuestType questType ,RewordType rewordType)
    {
        mQuestType = questType;
        mRewordType = rewordType;
        if (questType == QuestType.DispersePirate) { mEnemyType = EnemyType.Private; }
        else if (questType == QuestType.DisperseRobber) { mEnemyType = EnemyType.Robber; }
        else if (questType == QuestType.DisperseThief) { mEnemyType = EnemyType.Thief; }
        else if (questType == QuestType.DispreseCateran) { mEnemyType = EnemyType.Cateran; }
    }

    public bool Check(string str)
    {
        if (str.Equals(mEnemyType.ToString())) { return true; }
        else { return false; }
    }
}
