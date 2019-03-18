using System;
using UnityEngine;
using UnityEngine.UI;

public class MaxCubeUI : MonoBehaviour {
    private InputField mMaxCube;
    // Use this for initialization
    private void Awake()
    {
        mMaxCube = this.GetComponentInChildren<InputField>();
    }
    private void OnEnable()
    {
        mMaxCube.GetComponent<InputField>().text = 100f.ToString();
    }

    void Start ()
    {   
        mMaxCube.onValueChanged.AddListener(delegate { GameControl.GetGameControl.PlayNumber = Convert.ToInt32(mMaxCube.text.ToString()); });//把最大方块数传入到生成地图中
    }
	
}
