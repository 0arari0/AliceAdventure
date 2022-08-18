using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildBattleRound1 : MonoBehaviour
{
    [SerializeField]
    GameObject canvasInfo;

    public CheckBossTime checkBossTime { get; private set; }

    void Awake()
    {
        checkBossTime = canvasInfo.GetComponent<CheckBossTime>();
    }

    void Start()
    {
        SoundManager.instance.PlayBgm(SoundManager.BGM_Name_.Round1);
    }

    void OnEnable()
    {
        checkBossTime.TimeValueSliderOff();
        Player.instance.Deactivate();
        Player.instance.Activate();
        Player.instance.SetScript(GetComponent<BattleRoundUI>());
    }
}
