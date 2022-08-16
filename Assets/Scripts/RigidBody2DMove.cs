using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBody2DMove : MonoBehaviour
{
    [SerializeField]
    [Range(50f, 500f)]
    float speed;
    
    Rigidbody2D _rb;

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

    public Vector2 GetPosition() { return _rb.position; }
    public void SetPosition(Vector2 pos) { _rb.position = pos; }
    public float GetSpeed() { return speed; }
    public void SetSpeed(float speed) { this.speed = speed; }
}