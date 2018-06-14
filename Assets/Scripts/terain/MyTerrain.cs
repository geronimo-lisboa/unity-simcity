////https://catlikecoding.com/unity/tutorials/procedural-grid/
////TODO lista:
//SIMCITY
//10) Fazer elevar
//11) Fazer diminuir
//12) Criar o morro gaussiano
//13) Criar a meseta 
//14) Criar a elevação linear
//15) Criar a ravina
//7) Situação inicial de um mapa é plana
//8) Reativar o sea level
//9) Criar a barra de sea level
//8) Persistência do mapa
//9) Lista dos mapas persistidos
//10) Escolha de um mapa pra edição
//11) Criação de um novo mapa
//12) Deleção do mapa
//13) Vários tipos de modificações,
//14) O esquema de manter a intensidade inalterada se a posição do mouse estiver diferente não está bom pq aparentemente não está percebendo 100%
//quando é pra manter.Talvez ter um timer pra verificar se houve mudança?
//15) Fazer versão touch 
//16) Testar no S7, no S8 e no ipad
//17) Tiling: Ao invés de um unico mega-bloco, fazer um terreno maior usando vários blocos menores. Em nenhum momento eu preciso tocar
//nos vértices, só preciso da origem do mapa e da posição no world space pra saber onde mudar. Logo, não há nenhum problema em picotar,
//exceto nos casos em que uma operação de modificação afetar mais de um tile. Será preciso que a operação tenha algum conceito de raio
//para eu controlar isso.
//18) https://github.com/Whinarn/MeshDecimator pode ser util pois eu não estou usando os vértices. Se o decimator funcionar e minhas hipoteses
//estiverem certas eu poderei avaliar se o trade-off de maior lentidão na edição do mapa compensa a redução do numero de vertices. Talvez compense,
//quando o jogo estiver efetivamente sendo jogado ao invés de estar no editor de mapa.


//FEITO
//1) Ao clicar no botão, mudar o tipo de modificação, se é pra subir ou se é pra descer.
//2) Ao clicar no terreno, invocar o modificador e o modificador delegar pro tipo de modificação
//3) Armazenar a posição anterior do click
//4) Se a posição se mantiver, aumentar a intensidade e passar essa intensidade pro tipo de modificação
//5) Se a posição mudar, manter a intensidade constante
//6) Se o click acabar, intensidade vai a zero
//7)Conversão do click de world space pra image space
//8)Modificação da imagem
//9)Reconstrução do mapa

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
    private float currentSeaLevel = 0;
    private float currentBeachWidth = 0;
    /// <summary>
    /// Se true vai forçar o terreno a se regerar, necessário pro editor de terreno.
    /// </summary>
    public bool IsDirty = false;
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
    [Range(0.0f, 10f)]
    public float SeaLevel;
    [Range(0.2f, 2.0f)]
    public float BeachWidth;

    public GameObject SeaPlane;

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
		if(RunningOnEditorTest.IsRunningOnEditor() && HasChangedData())
        {
            Generate();
            generator = new HeightMapTerrainGenerator(Heightmap);//new RandomTerrainGenerator(scale);            
            float[,] heightData = generator.GetTerrainHeight((int)Mathf.Sqrt(GetComponent<MeshFilter>().sharedMesh.vertices.Length));
            SetTerrainHeight(heightData);
            UpdateShaderData();
            UpdateChangeTesters();
            SetSeaPlane();
            GetComponent<MeshCollider>().sharedMesh = GetComponent<MeshFilter>().sharedMesh;
        }
        if(!RunningOnEditorTest.IsRunningOnEditor())
        {
            Generate();
            generator = new HeightMapTerrainGenerator(Heightmap);
            float[,] heightData = generator.GetTerrainHeight(Size);            
            SetTerrainHeight(heightData);
            UpdateShaderData();
            UpdateChangeTesters();
            SetSeaPlane();
            GetComponent<MeshCollider>().sharedMesh = GetComponent<MeshFilter>().mesh;
        }
    }

    private void SetSeaPlane()
    {
        Vector3 t = new Vector3(0, SeaLevel - BeachWidth/2, 0);
        SeaPlane.transform.position = t;
        
        SeaPlane.transform.localScale = new Vector3(1000, 1000, 1000);
        
    }

    private void UpdateShaderData()
    {
        if (!RunningOnEditorTest.IsRunningOnEditor())
        {
            GetComponent<Renderer>().sharedMaterial.SetFloat("_SeaLevel", SeaLevel);
            GetComponent<Renderer>().sharedMaterial.SetFloat("_BeachWidth", BeachWidth);
        }
        else
        {
            GetComponent<Renderer>().material.SetFloat("_SeaLevel", SeaLevel);
            GetComponent<Renderer>().material.SetFloat("_BeachWidth", BeachWidth);
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
        currentSeaLevel = SeaLevel;
        currentBeachWidth = BeachWidth;
        IsDirty = false;
    }
    /// <summary>
    /// Retorna true se houve mudança no tamanho da malha, false caso contrário.
    /// </summary>
    /// <returns></returns>
    private bool HasChangedData()
    {
        return (currentHeightMultiplier != HeightMultiplier ||
            currentSize!= Size ||
            currentSeaLevel != SeaLevel ||
            currentBeachWidth != BeachWidth || IsDirty);
    }
}
