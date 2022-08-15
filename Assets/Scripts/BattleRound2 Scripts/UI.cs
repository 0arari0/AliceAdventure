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
    [SerializeField]
    GameObject warningUIs;
    [SerializeField]
    GameObject panelGameover;
    [SerializeField]
    GameObject panelWin;

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
        SoundManager.instance.PlaySfx(SoundManager.SFX_Name_.ButtonClick); // 20220814 18:12 김두현 - 버튼클릭 사운드 추가하였습니다
    }
    public void OnClickBack()
    {
        Time.timeScale = 1f;
        panelPause.SetActive(false);
        SoundManager.instance.PlaySfx(SoundManager.SFX_Name_.ButtonClick); // 20220814 18:12 김두현 - 버튼클릭 사운드 추가하였습니다
    }
    public void OnClickOption()
    {
        SoundManager.instance.PlaySfx(SoundManager.SFX_Name_.ButtonClick);
        OptionManager.instance.SelectOptionWindowOpen();
    }
    public void OnClickExit()
    {
        SoundManager.instance.PlaySfx(SoundManager.SFX_Name_.ButtonClick);
        SceneManager.LoadScene("MainMenu");
    }
    public void OnClickReplay()
    {
        SoundManager.instance.PlaySfx(SoundManager.SFX_Name_.ButtonClick);
        SceneManager.LoadScene("BattleRound1");
    }
    public void OnClickHome()
    {
        SoundManager.instance.PlaySfx(SoundManager.SFX_Name_.ButtonClick);
        SceneManager.LoadScene("MainMenu");
    }

    public void SetActiveOnPanelGameover()
    {
        Time.timeScale = 0f;
        panelGameover.SetActive(true);
    }
    public void SetActiveOnPanelWin()
    {
        Time.timeScale = 0f;
        panelWin.SetActive(true);
    }

    public void Warning()
    {
        warningUIs.SetActive(true);
    }
}
