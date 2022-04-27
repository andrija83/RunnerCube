using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCube : MonoBehaviour
{
    private GameObject blackHole;
    private BlackHole blackHoleScript;
    private Renderer renderer;
    private Renderer renderer1;

    private float heightOfDissolve = 0;
    public bool isDissolve = false;
    private bool isDissolveInvert;

    // Start is called before the first frame update

    void Start()
    {
        renderer = GetComponent<Renderer>();
        blackHole = GameObject.FindGameObjectWithTag("GroundHole");
        blackHoleScript = blackHole.GetComponent<BlackHole>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isDissolve)
        {
            StartInvoke();
        }
        //if (isDissolveInvert)
        //{
        //    StartInvokeDecrease();
        //}
    }
    public void StartInvoke()
    {
        isDissolve = true;
        InvokeRepeating("IncreaseDissolveFloat", 0.1f, 0.1f);

    }
    public void StartInvokeDecrease()
    {
        isDissolveInvert = true;
        InvokeRepeating("DecreaseDissolveFloat", 0.1f, 0.1f);
    }
    public void IncreaseDissolveFloat()
    {

        heightOfDissolve += 0.005f;
        if (heightOfDissolve != 1)
        {
            SetHeightOnDissasolveShader(heightOfDissolve);
        }

    }
    public void DecreaseDissolveFloat()
    {
        renderer1 = GetComponent<Renderer>();
        heightOfDissolve -= 0.1f;
        if (heightOfDissolve != 0)
        {
            SetHeightOnDissasolveShader1(heightOfDissolve);
        }
    }
    private void SetHeightOnDissasolveShader(float height)
    {
        renderer.material.SetFloat("_Dissolve", height);
        //renderer.materials[0].SetFloat("_NoiseStrength", noiseStrength);
    }
    private void SetHeightOnDissasolveShader1(float height)
    {
        renderer1.sharedMaterial.SetFloat("_Dissolve", height);
        //renderer.materials[0].SetFloat("_NoiseStrength", noiseStrength);
    }
}
