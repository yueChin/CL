using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSteeliness : BaseGears,ISteeliness {
    protected int _SteelPrice;
    public BaseSteeliness(int price) : base(price) { }
    public virtual int SetSteel(int steelprice)
    {
        _SteelPrice = steelprice;
        return _SteelPrice;
    }

    public override void SetName()
    {
        _Name = "钢制装备";
    }

    public override void SetDescription()
    {
        
    }

    public override void SetSprite()
    {
        
    }
}
