using System.Collections;
using System.Collections.Generic;
using terrainGenerator;
using UnityEngine;

public class HeightMapTerrainGenerator : ITerrainGenerator {
	private Texture2D sourceTexture;
	public HeightMapTerrainGenerator(Texture2D tex){
		sourceTexture = tex;
	}
    
	public  Vector2[,] GetTerrainDerivative(float[,] height)
	{
		throw new System.NotImplementedException();
	}

	public  float[,] GetTerrainHeight(int width, float scale)
	{
		float[,] map = new float[width, width];
		Color[] pixelData = sourceTexture.GetPixels();
		for (int zIndex = 0; zIndex < width; zIndex++)
		{
			for (int xIndex = 0; xIndex < width; xIndex++)
			{
				//float heightValueInTexture = pixelData[zIndex * width + xIndex].r;
				float heightValueInTexture = sourceTexture.GetPixel(xIndex, zIndex).r;
				map[zIndex, xIndex] = heightValueInTexture;
			}
		}
		return map;
	}

}
