using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TerrainMK2DashboardController : MonoBehaviour {

    public string idTerrenoTeste;

    public void OnTesteAbrirClick()
    {
        //Abre o terreno, passa ele pra próxima cena e abre a cena
        TerrainMK2Service terrainService = new TerrainMK2Service();
        TerrainMK2 terrain = terrainService.GetById(idTerrenoTeste);
        //Guarda o terreno nas playerPrefs pra passar os dados entre as cenas
        var jsonfiedTerrain = JsonConvert.SerializeObject(terrain);
        PlayerPrefs.SetString("currentTerrain", jsonfiedTerrain);
        SceneManager.LoadScene("TerrainEditor");
    }

    public void OnTesteNovoClick()
    {
        //Cria um novo terreno e vai pro editor
        TerrainMK2Service terrainService = new TerrainMK2Service();
        TerrainMK2 terrain = terrainService.NewMockTerrain();
        terrain = terrainService.SaveTerrain(terrain);

        var jsonfiedTerrain = JsonConvert.SerializeObject(terrain);
        PlayerPrefs.SetString("currentTerrain", jsonfiedTerrain);
        SceneManager.LoadScene("TerrainEditor");

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
