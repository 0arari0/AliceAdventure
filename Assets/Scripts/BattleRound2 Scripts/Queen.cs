using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : MonoBehaviour, IMove
{
    /// <summary>
    /// writer: 김희재
    /// update: 20220815
    /// explanation: Define all of the queen's behaviours.
    /// </summary>

    RigidBody2DMove scriptRB2DMove;
    QueenAttack scriptQueenAttack;
    SpriteRenderer spriteRenderer;
    BuildBattleRound2 buildBattleRound2;
    BattleRoundUI scriptUI;
    Animator animator;

    Coroutine corMovePattern; // 움직임 전체 코루틴
    Coroutine corCurPattern; // 현재 움직임 코루틴

    [SerializeField]
    Vector2 enterStartPos; // 등장 시작 지점
    [SerializeField]
    Vector2 enterEndPos; // 등장 마지막 지점
    Vector2 movableLimitUpRight = new Vector2(220f, 420f); // 여왕이 화면 내를 움직일 수 있는 범위 우상단 한도
    Vector2 movableLimitBottomDown = new Vector2(-220f, 150f); // 여왕이 화면 내를 움직일 수 있는 범위 좌하단 한도

    Color colorOrigin;
    Color colorAttacked;

    public bool isAlive { get; private set; } = true;
    public bool isAttacked { get; private set; } = false;
    public bool isMovingLeft { get; private set; } = true;
    [SerializeField]
    [Range(1, 1000)]
    int maxHp;
    int curHp;
    [SerializeField]
    [Range(0f, 5f)]
    float movingIntervalTime; // 움직임 사이 멈추는 시간
    [SerializeField]
    [Range(0, 100)]
    int movingIntervalTimeErrorPercent; // 움직임 사이 멈추는 시간 오차 퍼센트(10% 이면 +-10% 적용)

    int score = 2000;

    void Awake()
    {
        scriptRB2DMove = GetComponent<RigidBody2DMove>();
        scriptQueenAttack = GetComponent<QueenAttack>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        buildBattleRound2 = Camera.main.GetComponent<BuildBattleRound2>();
        scriptUI = Camera.main.GetComponent<BattleRoundUI>();
        animator = GetComponent<Animator>();
    }
    void OnEnable()
    {
        curHp = maxHp;
        name = "Queen";
        transform.position = enterStartPos;
        colorOrigin = spriteRenderer.color;
        colorAttacked = new Color(1f, 0.5f, 0.5f);
    }
    void Start()
    {
        corMovePattern = StartCoroutine(ObjectMove(enterStartPos));
    }

    Vector2 GetRandomMovablePosition()
    {
        return new Vector2(Random.Range(movableLimitBottomDown.x, movableLimitUpRight.x),
                           Random.Range(movableLimitBottomDown.y, movableLimitUpRight.y));
    }
    IEnumerator GetDamage(int damage)
    {
        if (isAttacked) yield break; // 공격 받는 동안은 무적
        isAttacked = true;
        curHp -= damage;
        if (curHp <= 0) // 여왕 소멸
        {
            isAlive = false;
            GameManager.instance.isClear = true;
            GameManager.instance.AddScore(score);
            SoundManager.instance.PlaySfx(SoundManager.SFX_Name_.EnemyDead);
            animator.speed = 0f; // 애니메이션 중지
            StopCoroutine(corCurPattern); // 부분 이동 중지
            StopCoroutine(corMovePattern); // 전체 이동 중지
            scriptQueenAttack.AttackStop(); // 공격 중지
            buildBattleRound2.DeployStop(); // 병정 소환 중지
            for (int i = 0; i < 50; i++) // 여왕 점차 희미하게 사라짐
            {
                spriteRenderer.color = new Color(colorOrigin.r, colorOrigin.g, colorOrigin.b, 1f - 0.02f * i);
                yield return null;
            }
            Ending ending = buildBattleRound2.ending.GetComponent<Ending>();
            yield return ending.StartCoroutine(ending.EndingStart());
            scriptUI.SetActiveOnPanelWin();
            Player.instance.Deactivate();
            Destroy(gameObject); // 여왕 파괴
            yield break;
        }
        SoundManager.instance.PlaySfx(SoundManager.SFX_Name_.EnemyAttacked);
        spriteRenderer.color = colorAttacked;
        yield return new WaitForSeconds(0.1f); // 0.1초간 피격 효과
        spriteRenderer.color = colorOrigin;
        isAttacked = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerAttack")
        {
            StartCoroutine(GetDamage(other.gameObject.GetComponent<PlayerAttackPrefab>().Damage));
            Destroy(other.gameObject); // 플레이어 총알 파괴
        }
        else if (other.name.Equals("Shield"))
            StartCoroutine(GetDamage(1));
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag.Equals("Player")) // 플레이어가 비비적거릴 때
            StartCoroutine(GetDamage(1));
    }

    /// <summary>
    /// IMove interface 구현
    /// </summary>
    /// <param name="initPos"></param>
    /// <returns></returns>
    public IEnumerator ObjectMove(Vector2 initPos)
    {
        yield return corCurPattern = scriptRB2DMove.StartCoroutine(scriptRB2DMove.MoveStart(initPos, enterEndPos));
        scriptQueenAttack.AttackStart();

        // 화면 돌아다님
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(
                movingIntervalTime * (1 - movingIntervalTimeErrorPercent / 100f),
                movingIntervalTime * (1 + movingIntervalTimeErrorPercent / 100f)));
            Vector2 start = scriptRB2DMove.GetPosition();
            Vector2 end = GetRandomMovablePosition();

            if (start.x < end.x) // 오른쪽으로 이동
            {
                spriteRenderer.flipX = true;
                isMovingLeft = false;
            }
            else // 왼쪽으로 이동
            {
                spriteRenderer.flipX = false;
                isMovingLeft = true;
            }
            yield return corCurPattern = scriptRB2DMove.StartCoroutine(scriptRB2DMove.MoveStart(start, end));
        }
    }
}