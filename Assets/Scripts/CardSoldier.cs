using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSoldier : MonoBehaviour
{
    /* 20220814 작성자 : 김두현
     * 카드 병사에 들어갈 스크립트
     */

    const int moveSpeedX = 60;
    public int soldierHp;
    public float moveSpeed;
    bool isDead = false;
    float deadTime = 1.16f;
    int score;
    public SoldierType_ soldierType;
    [SerializeField] GameObject speedUpItem;
    [SerializeField] GameObject damageUpItem;
    [SerializeField] GameObject shieldItem;
    Vector3 playerPosition;
    bool catchPlayerPosition = false;
    GameObject player;
    public bool isAttacked = false;
    public GameObject attackedWhite;

    public enum SoldierType_
    { Spade, Heart, Diamond, Clover }
    /* Spade - 돌진 패턴에서 사용
     * Heart - 유도 패턴에서 사용
     * Diamond - 격자 패턴에서 사용
     * Clover - 일반 패턴에서 사용
     */

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        switch (soldierType)
        {
            // 카드 병사의 타입에 따라서 점수 변경
            case SoldierType_.Spade: score = 200;
                break;
            case SoldierType_.Heart: score = 150;
                break;
            case SoldierType_.Diamond: score = 125;
                break;
            case SoldierType_.Clover: score = 100;
                break;
        }
    }

    void Update()
    {
        if (!isDead)
        {
            PlaySoldierPattern();
        }
        else
        {
            moveSpeed = 0;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, GetComponent<SpriteRenderer>().color.a - Time.deltaTime / deadTime);
            if (GetComponent<SpriteRenderer>().color.a <= 0) Destroy(gameObject);
        }
        if (isAttacked)
        {
            attackedWhite.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, attackedWhite.GetComponent<SpriteRenderer>().color.a - Time.deltaTime * 4f);
            if (attackedWhite.GetComponent<SpriteRenderer>().color.a <= 0) isAttacked = false;
        }
    }

    void PlaySoldierPattern()
    {
        switch(soldierType)
        {
            case SoldierType_.Spade:
                if (transform.position.y > 0f)
                {
                    transform.Translate(new Vector2(0, -1 * moveSpeed * Time.deltaTime));
                    playerPosition = player.gameObject.transform.position;
                }
                else
                {
                    // 플레이어 향해서 돌진
                    if (transform.position.y < -295f)
                    {
                        transform.Translate(new Vector2(0, -1 * moveSpeed * Time.deltaTime));
                        return;
                    }
                    transform.Translate((playerPosition - transform.position).normalized * Time.deltaTime * moveSpeed * 1.25f);
                }
                break;

            case SoldierType_.Heart:
                playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
                transform.Translate(new Vector2((playerPosition - transform.position).normalized.x * Time.deltaTime * moveSpeedX, -1 * Time.deltaTime * moveSpeed));
                break;

            case SoldierType_.Diamond:
            case SoldierType_.Clover:
                transform.Translate(new Vector2(0, -1 * moveSpeed * Time.deltaTime));
                break;
        }
    }

    void SpawnItem(int num)
    {
        switch (num)
        {
            case 0:case 1:
                Instantiate(speedUpItem, gameObject.transform.position, Quaternion.identity);
                break;
            case 2:
                Instantiate(damageUpItem, gameObject.transform.position, Quaternion.identity);
                break;
            case 3:
                Instantiate(shieldItem, gameObject.transform.position, Quaternion.identity);
                break;
            default:
                break;
        }
    }

    public void SoldierDead()
    {
        SoundManager.instance.PlaySfx(SoundManager.SFX_Name_.EnemyAttacked);
        SoundManager.instance.PlaySfx(SoundManager.SFX_Name_.EnemyDead);
        SpawnItem(Random.Range(1, 21));
        GameObject.Find("Main Camera").GetComponent<ScoreAndHeart>().AddScore(score);
        isDead = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag.Equals("Player"))
            SoldierDead();
        if(other.tag.Equals("PlayerAttack"))
        {
            soldierHp -= other.GetComponent<PlayerAttackPrefab>().Damage;
            Destroy(other.gameObject);
            if (soldierHp > 0)
            {
                SoundManager.instance.PlaySfx(SoundManager.SFX_Name_.EnemyAttacked);
                attackedWhite.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                isAttacked = true;
            }
            else
            {
                if(!isDead)
                {
                    SoldierDead();
                }
            }
        }
    }
}