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
    UI scriptUI;

    Coroutine corMovePattern; // 움직임 전체 코루틴
    Coroutine corCurPattern; // 현재 움직임 코루틴

    [SerializeField]
    Vector2 enterStartPos; // 등장 시작 지점
    [SerializeField]
    Vector2 enterEndPos; // 등장 마지막 지점
    Vector2 movableLimitUpRight = new Vector2(220f, 420f); // 여왕이 화면 내를 움직일 수 있는 범위 우상단 한도
    Vector2 movableLimitBottomDown = new Vector2(-220f, 150f); // 여왕이 화면 내를 움직일 수 있는 범위 좌하단 한도

    bool isAlive = true;
    public bool isMovingLeft { get; private set; } = true;
    [SerializeField]
    int maxHp;
    int curHp;

    void Awake()
    {
        ConnectComponents();
        curHp = maxHp;
        name = "Queen";
        transform.position = enterStartPos;
    }
    void Start()
    {
        corMovePattern = StartCoroutine(ObjectMove(enterStartPos));
    }
    void OnDestroy()
    {

    }

    void ConnectComponents()
    {
        scriptRB2DMove = GetComponent<RigidBody2DMove>();
        scriptQueenAttack = GetComponent<QueenAttack>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        scriptUI = Camera.main.GetComponent<UI>();
    }
    Vector2 GetRandomMovablePosition()
    {
        return new Vector2(Random.Range(movableLimitBottomDown.x, movableLimitUpRight.x),
                           Random.Range(movableLimitBottomDown.y, movableLimitUpRight.y));
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerAttack")
        {
            int damage = other.gameObject.GetComponent<PlayerAttackPrefab>().Damage;
            curHp -= damage;
            if (curHp <= 0f)
            {
                isAlive = false;
                // 게임 종료
                // 이겼으면
                // scriptUI.SetActiveOnPanelWin();
                // 졌으면
                // scriptUI.SetActiveOnPanelGameover();
            }
            Destroy(other.gameObject);
        }

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
            yield return new WaitForSeconds(Random.Range(0.5f, 2f)); // 0.5 ~ 2초 기다림
            Vector2 start = scriptRB2DMove.GetPosition();
            Vector2 end = GetRandomMovablePosition();

            if (start.x < end.x)
            {
                spriteRenderer.flipX = true;
                isMovingLeft = false;
            }
            else
            {
                spriteRenderer.flipX = false;
                isMovingLeft = true;
            }
            yield return corCurPattern = scriptRB2DMove.StartCoroutine(scriptRB2DMove.MoveStart(start, end));
        }
    }
}