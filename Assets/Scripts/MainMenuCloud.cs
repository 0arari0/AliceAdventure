using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCloud : MonoBehaviour
{
    public float moveSpeedX;
    void Update()
    {
        GetComponent<RectTransform>().position = new Vector2(GetComponent<RectTransform>().position.x + -1 * moveSpeedX * Time.deltaTime, 480);
        if (GetComponent<RectTransform>().position.x < -360)
        {
            GetComponent<RectTransform>().position = new Vector3(1080, 480);
        }
    }
}