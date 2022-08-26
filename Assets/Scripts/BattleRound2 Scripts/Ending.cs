using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    [SerializeField]
    GameObject rabbit;
    [SerializeField]
    GameObject alice;
    [SerializeField]
    GameObject rabbitSmile;
    [SerializeField]
    GameObject aliceSmile;

    TransformMove _rabbitMove;
    TransformMove _aliceMove;

    SpriteRenderer aliceSpriteRenderer;
    Color aliceColor;

    void Awake()
    {
        aliceColor = alice.GetComponent<SpriteRenderer>().color;
        aliceSpriteRenderer = alice.GetComponent<SpriteRenderer>();
        
        _rabbitMove = rabbit.GetComponent<TransformMove>();
        _aliceMove = rabbit.GetComponent<TransformMove>();
    }

    public IEnumerator EndingStart()
    {
        Vector2 lastPosition = Player.instance.GetPosition();
        yield return Player.instance.StartCoroutine(Player.instance.FadeOut());

        _aliceMove.SetPosition(new Vector2(0, 450f)); // 앨리스 위치 설정
        for (float i = 0f; i <= 1f; i += 0.01f) // fade in
        {
            aliceSpriteRenderer.color = new Color(aliceColor.r, aliceColor.g, aliceColor.b, i);
            yield return null;
        }

        _rabbitMove.StartCoroutine(_rabbitMove.MoveStart(new Vector2(0, 1200f), new Vector2(0, 550f)));


    }
}