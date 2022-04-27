using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public static SpawnPoint instance;

    private Transform player;
    private GameObject playerGO;
    private GameObject ground;
    private GameObject enterPoint;
    private GameObject exitPoint;

    private bool canSpawnGround = true;
    public bool playerExited { get; set; }
    public bool playerEntered { get; set; }
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        ground = GameObject.FindGameObjectWithTag("Ground");
        enterPoint = GameObject.FindGameObjectWithTag("EnterPoint");
        exitPoint = GameObject.FindGameObjectWithTag("ExitPoint");
    }

    // Update is called once per frame
    void Update()
    {
       
        if (playerEntered)
        {
            ObjectSpawner.instance.SpawnGround();
            //ObjectSpawner.instance.SpawnObstacle();

            canSpawnGround = false;
            playerEntered = false;


        }
        else if (playerExited)
        {
            canSpawnGround = true;
            //TODO OVDE da ga resetujem skpiptu 
            //ObjectPooler.instance.randomCube.GetComponent<Transform>();
            gameObject.SetActive(false);
            playerExited = false;
        }
  
    }
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        playerExited = true;
    //    }
    //}
}
