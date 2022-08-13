using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField]
    float speed;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public IEnumerator MoveAtoB(Vector2 startPos, Vector2 endPos)
    {
        SetPosition(startPos);

        while (GetPosition() != endPos)
        {
            SetPosition(Vector2.MoveTowards(rb.position, endPos, speed * Time.deltaTime));
            yield return null;
        }
    }

    public Vector2 GetPosition() { return rb.position; }
    public void SetPosition(Vector2 pos)
    {
        rb.position = pos;
    }
    public float GetSpeed() { return speed; }
    public void SetSpeed(float speed)
    {
        if (speed <= 0f)
            speed = 0f;
        this.speed = speed;
    }
}