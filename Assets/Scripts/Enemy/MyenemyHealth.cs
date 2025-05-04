using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyenemyHealth : MonoBehaviour
{
    public AudioClip DeathSound;

    public int startinghealth = 100;
    public bool isDeath = false;
    private bool isSinking = false;
    private int point = 10;
    private ParticleSystem Blood;
    private AudioSource enemyAudioSource;
    private Animator animator;
    private CapsuleCollider enemyCapsuleCollider;
    private Rigidbody rb;
    
    private void Awake()
    {
        enemyAudioSource = GetComponent<AudioSource>();
        Blood = GetComponentInChildren<ParticleSystem>();
        animator = GetComponent<Animator>();
        enemyCapsuleCollider = GetComponent<CapsuleCollider>();
        rb = GetComponentInChildren<Rigidbody>();
    }

    private void Update()
    {
        if (isSinking)
        {
            transform.Translate(-transform.up*Time.deltaTime);
        }
    }

    public void TakeDamage(int damage)
    {
        if (startinghealth > damage)
        {
            enemyAudioSource.Play();
        }

        startinghealth -= damage;
        Debug.Log(startinghealth);

        if (startinghealth <= 0)
            Death();
    }

    //击中效果
    public void BloodEfffect(Vector3 efffect)
    {
        Blood.transform.position = efffect;
        Blood.Play();
    }

    public void Death()
    {
        //已经死亡不再触发动画和声音
        if (isDeath)
            return;

        //变更UI分数
        ScoreCount.score += point;

        isDeath = true;
        animator.SetTrigger("Death");

        enemyCapsuleCollider.isTrigger = true;
        rb.isKinematic = true;

        enemyAudioSource.clip = DeathSound;
        enemyAudioSource.Play();
    }

    public void StartSinking()
    {
        isSinking = true;

        Destroy(gameObject, 2f);
    }
}
