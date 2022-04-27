using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBarierFromRightToLeft : MonoBehaviour
{
    public static MoveBarierFromRightToLeft instance;
    private Rigidbody rb;
    [SerializeField] private Vector3 boxColliderOffset;
    private float speed = 8f;
    private bool isMoving = false;
    public Transform startingPosition;
    Transform starPos;
    private BoxCollider boxCollider;


    private void Awake()
    {
        instance = this;
        starPos = ObjectPooler.instance.randomCube.transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
    }
    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            if (gameObject.tag == "LeftSideBarierCube")
            {
                if (transform.position.x >= -5.5f)
                {
                    gameObject.GetComponent<Renderer>().material.color = Color.red;
                    transform.Translate(Vector3.right * speed * Time.deltaTime);
                    if (transform.position.x > 5f)
                    {
                        Vector3 test = new Vector3(-5.5f, 0.5f, starPos.transform.position.z);
                        transform.position = test;
                        isMoving = false;
                        Destroy(rb);
                        Destroy(this);
                        Destroy(boxCollider);
                        gameObject.tag = "NoneTag";
                    }
                }
            }
            else if (gameObject.tag == "RightSideBarierCube")
            {
                if (transform.position.x <= 5.5f)
                {
                    gameObject.GetComponent<Renderer>().material.color = Color.red;

                    transform.Translate(Vector3.left * speed * Time.deltaTime);
                    if (transform.position.x < -5f)
                    {
                        Vector3 test = new Vector3(5.5f, 0.5f, starPos.transform.position.z);
                        transform.position = test;
                        isMoving = false;
                        Destroy(rb);
                        Destroy(this);
                        Destroy(boxCollider);
                        gameObject.tag = "NoneTag";

                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isMoving = true;
        }
    }

}
