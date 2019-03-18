using UnityEngine;

public class PlayerFactory {

    public void InitPlayer(IPlayer gamePlayer,Vector3 pos,Transform transform)
    {
        gamePlayer.AdaptName();
        gamePlayer.AdaptGameObject();
        gamePlayer.AdaptIcon();
        gamePlayer.AdaptCoins();
        gamePlayer.AdaptPostion(pos);
        gamePlayer.AddHeart(3);
        gamePlayer.SetParent(transform);
    }
}
