using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSoldier : MonoBehaviour
{
    /* 20220814 작성자 : 김두현
     * 잡몹에 들어갈 스크립트
     */

    public int soldierHp = 4;
    public float moveSpeed;
    bool isDead = false;
    float deadTime = 1.16f;
    [SerializeField] GameObject speedUpItem;
    [SerializeField] GameObject damageUpItem;
    [SerializeField] GameObject shieldItem;
    void Update()
    {
        if (!isDead)
        {
            transform.Translate(new Vector2(0, -1 * moveSpeed * Time.deltaTime));
        }
        else
        {
            moveSpeed = 0;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, GetComponent<SpriteRenderer>().color.a - Time.deltaTime / deadTime);
            if (GetComponent<SpriteRenderer>().color.a <= 0) Destroy(gameObject);
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
        //SoundManager.instance.PlaySfx(SoundManager.SFX_Name_.EnemyDead);
        SpawnItem(Random.Range(1, 21));
        isDead = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag.Equals("Player"))
        {
            other.GetComponent<Player>().playerHp -= 1;
            SoldierDead();
        }
        if(other.tag.Equals("PlayerAttack"))
        {
            soldierHp -= other.GetComponent<PlayerAttackPrefab>().Damage;
            Destroy(other.gameObject);
            if (soldierHp > 0)
            {
                //SoundManager.instance.PlaySfx(SoundManager.SFX_Name_.EnemyAttacked);
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