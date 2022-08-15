using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBody2DMove : MonoBehaviour
{
    [SerializeField]
    float speed;
    
    Rigidbody2D _rb;

    Coroutine _move = null;
    public bool _isMove { get; private set; } = false;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public IEnumerator MoveStart(Vector2 startPos, Vector2 endPos)
    {
        _isMove = true;
        _rb.position = startPos;

        while (_rb.position != endPos)
        {
            _rb.position = Vector2.MoveTowards(_rb.position, endPos, speed * Time.deltaTime);
            yield return null;
        }
    }
    public void MoveStop()
    {
        _isMove = false;
        StopCoroutine(_move);
    }

    public Vector2 GetPosition() { return _rb.position; }
    public void SetPosition(Vector2 pos) { _rb.position = pos; }
    public float GetSpeed() { return speed; }
    public void SetSpeed(float speed)
    {
        if (speed <= 0f) speed = 5f;
        this.speed = speed;
    }
}