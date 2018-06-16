using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using model.terrain;
using UnityEngine.UI;
using model.terrain.terrainModificationStrategy;

namespace application.terrainEditor
{
    public class TerrainEditorInteractor : MonoBehaviour
    {
        public Button ButtonRaiseTerrain;
        public Button ButtonLowerTerrain;
        public Camera SceneCamera;
        public GameObject TestCursor;
        private Modificator terrainModificator;
        private Vector3 oldEventPos;
        private float currentIntensity = 0;
        
        private void InitModificator()
        {
            if(terrainModificator == null)
            {
                MyTerrainV2 t = MyTerrainV2.GetTerrain();
                terrainModificator = new Modificator(t);
            }
        }
        // Use this for initialization
        void Start()
        {
            //Seta os delegates que vão tratar dos clicks
            ButtonLowerTerrain.onClick.AddListener(delegate
            {
                terrainModificator.SetModificationStrategy(new SimpleTerrainModificationStrategy(MyModificationStrategyV2.ElevationChange.Lower));
            });
            ButtonRaiseTerrain.onClick.AddListener(delegate
            {
                terrainModificator.SetModificationStrategy(new SimpleTerrainModificationStrategy(MyModificationStrategyV2.ElevationChange.Raise));
            });
        }

        private float CalculateIntensity(Vector3 currentMousePosInSC)
        {
            if (currentMousePosInSC.Equals(oldEventPos))
                return currentIntensity + 0.1f;
            else
                return currentIntensity;
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
                    currentIntensity = CalculateIntensity(hit.point);
                    //TODO: Fazer a 1a invocação de uma estratégia, no objeto modificador, na camada do modelo.
                    //if (currentModificationStrategy != null)
                    //    currentModificationStrategy.Execute(hit.point, currentIntensity, GetComponent<MyTerrain>());
                    oldEventPos = hit.point;
                }
            }
        }

        private void OnMouseUp()
        {
            currentIntensity = 0;
        }
        // Update is called once per frame
        void Update()
        {
            InitModificator();
        }
    }

}