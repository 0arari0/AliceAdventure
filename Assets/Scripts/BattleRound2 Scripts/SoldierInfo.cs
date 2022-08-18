using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierInfo : MonoBehaviour
{
    [SerializeField]
    int maxHp;
    int curHp;
    [SerializeField]
    [Range(0, 1000)]
    int score;
    float deadTime = 1.16f;

    [SerializeField]
    GameObject[] items;
    [SerializeField]
    [Range(0, 100)]
    int dropPercentage;
    [SerializeField]
    GameObject attackedWhite;

    RigidBody2DMove rbMove;

    public bool isAlive { get; private set; } = true;
    public bool isAttacked { get; private set; } = false;

    void Awake()
    {
        rbMove = GetComponent<RigidBody2DMove>();
        if (maxHp <= 0)
        {
            maxHp = 3;
            curHp = maxHp;
        }
        if (score < 0)
            score = 100;
    }
    void Update()
    {
        if (!isAlive)
        {
            rbMove.SetSpeed(0f);
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

    void SoldierDead()
    {
        SoundManager.instance.PlaySfx(SoundManager.SFX_Name_.EnemyDead);
        int percentage = Random.Range(0, 101);
        if (percentage <= dropPercentage)
            Instantiate(items[Random.Range(0, items.Length)], gameObject.transform.position, Quaternion.identity);
        isAlive = false;
        GameManager.instance.AddScore(score);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
            SoldierDead();
        if (other.tag.Equals("PlayerAttack"))
        {
            curHp -= other.GetComponent<PlayerAttackPrefab>().Damage;
            Destroy(other.gameObject); // 앨리스 총알 제거
            if (curHp > 0)
            {
                SoundManager.instance.PlaySfx(SoundManager.SFX_Name_.EnemyAttacked);
                attackedWhite.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                isAttacked = true;
            }
            else
            {
                if (isAlive)
                    SoldierDead();
            }
        }
    }
}
