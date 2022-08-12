using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuButton : MonoBehaviour
{
    public GameObject howPlayWindow, storyWindow, optionWindow; // 메인메뉴 버튼별 윈도우
    public GameObject howPlayGrayWindow, storyGrayWindow, optionGrayWindow;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public void SelectGameStartButton()
    {
        // 메인 메뉴에서 게임 시작 버튼 눌렀을 때 실행
        SoundManager.instance.PlaySfx(SoundManager.SFX_Name_.ButtonClick);
        try
        {
            SceneManager.LoadScene("BattleRound1");
        }
        catch { }
    }

    public void SelectHowPlayButton()
    {
        // 메인 메뉴에서 조작 방법 버튼 눌렀을 때 실행
        // 조작 방법 창에서 닫기 버튼 눌렀을 때 실행
        SoundManager.instance.PlaySfx(SoundManager.SFX_Name_.ButtonClick);
        howPlayGrayWindow.SetActive(!howPlayGrayWindow.activeSelf);
        howPlayWindow.SetActive(!howPlayWindow.activeSelf);
    }

    public void SelectShowStoryButton()
    {
        // 메인 메뉴에서 스토리 열람 버튼 눌렀을 때 실행
        // 스토리 열람 창에서 닫기 버튼 눌렀을 때 실행
        SoundManager.instance.PlaySfx(SoundManager.SFX_Name_.ButtonClick);
        storyGrayWindow.SetActive(!storyGrayWindow.activeSelf);
        storyWindow.SetActive(!storyWindow.activeSelf);
    }

    public void SelectOptionButtonOpen()
    {
        // 메인 메뉴에서 옵션 버튼 눌렀을 때 실행
        SoundManager.instance.PlaySfx(SoundManager.SFX_Name_.ButtonClick);
        optionGrayWindow.SetActive(true);
        optionWindow.SetActive(true);
    }

    public void SelectGameExitButton()
    {
        // 메인 메뉴에서 게임 종료 버튼 눌렀을 때 실행
        SoundManager.instance.PlaySfx(SoundManager.SFX_Name_.ButtonClick);
        Application.Quit();
    }
}