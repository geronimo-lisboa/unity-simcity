using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace model.terrain
{
    public class MyTerrainV2
    {
        private static MyTerrainV2 singleton = null;

        public static MyTerrainV2 GetTerrain()
        {
            if(singleton == null)
            {
                throw new System.InvalidOperationException();
            }
            return singleton;
        }

        public static MyTerrainV2 GetTerrainAndInitializeIfNeeded(Texture2D heightmap, float seaLevel, float beachWidth, float scaleFactor)
        {
            if(singleton == null)
            {
                singleton = new MyTerrainV2(heightmap, seaLevel, beachWidth, scaleFactor);
            }
            return singleton;
        }
        /// <summary>
        /// Se torna true se o heightmap for alterado. 
        /// </summary>
        public bool Dirty { get; private set; }
        /// <summary>
        /// O heightmap. Mudar ele marca como dirty.
        /// </summary>
        public Texture2D Heightmap {
            get
            {
                return _heigtmap;
            }
            set
            {
                _heigtmap = value;
                Dirty = true;
            }
        }
        private Texture2D _heigtmap;
        /// <summary>
        /// Nivel do mar, de 0 a 10
        /// </summary>
        public float SeaLevel {
            get
            { return _seaLevel; }
            set
            { _seaLevel = value;
                Dirty = true;
            }
        }
        private float _seaLevel;
        /// <summary>
        /// Espessura da praia (0 a 2)
        /// </summary>
        public float BeachWidth { get; set; }
        /// <summary>
        /// Um fator multiplicador aplicado às altitudes. Mudar ele marca como dirty.
        /// </summary>
        public float ScaleFactor {
            get
            {
                return _scaleFactor;
            }
            set
            {
                _scaleFactor = value;
                Dirty = true;
            }
        }
        private float _scaleFactor;
        /// <summary>
        /// As altitudes como uma lista de floats, gerada a partir do heightmap
        /// </summary>
        public List<float> Heights { get; private set; }

        public MyTerrainV2(Texture2D heightmap, float seaLevel, float beachWidth, float scaleFactor)
        {
            this.Heightmap = heightmap;
            this.SeaLevel = seaLevel;
            this.BeachWidth = beachWidth;
            this.ScaleFactor = scaleFactor;
            this.Dirty = true;
            Heights = new List<float>();
        }
        //Recalcula as altitudes e reseta o dirty pra false;
        public void RecalculateHeights()
        {
            Heights.Clear();
            int count = 0;
            for(int zIndex = 0; zIndex < Heightmap.width; zIndex++)
            {
                for(int xIndex = 0; xIndex < Heightmap.height; xIndex++)
                {
                    float heightMapValueInTexture = Heightmap.GetPixel(xIndex, zIndex).r;
                    Heights.Add(heightMapValueInTexture * ScaleFactor);
                    count++;
                }
            }
            Dirty = false;
        }
    }
}
