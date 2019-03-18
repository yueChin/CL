
using UnityEngine;
using UnityEngine.UI;

public class FightingUI : MonoBehaviour {
    private Button mButton;
    private Image mImage;
    private void Awake()
    {
        mImage = this.GetComponentInChildren<Image>();
        EventCenter.AddListener<Sprite,Sprite,float,float>(EventType.EnterBattleUI, Enable);
    }

    private void Enable(Sprite sprite1,Sprite sprite2,float f1,float f2)
    {
        mImage.gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener<Sprite, Sprite, float, float>(EventType.EnterBattleUI, Enable);
    }

    // Use this for initialization
    void Start () {
        mButton = this.GetComponentInChildren<Button>();
        mButton.onClick.AddListener(ToFight);
	}

    private void ToFight()
    {
        mImage.gameObject.SetActive(false);
        GameControl.GetGameControl.BattleFight();
    }
}
