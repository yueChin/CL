using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class ComboSkill<T> where T:BattleSkill  {
    public ComboSkill<T> PrevNode
    {
        get { return this.mPrevComboSkill; }
        set { this.mPrevComboSkill = value; }
    }

    public ComboSkill<T> NextNode
    {
        get { return this.mNextComboSkill; }
        set { this.mNextComboSkill = value; }
    }

    public ComboSkillList<T> List
    {
        get { return this.mComboSkillList; }
    }
    public string Name
    {
        get { return mName; }
    }
    public int MaxCombo { get { return mMaxCombo; } }
    //前驱
    private ComboSkill<T> mNextComboSkill;
    private ComboSkill<T> mPrevComboSkill;    
    private ComboSkillList<T> mComboSkillList;
    //数据
    private List<T> lSkills = new List<T>();
    private string mName;
    private const int mMaxCombo = 3;

    /// <summary>
    ///     构造函数：实例化一个正常的Node 数据域 前驱后继都有值
    /// </summary>
    /// <param name="value"></param>
    /// <param name="prev"></param>
    /// <param name="next"></param>
    public ComboSkill(ComboSkillList<T> ComboSkillList,T value, ComboSkill<T> prev = null, ComboSkill<T> next = null )
    {
        PushInEmpty(value);
        this.mPrevComboSkill = prev;        
        this.mNextComboSkill = next;
        this.mComboSkillList = ComboSkillList;
    }

    /// <summary>
    ///     Show 方法，调试用
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        string p = this.mPrevComboSkill == null ? string.Empty : this.mPrevComboSkill.Name;
        string n = this.mNextComboSkill == null ? string.Empty : this.mNextComboSkill.Name;
        string s = string.Format("Data:{0},Prev:{1},Next:{2}", this.mName, p, n);
        return s;
    }

    /// <summary>
    /// 是否包含该battleskill
    /// </summary>
    /// <param name="t">battleskill</param>
    /// <returns></returns>
    public bool IsContain(T t)
    {
        return lSkills.Contains(t);
    }

    /// <summary>
    /// 该连击技能是否已是最大连击块，即是否有空
    /// </summary>
    /// <returns></returns>
    public bool IsAnyEmpty()
    {
        if (lSkills.Count <= mMaxCombo) { return true; }
        else { return false; }
    }

    /// <summary>
    /// 剩下几个空位
    /// </summary>
    /// <returns></returns>
    public int EmptyHowMany()
    {
        if (lSkills.Count <= mMaxCombo)
            return mMaxCombo - lSkills.Count;
        else
            return 0;
    }

    /// <summary>
    /// 把战斗技能放入连击技能中，带空判断和是否可加入判断
    /// </summary>
    /// <param name="t">battleskill</param>
    /// <returns></returns>
    public bool PushInEmpty(T t)
    {
        if (t == null) return false;
        if (lSkills.Count == 0)
        {
            lSkills.Add(t);
            this.mName = t.Name;
            return true;
        }
        if (IsAnyEmpty() && t.Name == mName)
        {
            lSkills.Add(t);
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 移除某个技能
    /// </summary>
    /// <param name="t">battleskill</param>
    /// <returns></returns>
    public bool Remove(T t)
    {
        return lSkills.Remove(t);
    }

    /// <summary>
    /// 获取该combo中的技能
    /// </summary>
    /// <returns></returns>
    public T[] GetSkills()
    {
        return lSkills.ToArray();
    }

    //是否有打断/封印？
    public bool ReleaseComboSkill(ref RelaseSkill relaseSkill)
    {
        int i;
        for (i = 0; i < lSkills.Count; i++)
        {
            lSkills[i].ReleaseSkill(ref relaseSkill);
            GameControl.GetGameControl.ReleaseSkillUI(lSkills[i]);//如果用委托的话，那么只能通知到消除的那个块，左右连击块无法通知到
        }
        List.Remove(this);
        if (i != lSkills.Count - 1) { return false; }
        else return true; 
    }
}
