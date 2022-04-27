using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacles : MonoBehaviour
{
    public float speed = 4f;
      
    private void FixedUpdate()
    {
        transform.Translate(Vector3.back * Time.deltaTime * speed, Space.World);
    }
}
