using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenSoilderSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject[] spawns;
    [SerializeField]
    GameObject[] soilderPrefabs;
    [SerializeField]
    int shuffleCnt; // 병정이 랜덤으로 나오도록 섞는 횟수
    [SerializeField]
    float emergeIntervalTime; // 병정 간 나오는 시간

    Coroutine corDeploy = null;

    public void DeployStart()
    {
        if (corDeploy == null)
            corDeploy = StartCoroutine(_DeployStart());
    }
    IEnumerator _DeployStart()
    {
        while (true)
        {
            Shuffle();
            for (int i = 0; i < spawns.Length; i++)
            {
                GameObject soldierClone = Instantiate(soilderPrefabs[i], transform);
                soldierClone.GetComponent<RigidBody2DMove>().SetPosition(spawns[i].transform.position);
            }
            yield return new WaitForSeconds(emergeIntervalTime);
        }
    }
    public void DeployStop()
    {
        if (corDeploy != null)
        {
            StopCoroutine(corDeploy);
            corDeploy = null;
        }
    }

    void Shuffle()
    {
        if (shuffleCnt < 0)
            shuffleCnt = 5;

        for (int i = 0; i < shuffleCnt; i++)
        {
            int idx1 = Random.Range(0, spawns.Length);
            int idx2 = Random.Range(0, spawns.Length);

            GameObject temp = soilderPrefabs[idx1];
            soilderPrefabs[idx1] = soilderPrefabs[idx2];
            soilderPrefabs[idx2] = temp;
        }
    }

    public GameObject[] GetSpawns() { return spawns; }
}