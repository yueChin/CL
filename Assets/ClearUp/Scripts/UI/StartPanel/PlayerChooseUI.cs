using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerChooseUI : MonoBehaviour {
    private Dropdown mPlayerChoose;
    private GameObject mAniamtion;
    private GameObject mAniamtionGO = null;
    // Use this for initialization
    private void Awake()
    {
        mPlayerChoose = this.GetComponent<Dropdown>();
        mPlayerChoose.onValueChanged.AddListener(delegate { OnPlayerChoose(); });
        mAniamtion = FactoryManager.AssetFactory.LoadGameObject("UI/UIGO/Animation");
        SearchDropDownOption();//初始化下拉列表的选单
    }

    private void OnEnable()
    {       
        mAniamtion.SetActive(true);
        OnPlayerChoose();
    }

    private void OnDisable()
    {
        mAniamtion.SetActive(false);
    }

    /// <summary>
    /// 更换人物，startpanel界面下的3d物体跟着更换
    /// </summary>
    private void OnPlayerChoose()
    {        
        GameControl.GetGameControl.PlayerType = (PlayerType)(mPlayerChoose.value);
        if (mAniamtionGO != null)
        {
            mAniamtionGO.SetActive(false);
        }
        mAniamtionGO = UnityTool.FindOneOfAllChild(mAniamtion.transform, ((PlayerType)(mPlayerChoose.value)).ToString()).gameObject;
        mAniamtionGO.SetActive(true);
        mAniamtionGO.transform.localPosition = new Vector3(0, 3, 0);
    }

    /// <summary>
    /// 确定目前所有人物选择
    /// </summary>
    private void SearchDropDownOption()
    {
        for (int i = 0; i < System.Enum.GetValues(typeof(PlayerType)).Length; i++)
        {
            Dropdown.OptionData op1 = new Dropdown.OptionData();
            op1.text = PropertiesUtils.GetDescByProperties(((PlayerType)i));
            mPlayerChoose.options.Add(op1);
        }
    }
}
