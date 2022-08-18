using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StripeFade : MonoBehaviour
{
    public static StripeFade instance = null;

    public GameObject stripeUp;
    public GameObject stripeDown;

    RectTransform stripeUpRect;
    RectTransform stripeDownRect;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        stripeUpRect = stripeUp.GetComponent<RectTransform>();
        stripeDownRect = stripeDown.GetComponent<RectTransform>();
    }

    public IEnumerator FadeOut()
    {
        string _curSceneName = SceneManager.GetActiveScene().name;
        if (_curSceneName != "MainMenu")
        {
            stripeUpRect.position = new Vector2(360f, 2400f);
            stripeDownRect.position = new Vector2(360f, -1440f);
        }
        else
        {
            stripeUpRect.position = new Vector2(360f, 2400f);
            stripeDownRect.position = new Vector2(360f, -1440f);
        }

        float upCurY = stripeUpRect.position.y;
        float downCurY = stripeDownRect.position.y;
        for (int i = 1; i <= 100; i++)
        {
            stripeUpRect.position = new Vector2(stripeUpRect.position.x, upCurY - i * 27.2f);
            stripeDownRect.position = new Vector2(stripeUpRect.position.x, downCurY + i * 27.2f);
            yield return null;
        }
    }
    public IEnumerator FadeIn()
    {
        string _curSceneName = SceneManager.GetActiveScene().name;
        if (_curSceneName != "MainMenu")
        {
            stripeUpRect.position = new Vector2(360f, -320f);
            stripeDownRect.position = new Vector2(360f, 1280f);
        }
        else
        {
            stripeUpRect.position = new Vector2(360f, -320f);
            stripeDownRect.position = new Vector2(360f, 1280f);
        }

        float upCurY = stripeUpRect.position.y;
        float downCurY = stripeDownRect.position.y;
        for (int i = 1; i <= 100; i++)
        {
            stripeUpRect.position = new Vector2(stripeUpRect.position.x, upCurY + i * 27.2f);
            stripeDownRect.position = new Vector2(stripeUpRect.position.x, downCurY - i * 27.2f);
            yield return null;
        }
    }

    //void Update()
    //{
    //    if(fadeOut)
    //    {
    //        stripeUp.GetComponent<RectTransform>().position = new Vector2(360, stripeUp.GetComponent<RectTransform>().position.y - Time.deltaTime * 2880 / fadeTime);
    //        stripeDown.GetComponent<RectTransform>().position = new Vector2(360, stripeDown.GetComponent<RectTransform>().position.y + Time.deltaTime * 2880 / fadeTime);
    //        fadeTime_ += Time.deltaTime;
    //        if (fadeTime_ >= fadeTime)
    //        {
    //            stripeUp.GetComponent<RectTransform>().position = new Vector2(360, -400);
    //            stripeDown.GetComponent<RectTransform>().position = new Vector2(360, 1200);
    //            fadeIn = false;
    //        }
    //    }
    //    if(fadeIn)
    //    {

    //    }
    //}
    //public void PlayFadeIn(bool _tf)
    //{
    //    if (_tf)
    //    {
    //        fadeIn = true;
    //        stripeUp.GetComponent<RectTransform>().position = new Vector2(0, 800);
    //        stripeDown.GetComponent<RectTransform>().position = new Vector2(0, -800);
    //    }
    //    else
    //    {
    //        fadeOut = true;
    //        stripeUp.GetComponent<RectTransform>().position = new Vector2(0, 2400);
    //        stripeDown.GetComponent<RectTransform>().position = new Vector2(0, -1440);
    //    }
    //}
}