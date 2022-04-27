using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPoint : MonoBehaviour
{
    private GameObject ground;
    private SpawnPoint spawnPoint;
    private Rigidbody rb;
    private ObjectPooler objectPooler;
    private Vector3 test = new Vector3(0, 0, 0);
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ground = GameObject.FindGameObjectWithTag("Ground");
            spawnPoint = ground.GetComponent<SpawnPoint>();
            spawnPoint.playerExited = true;
        }

    }

}
