using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Audio;

public class PausePanel : BasePanel {
    [SerializeField]
    private Button mRePlayButton;
    [SerializeField]
    private Button mExitButton;
    [SerializeField]
    private CanvasGroup mCanvasGroup;
    [SerializeField]
    private Slider mVolume;
    [SerializeField]
    private Slider mMusic;
    [SerializeField]
    private Slider mSoundEffect;
    [SerializeField]
    private AudioMixer mAudioMixer;
    private AudioSource mAudioSource;
    private void Start()
    {
        mAudioSource = mRePlayButton.GetComponent<AudioSource>();
        mRePlayButton.GetComponent<Button>().onClick.AddListener(OnReplayClick);
        mExitButton.GetComponent<Button>().onClick.AddListener(OnExitClick);
        mVolume.onValueChanged.AddListener(SetMasterVolume);
        mMusic.onValueChanged.AddListener(SetMusicVolume);
        mSoundEffect.onValueChanged.AddListener(SetSoundEffectVolume);
    }

    public override void EnterPanel()
    {
        base.EnterPanel();        
        EnterAnimation();
    }

    public override void ExitPanel()
    {
        base.ExitPanel();
        ExitAnimation();
    }

    public void SetMasterVolume(float value)
    {
        mAudioMixer.SetFloat("MasterVolume",value);
    }

    public void SetMusicVolume(float value)
    {
        mAudioMixer.SetFloat("MusicVolume", value);
    }

    public void SetSoundEffectVolume(float value)
    {
        mAudioMixer.SetFloat("SoundEffectVolume", value);
    }

    private void OnReplayClick()
    {
        mAudioSource.Play();
        _GameControl.GameResume();
        _UIManager.PushPanel(UIPanelType.Play);
    }

    private void OnExitClick()
    {
        ExitPanel();
        _GameControl.GameExit();
    }

    #region 各类动画
    /// <summary>
    /// 进入面板的动画
    /// </summary>
    private void EnterAnimation()
    {
        gameObject.SetActive(true);
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
    #endregion
}
