using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShiled : BaseGears,IProtection
{
    public BaseShiled(int price) : base(price) { }

    public int GetDefenseValue()
    {
        throw new System.NotImplementedException();
    }

    public int SetDefenseValue()
    {
        throw new System.NotImplementedException();
    }
}
