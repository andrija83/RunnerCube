using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    private GameObject sphere;
    private Transform target;
    public float orbitDistance = 1f;
    public float orbitDegreesPerSec = 45f;
    public Vector3 relativeDistance = Vector3.zero;
    public bool canOrbit = true;
    public bool once = true;
    // Start is called before the first frame update
    void Start()
    {
        sphere = GameObject.FindGameObjectWithTag("GroundHole");
        target = sphere.transform;
        if (target != null)
        {
            relativeDistance = transform.position - target.position;
        }
    }
    void FixedUpdate()
    {

        Orbit();

    }
    // Update is called once per frame
    void Update()
    {
        //transform.RotateAround(target.transform.position, Vector3.up, 20 * Time.deltaTime);
        
    }


    void Orbit()
    {
       
            if (target != null)
            {
                // Keep us at the last known relative position
                transform.position = (target.position + relativeDistance);
                transform.RotateAround(target.position, Vector3.up, orbitDegreesPerSec * Time.deltaTime);
                // Reset relative position after rotate
                if (once)
                {
                    transform.position *= orbitDistance;
                    once = false;
                }
                relativeDistance = transform.position - target.position;
                //relativeDistance.x += 0.1f;
            }
        
    }
}
