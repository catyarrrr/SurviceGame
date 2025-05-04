using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyEnemyManager : MonoBehaviour
{
    public GameObject Enemy;
    public GameObject spawnPoint;
    public float spawnCoolDown = 2f;

    private void Start()
    {
        InvokeRepeating("Spawn", 0, spawnCoolDown);
    }

    private void Update()
    {
        
    }

    private void Spawn()
    {
        Instantiate(Enemy,spawnPoint.transform.position, spawnPoint.transform.rotation);
    }
}
