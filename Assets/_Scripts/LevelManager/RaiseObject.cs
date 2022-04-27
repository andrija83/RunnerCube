using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiseObject : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 startPosition;
    private Vector3 newPosition = new Vector3();
    public GameObject cube;
    public bool entered;
    [SerializeField]private float rotationSpeed = 20f;
    [SerializeField]private float raisingSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        startPosition = cube.transform.position;
        newPosition = startPosition;
        newPosition.y += 3f;
    }
    // Update is called once per frame
    void Update()
    {
        
            RaiseObjects();
            RotateObject();
        
        //TODO Da napravim laser da se isto rotira sa cube


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            entered = true;
        }
    }
    private void RaiseObjects()
    {
        cube.transform.position = Vector3.MoveTowards(cube.transform.position, newPosition, Time.deltaTime * raisingSpeed);
    }
    public void RotateObject()
    {
        cube.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
