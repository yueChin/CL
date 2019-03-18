using UnityEngine;

public abstract class BasePanel : MonoBehaviour {

    protected UIManager _UIManager;
    protected GameControl _GameControl;
    protected float _Score = 0;
    protected float _Time = 0;
    protected float _Record = 0;

    public float Score { set { _Score = value; } }
    public float Time { set { _Time = value; } }
    public float Record { set { _Record = value; } }

    public UIManager UIManager
    {
        set { _UIManager = value; }
    }

    public GameControl GameControl
    {
        set { _GameControl = value; }
    }

    public virtual void EnterPanel() { }
    public virtual void ExitPanel() { EventCenter.Broadcast(EventType.HitCubeOrDead); }
    public virtual void PausePanel() { }
    public virtual void ResumePanel() { }
}
