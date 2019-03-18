
using UnityEngine;
using UnityEngine.UI;

public class DialogUI : MonoBehaviour {
    private Text mPlayerText;
    private Text mNPCText;

    // Use this for initialization
    void Awake () {        
        mPlayerText = UITool.FindChild<Text>(this.transform, "PlayerTalk");
        mNPCText = UITool.FindChild<Text>(this.transform, "NPCTalk");
        EventCenter.AddListener<string>(EventType.PlayerSay, OnPlayerTalk);
        EventCenter.AddListener<string>(EventType.NPCSay,OnNPCTalk);
    }

    private void OnEnable()
    {
        mPlayerText.text = string.Empty;
        mNPCText.text = string.Empty;
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener<string>(EventType.PlayerSay, OnPlayerTalk);
        EventCenter.RemoveListener<string>(EventType.NPCSay, OnNPCTalk);
    }

    private void OnPlayerTalk(string context)
    {
        mNPCText.text = string.Empty;
        mPlayerText.text = context;
    }

    private void OnNPCTalk(string context)
    {
        mPlayerText.text = string.Empty;
        mNPCText.text = context;
    }
}
