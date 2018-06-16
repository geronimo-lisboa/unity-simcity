using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace model.terrain
{ 
    public abstract class TerrainModificationStrategy {
        /// <summary>
        /// Encapsula a mudança de sistema de coordenadas.
        /// </summary>
        /// <param name="terrain"></param>
        /// <param name="pointInWC"></param>
        /// <returns></returns>
        protected int[] WorldCoordinateToImageCoordinate(MyTerrain terrain, Vector3 pointInWC)
        {
            Vector3 origin = terrain.GetOrigin();
            Vector2 spacing = terrain.GetXZSpacing();
            Vector3 _temp = pointInWC - origin;
            _temp.x *= spacing.x;
            _temp.z *= spacing.y;//(spacing é de 2 coordenadas (xz), com z sendo chamado de y pela classe Vector2.
            int[] pointInImageCoordinate = new int[] { (int)_temp.x, (int)_temp.z };
            return pointInImageCoordinate;
        }
        public enum ElevationChange { Raise, Lower };
        public ElevationChange RaiseOrLower { get; private set; }
        public TerrainModificationStrategy(ElevationChange raiseOrLower)
        {
            RaiseOrLower = raiseOrLower;
        }

        //Pega a posição na textura a partir do ponto em wc. A origin dos meu terrenos é em (0,0,0) e a largura
        //é igual à width do bitmap. Não devo jamais tocar em vértices especificos pq a topologia da mesh não é
        //garantida e é responsabilidade da camada acima dessa, a camada de Aplicação. A partir dessa posição eu
        //aplico uma função à imagem pra modificá-la (função essa a ser definida pelos herdeiros) e modifico o terreno,
        //que ficará com o status de Dirty.
        abstract public void Execute(MyTerrain terrain, float intensity, Vector3 pointInWC);
     }
}
