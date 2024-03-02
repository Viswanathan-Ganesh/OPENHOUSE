using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Camera.main.transform.Rotate(Vector3.right, -Input.GetAxis("Mouse Y") * 10f);
        Camera.main.transform.Rotate(Camera.main.transform.InverseTransformDirection(Vector3.up), Input.GetAxis("Mouse X")*10f);
      

    }
}
