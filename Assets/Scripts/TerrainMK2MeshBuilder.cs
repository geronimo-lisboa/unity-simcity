using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//https://catlikecoding.com/unity/tutorials/procedural-grid/
[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
//TODO: Pivot ficar no centro
//TODO: Voltar a usar o terrain
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

        }
    }
    
    private void GenerateMesh()
    {
        if(terrain==null)
            return;
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Grid";
        float putInCenterX = terrain.X / 2;
        float putInCenterY = terrain.Y / 2;
        vertices = new Vector3[(terrain.X + 1) * (terrain.Y + 1)];
        for (int i = 0, y = 0; y <= terrain.Y; y++)
        {
            for (int x = 0; x <= terrain.X; x++, i++)
            {
                vertices[i] = new Vector3(x - putInCenterX, 0, y - putInCenterY);
            }
        }
        mesh.vertices = vertices;

        int[] triangles = new int[terrain.X * terrain.Y * 6];
        for (int ti = 0, vi = 0, y = 0; y < terrain.Y; y++, vi++)
        {
            for (int x = 0; x < terrain.X; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + terrain.X + 1;
                triangles[ti + 5] = vi + terrain.X + 2;
            }
        }
        mesh.triangles = triangles;
    }

    private void OnDrawGizmos()
    {
        if(vertices==null)
        {
            return;
        }
        Gizmos.color = Color.black;
        
        for(int i=0; i<vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i]+ GetComponent<Transform>().position, debugGizmoRadius);
        }
    }

    public TerrainMK2MeshBuilder()
    {
        MyTerrain = null;
    }

    void Start () {
        GenerateMesh();
    }

    protected virtual void Awake()
    {
        Debug.Log("TerrainMK2MeshBuilder.Awake()");
        GenerateMesh();
    }

    void Update ()
    {
        GenerateMesh();
    }


    public int debugXSize;
    public int debugYSize;
    public float debugGizmoRadius;
}
