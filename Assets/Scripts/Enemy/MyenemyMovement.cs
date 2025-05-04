using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MyenemyMovement : MonoBehaviour
{
    private NavMeshAgent nav;
    private GameObject player;
    private MyenemyHealth myEnemyHealth;
    private Animator enemyAnime;
    private MyplayerHealth myplayerHealth;

    // Start is called before the first frame update
    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        myEnemyHealth = GetComponent<MyenemyHealth>();
        enemyAnime = GetComponent<Animator>();
        myplayerHealth = player.GetComponent<MyplayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myEnemyHealth.isDeath)
            nav.enabled = false;
        else
        {
            if (myplayerHealth.isDeath)
            {
                nav.enabled = false ;
                enemyAnime.SetBool("isTarget", true);
            }else
                nav.SetDestination(player.transform.position);
        }
            
    }
}
