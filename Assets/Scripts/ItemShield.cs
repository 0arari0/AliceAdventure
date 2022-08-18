using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShield : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.position = Player.instance.transform.position;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("SpadeBullet"))
        {
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }
        else if (other.tag.Equals("Soldier"))
        {
            Destroy(this.gameObject);
            other.GetComponent<CardSoldier>().SoldierDead();
        }
        else if (other.tag.Equals("QueenBullet"))
        {
            Destroy(this.gameObject);
            ObjectPool.instance.ReturnQueenBullet(other.gameObject.GetComponent<RoseBullet>());
        }
    }
}