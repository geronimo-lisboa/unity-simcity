using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace terrainGenerator{
	//TODO: Por GetSize() pra alimentar o MyTerrain quando o Size deixar de ser uma propriedade.
	public interface ITerrainGenerator {
		/// <summary>
        /// Os herdeiros devem fornecer as altitudes do terreno aqui.
        /// </summary>
        /// <returns>The terrain height.</returns>
        /// <param name="width">Width.</param>
        /// <param name="scale">Scale.</param>
		float[,] GetTerrainHeight(int width);

        
        /// <summary>
        /// O gradiente do terreno deve ser fornecido aqui, baseado na altitude.
		/// Como a altitude é uma função de duas variáveis H(x,z) sua derivada é
		/// de duas dimensões (um vec2)
        /// </summary>
        /// <returns>The terrain derivative.</returns>
        /// <param name="height">Height.</param>
		Vector2[,] GetTerrainDerivative(float[,] height);
	}
}
