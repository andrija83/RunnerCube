using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public static BlackHole instance;

    [SerializeField] public float GRAVITY_PULL = .78f;
    public static float m_GravityRadius = 1f;
    public GameObject player;
    public float speedOfBlackHole = 25f;
    private Vector3 blackHolePosition;
    private const float gravitationalConstant = 6.672e-11f;
    public List<GameObject> itemsInBlackHole = new List<GameObject>();
    public List<GameObject> itemsToSpawn = new List<GameObject>();
    private List<GameController> blackHoleScaling = new List<GameController>();

    public float speedOfPull;
    public float rightSpeed = 50f;
    public float distanceFromHole = 5f;
    private int[] numbers = { -4, -2, 0, 2, 4 };
    private int[] numbers1 = { 2, 4, 6, 8 };
    public bool phase1 = true;
    public bool phase2;
    public bool phase3;
    private Vector3 trownCubePosition;
    private GameObject[] rotatingCubes;

    //ZA KONTROLISANJE VREMENA 
    [SerializeField] private float phaseZero;
    [SerializeField] private float phaseOne;
    [SerializeField] private float phaseTwo;
    [SerializeField] private float phaseThree;

    public bool isSuckingOBstackles;
    public bool isPukingOBstacles;

    public float multiplicationFactor = 1f;


    /*
     * KADA DA SPAWNUJEM BOSSA KORDINATE
     * SVE SU Z OSE
     * GROUNDSPAWNDISTANCE = 3600
     * PLAYER TRANSFORM  = 3350
     * BLACKHOLE = 2720(Z) (Y) = 23
     */



    void Awake()
    {
        instance = this;
        m_GravityRadius = GetComponent<SphereCollider>().radius;
        player = GameObject.FindGameObjectWithTag("Player");


    }
    private void Start()
    {
        StartCoroutine(GenerateObstacles());


    }
    private void Update()
    {
        blackHolePosition = transform.position;
        transform.Translate(0f, 0f, speedOfBlackHole * Time.deltaTime);
        if (isSuckingOBstackles)
        {
            speedOfPull += 1.2f * Time.deltaTime;

        }
        if (isPukingOBstacles)
        {

            speedOfPull -= 0.5f * Time.deltaTime;
        }
    }




    //OVO RADI PRIVLACI OBJEKTE UNUTAR RUPE 
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle") && !other.gameObject.CompareTag("Player"))
        {
            other.transform.position = Vector3.MoveTowards(other.transform.position, this.transform.position, speedOfPull * Time.deltaTime);
            if (other.transform.position.x == this.transform.position.x)
            {
                Destroy(other.gameObject);
            }
        }
    }

    IEnumerator DespawnAndSpawnCube()
    {

        GameObject randomInSpawn = GetRandomCube(itemsToSpawn);
        GameObject randomInRotate = GetRandomCube(itemsInBlackHole);
        randomInRotate.SetActive(false);
        for (int i = 0; i < itemsToSpawn.Count; i++)
        {
            System.Random random = new System.Random();
            int randomX = numbers[random.Next(numbers.Length)];
            Vector3 newPosition = new Vector3(randomX, 0.5f, player.transform.position.z + 10f);
            Instantiate(itemsToSpawn[i], newPosition, Quaternion.identity);
            yield return new WaitForSeconds(2f);
        }



    }

    public GameObject GetRandomCube(List<GameObject> list)
    {
        System.Random random = new System.Random();
        GameObject randomCube = list[random.Next(list.Count)];
        return randomCube;
    }



    //DA PREBACIM U NEKOM TRENUTNKU SVE U POOL I STATE MACHINE

    IEnumerator GenerateObstacles()
    {
        isSuckingOBstackles = true;

        //OVDE DA ISKONTROLISEM POCETNU FAZU KAD POVECAVAM SPEED OF PULL I SpawnObstacles.instance.speedOfSpawn
        //DA POCNE DA POVECAVA SPEEDOFPULL I SPAWNOBSTACLE DA SMANJUJE I NA KRAJ DA VRATI NA 0.5f;
        yield return new WaitForSeconds(phaseZero);

        //TODO FAZA 1 SPONUJEM BOSSA I POCINJEM DA PRIVLACIM KOCKE ** DA NAPRAVIM STO VISE PREPREKA PRIVUCE DA 
        // SE CRNA RUPA POVECAVA 
        if (phase1)
        {
            yield return new WaitForSeconds(10f);//OVDE CEKAM RECIMO 30 SEC PRE NEGO STO UDJE U PRVU FAZU DA KAO SKUPI PREPREKE

            SpawnObstacles.instance.speedOfSpawn = 2f;
            speedOfBlackHole = 0;
            player.gameObject.GetComponent<PlayerMovement>().speed = 0;


            Vector3 positionOfCube = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            for (int i = 0; i < itemsInBlackHole.Count; i++)
            {
                System.Random random = new System.Random();
                int randomY = numbers[random.Next(numbers.Length)];
                int randomX = numbers1[random.Next(numbers1.Length)];
                Vector3 movetoCube = new Vector3(positionOfCube.x + Random.Range(10, 16), positionOfCube.y + Random.Range(-6, 6), positionOfCube.z);
                itemsInBlackHole[i].SetActive(true);

                Instantiate(itemsInBlackHole[i], movetoCube, Quaternion.identity);
                yield return new WaitForSeconds(0.5f);
                //Debug.Log( i);
            }
            phase1 = false;
            phase2 = true;
            isSuckingOBstackles = false;

            // da promenim u true 
            //TODO FAZA 2 POCINJEM DA BACAM PREPREKE ISPRED IGRACA ** DA NAPRAVIM KAD SE rotatingcubes SMANJUJE DA SMANJUJE I SCALE CRNE RUPE 
            //TO CE TAMAN I DA BUDE END BOSS // MOGU DA NAPRAVIM CUSTOM DEO MAPE KAKO DA BACA KOCKE ISPRED PLAYERA 
            if (phase2)
            {

                speedOfBlackHole = 0;
                player.gameObject.GetComponent<PlayerMovement>().speed = 0;
                rotatingCubes = GameObject.FindGameObjectsWithTag("RotatingCubes");
                yield return new WaitForSeconds(0.1f);
                Debug.Log("FAZA 2");
                for (int i = 0; i < rotatingCubes.Length; i++)
                {
                    System.Random random = new System.Random();
                    int randomX = numbers[random.Next(numbers.Length)];
                    Vector3 newPosition = new Vector3(randomX, player.transform.position.y, player.transform.position.z + 30f);
                    //rotatingCubes[i].SetActive(false);

                    //InvokeRepeating("IncreaseDissolveFloat", 0.1f, 0.1f);


                    //rotatingCubes[i].GetComponent<ControlCube>().StartInvoke();
                    

                    //Destroy(rotatingCubes[i]);
                    //PRE NEGO STO PREBACI POZICIJU DA NAPRAVIM NEKU ANIMACIJU DA UZMEM POZICIJU ROTATINGCUBE I ODATLE DA NAPRAVIM ANIMACIJU 
                    //DO NEW POSITION POZICIJE\
                    //TODO ADD FOCRCE NA KOCKU PA DA VIDIM GDE SLECE .. 
                    //Instantiate(itemsToSpawn[i], newPosition, Quaternion.identity);

                    //itemsToSpawn[i].SetActive(true);
                    yield return new WaitForSeconds(2f);//OVDE MENJAM VREME KADA DA SE DESPAWNUJKE KOCKA U ROTATINCUBES
                }
                phase2 = false;
                phase3 = true;
                isSuckingOBstackles = false;
                speedOfPull = 60f;
            }
            //TODO ZADNJA FAZA NESTO DA SMISLIM 
            if (phase3)
            {
                Debug.Log("FAZA 3");
                foreach (var item in rotatingCubes)
                {
                    item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                    item.GetComponent<RotateAround>().enabled = false;
                }
                speedOfPull = 0;
                //TODO TREBA DA ZAUSTAVIM SPAWNOBSTACLES
                foreach (var item in rotatingCubes)
                {
                    yield return new WaitForSeconds(5f);


                    //item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    //item.GetComponent<Rigidbody>().isKinematic = true;
                    //item.GetComponent<Rigidbody>().AddForce((player.transform.position - item.transform.position).normalized * 500f * Time.smoothDeltaTime);
                }
                


                //for (int i = 0; i < rotatingCubes.Length; i++)
                //{

                //rotatingCubes[i].GetComponent<ControlCube>().StartInvoke();

                //    yield return new WaitForSeconds(2f);
                //}
                //SpawnObstacles.instance.speedOfSpawn = 0.5f;
                //yield return new WaitForSeconds(2f);
                //isPukingOBstacles = true;
            }

        }
    }

    // IEnumerator TakeRandomCubeFromList()
    //{


    //    System.Random random = new System.Random();
    //    GameObject randomCubeInBlackHole = itemsInBlackHole[random.Next(itemsInBlackHole.Count)];
    //    itemsInBlackHole.Remove(randomCubeInBlackHole);
    //    randomCubeInBlackHole.SetActive(false);
    //    //RotateAround test = randomCubeInBlackHole.GetComponent<RotateAround>();
    //    //test.enabled = false;
    //    Instantiate(randomCubeInBlackHole);
    //    yield return new WaitForSeconds(10f);
    //    Debug.Log(randomCubeInBlackHole);

    //}
    //void OnTriggerStay(Collider other)
    //{
    //    if (other.attachedRigidbody && !other.gameObject.CompareTag("Player"))
    //    {
    //        //other.gameObject.transform.position = Vector3.MoveTowards(other.gameObject.transform.position, blackHolePosition, speedOfPull * Time.deltaTime);
    //        //other.gameObject.GetComponent<Rigidbody>().useGravity = false;
    //        float gravityIntensity = Vector3.Distance(transform.position, other.transform.position) / m_GravityRadius;
    //        other.attachedRigidbody.AddForce((transform.position - other.transform.position)
    //            * gravityIntensity * other.attachedRigidbody.mass * GRAVITY_PULL * Time.smoothDeltaTime);
    //        other.attachedRigidbody.AddForce(Vector3.right * rightSpeed);
    //        itemsInBlackHole.Add(other.gameObject);

    //        Debug.DrawRay(other.transform.position, transform.position - other.transform.position);
    //    }
    //}

    public IEnumerator ScaleHole()
    {
        Vector3 StartScale = transform.localScale;
        Vector3 EndScale = StartScale * 2;
        float t = 0;
        while (t <= 0.4f)
        {
            t += Time.deltaTime;
            transform.localScale = Vector3.Lerp(StartScale, EndScale, t);
            yield return null;
        }
    }
    public static Vector3 GAcceleration(Vector3 position, float mass, Rigidbody r)
    {
        Vector3 direction = position - r.position;

        float gravityForce = gravitationalConstant * ((mass * r.mass) / direction.sqrMagnitude);
        gravityForce /= r.mass;

        return direction.normalized * gravityForce * Time.fixedDeltaTime;
    }

}
