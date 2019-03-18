using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitPanel : BasePanel {
    [SerializeField]
    private Button ReplayButton;
    [SerializeField]
    private Button ExitButton;

    private void Start()
    {
        ReplayButton.GetComponent<Button>().onClick.AddListener(OnReplayButtonClick);
        ExitButton.GetComponent<Button>().onClick.AddListener(OnExitButtonClick);
    }

    private void OnReplayButtonClick()
    {
        this.gameObject.SetActive(false);
    }

    private void OnExitButtonClick()
    {
        _GameControl.GameExit();
    }
}
