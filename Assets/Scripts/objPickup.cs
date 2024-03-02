using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.Demo.PunBasics;
using Autodesk.Fbx;

public class objPickup : MonoBehaviour
{
    public GameObject crosshair1, crosshair2;
    public bool interactable, pickedup;
    //public Rigidbody objRigidbody;
    public float throwAmount;
    private GameObject playerDummy;
    public Vector3 offset;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerDummy = other.GetComponent<FirstPersonController>().dummy;
            crosshair1.SetActive(false);
            crosshair2.SetActive(true);
            interactable = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (pickedup == false)
            {
                crosshair1.SetActive(true);
                crosshair2.SetActive(false);
                interactable = false;
            }
            if (pickedup == true)
            {
                transform.parent = null;
                //objRigidbody.useGravity = true;
                crosshair1.SetActive(true);
                crosshair2.SetActive(false);
                interactable = false;
                pickedup = false;
            }
        }
    }

    void Update()
    {
        if (interactable == true)
        {
            if (Input.GetKey(KeyCode.E))
            {
                //transform.parent = Camera.main.transform;
                //transform.SetParent(Camera.main.transform);
                //objRigidbody.useGravity = false;
                pickedup = true;
            }
            if (pickedup)
            {
                Debug.Log("Afsas");
                transform.position = playerDummy.transform.position + offset;
            }
            /*
            if (Input.GetMouseButtonUp(0))
            {
                transform.SetParent(null);
                transform.position += new Vector3(0f, -9.81f, 0f);
                //useGravity = true;
                pickedup = false;
            }
            */
        }
    }
            /*
            public GameObject crosshair1, crosshair2;
            public Transform objTransform;
            public bool interactable, pickedup;
            //public Rigidbody objRigidbody;
            public float throwAmount;

            void OnTriggerStay(Collider other)
            {
                if (other.CompareTag("MainCamera"))
                {
                    crosshair1.SetActive(false);
                    crosshair2.SetActive(true);
                    interactable = true;
                }
            }
            void OnTriggerExit(Collider other)
            {
                if (other.CompareTag("MainCamera"))
                {
                    if (pickedup == false)
                    {
                        crosshair1.SetActive(true);
                        crosshair2.SetActive(false);
                        interactable = false;
                    }
                    if (pickedup == true)
                    {
                        objTransform.parent = null;
                        //objRigidbody.useGravity = true;
                        crosshair1.SetActive(true);
                        crosshair2.SetActive(false);
                        interactable = false;
                        pickedup = false;
                    }
                }
            }

            void Update()
            {
                if (interactable == true)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        objTransform.parent = Camera.main.transform;
                        //objRigidbody.useGravity = false;

                        pickedup = true;
                    }
                    if (Input.GetMouseButtonUp(0))
                    {
                        objTransform.parent = null;
                        //objRigidbody.useGravity = true;
                        pickedup = false;
                    }
                    if (pickedup == true)
                    {
                        if (Input.GetMouseButtonDown(1))
                        {
                            objTransform.parent = null;
                            //objRigidbody.useGravity = true;
                            //objRigidbody.velocity = cameraTrans.forward * throwAmount * Time.deltaTime;
                           // objRigidbody.AddForce(cameraTrans.forward * throwAmount * Time.deltaTime);
                            pickedup = false;
                        }
                    }
                }
            }
            */
        }