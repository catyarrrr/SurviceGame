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
        //已经死亡时直接返回
        if (isDeath) return;

        //记录受到伤害
        damaged = true;

        //能够有效造成伤害时，播放受伤声音
        if (amount < health)
            playerAudioSource.Play();
        
        health -= amount;

        //设置UI
        PlayerHealth.text = health.ToString();

        if (health <= 0)
            Death();
    }

    private void Death()
    {
        isDeath = true;
        //播放死亡音效
        playerAudioSource.clip = playerAudio;
        playerAudioSource.Play();

        //播放死亡动画
        playerAnime.SetTrigger("isDead");

        //禁用移动、射击脚本
        myplayerMovement.enabled = false;
        myPlayerShooting.enabled = false;
    }

    private void Update()
    {
        //实现受击效果
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
