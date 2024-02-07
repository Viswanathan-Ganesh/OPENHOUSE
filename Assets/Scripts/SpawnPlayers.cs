using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;

    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;
    public float yPos;


    private void Start()
    {
        Vector3 randomPos = new Vector3(Random.Range(minX, maxX), yPos, Random.Range(minZ, maxZ));
        PhotonNetwork.Instantiate(playerPrefab.name, randomPos, Quaternion.identity);
    }
}
