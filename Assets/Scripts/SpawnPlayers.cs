using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject theifPrefab;
    public GameObject copPrefab;

    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;
    public float yPos;


    private void Start()
    {
        Hashtable cop = new Hashtable();
        Hashtable thief = new Hashtable();

        cop.Add("Team", "Cop");
        thief.Add("Team", "Thief");

        Vector3 randomPos = new Vector3(Random.Range(minX, maxX), yPos, Random.Range(minZ, maxZ));
        foreach(Player player in PhotonNetwork.PlayerList)
        {
            if(player.CustomProperties == thief)
            {
                PhotonNetwork.Instantiate(theifPrefab.name, randomPos, Quaternion.identity);
            }
            else if (player.CustomProperties == cop)
            {
                PhotonNetwork.Instantiate(copPrefab.name, randomPos, Quaternion.identity);
            }
            
        }
    }
}
