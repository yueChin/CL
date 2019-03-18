using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillsUI : MonoBehaviour {

    public void PushSkillIn(SkillSlot skillSlot)
    {        
        skillSlot.transform.SetParent(this.transform,true);
        skillSlot.transform.localScale = Vector3.one;
    }
}
