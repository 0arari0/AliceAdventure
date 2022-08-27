using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformMove : MonoBehaviour
{
    [SerializeField]
    float speed;

    Transform _transform;

    Coroutine _move = null;
    public bool _isMove { get; private set; } = false;

    void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    public IEnumerator MoveStart(Vector2 startPos, Vector2 endPos)
    {
        _isMove = true;
        
        _transform.position = startPos;
        while ((Vector2)_transform.position != endPos)
        {
            _transform.position = Vector2.MoveTowards(_transform.position, endPos, speed * Time.deltaTime);
            yield return null;
        }

        _isMove = false;
    }
    public void MoveStop()
    {
        _isMove = false;
        StopCoroutine(_move);
    }

    public Vector2 GetPosition() { return _transform.position; }
    public void SetPosition(Vector2 pos) { _transform.position = pos; }
    public float GetSpeed() { return speed; }
    public void SetSpeed(float speed)
    {
        if (speed <= 0f) speed = 0f;
        this.speed = speed;
    }
}
