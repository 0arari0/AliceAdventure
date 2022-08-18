using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShield : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("SpadeBullet"))
        {
            Player.instance.shieldEnable = false;
            Destroy(other.gameObject);
        }
        else if (other.tag.Equals("Soldier"))
        {
            Debug.Log("HI");
            Player.instance.shieldEnable = false;
            other.GetComponent<CardSoldier>().SoldierDead();
        }
        else if (other.tag.Equals("QueenBullet"))
        {
            Player.instance.shieldEnable = false;
            ObjectPool.instance.ReturnQueenBullet(other.gameObject.GetComponent<RoseBullet>());
        }
    }
}