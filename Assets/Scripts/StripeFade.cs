using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StripeFade : MonoBehaviour
{
    public GameObject stripeUp;
    public GameObject stripeDown;
    public float fadeTime;
    float fadeTime_ = 0;
    public bool fadeIn;
    public bool fadeOut;
    void Awake()
    {
    }
    void Start()
    {
    }
    void Update()
    {
        if(fadeOut)
        {
            stripeUp.GetComponent<RectTransform>().position = new Vector2(360, stripeUp.GetComponent<RectTransform>().position.y - Time.deltaTime * 2880 / fadeTime);
            stripeDown.GetComponent<RectTransform>().position = new Vector2(360, stripeDown.GetComponent<RectTransform>().position.y + Time.deltaTime * 2880 / fadeTime);
            fadeTime_ += Time.deltaTime;
            if (fadeTime_ >= fadeTime)
            {
                stripeUp.GetComponent<RectTransform>().position = new Vector2(360, -400);
                stripeDown.GetComponent<RectTransform>().position = new Vector2(360, 1200);
                fadeIn = false;
            }
        }
        if(fadeIn)
        {

        }
    }
    public void PlayFadeIn(bool _tf)
    {
        if (_tf)
        {
            fadeIn = true;
            stripeUp.GetComponent<RectTransform>().position = new Vector2(0, 800);
            stripeDown.GetComponent<RectTransform>().position = new Vector2(0, -800);
        }
        else
        {
            fadeOut = true;
            stripeUp.GetComponent<RectTransform>().position = new Vector2(0, 2400);
            stripeDown.GetComponent<RectTransform>().position = new Vector2(0, -1440);
        }
    }
}