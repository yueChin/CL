using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmorFactory {

    public BaseAmor GetAmor(int price)
    {
        return new BaseAmor(price);
    }
}
