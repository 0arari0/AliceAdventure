using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenSound : MonoBehaviour
{
    void Start()
    {
        SoundManager.instance.PlaySfx(SoundManager.SFX_Name_.Queen);
        StartCoroutine(PlayQueenBgm());
    }
    IEnumerator PlayQueenBgm()
    {
        yield return new WaitForSeconds(5.5f);
        SoundManager.instance.PlayBgm(SoundManager.BGM_Name_.Round2Boss);
    }
}