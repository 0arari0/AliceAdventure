using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuButton : MonoBehaviour
{
    public GameObject howPlayWindow, optionWindow; // 메인메뉴 버튼별 윈도우
    public GameObject howPlayGrayWindow, optionGrayWindow;
    void Awake()
    {
        // 이거 왜 이 함수 쓰셨죠...?
        // BattleRound1으로 안넘어가서 주석 처리 해놓았습니다.
        //DontDestroyOnLoad(gameObject);
    }
    public void SelectGameStartButton()
    {
        // 메인 메뉴에서 게임 시작 버튼 눌렀을 때 실행
        SoundManager.instance.PlaySfx(SoundManager.SFX_Name_.ButtonClick);
        SceneManager.LoadScene("BattleRound1");
    }

    public void SelectHowPlayButton()
    {
        // 메인 메뉴에서 조작 방법 버튼 눌렀을 때 실행
        // 조작 방법 창에서 닫기 버튼 눌렀을 때 실행
        SoundManager.instance.PlaySfx(SoundManager.SFX_Name_.ButtonClick);
        howPlayGrayWindow.SetActive(!howPlayGrayWindow.activeSelf);
        howPlayWindow.SetActive(!howPlayWindow.activeSelf);
    }

    public void SelectGameExitButton()
    {
        // 메인 메뉴에서 게임 종료 버튼 눌렀을 때 실행
        SoundManager.instance.PlaySfx(SoundManager.SFX_Name_.ButtonClick);
        Application.Quit();
    }
}