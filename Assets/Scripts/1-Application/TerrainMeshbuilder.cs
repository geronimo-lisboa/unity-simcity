using model.terrain;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
public class TerrainMeshbuilder : MonoBehaviour {

    /// <summary>
    /// O modelo do terreno
    /// </summary>
    private MyTerrainV2 Terrain; //TODO: isso aqui está null, ainda não é incializado em lugar algum.
    /// <summary>
    /// O plano do mar
    /// </summary>
    public GameObject SeaPlane;

    private Mesh mesh;
    // private Mesh mesh;
    private Vector3[] vertices;
    // Use this for initialization
    void Start () {
		
	}

    private void Awake()
    {
        GenerateMesh();
    }

    private void SetSeaPlane()
    {
        Vector3 t = new Vector3(0, GetComponent<TerrainProperties>().SeaLevel - GetComponent<TerrainProperties>().BeachWidth / 2, 0);
        SeaPlane.transform.position = t;
        SeaPlane.transform.localScale = new Vector3(1000, 1000, 1000);
    }

    // Update is called once per frame
    void Update () {
        if (Terrain == null)
        {
            Terrain = MyTerrainV2.GetTerrain(
                GetComponent<TerrainProperties>().Heightmap,
                GetComponent<TerrainProperties>().SeaLevel,
                GetComponent<TerrainProperties>().BeachWidth,
                GetComponent<TerrainProperties>().HeightMultiplier);
        }
            
        if (RunningOnEditorTest.IsRunningOnEditor() && GetComponent<TerrainProperties>().HasChangedData() && Terrain.Dirty==true)
        {
            GenerateMesh();
            SetSeaPlane();
            GetComponent<TerrainShaderManager>().UpdateShaderData();
            GetComponent<TerrainProperties>().UpdateChangeTesters();
            Vector3[] meshVertices = RunningOnEditorTest.IsRunningOnEditor() ? GetComponent<MeshFilter>().sharedMesh.vertices : GetComponent<MeshFilter>().mesh.vertices;
            // update the vertices in the mesh and update its properties
            GetComponent<MeshFilter>().sharedMesh.vertices = meshVertices;
            GetComponent<MeshFilter>().sharedMesh.RecalculateBounds();
            GetComponent<MeshFilter>().sharedMesh.RecalculateNormals();
            GetComponent<MeshFilter>().sharedMesh.RecalculateTangents();
            GetComponent<MeshCollider>().sharedMesh = GetComponent<MeshFilter>().sharedMesh;
        }
        if (!RunningOnEditorTest.IsRunningOnEditor())
        {
            GenerateMesh();
            SetSeaPlane();
            GetComponent<TerrainShaderManager>().UpdateShaderData();
            GetComponent<TerrainProperties>().UpdateChangeTesters();
            Vector3[] meshVertices = RunningOnEditorTest.IsRunningOnEditor() ? GetComponent<MeshFilter>().sharedMesh.vertices : GetComponent<MeshFilter>().mesh.vertices;
            // update the vertices in the mesh and update its properties
            GetComponent<MeshFilter>().mesh.vertices = meshVertices;
            GetComponent<MeshFilter>().mesh.RecalculateBounds();
            GetComponent<MeshFilter>().mesh.RecalculateNormals();
            GetComponent<MeshFilter>().mesh.RecalculateTangents();
            GetComponent<MeshCollider>().sharedMesh = GetComponent<MeshFilter>().mesh;
        }

    }

    /// <summary>
    /// O gerador da mesh. Terá que ser movido para uma classe própria logo.
    /// </summary>
    private void GenerateMesh()
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        //Recalcula o terreno
        Terrain.RecalculateHeights();
        //Aloca as estruturas de dados
        vertices = new Vector3[(GetComponent<TerrainProperties>().Size + 1) * (GetComponent<TerrainProperties>().Size + 1)];
        Vector2[] uv = new Vector2[vertices.Length];
        Vector4[] tangents = new Vector4[vertices.Length];
        Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);
        //Inicializa os vertices com altitude definida pelo terreno
        int thCount = 0;
        for (int i = 0, z = 0; z <= GetComponent<TerrainProperties>().Size; z++)
        {
            for (int x = 0; x <= GetComponent<TerrainProperties>().Size; x++, i++)
            {
                vertices[i] = new Vector3(x, Terrain.Heights[thCount], z);
                uv[i] = new Vector2((float)x / GetComponent<TerrainProperties>().Size, (float)z / GetComponent<TerrainProperties>().Size);
                tangents[i] = tangent;
                thCount++;
            }
        }
        //passa as coisas pra mesh
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.tangents = tangents;
        //Cria os triangulos
        int[] triangles = new int[GetComponent<TerrainProperties>().Size * GetComponent<TerrainProperties>().Size * 6];
        for (int ti = 0, vi = 0, y = 0; y < GetComponent<TerrainProperties>().Size; y++, vi++)
        {
            for (int x = 0; x < GetComponent<TerrainProperties>().Size; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + GetComponent<TerrainProperties>().Size + 1;
                triangles[ti + 5] = vi + GetComponent<TerrainProperties>().Size + 2;
            }
        }
        mesh.triangles = triangles;
    }


}
