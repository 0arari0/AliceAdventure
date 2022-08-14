using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : MonoBehaviour, IMove
{
    /// <summary>
    /// writer: 김희재
    /// update: 20220813
    /// explanation: Define all of the queen's behaviours.
    /// </summary>

    Move scriptMove;
    QueenAttack scriptQueenAttack;

    Coroutine corMovePattern; // 움직임 전체 코루틴
    Coroutine corCurPattern; // 현재 움직임 코루틴

    [SerializeField]
    Vector2 enterStartPos; // 등장 시작 지점
    [SerializeField]
    Vector2 enterEndPos; // 등장 마지막 지점
    Vector2 movableLimitUpRight = new Vector2(220f, 420f); // 여왕이 화면 내를 움직일 수 있는 범위 우상단 한도
    Vector2 movableLimitBottomDown = new Vector2(-220f, 150f); // 여왕이 화면 내를 움직일 수 있는 범위 좌하단 한도

    bool isAlive;
    [SerializeField]
    float maxHp;
    float curHp;

    void Awake()
    {
        ConnectComponent();
        curHp = maxHp;
        name = "Queen";
    }
    void OnEnable()
    {
        isAlive = true;
    }
    void Start()
    {
        corMovePattern = StartCoroutine(ObjectMove(enterStartPos));
    }
    void OnDestroy()
    {

    }

    void ConnectComponent()
    {
        scriptMove = GetComponent<Move>();
        scriptQueenAttack = GetComponent<QueenAttack>();
    }

    Vector2 GetRandomMovablePosition()
    {
        return new Vector2(Random.Range(movableLimitBottomDown.x, movableLimitUpRight.x),
                           Random.Range(movableLimitBottomDown.y, movableLimitUpRight.y));
    }



    /// <summary>
    /// IMove interface 구현
    /// </summary>
    /// <param name="initPos"></param>
    /// <returns></returns>
    public IEnumerator ObjectMove(Vector2 initPos)
    {
        yield return corCurPattern = StartCoroutine(scriptMove.MoveAtoB(initPos, enterEndPos));
        scriptQueenAttack.AttackStart();

        // 화면 돌아다님
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2f, 4f));
            Vector2 start = scriptMove.GetPosition();
            Vector2 end = GetRandomMovablePosition();
            yield return corCurPattern = StartCoroutine(scriptMove.MoveAtoB(start, end));
        }
    }
}