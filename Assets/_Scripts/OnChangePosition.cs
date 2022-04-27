using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnChangePosition : MonoBehaviour
{
    public PolygonCollider2D holeCollider;
    public PolygonCollider2D ground2dCollider;
    public MeshCollider generatedMeshCollider;
    public Collider GroundCollider;
    Mesh GeneratedMesh;
    public float initialscale = 0.5f;

    private void Start()
    {
        GameObject[] AllGos = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach (GameObject Gos in AllGos)
        {
            if (Gos.layer == LayerMask.NameToLayer("Obstacles"))
            {
                Physics.IgnoreCollision(Gos.GetComponent<Collider>(), generatedMeshCollider, true);
            }
        }
    }
    private void FixedUpdate()
    {
        if (transform.hasChanged == true)
        {
            transform.hasChanged = false;
            holeCollider.transform.position = new Vector2(transform.position.x, transform.position.z);
            holeCollider.transform.localScale = transform.localScale * initialscale;
            MakeHole2D();
            Make3dMeshCollider();
        }
    }
    private void MakeHole2D()
    {
        Vector2[] PointPositions = holeCollider.GetPath(0);
        for (int i = 0; i < PointPositions.Length; i++)
        {
            PointPositions[i] = holeCollider.transform.TransformPoint(PointPositions[i]);
                //(Vector2)holeCollider.transform.position;

        }

        ground2dCollider.pathCount = 2;
        ground2dCollider.SetPath(1,PointPositions);
    }
    private void Make3dMeshCollider()
    {
        if (GeneratedMesh != null)
        {
            Destroy(GeneratedMesh);
        }
        GeneratedMesh = ground2dCollider.CreateMesh(true, true);
        generatedMeshCollider.sharedMesh = GeneratedMesh;
    }
    private void OnTriggerEnter(Collider other)
    {
        Physics.IgnoreCollision(other,GroundCollider,true);
        Physics.IgnoreCollision(other, generatedMeshCollider, false);


    }
    private void OnTriggerExit(Collider other)
    {
        Physics.IgnoreCollision(other, GroundCollider, false);
        Physics.IgnoreCollision(other, generatedMeshCollider, true);
    }
}
