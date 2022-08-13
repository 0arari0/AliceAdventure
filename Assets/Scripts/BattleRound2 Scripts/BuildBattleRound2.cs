using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildBattleRound2 : MonoBehaviour
{
    /// <summary>
    /// writer: 김희재
    /// update: 20220813
    /// explanation: Define and render all of what round 2's will be happening.
    /// </summary>
    
    [SerializeField]
    GameObject queen_prefab;
    GameObject queen;

    void Awake()
    {
        queen = Instantiate(queen_prefab);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
