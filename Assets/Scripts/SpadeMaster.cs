using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpadeMaster : MonoBehaviour
{
    /* 20220815 작성자 : 김두현
     * 1라운드 보스인 스페이드 기사단장에 넣는 스크립트입니다.
     * 외부에서 쓸일 없음
     */

    public GameObject bigSpadeBullet;
    const float bulletTime = 1.5f;
    public float moveSpeed = 150f;
    bool canMove = true;
    bool canShoot = true;
    void Update()
    {
        if (canMove)
        {
            transform.Translate(new Vector2(0, moveSpeed * Time.deltaTime * -1));
            if (transform.position.y <= 360f) canMove = false;
        }
        else
        {
            if(canShoot)
            {
                canShoot = false;
                StartCoroutine(ShootBullet());
            }
        }
    }
    IEnumerator ShootBullet()
    {
        // 1.05 ~ 1.8 초의 랜덤 쿨타임을 가지는 큰 스페이드 공격
        yield return new WaitForSeconds(Random.Range(bulletTime * 0.7f, bulletTime * 1.2f));
        Instantiate(bigSpadeBullet, new Vector2(Random.Range(-120f, 120f), 370f), Quaternion.Euler(0, 0, 180f));
        canShoot = true;
    }
}