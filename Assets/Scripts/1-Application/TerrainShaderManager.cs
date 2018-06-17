using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace application.terrainEditor
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
    public class TerrainShaderManager : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            UpdateShaderData();
        }


        public void UpdateShaderData()
        {
            if (!RunningOnEditorTest.IsRunningOnEditor())
            {
                GetComponent<Renderer>().sharedMaterial.SetFloat("_SeaLevel", GetComponent<TerrainProperties>().SeaLevel);
                GetComponent<Renderer>().sharedMaterial.SetFloat("_BeachWidth", GetComponent<TerrainProperties>().BeachWidth);
            }
            else
            {
                GetComponent<Renderer>().sharedMaterial.SetFloat("_SeaLevel", GetComponent<TerrainProperties>().SeaLevel);
                GetComponent<Renderer>().sharedMaterial.SetFloat("_BeachWidth", GetComponent<TerrainProperties>().BeachWidth);

                //GetComponent<Renderer>().material.SetFloat("_SeaLevel", GetComponent<TerrainProperties>().SeaLevel);
                //GetComponent<Renderer>().material.SetFloat("_BeachWidth", GetComponent<TerrainProperties>().BeachWidth);
            }
        }
    }
}