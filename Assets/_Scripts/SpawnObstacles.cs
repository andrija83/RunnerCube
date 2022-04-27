using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour
{

    //TODO NEMOZE OVAKO MRDA CEO SPAWN MANAGER KAD MRDA PLAYER
    //Dobra je courotina ali nevalja spawn place

    public GameObject[] obstacles;
    public float respawnTime = 2f;
    private int sectionNumber;
    private int xObstaclePosition;
    private Transform childTransform;
    private Transform parentTransform;
    private Transform current;
    private int[] numbers = { -4, -2, 0, 2, 4 };
    public float speedOfSpawn;
    public static SpawnObstacles instance;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        parentTransform = GetComponent<Transform>();
        current = this.transform;
        StartCoroutine(GenerateObstacles());
    }

    // Update is called once per frame
    void Update()
    {
    }
    IEnumerator GenerateObstacles()
    {
        yield return new WaitForSeconds(speedOfSpawn);

        //OVAKO MOGU DA KONTROLISEM KAD DA SPAWNUJE 
        
            sectionNumber = UnityEngine.Random.Range(0, 3);
            System.Random random = new System.Random();
            int index = random.Next(numbers.Length);
            xObstaclePosition = numbers[index];
            Instantiate(obstacles[sectionNumber], new Vector3(xObstaclePosition,
               1f, transform.position.z), Quaternion.identity);
            StartCoroutine(GenerateObstacles());
        
       

    }
}
