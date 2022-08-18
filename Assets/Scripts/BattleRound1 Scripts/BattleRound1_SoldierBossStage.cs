using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleRound1_SoldierBossStage : MonoBehaviour
{
    public float moveSpeed = 100f;
    public bool goRight = false;
    public int soldierHp = 4;
    bool isAttacked = false;
    public GameObject soldierWhite;
    public Sprite[] Soldiersprite;
    bool isDead = false;
    bool canMove = true;
    public int score;

    [SerializeField] GameObject speedUpItem;
    [SerializeField] GameObject damageUpItem;
    [SerializeField] GameObject shieldItem;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = Soldiersprite[Random.Range(0, 4)];
    }
    void Update()
    {
        if (goRight && canMove)
        {
            transform.Translate(new Vector2(moveSpeed * Time.deltaTime, 0));
        }
        else if (!goRight && canMove)
        {
            transform.Translate(new Vector2(-1 * moveSpeed * Time.deltaTime, 0));
        }
        if (isAttacked)
        {
            soldierWhite.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, soldierWhite.GetComponent<SpriteRenderer>().color.a - Time.deltaTime * 4f);
            if (soldierWhite.GetComponent<SpriteRenderer>().color.a <= 0) isAttacked = false;
        }
        if (isDead)
        {
            SpawnItem();
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, GetComponent<SpriteRenderer>().color.a - Time.deltaTime * 4f);
            if (GetComponent<SpriteRenderer>().color.a <= 0) Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("PlayerAttack"))
        {
            soldierHp -= other.GetComponent<PlayerAttackPrefab>().Damage;
            Destroy(other.gameObject);
            if (soldierHp <= 0)
            {
                SoundManager.instance.PlaySfx(SoundManager.SFX_Name_.EnemyDead);
                isDead = true;
            }
            else
            {
                SoundManager.instance.PlaySfx(SoundManager.SFX_Name_.EnemyAttacked);
                isAttacked = true;
                soldierWhite.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }
        }
    }

    void SpawnItem()
    {
        int percentage = Random.Range(0, 100);
        if (percentage < 10)
            Instantiate(speedUpItem, gameObject.transform.position, Quaternion.identity);
        else if (percentage < 15)
            Instantiate(damageUpItem, gameObject.transform.position, Quaternion.identity);
        else if (percentage < 20)
            Instantiate(shieldItem, gameObject.transform.position, Quaternion.identity);
    }
}