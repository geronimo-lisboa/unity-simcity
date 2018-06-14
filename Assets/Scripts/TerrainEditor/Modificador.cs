using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

abstract class TipoDeModificacao
{
    public enum ElevationChange { Raise, Lower };
    public ElevationChange Incremento { get; private set; }
    public TipoDeModificacao(ElevationChange incremento)
    {
        this.Incremento = incremento;
    }

    public abstract void Execute(Vector3 point, float intensity, MyTerrain terrain);
}

class ModificationSimple : TipoDeModificacao
{
    public ModificationSimple(ElevationChange inc):base(inc)
    {
;
    }

    public override void Execute(Vector3 point, float intensity, MyTerrain terrain)
    {
        //o ponto está no espaço 3d. Cada vertice corresponde a um pixel da imagem
        //Achar o pixel mais proximo, desse vertice determinar o pixel. Modificar o valor
        //do pixel e reconstruir a mesh.
        Vector3 terrainOrigin = terrain.gameObject.transform.position;//Atualmente é 0,0,0
        float xzSpacing = 1.0f;
        Vector3 pointInImageSpace = (point - terrainOrigin) * xzSpacing;
        pointInImageSpace.x = Mathf.Round(pointInImageSpace.x);
        pointInImageSpace.y = Mathf.Round(pointInImageSpace.y);
        pointInImageSpace.z = Mathf.Round(pointInImageSpace.z);
        //O ponto está agora no espaço da imagem, pegar a imagem e mudá-la
        int[] _pix = { (int)pointInImageSpace.x, (int)pointInImageSpace.z };
        float height = terrain.Heightmap.GetPixel(_pix[0], _pix[1]).r;
        height = height + intensity;
        Color newIntensity = new Color(height, height, height, height) ;
        terrain.Heightmap.SetPixel(_pix[0], _pix[1], newIntensity);
        terrain.IsDirty = true;
    }
}

public class Modificador : MonoBehaviour {
    //Os botões
    public Button ButtonRaiseTerrain;
    public Button ButtonLowerTerrain;
    //Relativo ao picking da posição 
    public Camera SceneCamera;
    public GameObject TestCursor;
    private Vector3 oldEventPos;
    private float intensity = 0;

    private TipoDeModificacao currentTypeOfModification = null;

    // Use this for initialization
    void Start () {
        //Seta os delegates que vão tratar dos clicks
        ButtonLowerTerrain.onClick.AddListener(delegate
        {
            currentTypeOfModification = new ModificationSimple(TipoDeModificacao.ElevationChange.Lower);
        });
        ButtonRaiseTerrain.onClick.AddListener(delegate
        {
            currentTypeOfModification = new ModificationSimple(TipoDeModificacao.ElevationChange.Lower);
        });
	}

    private float CalculateIntensity(Vector3 currentMousePosInSC)
    {
        if (currentMousePosInSC.Equals(oldEventPos))
            return intensity + 0.1f;
        else
            return intensity;
    }

    private void OnMouseDown()
    {
        Vector3 mousePosInScreenCoordinate = Input.mousePosition;
        oldEventPos = mousePosInScreenCoordinate;
    }

    private void OnMouseDrag()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosInScreenCoordinate = Input.mousePosition;
            
            Ray mouseRay = SceneCamera.ScreenPointToRay(mousePosInScreenCoordinate);
            RaycastHit hit;
            if (GetComponent<Collider>().Raycast(mouseRay, out hit, Mathf.Infinity))
            {
                //TestCursor.transform.position = hit.point;
                intensity = CalculateIntensity(hit.point);
                if (currentTypeOfModification != null)
                    currentTypeOfModification.Execute(hit.point, intensity, GetComponent<MyTerrain>());
                oldEventPos = hit.point;
            }
        }
    }

    private void OnMouseUp()
    {
        intensity = 0;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
