using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Warning : MonoBehaviour
{
    [SerializeField]
    [Range(0f, 5f)]
    float stayDuration; // 화면 중앙에 머무는 시간(초)
    [SerializeField]
    [Range(0.01f, 2f)]
    float changeIntervalTime; // 색상이 변경되는 데 소요되는 시간

    Image image;
    RectTransformMove rectMove;

    void Awake()
    {
        rectMove = GetComponent<RectTransformMove>();
        image = GetComponent<Image>();
    }
    void Start()
    {
        StartCoroutine(Move());
        StartCoroutine(RandomChangeImageColor());
    }

    IEnumerator Move()
    {
        Player.instance.AttackStop(); // Warning 표시가 끝날 때까지 앨리스 공격 중지
        yield return new WaitForSeconds(1f); // Scene 넘어가는 효과 끝날 때까지 조금 기다림

        Vector2 pos = rectMove.GetPosition();
        Vector2 screenRight = new Vector2(pos.x, pos.y);
        Vector2 screenMiddle = new Vector2(0f, 150f);
        Vector2 screenLeft = new Vector2(-pos.x, pos.y);

        yield return rectMove.StartCoroutine(rectMove.MoveStart(screenRight, screenMiddle));
        yield return new WaitForSeconds(stayDuration);
        yield return rectMove.StartCoroutine(rectMove.MoveStart(screenMiddle, screenLeft));
        yield return new WaitForSeconds(0.2f);

        Player.instance.AttackStart(); // 공격 재개

        gameObject.transform.parent.gameObject.SetActive(false); // 다 끝나면 종료
    }

    /// <summary>
    /// 이미지의 색 변경
    /// </summary>
    IEnumerator RandomChangeImageColor()
    {
        Color origin = image.color;
        while (true)
        {
            yield return new WaitForSeconds(changeIntervalTime);
            image.color = new Color(origin.r, Random.Range(0.4f, 1f), Random.Range(0.4f, 1f));
        }
    }
}
