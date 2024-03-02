using UnityEngine;
using Photon.Pun;

public class PickUpSpawn : MonoBehaviour
{
    public GameObject pickUp;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.Instantiate(pickUp.name, transform.position, Quaternion.identity);
    }

}
