using System.Collections;
using System.Collections.Generic;
using model.terrain;
using UnityEngine;
namespace model.terrain.terrainModificationStrategy
{

    public class SimpleTerrainModificationStrategy:MyModificationStrategyV2 
    {
        public SimpleTerrainModificationStrategy(ElevationChange elevationChange)
            :base(elevationChange)
        {

        }


        public override void Execute(MyTerrainV2 terrain, float intensity, Vector3 pointInWC)
        {
            Vector3 origin = terrain.GetOrigin();
            Vector2 spacing = terrain.GetXZSpacing();
            Vector3 _temp = pointInWC - origin;
            _temp.x *= spacing.x;
            _temp.z *= spacing.y;//(spacing é de 2 coordenadas (xz), com z sendo chamado de y pela classe Vector2.
            int[] pointInImageCoordinate = new int[] { (int)_temp.x, (int)_temp.z };
            //Agora tenho a posição na imagem. Vou alterar o valor do pixel e atualizar o terreno
            Texture2D hm = terrain.Heightmap;//Necessário  criar essa variável local pra forçar o setter do Heightmap, que flaga como Dirty
            Color oldColor = hm.GetPixel(pointInImageCoordinate[0], pointInImageCoordinate[1]);
            Color newColor = new Color(0,0,0);
            if(RaiseOrLower==ElevationChange.Lower)
            {
                newColor = oldColor - new Color(1.0f * intensity, 1.0f * intensity, 1.0f * intensity);
            }
            if (RaiseOrLower == ElevationChange.Raise)
            {
                newColor = oldColor + new Color(1.0f * intensity, 1.0f * intensity, 1.0f * intensity);
            }
            hm.SetPixel(pointInImageCoordinate[0], pointInImageCoordinate[1], newColor);
            terrain.Heightmap = hm;
        }
    }
}
