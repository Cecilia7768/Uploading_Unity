using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerSfx
{
    public AudioClip[] fire;
    public AudioClip[] reload;
}
//struct = 구조체,클래스 열화판으로 생각하면 편함
//클래스는 구조체 이후 등장한 개념
//그러나 지금에와서는 메모리 적재 형태의 차이만 있을뿐 기능상의 큰 차이는 없음

[System.Serializable] //직렬화 인스펙터에 표시

public class FireCtrl : MonoBehaviour
{
    public enum WeaponType
    {
        RIFLE = 0,
        SHOTGUN
    }

    public WeaponType currWeapon = WeaponType.RIFLE;

    public GameObject bullet; //청일 프리팹 사영하기 위한 변수
    public Transform firePos; //총알 발사 위치
    public ParticleSystem catridge;//탄피 프리팹
    private ParticleSystem muzzleFlash;//총구화염 파티클

    AudioSource _audio;
    public PlayerSfx playerSfx; //오디어 클립 저장 변수

    Shake shake;
    private void Start()
    {
        muzzleFlash = firePos.GetComponentInChildren<ParticleSystem>();
        _audio = GetComponent<AudioSource>();
        shake = GameObject.Find("CameraRig").GetComponent<Shake>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) //0이면 좌 1이면 우클릭
                                         //Down함수는 눌렀을때 한번만 동작
        {
            //공격함수 호출
            Fire();
        }
    }

    void Fire()
    {
        //shake 스크립트 내부의 ShakeCamera 코루틴 함수 호출
        //매개변수 값을 생략했으므로 ShakCamera 함수에 설정된 기본값으로 동작함
        StartCoroutine(shake.ShakeCamera());
        Instantiate(bullet, firePos.position, firePos.rotation);
        //Instantiate (동적생성 할 오브젝트, 위치 , 방향)
        //사용되지 않는 객제(오브젝트)를 활성화 해주는 함수

        //총알 생성하고 나면-은!
        catridge.Play(); //탄피 파티클 재생
        muzzleFlash.Play();
        //FireSfx();
    }
    //void FireSfx()
    //{
    //    var _sfx = playerSfx.fire[(int)currWeapon]; //현재 무기유형에 따른 오디오소스변화
    //    //현재 선택된 무기의 넘버에 맞는 사운드를 선택해서 가지고옴
    //    _audio.PlayOneShot(_sfx ,1f);
    //}
}
