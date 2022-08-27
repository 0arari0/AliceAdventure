using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEngine;

public class Player : MonoBehaviour
{
    /* 20220812 작성자 : 김두현
     * Player 스크립트에는 주인공 앨리스의 자동공격, 좌우 이동, 아이템 획득을 포함되어 있습니다.
     */

    public static Player instance = null;
    const int lockPositionX = 280;
    const int lockPositionY = 440;
    [SerializeField] GameObject attackPrefab; // 앨리스가 던지는 시계 투사체
    [SerializeField] float attackSpeed = 6f; // 앨리스의 이동속도, 공격속도(1초당 n회 공격)
    const float itemDuration = 4f;
    float speedUpDuration = 0;
    float damageUpDuration = 0;
    public GameObject itemShield;
    public GameObject shieldClone;

    public bool isAlive { get; private set; }
    public bool isAttacked { get; private set; }
    public bool isEnhenced { get; private set; }
    public int playerHp { get; private set; }
    [SerializeField] int playerDamage = 1;

    [SerializeField]
    Vector2 enterStart, enterEnd; // 플레이어가 입장하는 출발점, 도착점
    RigidBody2DMove rbMove;
    Animator animator;
    SpriteRenderer spriteRenderer;
    BattleRoundUI battleRoundUI;

    Color colorOrigin;
    Color colorAttacked;

    Coroutine _attack = null; // 공격 코루틴
    Coroutine _move = null; // 움직임 코루틴
    Coroutine _checkItemDuration = null; // 아이템 먹은 상태 체크 코루틴

    public void Activate() // 플레이어 활성화
    {
        gameObject.SetActive(true);
        battleRoundUI = null;
    }
    public void Deactivate() // 플레이어 비활성화
    {
        ActStop();
        if (shieldClone != null) Destroy(shieldClone);
        battleRoundUI = null;
        gameObject.SetActive(false);
    }
    public void SetScript(BattleRoundUI battleRoundUI) // battleRoundUI만 배틀 중일 때 따로 저장
    {
        if (gameObject.activeSelf)
            this.battleRoundUI = battleRoundUI;
    }

