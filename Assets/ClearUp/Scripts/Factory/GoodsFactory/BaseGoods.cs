using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGoods:IGoods {
    public int Price { get { return _Price; } }
    public string Name { get { return _Name; } }
    public string Description { get { return _Description; } }
    public Sprite GetSprite { get { return _Sprite; } }
	protected int _Price;
    protected string _Description;
    protected string _Name;
    protected Sprite _Sprite;//根据物品类型来获取图标
    public BaseGoods(int price)
    {
        _Price = price;
    }
    public virtual void SetName() { _Name = "空物体"; }
    public virtual void SetSprite() { }//默认的图片
    public virtual void SetDescription() { _Description = "我也不知道这是啥，大概是bug吧"; }
}
