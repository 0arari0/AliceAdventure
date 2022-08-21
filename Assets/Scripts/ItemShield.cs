using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShield : MonoBehaviour
{
    void Awake()
    {
        name = "Shield";
    }
    void FixedUpdate()
    {
        transform.position = Player.instance.transform.position;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("SpadeBullet"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
        else if (other.tag.Equals("Soldier"))
        {
            Destroy(gameObject);
            if (other.GetComponent<CardSoldier>() != null)
                other.GetComponent<CardSoldier>().SoldierDead();
            else if (other.GetComponent<SoldierInfo>() != null)
                other.GetComponent<SoldierInfo>().SoldierDead();
        }
        else if (other.tag.Equals("QueenBullet"))
        {
            Destroy(gameObject);
            ObjectPool.instance.ReturnQueenBullet(other.gameObject.GetComponent<RoseBullet>());
        }
        else if (other.tag.Equals("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}