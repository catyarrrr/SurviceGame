using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayerShooting : MonoBehaviour
{
    public float timeBtBullets = .15f;

    private float cot, lightResist = .2f;
    private AudioSource GunSound;
    private LineRenderer GunLine;
    private Light GunLight;
    private ParticleSystem GunParticle;

    private int shootMask;
    private Ray GunRay;
  
    private void Awake()
    {
        GunSound = GetComponent<AudioSource>();
        GunLight = GetComponent<Light>();
        GunLine = GetComponent<LineRenderer>();
        GunParticle = GetComponent<ParticleSystem>();
        shootMask = LayerMask.GetMask("Shootable");

    }
    // Update is called once per frame
    void Update()
    {
        cot += Time.deltaTime;
        if (Input.GetButton("Fire1") && cot > timeBtBullets)
        {
            cot = 0;
            shoot();
        }

        //ǹ���Զ�Ϩ��
        if (cot > timeBtBullets*lightResist)
        {
            GunLight.enabled = false;
            GunLine.enabled = false;
        }
            
    }

    private void shoot()
    {
        //����ǹ��
        GunSound.Play();

        //��ʾǹ��
        GunLight.enabled = true;

        //���߳�ʼλ��
        GunLine.SetPosition(0,transform.position);

        //��¼���߻��еĵ㣨����еĻ���
        RaycastHit Rayhit;

        //�������߷���λ�á�����
        GunRay.origin = transform.position;
        GunRay.direction = transform.forward;

        //�������߻��������������ĩ��λ��
        if (Physics.Raycast(GunRay, out Rayhit, 100, shootMask))
        {
            GunLine.SetPosition(1, Rayhit.point);

            //��ȡ��Ѫ����ص����
            MyenemyHealth myenemyHealth = Rayhit.collider.GetComponent<MyenemyHealth>();
            myenemyHealth.TakeDamage(10);
            myenemyHealth.BloodEfffect(Rayhit.point);
        }
        else
            GunLine.SetPosition(1, transform.position + transform.forward*100);

        //��ʾ����
        GunLine.enabled = true;

        //��ʾ������Ч
        GunParticle.Play();

    }
}
