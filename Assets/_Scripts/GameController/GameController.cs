using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static float distance = 0;
    private static float difficultyMultyplier = 1;
    private static float difficultyOffset = 100f;
    private static int enemyCount = 0;

    public static float Distance { get => distance; set => distance = value; }
    public static float DifficultyMultyplier { get => difficultyMultyplier; set => difficultyMultyplier = value; }
    public static float DifficultyOffset { get => difficultyOffset; set => difficultyOffset = value; }
    public static int EnemyCount { get => enemyCount; set => enemyCount = value; }


    //BOSS CONTROL
    private GameObject blackHole;
    public bool phaseOne;
    public bool phaseTwo;
    public bool phaseThree;



    public static GameController instance;
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Object[] go = GameObject.FindObjectsOfType(typeof(MonoBehaviour));
        blackHole = GameObject.FindGameObjectWithTag("Test");


    }

    // Update is called once per frame
    void Update()
    {
        difficultyMultyplier = 1 + (distance / difficultyOffset); //svakih 100 metra difficutly za + 1
        //OVDE KONTROLISEM KADA CU DA SPAWNUJEM BOSSA
        if (ObjectSpawner.instance.groundSpawnDistance == 1100)
        {
            blackHole.transform.GetChild(0).gameObject.SetActive(true);

        }
    }
}
