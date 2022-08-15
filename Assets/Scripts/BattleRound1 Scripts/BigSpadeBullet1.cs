using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSpadeBullet1 : MonoBehaviour
{
    /* 20220815 작성자 : 김두현
     * 스페이드 기사단장이 발사하는 큰 스페이드에 들어가는 스크립트입니다.
     * 외부에서 쓸일 없음
     */

    public float timeBlock;
    public GameObject smallSpadeBullet;
    public float moveSpeed = 400f;
    public int smallSpadeBulletNum;
    bool isStop = false;
    public float limitPositionY;
    void Update()
    {
        if (transform.position.y < limitPositionY)
        {
            int rand = Random.Range(0, 360 / smallSpadeBulletNum);
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, GetComponent<SpriteRenderer>().color.a - Time.deltaTime);
            if (!isStop)
            {
                for (int i = 0; i < smallSpadeBulletNum; i++)
                {
                    StartCoroutine(ShootBullet(i, rand));
                }
            }
            isStop = true;
            if (GetComponent<SpriteRenderer>().color.a <= 0)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            transform.Translate(new Vector2(0, moveSpeed * Time.deltaTime));
        }
    }
    IEnumerator ShootBullet(int num, int _rand)
    {
        yield return new WaitForSeconds(num * timeBlock);
        Instantiate(smallSpadeBullet, transform.position, Quaternion.Euler(0, 0, _rand + num * (360 / smallSpadeBulletNum)));
    }
}