    void FixedUpdate()
    {
        if (GameManager.instance.isAllClear) return; // 게임을 클리어했으면 더 이상 치트 안먹힘

        if (Input.GetKeyDown(KeyCode.A) && shieldClone == null)
            shieldClone = Instantiate(itemShield, transform.position, Quaternion.identity);
        if (Input.GetKeyDown(KeyCode.S))
            damageUpDuration = 4f;
        if (Input.GetKeyDown(KeyCode.D))
            speedUpDuration = 4f;
    }

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        gameObject.SetActive(false);
    }
    void OnEnable()
    {
        rbMove = GetComponent<RigidBody2DMove>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        isAlive = true;
        isAttacked = false;
        isEnhenced = false;
        playerHp = 3;
        attackSpeed = 6f;
        rbMove.InitializeSpeed();
        playerDamage = 1;
        shieldClone = null;

        speedUpDuration = 0f;
        damageUpDuration = 0f;

        spriteRenderer.color = new Color(1, 1, 1, 1);
        colorOrigin = spriteRenderer.color;
        colorAttacked = new Color(1f, 0.5f, 0.5f);
        AnimationStart();

        StartCoroutine(_Start());
    }
    IEnumerator _Start()
    {
        rbMove.SetPosition(enterStart);
        yield return rbMove.StartCoroutine(rbMove.MoveStart(enterStart, enterEnd)); // 플레이어 입장
        ActStart();
    }
    void ActStart()
    {
        MoveStart();
        AttackStart();
        CheckItemDurationStart();
    }
    public void ActStop()
    {
        MoveStop();
        AttackStop();
        CheckItemDurationStop();
    }
    public void MoveStart(float animationSpeed = 1f)
    {
        if (_move == null)
        {
            AnimationStart(animationSpeed);
            _move = StartCoroutine(_Move());
        }
    }
    public void MoveStop()
    {
        if (_move != null)
        {
            AnimationStop();
            StopCoroutine(_move);
            _move = null;
        }
    }
    IEnumerator _Move()
    {
        while (true)
        {
            // 좌우방향키를 입력하여 앨리스가 좌우로 이동
            // 상하방향키 추가
            if (Input.GetKey(KeyCode.LeftArrow))
                gameObject.transform.Translate(new Vector2(-1 * rbMove.GetSpeed() * Time.deltaTime, 0));
            if (Input.GetKey(KeyCode.RightArrow))
                gameObject.transform.Translate(new Vector2(rbMove.GetSpeed() * Time.deltaTime, 0));
            if (Input.GetKey(KeyCode.UpArrow))
                gameObject.transform.Translate(new Vector2(0, rbMove.GetSpeed() * Time.deltaTime));
            if (Input.GetKey(KeyCode.DownArrow))
                gameObject.transform.Translate(new Vector2(0, -1 * rbMove.GetSpeed() * Time.deltaTime));

            // x좌표가 일정 범위를 벗어나면 다시 되돌아오게 함
            // y좌표가 일정 범위를 벗어나면 다시 되돌아오게 함
            if (GetPosition().x > lockPositionX)
                gameObject.transform.Translate(new Vector2(-1 * rbMove.GetSpeed() * Time.deltaTime, 0));
            else if (GetPosition().x < -1 * lockPositionX)
                gameObject.transform.Translate(new Vector2(rbMove.GetSpeed() * Time.deltaTime, 0));
            else if (GetPosition().y > lockPositionY)
                gameObject.transform.Translate(new Vector2(0, -1 * rbMove.GetSpeed() * Time.deltaTime));
            else if (GetPosition().y < -1 * lockPositionY)
                gameObject.transform.Translate(new Vector2(0, rbMove.GetSpeed() * Time.deltaTime));

            yield return null;
        }
    }

    public void AttackStart()
    {
        if (_attack == null)
            _attack = StartCoroutine(_Attack());
    }
    public void AttackStop()
    {
        if (_attack != null)
        {
            StopCoroutine(_attack);
            _attack = null;
        }
    }
    IEnumerator _Attack()
    {
        while (true)
        {
            // 시계가 플레이어 정수리에서 발사되도록 하였음
            GameObject obj = Instantiate(attackPrefab, rbMove.GetPosition() + new Vector2(18, 32), Quaternion.identity);
            PlayerAttackPrefab script = obj.GetComponent<PlayerAttackPrefab>();
            if (Random.Range(0, 10) == 0)
            {
                script.SetDamage(playerDamage * 2);
                obj.GetComponent<SpriteRenderer>().color = new Color(1, 0, 1, 1);
            }
            else
            {
                script.SetDamage(playerDamage);
                obj.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }
            if (isEnhenced)
                script.SetHardAttack();
            else
                script.SetNormalAttack();
            yield return new WaitForSeconds(1f / attackSpeed);
        }
    }

    public void TakeItem(Item.ItemType_ _itemType)
    {
        switch(_itemType)
        {
            case Item.ItemType_.MoveSpeedUp: // 이동속도 증가 아이템 획득
                speedUpDuration = itemDuration;
                break;
            case Item.ItemType_.DamageUp: // 공격력 증가 아이템 획득
                damageUpDuration = itemDuration;
                break;
            case Item.ItemType_.Shield: // 방어막 아이템 획득
                if (GameObject.FindGameObjectsWithTag("Shield").Length == 0)
                    shieldClone = Instantiate(itemShield, transform.position, Quaternion.identity);
                break;
        }
    }

    void CheckItemDurationStart()
    {
        if (_checkItemDuration == null)
            _checkItemDuration = StartCoroutine(_CheckItemDuration());
    }
    void CheckItemDurationStop()
    {
        if (_checkItemDuration != null)
        {
            StopCoroutine(_checkItemDuration);
            _checkItemDuration = null;
        }
    }
    IEnumerator _CheckItemDuration()
    {
        while (true)
        {
            if (speedUpDuration > 0)
            {
                speedUpDuration -= Time.deltaTime;
                rbMove.SetSpeed(300f);
            }
            else
                rbMove.InitializeSpeed();

            if (damageUpDuration > 0)
            {
                isEnhenced = true;
                damageUpDuration -= Time.deltaTime;
                playerDamage = 2;
            }
            else
            {
                isEnhenced = false;
                playerDamage = 1;
            }

            yield return null;
        }
    }

    public void AnimationStart(float animationSpeed = 1f) { animator.speed = animationSpeed; }
    public void AnimationStop() { animator.speed = 0f; }

    public IEnumerator GetDamage(int damage) // 여왕이랑 같은 매커니즘
    {
        if (isAttacked) yield break; // 공격 받는 동안은 무적
        isAttacked = true;
        playerHp -= damage;
        if (playerHp <= 0) // 플레이어 소멸
        {
            isAlive = false;
            ActStop(); // 플레이어 모든 행동 중지
            for (int i = 0; i < 50; i++) // 플레이어 점차 희미하게 사라짐
            {
                spriteRenderer.color = new Color(colorOrigin.r, colorOrigin.g, colorOrigin.b, 1f - 0.02f * i);
                yield return null;
            }
            battleRoundUI.SetActiveOnPanelGameover(); // 게임오버 패널 창 띄움
            gameObject.SetActive(false); // 플레이어가 죽으면 비활성화
            yield break;
        }
        spriteRenderer.color = colorAttacked;
        yield return new WaitForSeconds(0.2f); // 0.2초간 피격 효과
        spriteRenderer.color = colorOrigin;
        isAttacked = false;
    }

    public void PlayerGameOver()
    {
        ActStop();
        battleRoundUI.SetActiveOnPanelGameover();
    }
    public IEnumerator FadeOut()
    {
        for (float i = 1f; i >= 0f; i -= 0.01f)
        {
            spriteRenderer.color = new Color(colorOrigin.r, colorOrigin.g, colorOrigin.b, i);
            yield return null;
        }
        Deactivate();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Item")) // 아이템 충돌
        {
            SoundManager.instance.PlaySfx(SoundManager.SFX_Name_.GetItem);
            TakeItem(other.GetComponent<Item>().itemType);
            Destroy(other.gameObject);
        }
        else if (other.tag.Equals("SpadeBullet")) // 1라운드 보스 총알 충돌
        {
            if (GameManager.instance.isClear) // 클리어 됐다면 플레이어는 무적
                return;
            StartCoroutine(GetDamage(1));
            Destroy(other.gameObject);
        }
        else if (other.tag.Equals("QueenBullet")) // 2라운드 보스 총알 충돌
        {
            if (GameManager.instance.isClear) // 클리어 됐다면 플레이어는 무적
                return;
            StartCoroutine(GetDamage(2)); // 데미지 2
        }
        else if (other.tag.Equals("Soldier")) // 병정들 맞았을 때
        {
            if (GameManager.instance.isClear)
            {// 클리어 됐다면 플레이어는 무적
                return;
            }
            if (other.gameObject.GetComponent<CardSoldier>() != null && !other.gameObject.GetComponent<CardSoldier>().isDead) // 1라 병정이 죽지 않았다면
            {
                StartCoroutine(GetDamage(1));
                other.gameObject.GetComponent<CardSoldier>().SoldierDead();
            }
            else if (other.gameObject.GetComponent<SoldierInfo>() != null && other.gameObject.GetComponent<SoldierInfo>().isAlive) // 2라 병정이 죽지 않았다면
            {
                StartCoroutine(GetDamage(1));
                other.gameObject.GetComponent<SoldierInfo>().SoldierDead();
            }
        }
        else if (other.tag.Equals("Enemy")) // 스페이드 기사단장, 하트여왕
        {
            if (GameManager.instance.isClear) // 클리어 됐다면 플레이어는 무적
                return;
            StartCoroutine(GetDamage(1));
        }
    }
    void OnTriggerStay2D(Collider2D other) // 한 번에 죽지 않는 몹들 처리(플레이어와 몹이 겹쳐있을 때)
    {
        if (other.tag.Equals("Enemy")) // 스페이드 기사단장, 하트여왕
        {
            if (GameManager.instance.isClear) // 클리어 됐다면 플레이어는 무적
                return;
            StartCoroutine(GetDamage(1));
        }
    }

    public Vector2 GetPosition() { return rbMove.GetPosition(); }
    public void SetPosition(Vector2 newPos) { rbMove.SetPosition(newPos); }
}