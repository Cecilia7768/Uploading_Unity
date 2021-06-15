using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum State
    {
        PATROL,//순찰
        TRACE,//추적
        ATTACK,//공격
        DIE//사망
    }

    public State state = State.PATROL; //초기상태 지정

    Transform playerTr; //플레이어 위치 저장 변수 //추적해야할 플레이어 위치
    Transform enemyTr; //적캐릭터 위치 저장 변수

    public float attackDist = 5f;
    public float traceDist = 10f;
    public bool isDie = false;

    WaitForSeconds ws; //시간 지연 변수

    MoveAgent moveAgent;

    EnemyFire enemyFire;

    Animator animator;
    readonly int hashMove = Animator.StringToHash("IsMove");
    readonly int hashSpeed = Animator.StringToHash("Speed");
    readonly int hashDie = Animator.StringToHash("Die");
    readonly int hashDieIdx = Animator.StringToHash("DieIdx");
    readonly int hashOffset = Animator.StringToHash("Offset");
    readonly int hashWalkSoeed = Animator.StringToHash("WalkSpeed");
    readonly int hashPlayerDie = Animator.StringToHash("PlayerDie");
    //"" 쓰는거보다 성능이 더조탕
    private void Awake()
    {
        var player = GameObject.FindGameObjectWithTag("PLAYER");
        if (player != null)
        {
            playerTr = player.GetComponent<Transform>();
            //타인이라 땡겨오는거고
        }
        enemyTr = GetComponent<Transform>();
        moveAgent = GetComponent<MoveAgent>();
        animator = GetComponent<Animator>();

        enemyFire = GetComponent<EnemyFire>();
        //나 자신이니까 가져온다공?
        ws = new WaitForSeconds(0.3f);
        //시간지연변수를 0.3 값으로 설정

        animator.SetFloat(hashOffset, Random.Range(0f,1f));
        animator.SetFloat(hashWalkSoeed, Random.Range(1f, 1.2f));
    }
    private void OnEnable()
    {
        //Damage 스크립트의 OnPlayerDieEvent 에
        //EnemyAI스크립트의 OnPlayerDie함수를 연결시켜줌
        Damage.OnPlayerDieEvent += this.OnPlayerDie;


        //OnEnable은 해당 스크립트가 활성화 될때마다 실행됨
        //상태 체크하는 코루틴 함수 호출
        StartCoroutine(CheckState());

        //상태변화에 따라 행동을 지시하는 코루틴 함수 호출
        StartCoroutine(Action());
    }

    private void OnDisable()
    {
        //스크립트가 비활성화 될때에는
        //이벤트와 연결된 함수 연결해제
        Damage.OnPlayerDieEvent -= this.OnPlayerDie;

    }
    IEnumerator CheckState() //상태체크 코루틴 함수
    {
        while(!isDie) //적이 살아있는 동안 계속 실행되도록 while 사용
        {
            if (state == State.DIE) //뒤졋으면
                yield break;            
            float dist = Vector3.Distance(playerTr.position, enemyTr.position);
            //살아잇으면 적과 플레이어간의 거리를 계산한다 (A위치 - B위치)
            //magnitute 쓰는 경우도 잇음
            if (dist <= attackDist) //사정거리 안쪽이면
            {
                state = State.ATTACK; //공격 ㄱㄱ
            }
            else if( dist <= traceDist )//추적 사거리 이내면 추적 ㄱㄱ
            {
                state = State.TRACE;
            }
            else //둘다 아니면 걍 순찰 ㄱㄱ
            {
                state = State.PATROL; 
            }
            yield return ws; //위에서 설정한 0.3초 대기
        }
    }

    IEnumerator Action()
    {
        while (!isDie)
        {
            yield return ws;

            switch (state)
            {
                case State.PATROL:
                    enemyFire.isFire = false;
                    moveAgent.patrolling = true;
                    animator.SetBool(hashMove,true);
                    break;
                case State.TRACE:
                    enemyFire.isFire = false;
                    moveAgent.traceTarget = playerTr.position;
                    animator.SetBool(hashMove, true);
                    break;
                case State.ATTACK:    
                    moveAgent.Stop();
                    animator.SetBool(hashMove, false);
                    if (enemyFire.isFire == false)
                        enemyFire.isFire = true;
                    break;
                case State.DIE:
                    isDie = true;
                    enemyFire.isFire = false;

                    moveAgent.Stop();
                    //랜덤값에 의해서 애니메이션 3개중에 1개 랜덤하게 실행
                    animator.SetInteger(hashDieIdx, Random.Range(0, 3));
                    animator.SetTrigger(hashDie);

                    //사망후 남아있는 콜라이더 비활성화해서 계속 충돌하지 않도록
                    GetComponent<CapsuleCollider>().enabled = false;
                    break;
            }
        }
    }

    private void Update()
    {
        //애니메이터 변수의 set함수들의 종류는 여러가지가 있음
        //SetFloat등 해당 함수는 (해쉬값/파라메터 이름.전달하고자 하는 값)형태로 사용됨
        animator.SetFloat(hashSpeed, moveAgent.speed);        
    }
    public void OnPlayerDie()
    {
        moveAgent.Stop();
        enemyFire.isFire = false;
        StopAllCoroutines(); //모든 코루틴종료. 유한 상태머신 정지해야됭게

        animator.SetTrigger(hashPlayerDie);
    }
}
