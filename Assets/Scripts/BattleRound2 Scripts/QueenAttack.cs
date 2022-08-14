using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenAttack : MonoBehaviour
{
    [SerializeField]
    float attackRate; // 한 세트의 총알 쏘고 난 후 쿨타임
    [SerializeField]
    float interBulletRate; // 한 세트 안에서 각 총알 간의 간격
                                    // 한 세트가 총알 3개, bulletRatePerSecond == 0이면 3발이 겹쳐나감
    // (0, 0)일시 여왕 중앙에서 총알 생성
    // 이 값을 조율하여 여왕 중앙으로부터 얼마나 벗어나서 쏠 것인지 결정
    [SerializeField]
    Vector2 modifyFirePos;
    [SerializeField]
    int bulletCountPerShot;

    Coroutine _corAttack;

    [SerializeField]
    float normalAttackDegree; // 총알이 발사되는 방향(오른쪽 방향이 0도, 반시계 방향으로 증가)
    [SerializeField]
    float shotgunMaxDegree; // 샷건 최대 각도
    [SerializeField]
    float shotgunMinDegree; // 샷건 최소 각도

    public void AttackStart()
    {
        if (attackRate < 0f)
            attackRate = 0.01f;
        _corAttack = StartCoroutine(_ShotGunAttack());
    }
    public void AttackStop()
    {
        StopCoroutine(_corAttack);
    }

    IEnumerator _NormalAttack()
    {
        while (true)
        {
            for (int i = 0; i < bulletCountPerShot; i++)
            {
                ObjectPool.instance.GetQueenBullet().GetComponent<RoseBullet>().Set((Vector2)transform.position + modifyFirePos, normalAttackDegree);
                yield return new WaitForSeconds(interBulletRate);
            }
            yield return new WaitForSeconds(attackRate);
        }
    }
    IEnumerator _ShotGunAttack()
    {
        while (true)
        {
            for (int i = 0; i < bulletCountPerShot; i++)
                ObjectPool.instance.GetQueenBullet().GetComponent<RoseBullet>().Set((Vector2)transform.position + modifyFirePos, Random.Range(shotgunMinDegree, shotgunMaxDegree));
            yield return new WaitForSeconds(attackRate);
        }
    }
}
