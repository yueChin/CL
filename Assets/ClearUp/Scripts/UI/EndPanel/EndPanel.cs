using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public enum ScoreMulType
{
    DoneCubeMul,
    MaxCUbeMul,
    ComboAdd,
    PreTimeMul
}

public class EndPanel : BasePanel {
    private GameObject mRePlayButton;
    private GameObject mShareButton;
    private Text mPreTimeScore;
    private Text mScore;
    private CanvasGroup mCanvasGroup;
    private Text mNewHighScore;
    private GameObject mScoreResultPanel;
    private GameObject mScoreResultBar;
    private Dictionary<ScoreMulType, GameObject> dScoreResultBar;
    private int i = 0;
    private AudioSource mNewHighScoreAudioSource;
    private AudioSource mPreTimeScoreAudioSource;
    private AudioSource mScoreAudioSource;
    private delegate void OnOneBarAnimationDone();
    private event OnOneBarAnimationDone OnOneBarAniamtionDoneEventHandle;

    private void Awake()
    {
        mCanvasGroup = this.GetComponent<CanvasGroup>();
        mPreTimeScore = UITool.FindChild<Transform>(UITool.FindChild<Transform>(this.transform, "PerScoreTime").transform,"Field").GetComponent<Text>();
        mScore = UITool.FindChild<Transform>(UITool.FindChild<Transform>(this.transform, "CurrentScore").transform, "Field").GetComponent<Text>();
        mRePlayButton = UITool.FindChild<Transform>(this.transform, "RePlayButton").gameObject;
        mShareButton = UITool.FindChild<Transform>(this.transform, "ShareButton").gameObject;
        mNewHighScore = UITool.FindChild<Transform>(this.transform, "NewHishScore").GetComponent<Text>();
        mScoreResultPanel = UITool.FindChild<Transform>(this.transform, "Panel").gameObject;
        mNewHighScoreAudioSource = mNewHighScore.GetComponent<AudioSource>();
        mPreTimeScoreAudioSource = mPreTimeScore.GetComponent<AudioSource>();
        mScoreAudioSource = mScore.GetComponent<AudioSource>();         
    }

    private void OnEnable()
    {
        mRePlayButton.SetActive(false);
        mShareButton.SetActive(false);
        mNewHighScore.gameObject.SetActive(false);
    }

    public override void EnterPanel()
    {
        base.EnterPanel();
        EnterAnimation();
        _Record = _UIManager.Record;
        _Time = _UIManager.Time;
        _Score = _UIManager.Score;
        ShowScore();
        ShowPreScoreTime();              
        ShowScoreResult();        
    }

    public override void ExitPanel()
    {
        base.ExitPanel();
        ExitAnimation();
        _Record = 0;
        _Score = 0;
        _Time = 0;        
        for (i = 0; i < System.Enum.GetValues(typeof(ScoreMulType)).Length; i++)
        {
            GameObject go = dScoreResultBar.TryGet((ScoreMulType)i);
            go.SetActive(false);
        }
        mNewHighScore.gameObject.SetActive(false);
        mRePlayButton.gameObject.SetActive(false);
        mShareButton.gameObject.SetActive(false);
        i = 0;
    }

    #region 各类动画
    /// <summary>
    /// 进入面板的动画
    /// </summary>
    private void EnterAnimation()
    {
        this.gameObject.SetActive(true);
        this.transform.localPosition = Vector3.zero;
        mCanvasGroup.alpha = 0;
        mCanvasGroup.DOFade(1, 0.35f);
    }

    /// <summary>
    /// 离开面板的动画
    /// </summary>
    private void ExitAnimation()
    {
        mCanvasGroup.DOFade(0, 0.3f).OnComplete(() => gameObject.SetActive(false));
    }

    /// <summary>
    /// 显示新高分标识
    /// </summary>
    public void ShowNewHighScore()
    {
        mNewHighScore.gameObject.SetActive(true);
        mNewHighScoreAudioSource.Play();
        mNewHighScore.transform.localScale = Vector3.zero;
        mNewHighScore.transform.DOScale(1.2f, 0.5f).OnComplete(() => { mNewHighScore.transform.DOScale(1, 0.5f); });
    }
    
    /// <summary>
    /// 显示每分时间
    /// </summary>
    public void ShowPreScoreTime()
    {
        float preTimeScore = _Time / _Score;        
        mPreTimeScore.DOText(preTimeScore.ToString(),0.5f);
    }
    
    /// <summary>
    /// 显示得分
    /// </summary>
    private void ShowScore()
    {
        //得分动画        
        float nowScore = System.Convert.ToSingle(mScore.text);
        DOTween.To(delegate (float value) { mScore.text = _Score.ToString();}, nowScore, _Score, 0.5f);
        //Debug.Log("更新分数:" + _Score+ "目前分：" + nowScore);
    }

