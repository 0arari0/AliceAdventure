using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [SerializeField]
    GameObject bg;
    [SerializeField]
    GameObject panelPause;

    public void OnClickPause()
    {
        Time.timeScale = 0f;
        panelPause.SetActive(true);
    }
    public void OnClickBack()
    {
        Time.timeScale = 1f;
        panelPause.SetActive(false);
    }
    public void OnClickOption()
    {
        OptionManager.instance.SelectOptionWindowOpen();
    }
    public void OnClickExit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
