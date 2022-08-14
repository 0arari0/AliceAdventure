using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBGM : MonoBehaviour
{
    void Start()
    {
        SoundManager.instance.PlayBgm(SoundManager.BGM_Name_.MainMenu);
    }
}