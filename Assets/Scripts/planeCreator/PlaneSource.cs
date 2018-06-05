////https://catlikecoding.com/unity/tutorials/procedural-grid/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class PlaneSource : MonoBehaviour {
    //Como esse script roda tanto no jogo quanto no editor eu preciso controlar se houve alguma
    //alteração no tamanho do x e y. Se houve, será preciso reconstruir a malha.
    private int currentXSize, currentZSize;
    [Range(1,100)]
    public int xSize;
    [Range(1,100)]
    public int zSize;
    private Mesh mesh;
    private Vector3[] vertices;
	// Use this for initialization
	void Start () {
        currentXSize = 0;
        currentZSize = 0;
	}

    private void Awake()
    {
        Generate();
    }
    // Update is called once per frame
    void Update () {
		if(HasChangedSize())
        {
            currentXSize = xSize;
            currentZSize = zSize;
            Generate();
        }
	}

    private void Generate()
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Grid";
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        Vector2[] uv = new Vector2[vertices.Length];
        Vector4[] tangents = new Vector4[vertices.Length];
        Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);
        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                vertices[i] = new Vector3(x,0, z);
                uv[i] = new Vector2((float)x / xSize, (float)z / zSize);
                tangents[i] = tangent;
            }
        }
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.tangents = tangents;

        int[] triangles = new int[xSize * zSize * 6];
        for (int ti = 0, vi = 0, y = 0; y < zSize; y++, vi++)
        {
            for (int x = 0; x < xSize; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + xSize + 1;
                triangles[ti + 5] = vi + xSize + 2;
            }
        }
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
    /// <summary>
    /// Retorna true se houve mudança no tamanho da malha, false caso contrário.
    /// </summary>
    /// <returns></returns>
    private bool HasChangedSize()
    {
        return (currentZSize != zSize || currentXSize != xSize);
    }
}
