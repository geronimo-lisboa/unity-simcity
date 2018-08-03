using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//https://catlikecoding.com/unity/tutorials/procedural-grid/
[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
//TODO: Shader de terreno
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
        float putInCenterX = 0;//terrain.X / 2;
        float putInCenterY = 0;//terrain.Y / 2;
        vertices = new Vector3[(terrain.X + 1) * (terrain.Y + 1)];
        Vector2[] uv = new Vector2[vertices.Length];
        Vector4[] tangents = new Vector4[vertices.Length];
        Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);
        for (int i = 0, y = 0; y <= terrain.Y; y++)
        {
            for (int x = 0; x <= terrain.X; x++, i++)
            {
                vertices[i] = new Vector3(x - putInCenterX, terrain.HeightMap[i], y - putInCenterY);
                uv[i] = new Vector2(x / terrain.X, y / terrain.Y);
                tangents[i] = tangent;
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
        mesh.RecalculateNormals();
        GetComponent<MeshFilter>().sharedMesh.RecalculateBounds();
        mesh.tangents = tangents;

        GetComponent<MeshCollider>().sharedMesh = GetComponent<MeshFilter>().sharedMesh;
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
