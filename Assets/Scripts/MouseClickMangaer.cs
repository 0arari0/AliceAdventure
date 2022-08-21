using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseClickMangaer : MonoBehaviour
{
    public static MouseClickMangaer instance;
    GameObject[] particles;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        particles = new GameObject[transform.childCount];
        for (int i=0;i<transform.childCount;i++)
        {
            particles[i] = transform.GetChild(i).gameObject;
        }
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            PlayClickEffect(Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2, 0));
        }
    }

    void PlayClickEffect(Vector2 _mousePos)
    {
        DisableClickEffect();
        for (int i = 0; i < transform.childCount; i++)
        {
            particles[i].SetActive(true);
            particles[i].GetComponent<RectTransform>().position = _mousePos + new Vector2(Screen.width / 2, Screen.height / 2);
        }
    }

    void DisableClickEffect()
    {
        for (int i = 0; i < particles.Length; i++)
        {
            particles[i].SetActive(false);
        }
    }
}