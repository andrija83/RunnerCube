using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastLaser : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform laserOrigin;
    LineRenderer laserLine;
    Vector3 test;

    private void Awake()
    {
        laserLine = GetComponent<LineRenderer>();
        test = new Vector3(0, 0, 0);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        laserLine.SetPosition(0, test);
        laserLine.enabled = true;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider)
            {
                laserLine.SetPosition(1, hit.point);
                Debug.Log("LASER NASO TARGET");

            }
        }
        else
        {
            laserLine.SetPosition(1, transform.forward * 5000);
            //laserLine.enabled = false;
        }

    }
}
