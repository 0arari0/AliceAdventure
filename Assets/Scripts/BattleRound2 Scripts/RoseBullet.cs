using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoseBullet : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField]
    float speed;
    [SerializeField]
    float destroyAfterSeconds;

    void Awake()
    {
        // 1번째로 호출
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="firePos"></param>: 발사될 위치
    /// <param name="degree"></param>: 발사되는 각도(0도는 오른쪽 방향)
    /// <param name="speedMultiplyCoefficient"></param>: 기존 speed에 곱해지는 계수
    public void Set(Vector2 firePos, float degree, float speedMultiplyCoefficient)
    {
        // 2번째로 호출
        if (speedMultiplyCoefficient < 0f)
            speedMultiplyCoefficient = 1f;

        transform.position = firePos;
        rb.velocity = _GetNormalizedVector2(degree) * speed * speedMultiplyCoefficient;
        transform.rotation = Quaternion.Euler(0, 0, 135 + degree);
        StartCoroutine(_DestroyAfterSeconds());
    }

    IEnumerator _DestroyAfterSeconds()
    {
        yield return new WaitForSeconds(destroyAfterSeconds);
        ObjectPool.instance.ReturnQueenBullet(this);
    }

    // 각도를 넣으면 단위벡터 리턴
    Vector2 _GetNormalizedVector2(float degree)
    {
        float rad = degree * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;
    }
}
