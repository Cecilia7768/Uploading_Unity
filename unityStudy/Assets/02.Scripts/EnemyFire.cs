using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    AudioSource audio;
    Animator animator;
    Transform playerTr;
    Transform enemyTr;

    readonly int hashFire = Animator.StringToHash("Fire");
    readonly int hashReload = Animator.StringToHash("Reload");
    //공격 관련 변수
    float nextFire = 0f;
    readonly float fireRate = 0.1f;//발사 간격
    readonly float damping = 10f; //회전속도 계수

    public bool isFire = false; //총알 발사 여부 판단
    public AudioClip fireSfx; //총알 발사 사운드

    readonly float reloadTime = 2f; //재장전 시간
    readonly int maxBullet = 10; //탄창 최대 총알 수
    int currBullet = 10; //현재 총알 수

    bool isReload; //재장전 여부
    WaitForSeconds wsReload;//지연시간 변수
    public AudioClip reloadSfx;

    public GameObject Bullet;
    public Transform firePos;

    public MeshRenderer muzzleFlash;
    private void Start()
    {
        playerTr = GameObject.FindGameObjectWithTag("PLAYER").GetComponent<Transform>();
        enemyTr = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        wsReload = new WaitForSeconds(reloadTime);

        //게임 시작할때 머즐플래시 일단 비활성화
        muzzleFlash.enabled = false;
    }

    private void Update()
    {
        //공격신호가 들어오거나 재장전중이 아니라면 실행
        if (!isReload && isFire)
        {
            //Time.time 은 겜플레이 이후 실행된 시간
            if (Time.time >= nextFire)
            {
                //공격 함수 호출
                Fire();
                nextFire = Time.time + fireRate + Random.Range(0f, 0.3f);
            }
            //플레이어가 있는 위치의 회전각도 계산
            //A벡터 - B벡터 = B에서 A까지의 방향과 거리
            //B벡터 - A벡터 = A에서 B까지의 방향과 거리
            Quaternion rot = Quaternion.LookRotation(playerTr.position - enemyTr.position);
            enemyTr.rotation = Quaternion.Slerp(enemyTr.rotation, rot, Time.deltaTime * damping);
        }

        void Fire()
        {
            animator.SetTrigger(hashFire);
            audio.PlayOneShot(fireSfx, 1f);

            StartCoroutine(ShowMuzzleFlash()); //총구화염 코루틴 함수 호출

            GameObject _bullet = Instantiate(Bullet, firePos.position,firePos.rotation);
            Destroy(_bullet, 3f);
                
            currBullet--; //총알 1발 날리고
            isReload = (currBullet % maxBullet == 0);

            if (isReload)
            {
                //재장전 코루틴 함수 호출
                StartCoroutine(Reloading());
            }
        }

        IEnumerator Reloading()
        {
            muzzleFlash.enabled = false; //재장전시 불 켜져있으면 안되니께

            animator.SetTrigger(hashReload);
            audio.PlayOneShot(reloadSfx, 1f);
            yield return wsReload;
            currBullet = maxBullet;
            isReload = false;
        }

        IEnumerator ShowMuzzleFlash()
        {
            //비활성화했던 머즐플래시 활성화
            muzzleFlash.enabled = true;

            Quaternion rot = Quaternion.Euler(Vector3.forward * Random.Range(0, 360));
            //바람개비이이ㅣ이
            //머즐플래시 오브젝트를 0~360도로 회전하기 위해 사용
            muzzleFlash.transform.localRotation = rot;

            muzzleFlash.transform.localScale = Vector3.one * Random.Range(1, 2f);
            //기준벡터 !(1,1,1) 인 벡터에 스케일을 곱해서 크기를 변경하는것
            //Vector3(1,1,1)*2 = (2,2,2)가 됨
            //노멀라이즈랑 다름!

            //오프셋은 두개뿐잉게!
            //0.5씩 움직여야됭게 벡터2에다가 0.5곱해쥼
            Vector2 offset = new Vector2(Random.Range(0, 2), Random.Range(0, 2)) * 0.5f;

            muzzleFlash.material.SetTextureOffset("_MainTex", offset);
            //셰이더 내에서 사용되는 offset 값에 위에서 만든 offset값을 전달하기 위함
            //_Maintex 라는 명칭은 셰이더 자체에서 만들어진 것으로
            //사용자가 변경 불가

            yield return new WaitForSeconds(Random.Range(0.05f, 0.2f));
            //머즐플래시 생성되는 시간을 0.05 ~ 0.2초 랜덤하게 설정
            muzzleFlash.enabled = false;
        }


    }
}
