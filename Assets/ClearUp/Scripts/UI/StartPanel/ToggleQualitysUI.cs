
using UnityEngine;
using UnityEngine.UI;

public class ToggleQualitysUI : MonoBehaviour {
    private Toggle[] mToggleGroup;
    // Use this for initialization
    void Start () {
        mToggleGroup = this.GetComponentsInChildren<Toggle>();
        for (int i = 0; i < mToggleGroup.Length; i++)
        {
            mToggleGroup[i].onValueChanged.AddListener(OnToggleIsOn);
        }
    }

    private void OnToggleIsOn(bool ison)
    {
        if (ison)
        {
            for (int i = 0; i < mToggleGroup.Length; i++)
            {
                if (mToggleGroup[i].isOn)
                {
                    QualitySettings.SetQualityLevel(i, transform);
                    return;
                }
            }
        }
    }
}
