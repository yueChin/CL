using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentFacoty  {

    public BaseEquipment GetEquipment(int price)
    {
        return new BaseEquipment(price);
    }
}
