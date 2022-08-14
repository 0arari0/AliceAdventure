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

    public void Set(Vector2 firePos, float degree)
    {
        // 2번째로 호출
        transform.position = firePos;
        rb.velocity = _GetNormalizedVector2(degree) * speed;
        transform.rotation = Quaternion.Euler(0, 0, 135 + degree);
        StartCoroutine(_DestroyAfterSeconds());
    }
    // 아직 미완성
/*    public void Set(Vector2 firePos, Vector2 hitPos)
    {
        // 2번째로 호출
        StartCoroutine(_DestroyAfterSeconds());
        transform.position = firePos;
    }*/

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

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag.Equals("Player"))
        {
            Debug.Log("플레이어가 여왕의 총알에 피격 구현");
        }
    }
}
