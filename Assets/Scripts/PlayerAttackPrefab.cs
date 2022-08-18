using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackPrefab : MonoBehaviour
{
    const int destroyTime = 4;
    [SerializeField] int damage = 1;
    [SerializeField] float moveSpeed = 500f;
    [SerializeField] Sprite hardAttack;
    Sprite normalAttack = null;

    public int Damage { get { return damage; } }

    void Awake()
    {
        normalAttack = GetComponent<SpriteRenderer>().sprite;
    }
    void Start()
    {
        Destroy(this.gameObject, destroyTime);
    }

    void Update()
    {
        gameObject.transform.Translate(new Vector2(0, moveSpeed * Time.deltaTime));
    }

    public void SetDamage(int _dmg)
    {
        damage = _dmg;
    }

    public void SetHardAttack()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = hardAttack;
    }
    public void SetNormalAttack()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = normalAttack;
    }
}