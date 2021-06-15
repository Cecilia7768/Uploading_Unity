using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public Transform shakeCamera;
    //셰이크 효과를 줘서 흔들거릴 카메라의 트랜스폼
    public bool shakeRotate = false;
    Vector3 originPos; //원래 위치
    Quaternion originRot; //원래 회전값

    private void Start()
    {
        originPos = shakeCamera.localPosition;
        originRot = shakeCamera.localRotation;
    }

    public IEnumerator ShakeCamera(float durariom = 0.05f, float mPos = 0.03f, float mRot = 0.1f)
    {
        //경과 시간 저장용 변수
        float passTime = 0f;
        //설정한 진동 시간동안만 흔들리도록 설정
        while(passTime < durariom)
        {
            //반지름이 1인 가상의 구체모양에서 좌표를 추출
            //(x,y,z)가 최소 -1.-1.-1) ~ (1,1,1)사이의 값을 추출
            Vector3 shakePos = Random.insideUnitSphere;

            //카메라의 위치를 변경해줌
            shakeCamera.localPosition = shakePos * mPos;

            //카메라를 회전도 시킬경우
            if(shakeRotate)
            {
                float noise = Mathf.PerlinNoise(Time.time * mRot, 0f);
                Vector3 shakeRot = new Vector3(0, 0, noise);

                //위에서 구한 회전값을 카메라에 설정
                shakeCamera.localRotation = Quaternion.Euler(shakeRot);
            }            
            passTime += Time.deltaTime;
            yield return null;
        }
        //진동 후에 카메라의 위치와 회전값을 초기값으로 다시 설정
        shakeCamera.localPosition = originPos;
        shakeCamera.localRotation = originRot;
    }
}
