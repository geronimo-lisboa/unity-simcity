using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClick : MonoBehaviour {
    private void OnMouseDown()
    {
        if (Input.GetMouseButton(0))
        {
            Debug.Log(gameObject.name);
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
