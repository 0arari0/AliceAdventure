using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectTransformMove : MonoBehaviour
{
    [SerializeField]
    float speed;

    RectTransform _rectTransform;

    Coroutine _move = null;
    public bool _isMove { get; private set; } = false;

    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public IEnumerator MoveStart(Vector2 startPos, Vector2 endPos)
    {
        _isMove = true;
        _rectTransform.position = startPos;

        while ((Vector2)_rectTransform.position != endPos)
        {
            _rectTransform.position = Vector2.MoveTowards(_rectTransform.position, endPos, speed * Time.deltaTime);
            yield return null;
        }

        _isMove = false;
    }
    public void MoveStop()
    {
        _isMove = false;
        StopCoroutine(_move);
    }

    public Vector2 GetPosition() { return _rectTransform.position; }
    public void SetPosition(Vector2 pos) { _rectTransform.position = pos; }
    public float GetSpeed() { return speed; }
    public void SetSpeed(float speed)
    {
        if (speed <= 0f) speed = 0f;
        this.speed = speed;
    }
}
