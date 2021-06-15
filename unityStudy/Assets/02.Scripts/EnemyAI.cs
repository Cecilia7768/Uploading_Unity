using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum State
    {
        PATROL,//����
        TRACE,//����
        ATTACK,//����
        DIE//���
    }

    public State state = State.PATROL; //�ʱ���� ����

    Transform playerTr; //�÷��̾� ��ġ ���� ���� //�����ؾ��� �÷��̾� ��ġ
    Transform enemyTr; //��ĳ���� ��ġ ���� ����

    public float attackDist = 5f;
    public float traceDist = 10f;
    public bool isDie = false;

    WaitForSeconds ws; //�ð� ���� ����

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
    //"" ���°ź��� ������ ������
    private void Awake()
    {
        var player = GameObject.FindGameObjectWithTag("PLAYER");
        if (player != null)
        {
            playerTr = player.GetComponent<Transform>();
            //Ÿ���̶� ���ܿ��°Ű�
        }
        enemyTr = GetComponent<Transform>();
        moveAgent = GetComponent<MoveAgent>();
        animator = GetComponent<Animator>();

        enemyFire = GetComponent<EnemyFire>();
        //�� �ڽ��̴ϱ� �����´ٰ�?
        ws = new WaitForSeconds(0.3f);
        //�ð����������� 0.3 ������ ����

        animator.SetFloat(hashOffset, Random.Range(0f,1f));
        animator.SetFloat(hashWalkSoeed, Random.Range(1f, 1.2f));
    }
    private void OnEnable()
    {
        //Damage ��ũ��Ʈ�� OnPlayerDieEvent ��
        //EnemyAI��ũ��Ʈ�� OnPlayerDie�Լ��� ���������
        Damage.OnPlayerDieEvent += this.OnPlayerDie;


        //OnEnable�� �ش� ��ũ��Ʈ�� Ȱ��ȭ �ɶ����� �����
        //���� üũ�ϴ� �ڷ�ƾ �Լ� ȣ��
        StartCoroutine(CheckState());

        //���º�ȭ�� ���� �ൿ�� �����ϴ� �ڷ�ƾ �Լ� ȣ��
        StartCoroutine(Action());
    }

    private void OnDisable()
    {
        //��ũ��Ʈ�� ��Ȱ��ȭ �ɶ�����
        //�̺�Ʈ�� ����� �Լ� ��������
        Damage.OnPlayerDieEvent -= this.OnPlayerDie;

    }
    IEnumerator CheckState() //����üũ �ڷ�ƾ �Լ�
    {
        while(!isDie) //���� ����ִ� ���� ��� ����ǵ��� while ���
        {
            if (state == State.DIE) //�ڠ�����
                yield break;            
            float dist = Vector3.Distance(playerTr.position, enemyTr.position);
            //��������� ���� �÷��̾�� �Ÿ��� ����Ѵ� (A��ġ - B��ġ)
            //magnitute ���� ��쵵 ����
            if (dist <= attackDist) //�����Ÿ� �����̸�
            {
                state = State.ATTACK; //���� ����
            }
            else if( dist <= traceDist )//���� ��Ÿ� �̳��� ���� ����
            {
                state = State.TRACE;
            }
            else //�Ѵ� �ƴϸ� �� ���� ����
            {
                state = State.PATROL; 
            }
            yield return ws; //������ ������ 0.3�� ���
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
                    //�������� ���ؼ� �ִϸ��̼� 3���߿� 1�� �����ϰ� ����
                    animator.SetInteger(hashDieIdx, Random.Range(0, 3));
                    animator.SetTrigger(hashDie);

                    //����� �����ִ� �ݶ��̴� ��Ȱ��ȭ�ؼ� ��� �浹���� �ʵ���
                    GetComponent<CapsuleCollider>().enabled = false;
                    break;
            }
        }
    }

    private void Update()
    {
        //�ִϸ����� ������ set�Լ����� ������ ���������� ����
        //SetFloat�� �ش� �Լ��� (�ؽ���/�Ķ���� �̸�.�����ϰ��� �ϴ� ��)���·� ����
        animator.SetFloat(hashSpeed, moveAgent.speed);        
    }
    public void OnPlayerDie()
    {
        moveAgent.Stop();
        enemyFire.isFire = false;
        StopAllCoroutines(); //��� �ڷ�ƾ����. ���� ���¸ӽ� �����ؾ߉��

        animator.SetTrigger(hashPlayerDie);
    }
}
