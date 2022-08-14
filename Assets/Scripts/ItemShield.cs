using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShield : MonoBehaviour
{
    public GameObject player;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag.Equals("enemyAttack"))
        {
            Destroy(other.gameObject);
            player.GetComponent<Player>().shieldEnable = false;
        }
        if (other.tag.Equals("Soldier"))
        {
            player.GetComponent<Player>().shieldEnable = false;
            other.GetComponent<CardSoldier>().SoldierDead();
        }
    }
}