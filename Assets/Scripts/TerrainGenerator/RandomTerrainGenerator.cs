using System.Collections;
using System.Collections.Generic;
using terrainGenerator;
using UnityEngine;

public class RandomTerrainGenerator : ITerrainGenerator { 

	public Vector2[,] GetTerrainDerivative(float[,] height)
	{
		throw new System.NotImplementedException();
	}



    public float[,] GetTerrainHeight(int width, float scale)
	{
		float[,] map = new float[width, width];
		for (int zIndex = 0; zIndex < width; zIndex++){
			for (int xIndex = 0; xIndex < width; xIndex++){
				float sampleX = xIndex / scale;
				float sampleZ = zIndex / scale;
				float noiseVal = Mathf.PerlinNoise(sampleX, sampleZ);
				map[zIndex, xIndex] = noiseVal;
			}
		}      
		return map;
	}

}
