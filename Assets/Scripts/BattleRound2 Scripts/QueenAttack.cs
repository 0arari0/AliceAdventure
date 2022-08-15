using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenAttack : MonoBehaviour
{
    [SerializeField]
    float secondsToAttack; // 이 시간이 지나야 공격 시작
    [SerializeField]
    float attackRate; // 한 세트의 총알 쏘고 난 후 쿨타임
    [SerializeField]
    float interBulletRate; // 한 세트 안에서 각 총알 간의 간격(shotgun은 영향 없음)
                           // 한 세트가 총알 3개, bulletRatePerSecond == 0이면 3발이 겹쳐나감
    [SerializeField]
    Vector2 modifyFirePosWhenLeftMoving; // (0, 0)일시 여왕 중앙에서 총알 생성
                                          // 이 값을 조율하여 여왕 중앙으로부터 얼마나 벗어나서 쏠 것인지 결정
                                          // 여왕이 왼쪽으로 이동 중일 때 기준
    [SerializeField]
    int normalBulletCountPerShot;
    [SerializeField]
    int shotgunBulletCoundPerShot;

    Coroutine _totalAttack = null;
    Coroutine _corAttack = null;

    [SerializeField]
    float normalAttackDegree; // 총알이 발사되는 방향(오른쪽 방향이 0도, 반시계 방향으로 증가)
    [SerializeField]
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
        ValidCheck();
    }

    void ConnectComponents()
    {
        scriptQueen = GetComponent<Queen>();
    }
    void ValidCheck()
    {
        // 이상 값이 들어올 시 초기화값을 정해줌
        if (attackRate < 0f) attackRate = 0.01f;
        if (interBulletRate < 0f) interBulletRate = 0.2f;
        if (normalBulletCountPerShot < 0) normalBulletCountPerShot = 5;
        if (shotgunBulletCoundPerShot < 0) shotgunBulletCoundPerShot = 8;
    }

    public void AttackStart()
    {
        if (_totalAttack == null)
            _totalAttack = StartCoroutine(_AttackStart());
    }
    IEnumerator _AttackStart()
    {
        yield return new WaitForSeconds(secondsToAttack);
        while (true)
        {
            _corAttack = StartCoroutine(_attackName[Random.Range(0, _kindOfAttack)]);
            yield return new WaitForSeconds(attackRate);
        }
    }
    public void AttackStop()
    {
        StopCoroutine(_corAttack);
        StopCoroutine(_totalAttack);
        _totalAttack = null;
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
        for (int i = 0; i < normalBulletCountPerShot; i++)
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
        for (int i = 0; i < shotgunBulletCoundPerShot; i++)
            ObjectPool.instance.GetQueenBullet().GetComponent<RoseBullet>().Set(
                (Vector2)transform.position + ModifyFirePos(),
                shotgunAttackDegree + Random.Range(-shotgunErrorRangeDegree, shotgunErrorRangeDegree),
                Random.Range(0.8f, 1.2f));
        yield return null;
    }
}
