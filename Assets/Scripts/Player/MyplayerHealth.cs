using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MyplayerHealth : MonoBehaviour
{
    public int health = 100;
    public bool isDeath = false;
    public Text PlayerHealth;
    public AudioClip playerAudio;
    public Image hurtEffect;

    private AudioSource playerAudioSource;
    private bool damaged = false;
    private Animator playerAnime;
    private MyplayerMovement myplayerMovement;
    private MyPlayerShooting myPlayerShooting;

    private void Awake()
    {
        playerAudioSource = GetComponent<AudioSource>();
        playerAnime = GetComponent<Animator>();
        myplayerMovement = GetComponent<MyplayerMovement>();
        myPlayerShooting = GetComponentInChildren<MyPlayerShooting>();
    }
    public void TakeDamage(int amount)
    {
        //�Ѿ�����ʱֱ�ӷ���
        if (isDeath) return;

        //��¼�ܵ��˺�
        damaged = true;

        //�ܹ���Ч����˺�ʱ��������������
        if (amount < health)
            playerAudioSource.Play();
        
        health -= amount;

        //����UI
        PlayerHealth.text = health.ToString();

        if (health <= 0)
            Death();
    }

    private void Death()
    {
        isDeath = true;
        //����������Ч
        playerAudioSource.clip = playerAudio;
        playerAudioSource.Play();

        //������������
        playerAnime.SetTrigger("isDead");

        //�����ƶ�������ű�
        myplayerMovement.enabled = false;
        myPlayerShooting.enabled = false;
    }

    private void Update()
    {
        //ʵ���ܻ�Ч��
        if (damaged)
        {
            hurtEffect.color = new Color(1f, 0f, 0f, .1f);
        }
        else
        {
            hurtEffect.color = Color.Lerp(hurtEffect.color, new Color(1f, 0f, 0f, 0f), 5f * Time.deltaTime);
        }

        damaged = false;             
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }
}
