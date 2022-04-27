using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    //[System.Serializable]
    //public struct Spawnable
    //{
    //    public string type;
    //    public float weight; // za multiple type of obstacles i sansa koje da se spawnuje 

    //}
    //[System.Serializable]
    //public struct SpawnSettings
    //{
    //    public string type;
    //    public float minWait;
    //    public float maxWait;
    //    public int maxObjects;
    //}
    //private float totalWeight;
    //public List<Spawnable> enemySpawnables = new List<Spawnable>();
    //public List<SpawnSettings> spawnSettings = new List<SpawnSettings>();   



    private bool spawningObject = false;
    public static ObjectSpawner instance;
    ObjectPooler pooler;

    public float groundSpawnDistance = 500f;


    private void Awake()
    {
        instance = this;
        //totalWeight = 0;
        //foreach (var item in enemySpawnables)
        //{
        //    totalWeight += item.weight;
        //}
    }


    // Start is called before the first frame update
    void Start()
    {
        pooler = GetComponent<ObjectPooler>();

    }
    private void Update()
    {
        //if (!spawningObject && GameController.EnemyCount < spawnSettings[0].maxObjects)
        //{
        //    spawningObject = true;
        //    float pick = Random.value * totalWeight;
        //    int chosenIndex = 0;
        //    float cumulativeWeight = enemySpawnables[0].weight;
        //    while (pick > cumulativeWeight && chosenIndex < enemySpawnables.Count -1)
        //    {
        //        chosenIndex++;
        //        cumulativeWeight += enemySpawnables[chosenIndex].weight;
        //    }
        //    StartCoroutine(SpawnObstacles(enemySpawnables[chosenIndex].type, Random.Range(spawnSettings[0].minWait / GameController.DifficultyMultyplier, spawnSettings[0].maxWait / GameController.DifficultyMultyplier)));
        //}
    }
    public void SpawnGround()
    {
        //Debug.Log(groundSpawnDistance);
        ObjectPooler.instance.SpawnFromPool("ground", new Vector3(0, 0, groundSpawnDistance), Quaternion.identity);  //MENJAM OVDE Z VEKTOR U ZAVISNOSTI KOLIKI MI JE GROUND

        //pooler.SpawnFromPool("ground", new Vector3(0, 0, groundSpawnDistance), Quaternion.identity);  //MENJAM OVDE Z VEKTOR U ZAVISNOSTI KOLIKI MI JE GROUND


    }
    //private IEnumerator SpawnObstacles(string type,float time) {
    
    //yield return new WaitForSeconds(time);
    //    ObjectPooler.instance.SpawnFromPool(type, new Vector3(Random.Range(-5f, 5f), 0.5f, 40f), Quaternion.identity);
    //    spawningObject = false;
    //    GameController.EnemyCount++;
    //}
    //public void SpawnObstacle()
    //{

    //    //ObjectPooler.instance.SpawnFromPoolObstacles("obstacle", new Vector3(0, 0, groundSpawnDistance), Quaternion.identity);
    //    //ObjectPooler.instance.SpawnFromPoolObstacles("obstacle",new Vector3(2,0, groundSpawnDistance), Quaternion.identity);
    //}
}
