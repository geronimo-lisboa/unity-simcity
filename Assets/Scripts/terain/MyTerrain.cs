////https://catlikecoding.com/unity/tutorials/procedural-grid/

using System.Collections;
using System.Collections.Generic;
using terrainGenerator;
using UnityEngine;
[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MyTerrain : MonoBehaviour {
    //Como esse script roda tanto no jogo quanto no editor eu preciso controlar se houve alguma
    //alteração nos dados qdo estiver rodando no editor
    private int currentSize = 0;
    private float currentHeightMultiplier = 0;
     /// <summary>
    /// O tamanho do lado, em quadradinhos
    /// </summary>
    public int Size = 256; //TODO: Esse campo vai morrer, com o size vindo a ser determinado pelo ITerrainGenerator.
    /// <summary>
    /// O multiplicador a ser aplicado nas alturas
    /// </summary>
    [Range(0.5f, 500f)]
    public float HeightMultiplier;
    /// <summary>
    /// O gerador das alturas 
    /// </summary>
    private ITerrainGenerator generator;
    /// <summary>
    /// O heightmap
    /// </summary>
    public Texture2D Heightmap;

    private Mesh mesh;
    private Vector3[] vertices;
    /// <summary>
    /// Só foi usada no teste do perlin noise
    /// </summary>
 

	// Use this for initialization
	void Start () {
       
    }

    private void Awake()
    {
        Generate();
    }
    // Update is called once per frame
    void Update () {
		if(RunningOnEditorTest.IsRunningOnEditor() && HasChangedSize())
        {
            Generate();
            generator = new HeightMapTerrainGenerator(Heightmap);//new RandomTerrainGenerator(scale);            
            float[,] heightData = generator.GetTerrainHeight((int)Mathf.Sqrt(GetComponent<MeshFilter>().sharedMesh.vertices.Length));
            SetTerrainHeight(heightData);
            UpdateChangeTesters();
        }
        if(!RunningOnEditorTest.IsRunningOnEditor())
        {
            Generate();
            generator = new HeightMapTerrainGenerator(Heightmap);
            float[,] heightData = generator.GetTerrainHeight(Size);
            SetTerrainHeight(heightData);
            UpdateChangeTesters();
        }
    }

    private void SetTerrainHeight(float[,] height)
    {
        int tileDepth = height.GetLength(0);
        int tileWidth = height.GetLength(1);

        Vector3[] meshVertices = RunningOnEditorTest.IsRunningOnEditor() ? GetComponent<MeshFilter>().sharedMesh.vertices : GetComponent<MeshFilter>().mesh.vertices;
        int vertexIndex = 0;
        for (int zIndex = 0; zIndex < tileDepth; zIndex++)
        {
            for (int xIndex = 0; xIndex < tileWidth; xIndex++)
            {
                float currHeight = height[zIndex, xIndex];
                Vector3 vert = meshVertices[vertexIndex];
                meshVertices[vertexIndex] = new Vector3(vert.x, currHeight * HeightMultiplier, vert.z);
                vertexIndex++;
            }
        }
        if (RunningOnEditorTest.IsRunningOnEditor())
        {
            // update the vertices in the mesh and update its properties
            GetComponent<MeshFilter>().sharedMesh.vertices = meshVertices;
            GetComponent<MeshFilter>().sharedMesh.RecalculateBounds();
            GetComponent<MeshFilter>().sharedMesh.RecalculateNormals();
            GetComponent<MeshFilter>().sharedMesh.RecalculateTangents();
        }
        else
        {
            // update the vertices in the mesh and update its properties
            GetComponent<MeshFilter>().mesh.vertices = meshVertices;
            GetComponent<MeshFilter>().mesh.RecalculateBounds();
            GetComponent<MeshFilter>().mesh.RecalculateNormals();
            GetComponent<MeshFilter>().mesh.RecalculateTangents();
        }
    }

    private void Generate()
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Grid";
        vertices = new Vector3[(Size + 1) * (Size + 1)];
        Vector2[] uv = new Vector2[vertices.Length];
        Vector4[] tangents = new Vector4[vertices.Length];
        Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);
        for (int i = 0, z = 0; z <= Size; z++)
        {
            for (int x = 0; x <= Size; x++, i++)
            {
                vertices[i] = new Vector3(x, 0, z);
                uv[i] = new Vector2((float)x / Size, (float)z / Size);
                tangents[i] = tangent;
            }
        }
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.tangents = tangents;

        int[] triangles = new int[Size * Size * 6];
        for (int ti = 0, vi = 0, y = 0; y < Size; y++, vi++)
        {
            for (int x = 0; x < Size; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + Size + 1;
                triangles[ti + 5] = vi + Size + 2;
            }
        }
        mesh.triangles = triangles;
       // mesh.RecalculateNormals();
    }

    private void UpdateChangeTesters()
    {
        currentHeightMultiplier = HeightMultiplier;
        currentSize = Size;
 
    }
    /// <summary>
    /// Retorna true se houve mudança no tamanho da malha, false caso contrário.
    /// </summary>
    /// <returns></returns>
    private bool HasChangedSize()
    {
        return (currentHeightMultiplier != HeightMultiplier || currentSize!= Size );
    }
}
