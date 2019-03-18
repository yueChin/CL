using UnityEngine;

public interface IBattleCreatureAction  {
    int GetDamageValue();
    int GetDefenceValue();
    int GetArmamentValue();
    int BattleFailLoseCoins();
    int BattleWinGainCoins(int coins);
    BaseGears BattleFailLoseGear();
    bool BattleWinGainGear(BaseGears baseGears);
    GameObject GetGameObject();
    string GetName();
}
