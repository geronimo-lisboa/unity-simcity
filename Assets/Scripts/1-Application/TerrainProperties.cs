using model.terrain;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace application.terrainEditor
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
    public class TerrainProperties : MonoBehaviour
    {
        //Como esse script roda tanto no jogo quanto no editor eu preciso controlar se houve alguma
        //alteração nos dados qdo estiver rodando no editor
        private int currentSize = 0;
        private float currentHeightMultiplier = 0;
        private float currentSeaLevel = 0;
        private float currentBeachWidth = 0;

        public int Size;
        /// <summary>
        /// O multiplicador a ser aplicado nas alturas
        /// </summary>
        [Range(0.5f, 500f)]
        public float HeightMultiplier;
        /// <summary>
        /// Nivel do mar
        /// </summary>
        [Range(0.0f, 10f)]
        public float SeaLevel;
        /// <summary>
        /// Espessura da praia
        /// </summary>
        [Range(0.2f, 2.0f)]
        public float BeachWidth;
        /// <summary>
        /// A textura 2d do heightmap
        /// </summary>
        public Texture2D Heightmap;

        /// <summary>
        /// Retorna true se houve mudança no tamanho da malha, false caso contrário.
        /// </summary>
        /// <returns></returns>
        public bool HasChangedData()
        {
            return (currentHeightMultiplier != HeightMultiplier ||
                currentSize != Size ||
                currentSeaLevel != SeaLevel ||
                currentBeachWidth != BeachWidth);

        }

        public void UpdateChangeTesters()
        {
            currentHeightMultiplier = HeightMultiplier;
            currentSize = Size;
            currentSeaLevel = SeaLevel;
            currentBeachWidth = BeachWidth;
        }

        private void UpdateTerrain()
        {
            if (HasChangedData() && MyTerrain.IsInitalized())
            {
                MyTerrain.GetTerrain().BeachWidth = BeachWidth;
                MyTerrain.GetTerrain().SeaLevel = SeaLevel;
                MyTerrain.GetTerrain().ScaleFactor = HeightMultiplier;
                //  MyTerrainV2.GetTerrain().BeachWidth = BeachWidth;

            }
        }

        private void Awake()
        {
            UpdateTerrain();
        }
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            UpdateTerrain();

        }
    }
}