using UnityEngine.UI;
using UnityEngine;

public class BattleOneIconUI : MonoBehaviour {
    private Image mImage;
    // Use this for initialization
    void Awake()
    {
        mImage = this.GetComponent<Image>();
        EventCenter.AddListener<Sprite,Sprite,float,float>(EventType.EnterBattleUI, SetBattleSprite);
    }

    void OnDestroy()
    {
        EventCenter.RemoveListener<Sprite, Sprite, float, float>(EventType.EnterBattleUI, SetBattleSprite);
    }

    // Update is called once per frame
    private void SetBattleSprite(Sprite one, Sprite two, float valueOne, float valueTwo)
    {
        mImage.sprite = one;
    }
}
