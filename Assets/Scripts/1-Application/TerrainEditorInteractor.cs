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
        
        private void InitModificator()
        {
            if(terrainModificator == null)
            {
                MyTerrain t = MyTerrain.GetTerrain();
                terrainModificator = new Modificator(t);
            }
        }
        // Use this for initialization
        void Start()
        {
            //Seta os delegates que vão tratar dos clicks
            ButtonLowerTerrain.onClick.AddListener(delegate
            {
                terrainModificator.SetModificationStrategy(
                    new SimpleTerrainModificationStrategy(TerrainModificationStrategy.ElevationChange.Lower)
                    );
            });
            ButtonRaiseTerrain.onClick.AddListener(delegate
            {
                terrainModificator.SetModificationStrategy(
                    new SimpleTerrainModificationStrategy(TerrainModificationStrategy.ElevationChange.Raise)
                    );
            });
        }


        private void OnMouseDown()
        {
            //Vector3 mousePosInScreenCoordinate = Input.mousePosition;
            //oldEventPos = mousePosInScreenCoordinate;
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
                    if(oldEventPos.Equals(hit.point))
                    {
                        terrainModificator.IncreaseIntensity(0.1f);
                    }
                    terrainModificator.Modify(mouseRay, hit);
                    oldEventPos = hit.point;
                }
            }
        }

        private void OnMouseUp()
        {
            terrainModificator.ResetIntensity();
        }
        // Update is called once per frame
        void Update()
        {
            InitModificator();
        }
    }

}