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
        //TODO:Implementar derivada parcial.
		throw new System.NotImplementedException();
	}

	public  float[,] GetTerrainHeight(int width)
	{
        float imageWidth = sourceTexture.width;
        float imageHeight = sourceTexture.height;
        if(imageWidth != width)
        {
            Debug.LogWarning("Width do componente deve ser igual à width da imagem. componente = "+width+", imagem="+imageWidth);
            return new float[width, width];
        }
        else
        {
            float[,] map = new float[width, width];
            Color[] pixelData = sourceTexture.GetPixels();
            for (int zIndex = 0; zIndex < width; zIndex++)
            {
                for (int xIndex = 0; xIndex < width; xIndex++)
                {
                    float heightValueInTexture = sourceTexture.GetPixel(xIndex, zIndex).r;
                    map[zIndex, xIndex] = heightValueInTexture;
                }
            }
            return map;
        }

	}

}
