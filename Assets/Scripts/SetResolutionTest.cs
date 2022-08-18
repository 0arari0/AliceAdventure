using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetResolutionTest : MonoBehaviour
{
    void Awake()
    {
        Screen.SetResolution(720, 960, false); // PC 버전만
        Application.targetFrameRate = 60;
    }
}