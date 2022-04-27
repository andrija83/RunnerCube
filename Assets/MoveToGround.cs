using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToGround : MonoBehaviour
{

    private GameObject player;
    private int[] lanes = { -4,-2,0,2,4 };



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        System.Random random = new System.Random();
        int randomX = lanes[random.Next(lanes.Length)];

        //TODO DA NAPRAVIM NEKU ANIMACIJU GDE CE DA SE STVORI OBJEKAT 
        transform.position = new Vector3(randomX, 0.5f, player.transform.position.x + 5f);
    }

    // Update is called once per frame
    void Update()
    {
        

    }
}
