using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerResourceUI : MonoBehaviour { 
    private Image mPlayerIcon;

	// Use this for initialization
	void Start ()
    {
        mPlayerIcon = this.GetComponent<Image>();
        //事件添加侦听 玩家的资源在playGameState
        GameControl.GetGameControl.SetPlayerIcon(SetPlayerIcon);        
	}

    private void SetPlayerIcon(Sprite image)
    {
        mPlayerIcon.sprite = image;
    }
}
