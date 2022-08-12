using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float moveSpeed;
    public ItemType_ itemType;
    public enum ItemType_
    { MoveSpeedUp, DamageUp, Shield }

    void Update()
    {
        gameObject.transform.Translate(new Vector2(0, -1 * moveSpeed * Time.deltaTime));
    }
}