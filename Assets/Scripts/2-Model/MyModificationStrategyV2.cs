using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace model.terrain
{ 
    //TODO: Renomear p/ TerrainModificationStrategy
    public abstract class MyModificationStrategyV2 {
        public enum ElevationChange { Raise, Lower };
        public ElevationChange RaiseOrLower { get; private set; }
        public MyModificationStrategyV2(ElevationChange raiseOrLower)
        {
            RaiseOrLower = raiseOrLower;
        }

        //Pega a posição na textura a partir do ponto em wc. A origin dos meu terrenos é em (0,0,0) e a largura
        //é igual à width do bitmap. Não devo jamais tocar em vértices especificos pq a topologia da mesh não é
        //garantida e é responsabilidade da camada acima dessa, a camada de Aplicação. A partir dessa posição eu
        //aplico uma função à imagem pra modificá-la (função essa a ser definida pelos herdeiros) e modifico o terreno,
        //que ficará com o status de Dirty.
        abstract public void Execute(MyTerrainV2 terrain, float intensity, Vector3 pointInWC);
     }
}
