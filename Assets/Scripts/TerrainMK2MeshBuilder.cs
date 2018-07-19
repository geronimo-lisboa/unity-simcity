using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//https://catlikecoding.com/unity/tutorials/procedural-grid/
[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
public class TerrainMK2MeshBuilder : MonoBehaviour {
    private TerrainMK2 terrain;
    private Mesh mesh;
    private Vector3[] vertices;

    public TerrainMK2 MyTerrain {
        get {
            return terrain;
        }
        set {
            terrain = value;
            GenerateIfNotNull();
        }
    }

    private void GenerateIfNotNull()
    {   
        //TODO: voltar a usar o terrain
        //if (terrain != null)
        if(true)
        {
            GenerateMesh();
        }
    }

    private void GenerateMesh()
    {
        vertices = new Vector3[( (debugXSize + 1) * (debugYSize + 1) )];
        for (int i = 0, y = 0; y <= debugYSize; y++)
        {
            for (int x = 0; x <= debugXSize; x++, i++)
            {
                vertices[i] = new Vector3(x, y);
            }
        }

    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawCube(new Vector3(0, 0, 0), new Vector3(10, 10, 10));
        if(vertices==null)
        {
            return;
        }
        Gizmos.color = Color.black;
        for(int i=0; i<vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], debugGizmoRadius);
        }
    }

    public TerrainMK2MeshBuilder()
    {
        MyTerrain = null;
    }

    void Start () {
        GenerateIfNotNull();
    }

    public void Awake()
    {
        Debug.Log("TerrainMK2MeshBuilder.Awake()");
        GenerateIfNotNull();
    }

    void Update ()
    {
        Debug.Log("TerrainMK2MeshBuilder.Update()");
        GenerateIfNotNull();
    }

    public int debugXSize;
    public int debugYSize;
    public float debugGizmoRadius;
}
