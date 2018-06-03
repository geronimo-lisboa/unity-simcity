using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlaneSource : MonoBehaviour {
	private int currentWidth = 0;
	public int Width = 2;

	public PlaneSource(){
		
	}

	void Awake()
	{
        
    }

    void Update()
	{
		//Debug.Log("Width=" + Width + ", currentWidth=" + currentWidth);
		if(currentWidth!=Width)//Mexeu na width, reconstruir a geometria.
		{
			Debug.Log("alteracao");
			BuildPlane();
			currentWidth = Width;         
		}
    }

	private void BuildPlane(){
		Mesh mesh = new Mesh();
		mesh.name = "scripted plane";

		List<Vector3> vertexData = new List<Vector3>();
        vertexData.Add(new Vector3(-1, -1, 0));
		vertexData.Add(new Vector3( 1, -1, 0));
		vertexData.Add(new Vector3( 1,  1, 0));
		vertexData.Add(new Vector3(-1,  1, 0));

		List<Vector2> uvs = new List<Vector2>();
		uvs.Add(new Vector2(0, 0));
		uvs.Add(new Vector2(0, 1));
		uvs.Add(new Vector2(1, 1));
		uvs.Add(new Vector2(1, 0));

		List<int> triangles = new List<int>();
		triangles.Add(0);
		triangles.Add(1);
		triangles.Add(2);
		triangles.Add(0);
		triangles.Add(2);
		triangles.Add(3);

		mesh.vertices = vertexData.ToArray();
		mesh.uv = uvs.ToArray();
		mesh.triangles = triangles.ToArray();
		mesh.RecalculateBounds();
		mesh.RecalculateNormals();
		mesh.RecalculateTangents();

		GetComponent<MeshFilter>().mesh = mesh;
		GetComponent<MeshCollider>().sharedMesh = mesh; 

	}
}
