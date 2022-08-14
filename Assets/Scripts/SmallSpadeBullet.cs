using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallSpadeBullet : MonoBehaviour
{
    /* 20220815 작성자 : 김두현
     * 1라운드 보스인 스페이드 기사단장이 날리는 큰 스페이드에서 갈라지는 작은 스페이드에 들어가는 스크립트입니다.
     * 외부에서 쓸일 없음
     */

    public float moveSpeed = 400f;

    void Update()
    {
        transform.Translate(new Vector2(0, moveSpeed * Time.deltaTime), Space.Self);
    }
}