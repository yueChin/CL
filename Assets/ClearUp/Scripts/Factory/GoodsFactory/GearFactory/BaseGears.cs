using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGears :BaseGoods ,IGear,IGearAction{
    public PartOfBodyType PartOfBodyType { get { return _PartOfBodyType; }}
    protected DurabilityType _DurabilityType;
    protected CategoryType _CategoryType;
    protected PartOfBodyType _PartOfBodyType;
    protected int _ArmamentValue;
    public BaseGears(int price) : base(price) { }     
    public int GetArmamentValue()
    {
        return _ArmamentValue;
    }

    public virtual DurabilityType SetDurabilityType(DurabilityType durabilityType)
    {
        _DurabilityType = durabilityType;
        return _DurabilityType;
    }

    public virtual CategoryType SetCategoryType(CategoryType categoryType)
    {
        _CategoryType = categoryType;
        return _CategoryType;
    }

    public virtual PartOfBodyType SetPartOfBodyType(PartOfBodyType partOfBodyType)
    {
        _PartOfBodyType = partOfBodyType;
        return partOfBodyType;
    }

    public virtual void SetPartOfBodyType() { }
    public override void SetDescription()
    {
        _Description = ""; 
    }

    public override void SetName()
    {
        _Name = "这是一件装备";
    }

    public override void SetSprite()
    {
        Debug.Log("错误？");
        _Sprite = default(Sprite);//默认装备图片
    }
    protected virtual int RareMultimes(int rare, int max) { return (rare + max) / max; }
}