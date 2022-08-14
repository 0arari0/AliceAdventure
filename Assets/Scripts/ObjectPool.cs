using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    [SerializeField]
    GameObject queenBullet;

    Queue<GameObject> queenBulletQ;

    void Awake()
    {
        ApplySingletonPattern();
        Assign();
    }

    void Assign()
    {
        queenBulletQ = new Queue<GameObject>();
    }

    public GameObject GetQueenBullet()
    {
        if (queenBulletQ.Count == 0)
            return Instantiate(queenBullet);
        GameObject obj = queenBulletQ.Dequeue();
        obj.SetActive(true);
        return obj;
    }

    public void ReturnQueenBullet(RoseBullet obj)
    {
        if (obj == null)
            return;
        obj.gameObject.SetActive(false);
        queenBulletQ.Enqueue(obj.gameObject);
    }

    public void DestroyAll()
    {
        GameObject obj;

        while (queenBulletQ.Count != 0)
        {
            obj = queenBulletQ.Dequeue();
            DestroyImmediate(obj);
        }
        queenBulletQ = null;
    }

    void ApplySingletonPattern()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
