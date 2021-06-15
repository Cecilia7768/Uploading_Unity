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
    //���� ���� ����
    float nextFire = 0f;
    readonly float fireRate = 0.1f;//�߻� ����
    readonly float damping = 10f; //ȸ���ӵ� ���

    public bool isFire = false; //�Ѿ� �߻� ���� �Ǵ�
    public AudioClip fireSfx; //�Ѿ� �߻� ����

    readonly float reloadTime = 2f; //������ �ð�
    readonly int maxBullet = 10; //źâ �ִ� �Ѿ� ��
    int currBullet = 10; //���� �Ѿ� ��

    bool isReload; //������ ����
    WaitForSeconds wsReload;//�����ð� ����
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

        //���� �����Ҷ� �����÷��� �ϴ� ��Ȱ��ȭ
        muzzleFlash.enabled = false;
    }

    private void Update()
    {
        //���ݽ�ȣ�� �����ų� ���������� �ƴ϶�� ����
        if (!isReload && isFire)
        {
            //Time.time �� ���÷��� ���� ����� �ð�
            if (Time.time >= nextFire)
            {
                //���� �Լ� ȣ��
                Fire();
                nextFire = Time.time + fireRate + Random.Range(0f, 0.3f);
            }
            //�÷��̾ �ִ� ��ġ�� ȸ������ ���
            //A���� - B���� = B���� A������ ����� �Ÿ�
            //B���� - A���� = A���� B������ ����� �Ÿ�
            Quaternion rot = Quaternion.LookRotation(playerTr.position - enemyTr.position);
            enemyTr.rotation = Quaternion.Slerp(enemyTr.rotation, rot, Time.deltaTime * damping);
        }

        void Fire()
        {
            animator.SetTrigger(hashFire);
            audio.PlayOneShot(fireSfx, 1f);

            StartCoroutine(ShowMuzzleFlash()); //�ѱ�ȭ�� �ڷ�ƾ �Լ� ȣ��

            GameObject _bullet = Instantiate(Bullet, firePos.position,firePos.rotation);
            Destroy(_bullet, 3f);
                
            currBullet--; //�Ѿ� 1�� ������
            isReload = (currBullet % maxBullet == 0);

            if (isReload)
            {
                //������ �ڷ�ƾ �Լ� ȣ��
                StartCoroutine(Reloading());
            }
        }

        IEnumerator Reloading()
        {
            muzzleFlash.enabled = false; //�������� �� ���������� �ȵǴϲ�

            animator.SetTrigger(hashReload);
            audio.PlayOneShot(reloadSfx, 1f);
            yield return wsReload;
            currBullet = maxBullet;
            isReload = false;
        }

        IEnumerator ShowMuzzleFlash()
        {
            //��Ȱ��ȭ�ߴ� �����÷��� Ȱ��ȭ
            muzzleFlash.enabled = true;

            Quaternion rot = Quaternion.Euler(Vector3.forward * Random.Range(0, 360));
            //�ٶ��������̤���
            //�����÷��� ������Ʈ�� 0~360���� ȸ���ϱ� ���� ���
            muzzleFlash.transform.localRotation = rot;

            muzzleFlash.transform.localScale = Vector3.one * Random.Range(1, 2f);
            //���غ��� !(1,1,1) �� ���Ϳ� �������� ���ؼ� ũ�⸦ �����ϴ°�
            //Vector3(1,1,1)*2 = (2,2,2)�� ��
            //��ֶ������ �ٸ�!

            //�������� �ΰ����װ�!
            //0.5�� �������߉�� ����2���ٰ� 0.5������
            Vector2 offset = new Vector2(Random.Range(0, 2), Random.Range(0, 2)) * 0.5f;

            muzzleFlash.material.SetTextureOffset("_MainTex", offset);
            //���̴� ������ ���Ǵ� offset ���� ������ ���� offset���� �����ϱ� ����
            //_Maintex ��� ��Ī�� ���̴� ��ü���� ������� ������
            //����ڰ� ���� �Ұ�

            yield return new WaitForSeconds(Random.Range(0.05f, 0.2f));
            //�����÷��� �����Ǵ� �ð��� 0.05 ~ 0.2�� �����ϰ� ����
            muzzleFlash.enabled = false;
        }


    }
}