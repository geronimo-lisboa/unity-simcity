using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//https://catlikecoding.com/unity/tutorials/procedural-grid/
[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
//TODO: Fazer o plano ser ao longo de xz e não de xy
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
