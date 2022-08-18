using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSpadeBullet : MonoBehaviour
{
    /* 20220815 작성자 : 김두현
     * 스페이드 기사단장이 발사하는 큰 스페이드에 들어가는 스크립트입니다.
     * 외부에서 쓸일 없음
     */

    public GameObject smallSpadeBullet;
    public float moveSpeed = 400f;
    public int smallSpadeBulletNum;
    float limitPositionY = 0;

    void OnEnable()
    {
        limitPositionY = Random.Range(-50f, 90f);
    }
    void Update()
    {
        if (transform.position.y < limitPositionY)
        {
            int rand = Random.Range(0, 360 / smallSpadeBulletNum);
            for (int i = 0; i < smallSpadeBulletNum; i++)
            {
                Instantiate(smallSpadeBullet, transform.position, Quaternion.Euler(0, 0, rand + i * 360 / smallSpadeBulletNum));
            }
            Destroy(gameObject);
        }
        else
        {
            transform.Translate(new Vector2(0, moveSpeed * Time.deltaTime));
        }
    }
}