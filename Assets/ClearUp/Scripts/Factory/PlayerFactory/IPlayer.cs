using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayer :ICreature {
    void AddHeart(int heart = 1);
    void LoseHeart(int heart = 1);
    void SetParent(Transform transform);
}
