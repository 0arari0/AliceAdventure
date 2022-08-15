using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenSoilderMove : MonoBehaviour, IMove
{
    [SerializeField]
    float standingTime;

    RigidBody2DMove rb2DMove;

    void Awake()
    {
        // 1번째 호출
        rb2DMove = GetComponent<RigidBody2DMove>();

        ValidCheck();
    }
    void Start()
    {
        StartCoroutine(ObjectMove(rb2DMove.GetPosition()));
    }
    void ValidCheck()
    {
        if (standingTime < 0f) standingTime = 1f;
    }

    public IEnumerator ObjectMove(Vector2 initPos)
    {
        Vector2 horizontalEndPos = new Vector2(initPos.x + (initPos.x > 0 ? -350f : 350f), initPos.y);
        yield return rb2DMove.StartCoroutine(rb2DMove.MoveStart(initPos, horizontalEndPos));

        yield return new WaitForSeconds(standingTime);

        Vector2 verticalEndPos = new Vector2(horizontalEndPos.x, -550f);
        yield return rb2DMove.StartCoroutine(rb2DMove.MoveStart(horizontalEndPos, verticalEndPos));

        Destroy(gameObject);
    }
}
