using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
public class TerrainShaderManager : MonoBehaviour {
    [Range(0.0f, 10f)]
    public float SeaLevel;
    [Range(0.2f, 2.0f)]
    public float BeachWidth;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        UpdateShaderData();
    }


    public void UpdateShaderData()
    {
        if (!RunningOnEditorTest.IsRunningOnEditor())
        {
            GetComponent<Renderer>().sharedMaterial.SetFloat("_SeaLevel", SeaLevel);
            GetComponent<Renderer>().sharedMaterial.SetFloat("_BeachWidth", BeachWidth);
        }
        else
        {
            GetComponent<Renderer>().material.SetFloat("_SeaLevel", SeaLevel);
            GetComponent<Renderer>().material.SetFloat("_BeachWidth", BeachWidth);
        }
    }
}
