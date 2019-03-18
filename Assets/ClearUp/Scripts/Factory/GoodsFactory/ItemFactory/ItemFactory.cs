using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory  {

    public BaseItem GetItem(int price)
    {
        return new BaseItem(price);
    }
}