    /// <summary>
    /// 显示得分倍率
    /// </summary>
    public void ShowScoreResult()
    {
        if (dScoreResultBar == null)
        {
            dScoreResultBar = new Dictionary<ScoreMulType, GameObject>();
        }
        GameObject bar = dScoreResultBar.TryGet((ScoreMulType)i);
        if (bar == null)
        {
            bar = FactoryManager.AssetFactory.LoadGameObject("UI/Bars/ScoreBar");
            dScoreResultBar.Add((ScoreMulType)i, bar);            
        }
        bar.SetActive(true);
        Vector3 vector3 = bar.transform.position;
        bar.transform.SetParent(mScoreResultPanel.transform);
        bar.transform.localScale = Vector3.one;
        //Debug.Log("设置倍率棒名称");
        Text mScoreResultText = bar.transform.Find("Text").GetComponent<Text>();
        string mSRT = string.Empty;
        GetScoreResultBar_Text((ScoreMulType)i, out mSRT);
        mScoreResultText.text = mSRT;
        //Debug.Log("设置倍率数值");
        Text mScoreResultField = bar.transform.Find("Field").GetComponent<Text>();
        float mulNumber, times;
        GetScoreResultBar_Mul((ScoreMulType)i, out mulNumber, out times);
        _Score = _Score * mulNumber;
        mScoreResultField.text = "x   " + mulNumber.ToString();
        mScoreResultField.color = Color.black;
        TextStringAnimation_OneByOne(mScoreResultField.text, mScoreResultField.fontSize, mScoreResultField);
        mScoreResultField.GetComponent<AudioSource>().Play();
        mScoreResultField.DOColor(new Color((mulNumber / times), (mulNumber / times), 0), 0.6f).OnComplete(()=>
        {           
            float nowScore = System.Convert.ToSingle(mScore.text);
            mScore.text = string.Empty;
            if (_Record == 0) { _Record = 1; }
            mScoreAudioSource.Play();
            mScore.DOColor(new Color((_Score / _Record), (_Score / _Record), 0), 0.5f);
            DOTween.To(delegate (float value) { mScore.text = _Score.ToString(); }, nowScore, _Score, 0.4f).OnComplete(() => 
            {
                float preTimeScore = _Time / _Score;
                mPreTimeScoreAudioSource.Play();
                mPreTimeScore.DOText(preTimeScore.ToString(), 0.3f).OnComplete(() => 
                {
                    i++;
                    if (i < System.Enum.GetValues(typeof(ScoreMulType)).Length)
                    {
                        ShowScoreResult();
                    }
                    else
                    {
                        if (_Score > _Record)
                        {
                            ShowNewHighScore();
                            _GameControl.SaveToJson(_Score);                            
                        }
                        mRePlayButton.gameObject.SetActive(true);
                        mShareButton.gameObject.SetActive(true);
                    }
                });
            });
        });
    }


    /// <summary>
    /// 取得倍率名称
    /// </summary>
    /// <param name="scoreMulType">当前倍率类型</param>
    /// <returns></returns>
    private void GetScoreResultBar_Text(ScoreMulType scoreMulType,out string scoreName)
    {
        if (scoreMulType == ScoreMulType.DoneCubeMul) { scoreName = "消块率倍率"; }
        else if (scoreMulType == ScoreMulType.MaxCUbeMul) { scoreName = "最大块数倍率"; }
        else if (scoreMulType == ScoreMulType.ComboAdd) { scoreName = "最大连击分"; }
        else if (scoreMulType == ScoreMulType.PreTimeMul) { scoreName = "每分时间倍率"; }
        else { scoreName = string.Empty; UnityEngine.Debug.Log("EndPanel参数为空"); }
    }

    /// <summary>
    /// 取得倍率
    /// </summary>
    /// <param name="scoreMulType">当前倍率类型</param>
    /// <returns></returns>
    private void GetScoreResultBar_Mul(ScoreMulType scoreMulType, out float mulNumber, out float times)
    {
        mulNumber = 1;
        times = 1;
        GameControl.GetGameControl.GetScoreResultBar_Mul(scoreMulType,out mulNumber,out times);
        //Debug.Log("倍率： "+mulNumber + "优秀比值：" +times);       
    }    
   
    
    /// <summary>
    /// 文本动画
    /// </summary>
    /// <param name="origintext"></param>
    /// <param name="fortsize"></param>
    private void TextStringAnimation_OneByOne(string origintext, int fortsize ,Text text)
    {
        string newString;
        List<string> lString = new List<string>();
        for (int i = 0; i < origintext.Length + 1; i++)
        {
            newString = string.Empty;
            for (int j = 0; j < origintext.Length; j++) //文本替换
            {
                if (i == j)
                {
                    newString += "<size=" + (fortsize*1.25) + ">" + origintext[j] + "</size>";
                }
                else
                {
                    newString += "<size=" + fortsize + ">" + origintext[j] + "</size>";
                }
            }
            lString.Add(newString);
        }
        for (int i = 0; i < origintext.Length + 1; i++)
        {
            float intervalTime = 0.6f / origintext.Length;
            StartCoroutine(TextStringAnimation_Delay(intervalTime * i,text, lString[i]));
        }
    }

    private IEnumerator TextStringAnimation_Delay(float delaySeconds, Text text, string replaceString)
    {
        yield return new WaitForSeconds(delaySeconds);
        text.text = replaceString;
    }
    #endregion

}
