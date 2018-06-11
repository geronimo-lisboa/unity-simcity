using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class TipoDeModificacao
{
    public bool Incremento { get; private set; }
    public TipoDeModificacao(bool incremento)
    {
        this.Incremento = incremento;
    }
}

class ModificacaoLinear : TipoDeModificacao
{
    public ModificacaoLinear(bool inc):base(inc)
    {
;
    }
}

public class Modificador : MonoBehaviour {
    //Os botões
    public Button ButtonRaiseTerrain;
    public Button ButtonLowerTerrain;

    private TipoDeModificacao currentTypeOfModification = null;
	// Use this for initialization
	void Start () {
        //Seta os delegates que vão tratar dos clicks
        ButtonLowerTerrain.onClick.AddListener(delegate
        {
            currentTypeOfModification = new ModificacaoLinear(false);
        });
        ButtonRaiseTerrain.onClick.AddListener(delegate
        {
            currentTypeOfModification = new ModificacaoLinear(true);
        });
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
