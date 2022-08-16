using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Warning : MonoBehaviour
{
    [SerializeField]
    float stayDuration; // 화면 중앙에 머무는 시간(초)
    [SerializeField]
    float changeIntervalTime; // 색상이 변경되는 데 소요되는 시간

    Image image;
    RectTransformMove rectMove;

    void Awake()
    {
        rectMove = GetComponent<RectTransformMove>();
        image = GetComponent<Image>();

        ValidCheck();
    }
    void Start()
    {
        StartCoroutine(Move());
        StartCoroutine(RandomChangeImageColor());
    }

    void ValidCheck()
    {
        if (stayDuration < 0f) stayDuration = 1f;
    }

    IEnumerator Move()
    {
        Vector2 pos = rectMove.GetPosition();
        Vector2 screenRight = new Vector2(pos.x, pos.y);
        Vector2 screenMiddle = new Vector2(0f, 150f);
        Vector2 screenLeft = new Vector2(-pos.x, pos.y);

        yield return rectMove.StartCoroutine(rectMove.MoveStart(screenRight, screenMiddle));
        yield return new WaitForSeconds(stayDuration);
        yield return rectMove.StartCoroutine(rectMove.MoveStart(screenMiddle, screenLeft));
        yield return new WaitForSeconds(0.2f);

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
