using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    [SerializeField]
    RabbitController rabbitController;
    [SerializeField]
    AliceController aliceController;
    [SerializeField]
    QueenSoilderSpawner leftSoldierSpawner;
    [SerializeField]
    QueenSoilderSpawner rightSoldierSpawner;
    [SerializeField]
    BattleRoundUI battleRoundUI;

    public bool isProceeding { get; private set; } = false; // 엔딩이 진행 중인지

    Coroutine _endingStart = null;

    /// <summary>
    /// 단순 호출로 실행
    /// </summary>
    public void EndingStart()
    {
        if (_endingStart == null && !isProceeding)
            _endingStart = StartCoroutine(CEndingStart());
    }
    public void EndingStop()
    {
        if (_endingStart != null)
        {
            StopCoroutine(_endingStart);
            _endingStart = null;
            isProceeding = false;
        }
    }

    /// <summary>
    /// IEnumerator로 실행
    /// </summary>
    /// <returns></returns>
    public IEnumerator CEndingStart()
    {
        if (isProceeding) yield break;
        isProceeding = true;

        // 플레이어 앨리스 중지
        // 총알들이나 병정들이 사라질 때까지 잠시 기다림
        // 긴박한 BGM 중지
        Player.instance.ActStop();
        yield return new WaitForSeconds(2f);
        SoundManager.instance.StopBGM();

        // 플레이어 앨리스 비활성화
        Player.instance.Deactivate();

        // 그림 앨리스 등장
        aliceController.transformMove.SetPosition(new Vector2(0f, -100f));
        yield return aliceController.StartCoroutine(aliceController.CFadeIn());

        // 화면 위에서 토끼 걸어나옴
        // bgm 전환
        SoundManager.instance.PlayBgm(SoundManager.BGM_Name_.Ending);
        Vector2 startPos = rabbitController.transformMove.GetPosition();
        Vector2 endPos = new Vector2(0f, 0f);
        yield return rabbitController.transformMove.StartCoroutine(rabbitController.transformMove.MoveStart(startPos, endPos));

        // 1.5초 뒤에 앨리스와 토끼 빵끗 웃음
        yield return new WaitForSeconds(1.5f);
        aliceController.bubbleController.BubbleOn();
        rabbitController.bubbleController.BubbleOn();

        // 2초 기다린 뒤 win panel 등장
        yield return new WaitForSeconds(2f);
        battleRoundUI.SetActiveOnPanelWin();

        isProceeding = false;
    }
}