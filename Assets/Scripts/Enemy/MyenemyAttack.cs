using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyenemyAttack : MonoBehaviour
{
    private bool isInZone = false;
    private float attackCooldown = 1f;
    private float cot = 1f;
    private GameObject player;
    private MyplayerHealth myPlayerHealth;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        myPlayerHealth = player.GetComponent<MyplayerHealth>();
    }

    void Update()
    {
        cot += Time.deltaTime;

        if (isInZone && cot > attackCooldown)
            Attack();
    }

    private void Attack()
    {
        cot = 0;
        myPlayerHealth.TakeDamage(10);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
            isInZone = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
            isInZone = false;
    }


}
