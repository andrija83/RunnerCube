using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterPoint : MonoBehaviour
{
    private GameObject ground;
    private SpawnPoint spawnPoint;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ground = GameObject.FindGameObjectWithTag("Ground");
            spawnPoint = ground.GetComponent<SpawnPoint>();
            //SpawnPoint.instance.playerEntered = true;
            spawnPoint.playerEntered = true;
        }

    }
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {

    //        spawnPoint.playerEntered = false;
    //        spawnPoint.playerExited = true;

    //    }
    //}
}
