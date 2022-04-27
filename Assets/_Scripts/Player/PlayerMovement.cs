using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody rb;
    private Renderer renderer;
    private Material material;
    private RaycastHit hit2;
    public LayerMask groundMask;

    //Scale Variables for Shrinking
    private Vector3 scaleChange;

    //MOVING VARIABLES
    public float speed = 5f;//speed of the player
    private int laneShift;
    [SerializeField] private float laneShiftSpeed = 5f;

    //Grounded
    public bool isGrounded;

    //Jumping
    public float jumpForce = 10f;
    public bool isJumping;

    //RayCast
    [SerializeField] private float raycastRadius = 0.55f;

    //gamchildSPawnPoint
    private GameObject spawnPoint;


    private bool isDead;


    //Disasolve
    private float heightOfDissolve = 0;
    public bool isDissolve = false;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        renderer = GetComponent<Renderer>();
        material = GetComponent<Material>();
        spawnPoint = GameObject.FindGameObjectWithTag("SpawningPoint");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (laneShift > -2.45)
            {

                laneShift -= 2;
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (laneShift < 2.45)
            {
                laneShift += 2;
            }
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            scaleChange = transform.localScale;
            scaleChange.y -= 0.05f;
            transform.localScale = scaleChange;
            if (transform.localScale.y <= 0.5)
            {
                scaleChange.y = 0.3f;
                transform.localScale = scaleChange;
            }
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            scaleChange = transform.localScale;
            scaleChange.y += 1f;
            transform.localScale = scaleChange;
            isGrounded = true;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isJumping = true;
        }
        Vector3 pos = rb.position;
        pos.x = Mathf.Lerp(pos.x, laneShift, laneShiftSpeed * Time.deltaTime);
        rb.position = pos;


        if (isDead == true)
        {
            //transform.Translate(0, 0, 0);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            InvokeRepeating("IncreaseDissolveFloat", 0.1f, 0.1f);
        }

    }


    private void FixedUpdate()
    {
        if (!isDead)
        {
            GroundCheck();
            Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + forwardMove);
        }

        //Debug.Log(isJumping);
        if (isJumping)
        {
            Jumping();
            isJumping = false;
        }


    }

    //TODO DA POPRAVIM JUMP DA SMANJIM FORCE DA DODAM GRAVITY DA RADI NA OBJEKAT I FIZLIKA ??
    void Jumping()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }


    public void GroundCheck()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out hit2, raycastRadius,
                groundMask))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Test"))
        {
            isDead = true;
            isDissolve = true;
            Debug.Log("DEAD");
        }
    }


    void IncreaseDissolveFloat()
    {
        if (isDead)
        {
            heightOfDissolve += 0.005f;
            if (heightOfDissolve != 1)
            {
                SetHeightOnDissasolveShader(heightOfDissolve);

            }
        }
    }
    private void SetHeightOnDissasolveShader(float height)
    {
        renderer.materials[0].SetFloat("_Dissolve", height);
        //renderer.materials[0].SetFloat("_NoiseStrength", noiseStrength);
    }
}
