using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class MouseClick : MonoBehaviour {
    public Camera SceneCamera;
    private void OnMouseDown()
    {
        Debug.Assert(SceneCamera != null, "Camera não pode ser null");
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosInScreenCoordinate = Input.mousePosition;
            Ray mouseRay = SceneCamera.ScreenPointToRay(mousePosInScreenCoordinate);
            RaycastHit hit;
            if(GetComponent<Collider>().Raycast(mouseRay,out hit, Mathf.Infinity))
            {
                Debug.Log(hit);
            }
            else
            {
                Debug.Log("no clic");
            }
            //Debug.Log(gameObject.name);
        }
    }


    //private void Update()
    //{
    //    var up = transform.TransformDirection(Vector3.up);
    //    RaycastHit hit;
    //    Debug.DrawRay(transform.position, -up * 2, Color.green);
    //    if (Physics.Raycast(transform.position, -up, out hit, 2))
    //    {
    //        Debug.Log(System.DateTime.Now +" HIT " + hit.collider.gameObject.name);
    //    }

    //}

    //function Update()
    //{
    //    var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    var hit : RaycastHit;
    //    if (Physics.Raycast(ray, hit))
    //    {
    //        if (hit.collider.tag == "clickableCube"{
    //            //hit.collider.gameObject now refers to the 
    //            //cube under the mouse cursor if present
    //        }
    //    }
    //}
}
