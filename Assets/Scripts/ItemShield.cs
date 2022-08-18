using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShield : MonoBehaviour
{
    public GameObject player;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag.Equals("SpadeBullet"))
        {
            Destroy(other.gameObject);
            player.GetComponent<Player>().shieldEnable = false;
        }
        if (other.tag.Equals("Soldier"))
        {
            player.GetComponent<Player>().shieldEnable = false;
            other.GetComponent<CardSoldier>().SoldierDead();
        }
        if (other.tag.Equals("QueenBullet"))
        {
            player.GetComponent<Player>().shieldEnable = false;
            ObjectPool.instance.ReturnQueenBullet(other.gameObject.GetComponent<RoseBullet>());
        }
    }
}