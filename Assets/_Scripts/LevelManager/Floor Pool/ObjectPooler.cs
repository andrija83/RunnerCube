using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler instance;
    //da mozemo da editujemo klasu u unity editor
    [System.Serializable]
    public class Pool
    {
        public string type;
        public GameObject prefab;
        public int size;
    }
    //Za floor POOL
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    GameObject objectToSpawn;
    public ObjectSpawner spawner;


    //ZA OBSTACLES POOL
    public List<Pool> obstaclePool;
    public Dictionary<string, Queue<GameObject>> obstacleDictionary;
    GameObject obstacleToSpawn;
    public ObjectSpawner obstacleSpawner;
    private GameObject obj1;


    //Test Za random NumberCube
    private GameObject obj;
    private int cubeNumber;
    private Transform[] objects;
    private Transform[] cubesInSides;
    private List<Transform> cubesList;
    public Transform randomCube { get; set; }
    private GameObject test;
    private Vector3 sizeOfBoxCollider = new Vector3(13, 1, 10);
    private Vector3 boxColliderCenter = new Vector3(-6, 0, -15);
    private Vector3 boxColliderCenterRight = new Vector3(6, 0, -20); //-10 da regulisem z osu u +
    private Vector3 sizeOfBoxColliderRight = new Vector3(13, 1, 8);
    public Vector3 startingPosition; //Starting position of the cube
    private Transform cubeWithStartingPosition;
    GameObject childGO;

    //TEST ZA VISE CUBA DA UZMEM
    private Transform[] transformOfObjectTSpawn;
    private List<Transform> multlyCubesList;
    List<Transform> randomNumberList = new List<Transform>();
    public Vector3 startingPosition1; //Starting position of the cube

    private void Awake()
    {
        instance = this;

    }

    // Start is called before the first frame update
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        obstacleDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.type, objectPool);

        }
        foreach (Pool pool in obstaclePool)
        {
            Queue<GameObject> obstacleQueue = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject objectObstacle = Instantiate(pool.prefab);
                objectObstacle.SetActive(false);
                obstacleQueue.Enqueue(objectObstacle);
                //obstacleQueue.DequeueChunk(10);
            }
            obstacleDictionary.Add(pool.type, obstacleQueue);

        }

    }




    //FLOOR POOL
    public GameObject SpawnFromPool(string type, Vector3 position, Quaternion rotation)
    {
        //Debug.Log(position);
        if (!poolDictionary.ContainsKey(type))
        {
            Debug.LogWarning("Pool with type " + type + " doesn't exist");
            return null;
        }


        objectToSpawn = poolDictionary[type].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        poolDictionary[type].Enqueue(objectToSpawn);
        ObjectSpawner.instance.groundSpawnDistance += 500f;

        obj = objectToSpawn;
        //GetRandomCubeFromDictionary(obj);
        return objectToSpawn;
    }
    //OBSTALCE POOL
    public GameObject SpawnFromPoolObstacles(string type, Vector3 position, Quaternion rotation)
    {
        if (!obstacleDictionary.ContainsKey(type))
        {
            Debug.LogWarning("Pool with type " + type + " doesn't exist");
            return null;
        }
        obstacleToSpawn = obstacleDictionary[type].Dequeue();
        obstacleToSpawn.SetActive(true);
        obstacleToSpawn.transform.position = position;
        obstacleToSpawn.transform.rotation = rotation;
        obstacleDictionary[type].Enqueue(obstacleToSpawn);

        return obstacleToSpawn;
    }



    public GameObject GetRandomCubeFromDictionary(GameObject type)
    {
        objects = type.GetComponentsInChildren<Transform>();
        cubesList = new List<Transform>();
        foreach (var item in objects)
        {
            if (item.tag == "CubeSides")
            {
                cubesList.Add(item);
            }
        }
        //TODO DA NAPRAVIM DA UZIMA VISE CUBA 
        cubeNumber = UnityEngine.Random.Range(10, cubesList.Count);
        cubeWithStartingPosition = cubesList[0];
        randomCube = cubesList[cubeNumber];
        randomCube.gameObject.AddComponent<Rigidbody>();
        randomCube.gameObject.AddComponent<BoxCollider>();
        randomCube.gameObject.GetComponent<BoxCollider>().isTrigger = true;
        randomCube.gameObject.AddComponent<MoveBarierFromRightToLeft>();
        randomCube.gameObject.GetComponent<Rigidbody>().useGravity = false;
        randomCube.gameObject.GetComponent<Rigidbody>().freezeRotation = true;
        if (randomCube.transform.position.x == 5.5)
        {
            randomCube.gameObject.GetComponent<BoxCollider>().size = sizeOfBoxCollider;
            randomCube.gameObject.GetComponent<BoxCollider>().center = boxColliderCenter;
            randomCube.gameObject.tag = "RightSideBarierCube";
        }
        else if (randomCube.transform.position.x == -5.5)
        {
            randomCube.gameObject.GetComponent<BoxCollider>().size = sizeOfBoxColliderRight;
            randomCube.gameObject.GetComponent<BoxCollider>().center = boxColliderCenterRight;
            randomCube.gameObject.tag = "LeftSideBarierCube";

        }
        startingPosition = randomCube.position;

        return null;
    }
    public GameObject GetRandomCubesFromDictionary(GameObject type)
    {
        transformOfObjectTSpawn = type.GetComponentsInChildren<Transform>();
        multlyCubesList = new List<Transform>();
        foreach (var item in transformOfObjectTSpawn)
        {
            if (item.tag == "CubeSides")
            {
                multlyCubesList.Add(item);
            }
        }
        int[] randomNumber = new int[10];
        for (int i = 0; i < randomNumber.Length; i++)
        {
            randomNumber[i] = UnityEngine.Random.Range(10, multlyCubesList.Count);
        }
        randomNumberList = GetRandomElements(multlyCubesList, 10);
        foreach (var item in randomNumberList)
        {
            item.gameObject.AddComponent<Rigidbody>();
            item.gameObject.AddComponent<BoxCollider>();
            item.gameObject.GetComponent<BoxCollider>().isTrigger = true;
            item.gameObject.AddComponent<MoveBarierFromRightToLeft>();
            item.gameObject.GetComponent<Rigidbody>().useGravity = false;
            item.gameObject.GetComponent<Rigidbody>().freezeRotation = true;

            if (item.transform.position.x == 5.5)
            {
                item.gameObject.GetComponent<BoxCollider>().size = sizeOfBoxCollider;
                item.gameObject.GetComponent<BoxCollider>().center = boxColliderCenter;
                randomCube.gameObject.tag = "RightSideBarierCube";
            }
            else if (item.transform.position.x == -5.5)
            {
                item.gameObject.GetComponent<BoxCollider>().size = sizeOfBoxColliderRight;
                item.gameObject.GetComponent<BoxCollider>().center = boxColliderCenterRight;
                item.gameObject.tag = "LeftSideBarierCube";

            }
            startingPosition = item.position;
        }

        return null;
    }

    public static List<t> GetRandomElements<t>(IEnumerable<t> list, int elementsCount)
    {
        return list.OrderBy(x => Guid.NewGuid()).Take(elementsCount).ToList();
    }



}
