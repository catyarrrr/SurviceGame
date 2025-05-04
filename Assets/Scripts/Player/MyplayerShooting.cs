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

        //枪火自动熄灭
        if (cot > timeBtBullets*lightResist)
        {
            GunLight.enabled = false;
            GunLine.enabled = false;
        }
            
    }

    private void shoot()
    {
        //播放枪声
        GunSound.Play();

        //显示枪火
        GunLight.enabled = true;

        //膛线初始位置
        GunLine.SetPosition(0,transform.position);

        //记录射线击中的点（如果有的话）
        RaycastHit Rayhit;

        //设置射线发出位置、方向
        GunRay.origin = transform.position;
        GunRay.direction = transform.forward;

        //根据射线击中与否，设置膛线末端位置
        if (Physics.Raycast(GunRay, out Rayhit, 100, shootMask))
        {
            GunLine.SetPosition(1, Rayhit.point);

            //获取与血量相关的组件
            MyenemyHealth myenemyHealth = Rayhit.collider.GetComponent<MyenemyHealth>();
            myenemyHealth.TakeDamage(10);
            myenemyHealth.BloodEfffect(Rayhit.point);
        }
        else
            GunLine.SetPosition(1, transform.position + transform.forward*100);

        //显示膛线
        GunLine.enabled = true;

        //显示粒子特效
        GunParticle.Play();

    }
}
