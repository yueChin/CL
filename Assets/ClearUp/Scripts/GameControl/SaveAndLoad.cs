using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveAndLoad {
    private GameData mGameData;
    public SaveAndLoad(GameData gameData)
    {
        mGameData = gameData;
    }
    #region 保存和读取记录
    //private Dictionary<ScoreMulType, float> dMulNumberDict; //记录倍率

    [Serializable]
    public class Record
    {
        public float HighScore;
        //public List<RecordMulInfo> RecordInfoList;
    }

#if UNITY_STANDALONE_WIN
    /// <summary>
    /// 读取记录
    /// </summary>
    public void ParseRecordFormJson()
    {
        //if (dMulNumberDict == null) { dMulNumberDict = new Dictionary<ScoreMulType, float>(); }
        TextAsset ta = Resources.Load<TextAsset>("GameRecord");
        if (ta == null) { mGameData.Record = 0;return; }
        Record record = JsonUtility.FromJson<Record>(ta.text);
        mGameData.Record = record.HighScore;
        //foreach (RecordMulInfo infos in record.RecordInfoList)
        //{
        //     dMulNumberDict.Add(infos.MulType, infos.Score);
        //}
        //Debug.Log("读取记录："+ mRecord);
    }

    /// <summary>
    /// 保存记录
    /// </summary>
    public void SaveToJson(float score)
    {
        Record rd = new Record();
        rd.HighScore = score;
        //rd.RecordInfoList = new List<RecordMulInfo>();
        //RecordMulInfo recordMulInfo = new RecordMulInfo();
        //foreach (KeyValuePair<ScoreMulType,float> kvp in dMulNumberDict)
        //{
        //    recordMulInfo.MulTypeString = kvp.Key.ToString();
        //    recordMulInfo.Score = kvp.Value;
        //    rd.RecordInfoList.Add(recordMulInfo);
        //}
        string json = JsonUtility.ToJson(rd);
        string path = Application.dataPath + @"/Resources/GameRecord.json";
        //写入Josn
        if (File.Exists(path))
        {
            //Debug.Log("写入记录：" + json);
            File.WriteAllText(path, json);
        }
        else
        {
            StreamWriter streamWriter = File.CreateText(path);
            streamWriter.Write(json);
            streamWriter.Close();
            streamWriter.Dispose();
        }
    }
#endif

#if UNITY_ANDROID

    public void ParseRecordFormJson()
    {
        //if (dMulNumberDict == null) { dMulNumberDict = new Dictionary<ScoreMulType, float>(); }
        FileInfo m_file = new FileInfo(Application.persistentDataPath + @"/GameRecord.Json");
        if (!m_file.Exists)
        {
            File.CreateText(Application.persistentDataPath + @"/GameRecord.txt");
            mGameData.Record = 0;
            return;
        }
        StreamReader reader = new StreamReader(Application.persistentDataPath + @"/GameRecord.Json");
        string jsonData = reader.ReadToEnd();
        reader.Close();
        reader.Dispose();
        Record record = JsonUtility.FromJson<Record>(jsonData);
        mGameData.Record = record.HighScore;
    }

    public void SaveToJson(float score)
    {
        Record rd = new Record();
        rd.HighScore = score;
        string json = JsonUtility.ToJson(rd);
        string path = Application.persistentDataPath + @"/GameRecord.json";
        //写入Josn
        if (File.Exists(path))
        {
            //Debug.Log("写入记录：" + json);
            File.WriteAllText(path, json);
        }
        else
        {
            StreamWriter streamWriter = File.CreateText(path);
            streamWriter.Write(json);
            streamWriter.Close();
            streamWriter.Dispose();
        }
    }
#endif

    //[Obsolete]
    //public void IsRefreshRecord(float record)
    //{
    //    if (record > mRecord)
    //    {
    //        mRecord = record;
    //    }
    //}

    //[Obsolete]
    //public void IsRefreshMulRecord(ScoreMulType scoreMulType, float Mulrecord)
    //{
    //    if (Mulrecord > dMulNumberDict[scoreMulType])
    //    {
    //        dMulNumberDict[scoreMulType] = Mulrecord;
    //    }
    //}
    #endregion
}
