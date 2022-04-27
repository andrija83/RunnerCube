using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCubeToSpawn : MonoBehaviour
{
    private GameObject blackHole;
    private BlackHole blackHoleScript;
    private Renderer renderer;
    public ParticleSystem cirlceSpawn;

    private float heightOfDissolve = 0;
    public bool isDissolve = false;
    private bool isDissolveInvert = true;

    // Start is called before the first frame update

    void Start()
    {
        renderer = GetComponent<Renderer>();
        blackHole = GameObject.FindGameObjectWithTag("GroundHole");
        blackHoleScript = blackHole.GetComponent<BlackHole>();
    }

    private void Awake()
    {
    }
    // Update is called once per frame
    void Update()
    {
        //if (isDissolveInvert)
        //{
        //    StartInvokeDecrease();
        //}

    }
   
    
    public void StartInvokeDecrease()
    {
        isDissolveInvert = true;
        InvokeRepeating("DecreaseDissolveFloat", 0.1f, 0.1f);
    }
 
    public void DecreaseDissolveFloat()
    {
        heightOfDissolve -= 0.2f;
        if (heightOfDissolve != 0 && heightOfDissolve < 0)
        {
            SetHeightOnDissasolveShader(heightOfDissolve);
        }
    }
    private void SetHeightOnDissasolveShader(float height)
    {
        renderer.material.SetFloat("_Dissolve", height);
        //renderer.materials[0].SetFloat("_NoiseStrength", noiseStrength);
    }
   
}
