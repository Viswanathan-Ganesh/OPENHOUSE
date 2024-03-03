using Photon.Pun;
using UnityEngine;

public class objPickup : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField] private GameObject crosshair1;
    [SerializeField] private GameObject crosshair2;
    [SerializeField] private bool interactable;
    [SerializeField] private bool pickedup;
    [SerializeField] private float throwAmount;
    [SerializeField] private Vector3 offset;

    private GameObject playerDummy;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && photonView.IsMine)
        {
            playerDummy = other.GetComponent<FirstPersonController>().dummy;
            crosshair1.SetActive(false);
            crosshair2.SetActive(true);
            interactable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && photonView.IsMine)
        {
            if (!pickedup)
            {
                crosshair1.SetActive(true);
                crosshair2.SetActive(false);
            }
            else
            {
                photonView.RPC("DropObject", RpcTarget.All);
            }
        }
    }

    private void Update()
    {
        if (interactable && photonView.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                photonView.RPC("PickupObject", RpcTarget.All);
            }

            if (pickedup)
            {
                transform.position = playerDummy.transform.position + offset;
            }
        }
    }

    [PunRPC]
    private void PickupObject()
    {
        transform.SetParent(playerDummy.transform);
        pickedup = true;
    }

    [PunRPC]
    private void DropObject()
    {
        transform.SetParent(null);
        pickedup = false;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // Add serialization logic here if needed
    }
}

/*
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
    private PhotonView photonView;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerDummy = other.GetComponent<FirstPersonController>().dummy;
            photonView = other.GetComponent<PhotonView>();
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
                //interactable = false;
            }
            if (pickedup == true)
            {
                transform.parent = null;
                //objRigidbody.useGravity = true;
                crosshair1.SetActive(true);
                crosshair2.SetActive(false);
                //interactable = false;
                pickedup = false;
            }
        }
    }

    void Update()
    {
        if (interactable == true && photonView.IsMine)
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
            */
            /*
            if (Input.GetMouseButtonUp(0))
            {
                transform.SetParent(null);
                transform.position += new Vector3(0f, -9.81f, 0f);
                //useGravity = true;
                pickedup = false;
            }
            
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
            
        }
*/