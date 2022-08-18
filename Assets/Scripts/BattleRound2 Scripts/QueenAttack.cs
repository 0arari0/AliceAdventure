using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenAttack : MonoBehaviour
{
    [SerializeField]
    [Range(0f, 10f)]
    float secondsToAttack; // 이 시간이 지나야 공격 시작
    [SerializeField]
    [Range(0.1f, 5f)]
    float attackRate; // 한 세트의 총알 쏘고 난 후 쿨타임
    [SerializeField]
    [Range(0.01f, 3f)]
    float interBulletRate; // 한 세트 안에서 각 총알 간의 간격(shotgun은 영향 없음)
                           // 한 세트가 총알 3개, interBulletRate == 0이면 3발이 겹쳐나감
    [SerializeField]
    Vector2 modifyFirePosWhenLeftMoving; // (0, 0)일시 여왕 중앙에서 총알 생성
                                          // 이 값을 조율하여 여왕 중앙으로부터 얼마나 벗어나서 쏠 것인지 결정
                                          // 여왕이 왼쪽으로 이동 중일 때 기준
    [SerializeField]
    [Range(1, 20)]
    int normalBulletCountPerShot;
    [SerializeField]
    [Range(1, 50)]
    int shotgunBulletCoundPerShot;

    Coroutine _totalAttack = null;
    Coroutine _corAttack = null;

    [SerializeField]
    [Range(0f, 360f)]
    float normalAttackDegree; // 총알이 발사되는 방향(오른쪽 방향이 0도, 반시계 방향으로 증가)
    [SerializeField]
    [Range(0f, 360f)]
    float shotgunAttackDegree;
    [SerializeField]
    [Range(0f, 180f)]
    float shotgunErrorRangeDegree; // 샷건 발사 오차 각도

    const int _kindOfAttack = 2;
    string[] _attackName = {
        "_NormalAttack", "_ShotgunAttack"
    };

    Queen scriptQueen = null;

    void Awake()
    {
        ConnectComponents();
    }

    void ConnectComponents()
    {
        scriptQueen = GetComponent<Queen>();
    }

    public void AttackStart()
    {
        if (_totalAttack == null && gameObject.activeSelf)
            _totalAttack = StartCoroutine(_AttackStart());
    }
    IEnumerator _AttackStart()
    {
        yield return new WaitForSeconds(secondsToAttack);
        while (true && gameObject.activeSelf)
        {
            _corAttack = StartCoroutine(_attackName[Random.Range(0, _kindOfAttack)]);
            yield return new WaitForSeconds(attackRate);
        }
    }
    public void AttackStop()
    {
        if (_corAttack != null && gameObject.activeSelf)
        {
            StopCoroutine(_corAttack);
            _corAttack = null;
        }
        if (_totalAttack != null && gameObject.activeSelf)
        {
            StopCoroutine(_totalAttack);
            _totalAttack = null;
        }
    }

    /// <summary>
    /// 여왕이 움직이는 방향에 따라 총알이 발사되는 위치 수정
    /// </summary>
    Vector2 ModifyFirePos()
    {
        return scriptQueen.isMovingLeft ?
            modifyFirePosWhenLeftMoving : new Vector2(-modifyFirePosWhenLeftMoving.x, modifyFirePosWhenLeftMoving.y);
    }

    IEnumerator _NormalAttack()
    {
        for (int i = 0; i < normalBulletCountPerShot && gameObject.activeSelf; i++)
        {
            ObjectPool.instance.GetQueenBullet().GetComponent<RoseBullet>().Set(
                (Vector2)transform.position + ModifyFirePos(),
                normalAttackDegree,
                1f);
            yield return new WaitForSeconds(interBulletRate);
        }
    }
    IEnumerator _ShotgunAttack()
    {
        for (int i = 0; i < shotgunBulletCoundPerShot && gameObject.activeSelf; i++)
            ObjectPool.instance.GetQueenBullet().GetComponent<RoseBullet>().Set(
                (Vector2)transform.position + ModifyFirePos(),
                shotgunAttackDegree + Random.Range(-shotgunErrorRangeDegree, shotgunErrorRangeDegree),
                Random.Range(0.8f, 1.2f));
        yield return null;
    }
}
