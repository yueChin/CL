
using UnityEngine;
using DG.Tweening;

public class BattleScene : MonoBehaviour {

    private Transform mBattleField;
    private GameObject mWallOne;
    private GameObject mWallTwo;
    private GameObject mBattleGOOne;
    private GameObject mBattleGOTwo;
    private Vector3 mOnePos;
    private Vector3 mTwoPos;
    private bool mIsOneAttack;
    private bool mIsTwoAttack;
    private bool mIsFight;

    // Use this for initialization
    private void Awake () {
        mBattleField = UnityTool.FindOneOfActiveChild(this.gameObject, "BattleField").transform;
        mWallOne = UnityTool.FindOneOfActiveChild(this.gameObject, "WallOne");
        mWallTwo = UnityTool.FindOneOfActiveChild(this.gameObject, "WallTwo");
        EventCenter.AddListener<float,float,GameObject,GameObject>(EventType.EnterBattle, BattleStart);
        EventCenter.AddListener(EventType.AttackUI, BeginFight);
        EventCenter.AddListener<float, float>(EventType.UnderAttackUI, KnockOther);
        EventCenter.AddListener(EventType.LevelBattle, BattleEnd);
    }

    private void Update()
    {
        if (!mIsFight) return;
        //Debug.Log(mBattleGOOne.transform.localPosition);
        if (mIsOneAttack) {
            mBattleGOOne.transform.localPosition = Vector3.Lerp(mBattleGOOne.transform.localPosition, mTwoPos, Time.deltaTime);
        }
        if (mIsTwoAttack) {
            mBattleGOTwo.transform.localPosition = Vector3.Lerp(mBattleGOTwo.transform.localPosition, mOnePos, Time.deltaTime);
        }
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener<float, float, GameObject, GameObject>(EventType.EnterBattle, BattleStart);
        EventCenter.RemoveListener(EventType.AttackUI, BeginFight);
        EventCenter.RemoveListener<float, float>(EventType.UnderAttackUI, KnockOther);
        EventCenter.RemoveListener(EventType.LevelBattle, BattleEnd);
    }

    private void BattleStart(float wallOnePos,float wallTwoPos,GameObject battleOne,GameObject battleTwo)
    {
        mWallOne.transform.localPosition += new Vector3( - wallOnePos , 0, 0);
        mWallTwo.transform.localPosition += new Vector3( + wallTwoPos, 0, 0);
        mBattleGOOne = battleOne;
        mBattleGOTwo = battleTwo;
        mBattleGOOne.transform.forward = Vector3.right;
        mBattleGOTwo.transform.forward = Vector3.left;
        mBattleGOOne.transform.SetParent(mBattleField);
        mBattleGOTwo.transform.SetParent(mBattleField);
        mBattleGOOne.transform.localPosition = new Vector3(-2, 4.5f, 0);
        mBattleGOTwo.transform.localPosition = new Vector3(2, 4.5f, 0);
        
    }

    private void BattleEnd()
    {
        mIsFight = false;
        Destroy(mBattleGOOne);
        Destroy(mBattleGOTwo);
    }

    private void BeginFight()
    {
        GameControl.GetGameControl.ChangeBattleMusic("Audios/BGM19");
        mIsFight = true;
        PlayerAttack();
        EnemyAttack();
    }

    public void KnockOther(float DistanceOne, float DistanceTwo)
    {
        PlayerReturn(DistanceOne);
        EnemyReturn(DistanceTwo);
    }

    private void PlayerReturn(float backDistance)
    {
        mIsOneAttack = false;
        //Debug.Log("玩家退后距离" + backDistance);
        mBattleGOOne.transform.DOLocalMoveX(mBattleGOOne.transform.localPosition.x - backDistance * 1f, Mathf.Sqrt(backDistance / 10)).
            SetEase(Ease.OutExpo).OnComplete(() => PlayerAttack());
    }

    private void EnemyReturn(float backDistance)
    {
        mIsTwoAttack = false;
        //Debug.Log("敌人退后距离" + backDistance);
        mBattleGOTwo.transform.DOLocalMoveX(mBattleGOTwo.transform.localPosition.x + backDistance * 1f, Mathf.Sqrt(backDistance / 10)).
            SetEase(Ease.OutExpo).OnComplete(() => EnemyAttack()) ;
    }

    private void PlayerAttack()
    {
        mOnePos = new Vector3(mBattleGOOne.transform.localPosition.x,4.5f, 0);
        mBattleGOOne.transform.forward = Vector3.right;       
        mIsTwoAttack = true;        
    }

    private void EnemyAttack()
    {
        mTwoPos = new Vector3(mBattleGOTwo.transform.localPosition.x,4.5f, 0);
        mBattleGOTwo.transform.forward = Vector3.left;
        mIsOneAttack = true;        
    }
}
