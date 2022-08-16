using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpadeMaster : MonoBehaviour
{
    /* 20220815 작성자 : 김두현
     * 1라운드 보스인 스페이드 기사단장에 넣는 스크립트입니다.
     * 외부에서 쓸일 없음
     */
    [SerializeField]
    [Range(1, 500)]
    float maxHp;
    float curHp;

    public GameObject[] bigSpadeBullets;
    public  float bulletTime = 1.5f;
    public float moveSpeed = 150f;
    bool canMove = true;
    bool canShoot = true;

    public bool isAlive { get; private set; } = true;
    public bool isAttacked { get; private set; } = false;

    Animator animator;
    SpriteRenderer spriteRenderer;
    Coroutine _act = null;

    Color colorOrigin, colorAttacked;

    void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void OnEnable()
    {
        curHp = maxHp;
        colorOrigin = spriteRenderer.color;
        colorAttacked = new Color(1f, 0.5f, 0.5f);
    }
    void Start()
    {
        _act = StartCoroutine(_Act());
    }

    IEnumerator _Act()
    {
        while (true)
        {
            if (canMove)
            {
                transform.Translate(new Vector2(0, moveSpeed * Time.deltaTime * -1));
                if (transform.position.y <= 360f) canMove = false;
            }
            else
            {
                if (canShoot)
                {
                    canShoot = false;
                    StartCoroutine(ShootBullet());
                }
            }
            yield return null;
        }
    }
    IEnumerator ShootBullet()
    {
        // 1.05 ~ 1.8 초의 랜덤 쿨타임을 가지는 큰 스페이드 공격
        yield return new WaitForSeconds(Random.Range(bulletTime * 0.7f, bulletTime * 1.2f));
        Instantiate(bigSpadeBullets[Random.Range(0, 2)], new Vector2(Random.Range(-120f, 120f), 370f), Quaternion.Euler(0, 0, 180f));
        canShoot = true;
    }

    public IEnumerator GetDamage(int damage) // 여왕이랑 같은 매커니즘
    {
        if (isAttacked) yield break; // 공격 받는 동안은 무적
        isAttacked = true;
        curHp -= damage;
        if (curHp <= 0) // 1라 보스 소멸
        {
            isAlive = false;
            animator.speed = 0f; // 애니메이션 중지
            StopCoroutine(_act); // 1라 보스 모든 행동 중지
            for (int i = 0; i < 50; i++) // 1라 보스 점차 희미하게 사라짐
            {
                spriteRenderer.color = new Color(colorOrigin.r, colorOrigin.g, colorOrigin.b, 1f - 0.02f * i);
                yield return null;
            }
            SceneManager.LoadScene("BattleRound2");
            Destroy(gameObject); // 플레이어 파괴
        }
        spriteRenderer.color = colorAttacked;
        yield return new WaitForSeconds(0.2f); // 0.2초간 피격 효과
        spriteRenderer.color = colorOrigin;
        isAttacked = false;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "PlayerAttack")
        {
            StartCoroutine(GetDamage(other.gameObject.GetComponent<PlayerAttackPrefab>().Damage));
            Destroy(other.gameObject);
        }
    }
}