using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelCtrl : MonoBehaviour
{
    public GameObject expEffect; //폭발 이펙트 프리팸 변수
    int hitCount = 0;//총알 맞은 횟수
    Rigidbody rb;
    
    public Mesh[] meshes; //모양을 담당하는 메쉬 //ex 애니메이션 클립
    MeshFilter meshFilter;//메쉬를 적용해줄 메쉬필터 //ex 애니메이터

    public Texture[] textures; //껍데기를 담당하는 텍스쳐
    MeshRenderer _renderer;  //텍스쳐를 적용해줄 메쉬 렌더러

    public float expRadius = 10f; //폭발반경

    AudioSource _audio;
    public AudioClip expSfx;

    public Shake shake;
    private void Start()
    {        
        rb = GetComponent<Rigidbody>();
        meshFilter = GetComponent<MeshFilter>();
        _renderer = GetComponent<MeshRenderer>();
        _renderer.material.mainTexture = textures[Random.Range(0,textures.Length)];
        _audio = GetComponent<AudioSource>();

        shake = GameObject.Find("CameraRig").GetComponent<Shake>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("BULLET"))
        {
            hitCount++; //총알이랑 충돌하면 충돌횟수 증가
            if (hitCount == 3) //세번 쳐맞으면
            {
                //폭발 효과 함수 호출
                ExpBarrel();
            }
        }
    }
    void ExpBarrel()
    {
        GameObject effect = Instantiate(expEffect, transform.position, Quaternion.identity);
        //동적생성이 되는 순간 effect라는 객체이름을 부여해줌
        //이후 effect라는 객체명을 통해서 제어가능

        Destroy(effect,2f); //2초뒤에 효과 삭제

        ////Quaternion.identity :디자이너가 만든 방향 님 그대로 터지세여! 존중해드림
        //rb.mass = 1f; //나즁에 mass 무게 조정해야됨 / 드럼이랑 총알이랑 충돌시 알잘딱 균형되게
        //rb.AddForce(Vector3.up * 500f); //위로 500의 힘으로 날려보냄

        IndirectDamage(transform.position);

        //등록된 메쉬중에서 하나를 선택하기 위해 랜덤숫자 뽑고
        int idx = Random.Range(0, meshes.Length);
        //뽑은 인덱스에 해당하는 메쉬를 선택해서 메쉬필터에 적용
        meshFilter.sharedMesh = meshes[idx];

        _audio.PlayOneShot(expSfx, 1f);

        StartCoroutine(shake.ShakeCamera(0.1f, 0.2f, 0.5f));
    }
    void IndirectDamage(Vector3 pos) //(폭발이 일어난 원점을 매개변수로 받아옴)
    {
        Collider[] colls = Physics.OverlapSphere(pos, expRadius, 1 << 8); //다수일 경우에는 배열을 쓰쟈 
        //8번 레이어를 1번으로 옮기셈
        //(폭발원점, 반경, 영향을 줄 레이어)

        //검출된 오브젝트를 순차적으로 하나씩 선택하도록 함
        //1씩 증가하는 for문과 동일함
        foreach (var coll in colls)
        {
            var _rb = coll.GetComponent<Rigidbody>();
            _rb.mass = 1;
            _rb.AddExplosionForce(600f, pos, expRadius, 500f);
            //(가로 폭발력, 폭발원점, 폭발변경, 세로 폭발력)
        }

    }
}
