using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickParticle : MonoBehaviour
{
    public float moveSpeed;
    float particleSize;

    void OnEnable()
    {
        GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        particleSize = Random.Range(0.25f, 1f);
        gameObject.transform.localScale = new Vector3(particleSize, particleSize, 1);
        StartCoroutine(DisableParticle());
        StartCoroutine(MoveParticle());
    }
    IEnumerator MoveParticle()
    {
        while (true)
        {
            transform.Translate(new Vector2(moveSpeed, moveSpeed), Space.Self);
            yield return new WaitForSecondsRealtime(0.01f);
        }
    }

    IEnumerator DisableParticle()
    {
        yield return new WaitForSecondsRealtime(Random.Range(0.15f, 0.35f));
        gameObject.SetActive(false);
    }
}