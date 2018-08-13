using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PowerUI;

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

    public void ResetDataClick()
    {
        PlayerPrefs.DeleteAll();
    }

    public void OnTesteWidgetClick()
    {
        Debug.Log("clicou");
    }

    private Dom.Element divModalNovoTerreno;

    // Use this for initialization
    void Start () {
        //Pega a lista de terrenos
        TerrainMK2Service terrainService = new TerrainMK2Service();
        List<TerrainMK2> terrainlist = terrainService.GetAll();
        //cria as linhas de terreno.

        //var document = UI.document;
        //var btnTestWidget = document.getElementById("btnTesteWidget");
        //btnTestWidget.onmousedown = delegate (MouseEvent mouseEvent)
        //{
        //    OnTesteWidgetClick();
        //};

        //var btnTestOpen = document.getElementById("btnTestOpen");        
        //btnTestOpen.onmousedown = delegate (MouseEvent mouseEvent)
        //{
        //    OnTesteAbrirClick();
        //};

        //var btnOpenEditor = document.getElementById("btnOpenEditor");
        //btnOpenEditor.onmousedown = delegate (MouseEvent mouseEvent)
        //{
        //    OnTesteNovoClick();
        //};

        //var btnResetEverything = document.getElementById("btnResetEverything");
        //btnResetEverything.onmousedown = delegate (MouseEvent mouseEvent)
        //{
        //    ResetDataClick();
        //};

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
