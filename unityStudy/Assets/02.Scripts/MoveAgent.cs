using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))] //해당 
public class MoveAgent : MonoBehaviour
{
    public List<Transform> wayPoints;
    //list도 배열임
    //차이점 - 가변길이로서 내용물에 따라 길이가 변함

    public int nextIdx; //다음 순찰지점의 배열 인덱스

    NavMeshAgent agent;

    float damping = 1f;//회전속도 조절하는 계수
    Transform enemyTr;

    //프로퍼티 작성
    //프로퍼티란 함수인데 변수처럼 쓰이는 녀서쿠....
    //인라인 같은???건가
    readonly float patrolSpeed = 1.5f; //읽기전용 순찰속도 변수
    readonly float traceSpeed = 4f; //추적속도 변수
    //readonly는 값 대입은 안됨

    bool _patrolling; //순찰 여부 판단 변수
    public bool patrolling
    {
        get
        {
            return _patrolling;
        }
        set
        {
            _patrolling = value;
            //set 동작시 전달받은 값은 value 에 들어감
            //value를 _patrolling에 대입
            if(_patrolling)
            {
                agent.speed = patrolSpeed;
                damping = 1f;
                MoveWayPoint();
            }
        }
    }

    Vector3 _treaceTarget; //추적대상 지정
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
            damping = 7f; //추적할때가 순찰할때보다 7배빠르게 몸을 돌리겠다는 뜻
            //추적대상 지정 함수 호출
            TraceTarget(_treaceTarget);
        }
    }

    public float speed //agent 이동속도를 가져오는 프로퍼티 정의
    {
        get
        {
            //get만 존재하므로 따로 성정은 하지 못하고 값만 가져올수 있음
            return agent.velocity.magnitude;
            //리턴값이 기니까 프로퍼티를 쓰자!
        }
    }
    private void Start()
    {
        //patrolling = false;
        
        agent = GetComponent<NavMeshAgent>();

        agent.autoBraking = false;
        agent.speed = patrolSpeed;
        agent.updateRotation = false; //자동으로 회전하는 기능 비활성화


        enemyTr = GetComponent<Transform>();
        

        //브레이크를 꺼서 자동 감속하지않도록 해줌
        //목적지에 가까워질수록 속도를 줄이는 옵션

        var group = GameObject.Find("WayPointGroup");
        //하이어라키에서 "오브젝트이름"으로 된 오브젝트를 검색
        if(group != null) //오브젝트 정보가 존재할 경우 != null
        {
            group.GetComponentsInChildren<Transform>(wayPoints);
            //WayPointGroup 하위에 있는 모든 Transform 컴포넌트 가지고와서
            //wayPoints 변수에 넣어줌
            wayPoints.RemoveAt(0); //리스트요소중에 지정된 인덱스의 오브젝트 삭제

            nextIdx = Random.Range(0, wayPoints.Count);
            //하이어라키에 생성한 point 들의 갯수들 중에서 랜덤한 위치를 하나 뽑아옴
        }

        //웨이포인트 변경하는 함수 호출    
        MoveWayPoint();
    }

    void MoveWayPoint()
    {
        if (agent.isPathStale) //isPathStale 경로 계산중일때는 true 끝나면 false
                               //거리계산중일때는 순찰경로 변경하지 않도록 하기 위함
        {
            return;
        }
        agent.destination = wayPoints[nextIdx].position;
        //만들어둔 point 중에서 한곳으로 목적지를 설정
        agent.isStopped = false;
        //네비게이션 기능 활성화해서 이동 시작하도록 변경
    }
    void TraceTarget(Vector3 pos)
    {
        if (agent.isPathStale)
            return;

        agent.destination = pos;//추적대상지정
        agent.isStopped = false;
    }
    public void Stop()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;//바로 정지하기 위해 잔여 속도 0으로 초기화
        _patrolling = false;

    }
    private void Update()
    {
        if(!agent.isStopped)//적이 움직이는 중일때
        {
            Quaternion rot = Quaternion.LookRotation(agent.desiredVelocity);
            //적이 진행되야될 방향 벡터를 통해 회전각도 계산
            enemyTr.rotation = Quaternion.Slerp(enemyTr.rotation,rot, Time.deltaTime * damping);
        }
        

        if (!_patrolling)
            return;
     if(agent.velocity.sqrMagnitude >= 0.2f * 0.2f && agent.remainingDistance <= 0.5f)
        //목적지에 도착했는지 판단하기 위한 조건
        //속도가 0.2보다 크고 남은 이동거리가 0.5이하일 경우
        //그냥 Magnitude보다 sqrMagnitude가 성능이 더 좋다(기능은 똑같)
        {
            //목적지에 거의 도착했을때
            //nextIdx++;
            //0 1 2 3 4.. watPoints를 0이라고 가정
            //0%10 = 0 ,1%10=10...해서 순환구조를 이루기위해 나머지값 연산을함
            //처름부터 마지막 순찰지 돌고나면 다시 처음으로 돌아가게 인덱스 변경
            //nextIdx = nextIdx % wayPoints.Count;
            //인덱스 변경 후 이동시작하기위해 함수 호출

            //위 코드는 순찰지점을 순차적으로 순환하도록 했으르모 주석처리함
            nextIdx = Random.Range(0, wayPoints.Count);
            MoveWayPoint();
        }
    }
}
