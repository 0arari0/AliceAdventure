using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleRoundUI : MonoBehaviour
{
    [SerializeField]
    GameObject bg;
    [SerializeField]
    GameObject panelPause;
    [SerializeField]
    GameObject warningUIs;
    [SerializeField]
    GameObject panelGameover;
    [SerializeField]
    GameObject panelWin;

    void Awake()
    {
        Player.instance.Activate();
        Player.instance.SetScript(this);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) // esc 눌렀을 때
        {
            if (panelPause.activeSelf) OnClickBack(); // 일시정지 윈도우 켜져있으면 닫기
            else OnClickPause(); // 일시정지 윈도우 꺼져있으면 일시정지
        }
    }
    public void OnClickPause()
    {
        Time.timeScale = 0f;
        panelPause.SetActive(true);
        //SoundManager.instance.PlaySfx(SoundManager.SFX_Name_.ButtonClick);
    }
    public void OnClickBack()
    {
        Time.timeScale = 1f;
        panelPause.SetActive(false);
        //SoundManager.instance.PlaySfx(SoundManager.SFX_Name_.ButtonClick);
    }
    public void OnClickOption()
    {
        SoundManager.instance.PlaySfx(SoundManager.SFX_Name_.ButtonClick);
        OptionManager.instance.SelectOptionWindowOpen();
    }
    public void OnClickExit()
    {
        SoundManager.instance.PlaySfx(SoundManager.SFX_Name_.ButtonClick);
        Time.timeScale = 1f;
        Player.instance.ActStop();
        Player.instance.Deactivate();
        GameManager.instance.InitializeScore();
        GameManager.instance.StartCoroutine(GameManager.instance.CorLoadScene("MainMenu"));
    }
    public void OnClickReplay()
    {
        SoundManager.instance.PlaySfx(SoundManager.SFX_Name_.ButtonClick);
        Time.timeScale = 1f;
        GameManager.instance.InitializeScore();
        GameManager.instance.StartCoroutine(GameManager.instance.CorLoadScene("BattleRound1"));
    }
    public void OnClickHome()
    {
        SoundManager.instance.PlaySfx(SoundManager.SFX_Name_.ButtonClick);
        Time.timeScale = 1f;
        Player.instance.ActStop();
        Player.instance.Deactivate();
        GameManager.instance.InitializeScore();
        GameManager.instance.StartCoroutine(GameManager.instance.CorLoadScene("MainMenu"));
    }

    public void SetActiveOnPanelGameover()
    {
        if (SceneManager.GetActiveScene().name == "BattleRound2" && panelWin.activeSelf)
            panelWin.SetActive(false);
        Time.timeScale = 0f;
        panelGameover.SetActive(true);
        if (SceneManager.GetActiveScene().name == "BattleRound2" && panelWin.activeSelf)
            panelWin.SetActive(false);
    }
    public void SetActiveOnPanelWin()
    {
        if (panelGameover.activeSelf)
            panelGameover.SetActive(false);
        Time.timeScale = 0f;
        panelWin.SetActive(true);
        if (panelGameover.activeSelf)
            panelGameover.SetActive(false);
    }

    public void Warning()
    {
        warningUIs.SetActive(true);
    }
}
