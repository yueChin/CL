using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BattlePanel : BasePanel {

    private Slider mEnemySlider;
    private Slider mPlayerSlider;
    private Text mPlayerText;
    private Text mEnemyText;
    private AudioSource mPlayerTextAudio;
    private AudioSource mEnemyTextAudio;
    private Image mPlayerTextImage;
    private Image mEnemyTextImage;
    private RectTransform mPlayerValue;
    private RectTransform mEnemyValue;
    private RectTransform mPlayerSkills;
    private RectTransform mEnemySkills;       
    private RectTransform mPlayer;
    private RectTransform mEnemy;
    private GameObject mBattleScene;
    // Use this for initialization
    private void Awake() {
        mEnemy = UITool.FindChild<RectTransform>(this.transform,"Enemy");
        mPlayer = UITool.FindChild<RectTransform>(this.transform, "Player");
        mEnemyValue = UITool.FindChild<RectTransform>(mEnemy, "Text");
        mPlayerValue = UITool.FindChild<RectTransform>(mPlayer, "Text");
        mEnemySkills = UITool.FindChild<RectTransform>(mEnemy, "BattleSkills");
        mPlayerSkills = UITool.FindChild<RectTransform>(mPlayer, "BattleSkills");
        mEnemySlider = mEnemy.GetComponentInChildren<Slider>();
        mPlayerSlider = mPlayer.GetComponentInChildren<Slider>();
        mPlayerText = UITool.FindChild<Text>(mPlayerValue.transform, "Value");
        mEnemyText = UITool.FindChild<Text>(mEnemyValue.transform, "Value");
        mPlayerTextAudio = mPlayerValue.GetComponent<AudioSource>();
        mEnemyTextAudio = mEnemyValue.GetComponent<AudioSource>();
        mPlayerTextImage = mPlayerValue.GetComponentInChildren<Image>();
        mEnemyTextImage = mEnemyValue.GetComponentInChildren<Image>();
        mBattleScene = FactoryManager.AssetFactory.LoadGameObject("UI/UIGO/BattleScene");
        EventCenter.AddListener<Sprite,Sprite,float,float>(EventType.EnterBattleUI, SetBattleSprite);
    }

    private void OnEnable()
    {
        mEnemySlider.gameObject.SetActive(false);
        mEnemySkills.gameObject.SetActive(false);
        mPlayerSlider.gameObject.SetActive(false);
        mPlayerSkills.gameObject.SetActive(false);
        mPlayerTextImage.fillAmount = 0;
        mEnemyTextImage.fillAmount = 0;
        mPlayerTextImage.color = new Color(mPlayerTextImage.color.r, mPlayerTextImage.color.g, mPlayerTextImage.color.b, 0);
        mEnemyTextImage.color = new Color(mEnemyTextImage.color.r, mEnemyTextImage.color.g, mEnemyTextImage.color.b, 0);
        mPlayerText.text = string.Empty;
        mEnemyText.text = string.Empty;
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener<Sprite, Sprite, float, float>(EventType.EnterBattleUI, SetBattleSprite);
    }

    private void SetBattleSprite(Sprite one,Sprite two,float valueOne,float valueTwo)
    {
        mBattleScene.gameObject.SetActive(true);
        mPlayerText.text = valueOne.ToString();
        mEnemyText.text = valueTwo.ToString();
    }    

    public override void EnterPanel()
    {
        base.EnterPanel();
        EnterAniamtion();
        GameControl.GetGameControl.ChangeBattleMusic("Audios/BGM18");
        BattleCharacterEnter();
    }

    public override void ExitPanel()
    {
        base.ExitPanel();
        GameControl.GetGameControl.ChangeBattleMusic("Audios/BGM1");
        //Debug.Log("战斗推出动画");
        ExitAnimation();
    }

    #region 动画相关
    private void EnterAniamtion()
    {
        this.gameObject.SetActive(true);
        this.transform.localScale = new Vector3(1, 0, 1);
        this.transform.DOScaleY(1, 0.5f);
    }

    private void ExitAnimation()
    {
        this.transform.DOScaleY(0, 0.5f);
        mBattleScene.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }

    private void ShowEnemyValue()
    {        
        mEnemyTextAudio.Play();
        mEnemyTextImage.DOFade(1, 0.2f).OnComplete(() => { mEnemyTextImage.DOFade(0, 0.2f); });
        mEnemyTextImage.DOFillAmount(1, 0.5f);
    }

    private void ShowPlayerValue()
    {        
        mPlayerTextAudio.Play();
        mPlayerTextImage.DOFade(1, 0.2f).OnComplete(() => { mPlayerTextImage.DOFade(0, 0.2f); });
        mPlayerTextImage.DOFillAmount(1, 0.5f);
    }

    private void BattleCharacterEnter()
    {
        Vector2 playerBeginPos = mPlayer.anchoredPosition;
        Vector2 enemyBeginPos = mEnemy.anchoredPosition;
        mPlayer.anchoredPosition = new Vector2(playerBeginPos.x, -150);
        mEnemy.anchoredPosition = new Vector2(enemyBeginPos.y, 150);
        mPlayer.DOAnchorPos(playerBeginPos, 1f).OnComplete(()=> {
            mPlayerSlider.transform.localScale = new Vector3(1, 0, 2); mPlayerSkills.localScale = new Vector3(0, 1, 1);
            mPlayerSlider.gameObject.SetActive(true);mPlayerSlider.transform.DOScaleY(1, 1f);
            mPlayerSkills.gameObject.SetActive(true);mPlayerSkills.DOScaleX(1, 1f).OnComplete(()=> { ShowPlayerValue(); });
        });
        mEnemy.DOAnchorPos(enemyBeginPos, 1f).OnComplete(()=> {
            mEnemySlider.transform.localScale = new Vector3(1, 0, 2); mEnemySkills.localScale = new Vector3(0, 1, 1);
            mEnemySlider.gameObject.SetActive(true); mEnemySlider.transform.DOScaleY(1, 1f);
            mEnemySkills.gameObject.SetActive(true); mEnemySkills.DOScaleX(1, 1f).OnComplete(()=> { ShowEnemyValue(); });
        });
    }
    #endregion
}
