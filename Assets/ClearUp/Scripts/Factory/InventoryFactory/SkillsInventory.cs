using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 技能栏
/// </summary>
public class SkillsInventory : BaseInventory
{
    private Dictionary<string, BaseSkill> dSkillsDict;
    public override void ClearInventory()
    {
        if (dSkillsDict == null) { Debug.Log("技能栏为空"); return; }
        dSkillsDict.Clear();
    }

    public void LearnSkill(BaseSkill baseSkill)
    {
        if (dSkillsDict == null) { dSkillsDict = new Dictionary<string, BaseSkill>(); }
        if (dSkillsDict.ContainsKey(baseSkill.Name))
        {
            BaseSkill skill = dSkillsDict[baseSkill.Name];
            skill.LevelUp();
        }
        else
        {
            dSkillsDict.Add(baseSkill.Name, baseSkill);
        }        
    }

    public BaseSkill ReleaseSkill(string name)
    {
        if (dSkillsDict == null)
        {
            Debug.Log("技能栏为空");
            return null;
        }
        if (name == string.Empty)
        {
            Debug.Log("技能为空");
            return null;
        }
        BaseSkill baseSkill = dSkillsDict.TryGet(name);
        if (baseSkill == null)
        {
            UnityEngine.Debug.Log("无法通过[技能]："+ name + ",来获取技能");
        }
        return baseSkill;
    }

    public List<BattleSkill> GetBattleSkills()
    {
        if (dSkillsDict == null)
        {
            Debug.Log("技能栏为空");
            return null;
        }
        List<BattleSkill> battleSkills = new List<BattleSkill>();
        foreach (KeyValuePair<string,BaseSkill> kvp in dSkillsDict)
        {
            if (kvp.Value is BattleSkill)
            {
                battleSkills.Add(kvp.Value as BattleSkill);
            }
        }
        return battleSkills;
    }

    /// <summary>
    /// 获取技能等级，暂时用来判断 普通技能触发和 战斗相关的技能对战斗的影响
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public int GetSkillLevel(string name)
    {
        if (dSkillsDict == null) return 0;
        BaseSkill baseSkill = dSkillsDict.TryGet(name);
        return baseSkill == null ? 0: baseSkill.SkillLevel;        
    }
}
