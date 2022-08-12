using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /* 20220812 작성자 : 김두현
     * Player 스크립트에는 주인공 앨리스의 자동공격, 좌우 이동, 아이템 획득을 포함되어 있습니다.
     */

    [SerializeField] GameObject attackPrefab; // 앨리스가 던지는 시계 투사체
    [SerializeField] float moveSpeed = 200f, attackSpeed = 4f; // 앨리스의 이동속도, 공격속도(1초당 n회 공격)
    bool canAttack = true;
    const float itemDuration = 4f;
    float speedUpDuration = 0;
    float damageUpDuration = 0;
    public bool shieldEnable = false;
    public GameObject itemShield;

    public int playerHp = 3;
    [SerializeField] int playerDamage = 1;

    void Update()
    {
        Move();
        if(canAttack)
        {
            Attack();
            canAttack = false;
            StartCoroutine(C_Attack());
        }
        CheckItemDuration();
    }

    void Move()
    {
        // 좌우방향키를 입력하여 앨리스가 좌우로 이동
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.transform.Translate(new Vector2(-1 * moveSpeed * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.transform.Translate(new Vector2(moveSpeed * Time.deltaTime, 0));
        }
    }

    void Attack()
    {
        GameObject obj = Instantiate(attackPrefab, gameObject.transform.position, Quaternion.identity);
        obj.GetComponent<PlayerAttackPrefab>().SetDamage(playerDamage);
    }

    IEnumerator C_Attack()
    {
        yield return new WaitForSeconds(1f / attackSpeed);
        canAttack = true;
    }

    public void TakeItem(Item.ItemType_ _itemType)
    {
        switch(_itemType)
        {
            case Item.ItemType_.MoveSpeedUp: // 이동속도 증가 아이템 획득
                speedUpDuration = itemDuration;
                break;
            case Item.ItemType_.DamageUp: // 공격력 증가 아이템 획득
                damageUpDuration = itemDuration;
                break;
            case Item.ItemType_.Shield: // 방어막 아이템 획득
                shieldEnable = true;
                break;
        }
    }

    void CheckItemDuration()
    {
        if (speedUpDuration > 0)
        {
            speedUpDuration -= Time.deltaTime;
            moveSpeed = 300f;
        }
        else moveSpeed = 200f;

        if (damageUpDuration > 0)
        {
            damageUpDuration -= Time.deltaTime;
            playerDamage = 2;
        }
        else playerDamage = 1;

        if (shieldEnable == true)
        {
            itemShield.SetActive(true);
        }
        else itemShield.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Item"))
        {
            TakeItem(other.GetComponent<Item>().itemType);
            Destroy(other.gameObject);
        }
    }
}