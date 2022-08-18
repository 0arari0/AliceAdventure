using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    int score = 1000;

    public GameObject[] bigSpadeBullets;
    public  float bulletTime = 1.5f;
    public float moveSpeed = 150f;
    bool canMove = true;
    bool canShoot = true;

    public bool isAlive { get; private set; } = true;
    public bool isAttacked { get; private set; } = false;

    public float CurHp { get { return curHp; } }

    public float MaxHp { get { return maxHp; } }

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
        SoundManager.instance.PlayBgm(SoundManager.BGM_Name_.Round1Boss);
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
        yield return new WaitForSeconds(Random.Range(bulletTime * 0.7f, bulletTime * 1.2f));
        Instantiate(bigSpadeBullets[Random.Range(0, 2)], new Vector2(Random.Range(-120f, 120f), 370f), Quaternion.Euler(0, 0, 180f));
        canShoot = true;
    }

    public IEnumerator GetDamage(int damage) // 여왕이랑 같은 매커니즘
    {
        if (isAttacked) yield break; // 공격 받는 동안은 무적
        isAttacked = true;
        curHp -= damage;
        SoundManager.instance.PlaySfx(SoundManager.SFX_Name_.EnemyAttacked);
        spriteRenderer.color = colorAttacked;
        yield return new WaitForSeconds(0.1f); // 0.1초간 피격 효과
        spriteRenderer.color = colorOrigin;
        isAttacked = false;

        if (curHp <= 0) // 1라 보스 소멸
        {
            isAlive = false;
            GameManager.instance.isClear = true;
            SoundManager.instance.PlaySfx(SoundManager.SFX_Name_.EnemyDead);
            GameManager.instance.AddScore(score);
            animator.speed = 0f; // 애니메이션 중지
            StopCoroutine(_act); // 1라 보스 모든 행동 중지
            for (int i = 0; i < 50; i++) // 1라 보스 점차 희미하게 사라짐
            {
                spriteRenderer.color = new Color(colorOrigin.r, colorOrigin.g, colorOrigin.b, 1f - 0.02f * i);
                yield return null;
            }
            Destroy(gameObject); // 1라 보스 파괴
            ClearSpade();
            SoundManager.instance.bgmPlayer.Stop();
            GameManager.instance.LoadNextScene();
        }
    }

    void ClearSpade()
    {
        for(int i=0;i<GameObject.FindGameObjectsWithTag("SpadeBullet").Length;i++)
        {
            Destroy(GameObject.FindGameObjectsWithTag("SpadeBullet")[i].gameObject);
        }
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