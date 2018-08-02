using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMK2DashboardController : MonoBehaviour {

    public void OnTesteAbrirClick()
    {
        //Abre o terreno, passa ele pra próxima cena e abre a cena
        TerrainMK2Service terrainService = new TerrainMK2Service();
        TerrainMK2 terrain = terrainService.GetById("FBDHFJYMMIIOUPHTYBGU");
        Debug.Log("ehkllo");
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
