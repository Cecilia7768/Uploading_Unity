using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))] //�ش� 
public class MoveAgent : MonoBehaviour
{
    public List<Transform> wayPoints;
    //list�� �迭��
    //������ - �������̷μ� ���빰�� ���� ���̰� ����

    public int nextIdx; //���� ���������� �迭 �ε���

    NavMeshAgent agent;

    float damping = 1f;//ȸ���ӵ� �����ϴ� ���
    Transform enemyTr;

    //������Ƽ �ۼ�
    //������Ƽ�� �Լ��ε� ����ó�� ���̴� �༭��....
    //�ζ��� ����???�ǰ�
    readonly float patrolSpeed = 1.5f; //�б����� �����ӵ� ����
    readonly float traceSpeed = 4f; //�����ӵ� ����
    //readonly�� �� ������ �ȵ�

    bool _patrolling; //���� ���� �Ǵ� ����
    public bool patrolling
    {
        get
        {
            return _patrolling;
        }
        set
        {
            _patrolling = value;
            //set ���۽� ���޹��� ���� value �� ��
            //value�� _patrolling�� ����
            if(_patrolling)
            {
                agent.speed = patrolSpeed;
                damping = 1f;
                MoveWayPoint();
            }
        }
    }

    Vector3 _treaceTarget; //������� ����
    public Vector3 traceTarget
    {
        get
        {
            return _treaceTarget;
        }
        set
        {
            _treaceTarget = value;
            agent.speed = traceSpeed;
            damping = 7f; //�����Ҷ��� �����Ҷ����� 7������� ���� �����ڴٴ� ��
            //������� ���� �Լ� ȣ��
            TraceTarget(_treaceTarget);
        }
    }

    public float speed //agent �̵��ӵ��� �������� ������Ƽ ����
    {
        get
        {
            //get�� �����ϹǷ� ���� ������ ���� ���ϰ� ���� �����ü� ����
            return agent.velocity.magnitude;
            //���ϰ��� ��ϱ� ������Ƽ�� ����!
        }
    }
    private void Start()
    {
        //patrolling = false;
        
        agent = GetComponent<NavMeshAgent>();

        agent.autoBraking = false;
        agent.speed = patrolSpeed;
        agent.updateRotation = false; //�ڵ����� ȸ���ϴ� ��� ��Ȱ��ȭ


        enemyTr = GetComponent<Transform>();
        

        //�극��ũ�� ���� �ڵ� ���������ʵ��� ����
        //�������� ����������� �ӵ��� ���̴� �ɼ�

        var group = GameObject.Find("WayPointGroup");
        //���̾��Ű���� "������Ʈ�̸�"���� �� ������Ʈ�� �˻�
        if(group != null) //������Ʈ ������ ������ ��� != null
        {
            group.GetComponentsInChildren<Transform>(wayPoints);
            //WayPointGroup ������ �ִ� ��� Transform ������Ʈ ������ͼ�
            //wayPoints ������ �־���
            wayPoints.RemoveAt(0); //����Ʈ����߿� ������ �ε����� ������Ʈ ����

            nextIdx = Random.Range(0, wayPoints.Count);
            //���̾��Ű�� ������ point ���� ������ �߿��� ������ ��ġ�� �ϳ� �̾ƿ�
        }

        //��������Ʈ �����ϴ� �Լ� ȣ��    
        MoveWayPoint();
    }

    void MoveWayPoint()
    {
        if (agent.isPathStale) //isPathStale ��� ������϶��� true ������ false
                               //�Ÿ�������϶��� ������� �������� �ʵ��� �ϱ� ����
        {
            return;
        }
        agent.destination = wayPoints[nextIdx].position;
        //������ point �߿��� �Ѱ����� �������� ����
        agent.isStopped = false;
        //�׺���̼� ��� Ȱ��ȭ�ؼ� �̵� �����ϵ��� ����
    }
    void TraceTarget(Vector3 pos)
    {
        if (agent.isPathStale)
            return;

        agent.destination = pos;//�����������
        agent.isStopped = false;
    }
    public void Stop()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;//�ٷ� �����ϱ� ���� �ܿ� �ӵ� 0���� �ʱ�ȭ
        _patrolling = false;

    }
    private void Update()
    {
        if(!agent.isStopped)//���� �����̴� ���϶�
        {
            Quaternion rot = Quaternion.LookRotation(agent.desiredVelocity);
            //���� ����Ǿߵ� ���� ���͸� ���� ȸ������ ���
            enemyTr.rotation = Quaternion.Slerp(enemyTr.rotation,rot, Time.deltaTime * damping);
        }
        

        if (!_patrolling)
            return;
     if(agent.velocity.sqrMagnitude >= 0.2f * 0.2f && agent.remainingDistance <= 0.5f)
        //�������� �����ߴ��� �Ǵ��ϱ� ���� ����
        //�ӵ��� 0.2���� ũ�� ���� �̵��Ÿ��� 0.5������ ���
        //�׳� Magnitude���� sqrMagnitude�� ������ �� ����(����� �Ȱ�)
        {
            //�������� ���� ����������
            //nextIdx++;
            //0 1 2 3 4.. watPoints�� 0�̶�� ����
            //0%10 = 0 ,1%10=10...�ؼ� ��ȯ������ �̷������ �������� ��������
            //ó������ ������ ������ ������ �ٽ� ó������ ���ư��� �ε��� ����
            //nextIdx = nextIdx % wayPoints.Count;
            //�ε��� ���� �� �̵������ϱ����� �Լ� ȣ��

            //�� �ڵ�� ���������� ���������� ��ȯ�ϵ��� �������� �ּ�ó����
            nextIdx = Random.Range(0, wayPoints.Count);
            MoveWayPoint();
        }
    }
}
