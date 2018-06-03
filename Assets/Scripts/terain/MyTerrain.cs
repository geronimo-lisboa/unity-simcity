using System.Collections;
using System.Collections.Generic;
using terrainGenerator;
using UnityEngine;
using UnityEngine.Assertions;

public class MyTerrain : MonoBehaviour {
	public Texture2D HeightmapTexture;
	/// <summary>
    /// Um multiplicador que é a aplicado à altitude.
    /// </summary>
	public float HeightMultiplier;
	/// <summary>
    /// O gerador do terreno.
    /// </summary>
	private ITerrainGenerator generator;
	// Use this for initialization
	void Start () {
		Assert.IsNotNull(HeightmapTexture, "É obrigatório informar a textura do heightmap");
		Assert.IsFalse(HeightMultiplier == 0, "HeightMultiplier não pode ser zero.");
		Assert.IsNotNull(GetComponent<MeshCollider>(), "Tem que ter mesh collider");
		//generator = new HeightMapTerrainGenerator(HeightmapTexture);      
		generator = new RandomTerrainGenerator();
		int width = CalculateWidth();
		float[,] heightData = generator.GetTerrainHeight(width, 10);
		SetTerrainHeight(heightData);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void SetTerrainHeight(float[,] height){
		int tileDepth = height.GetLength(0);
		int tileWidth = height.GetLength(1);
		Vector3[] meshVertices = GetComponent<MeshFilter>().mesh.vertices;
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
		// update the vertices in the mesh and update its properties
		GetComponent<MeshFilter>().mesh.vertices = meshVertices;
		GetComponent<MeshFilter>().mesh.RecalculateBounds();
		GetComponent<MeshFilter>().mesh.RecalculateNormals();
		GetComponent<MeshFilter>().mesh.RecalculateTangents();
		// update the mesh collider
		GetComponent<MeshCollider>().sharedMesh = GetComponent<MeshFilter>().mesh;
	}
    /// <summary>
    /// Meu objeto é um plano. Planos da unity são quadrados.
    /// </summary>
    /// <returns>The width.</returns>
	private int CalculateWidth(){
		Vector3[] vertices = GetComponent<MeshFilter>().mesh.vertices;
		return (int)Mathf.Sqrt(vertices.Length);
	}

}
