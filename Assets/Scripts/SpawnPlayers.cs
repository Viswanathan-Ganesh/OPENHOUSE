using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject thiefPrefab;
    public GameObject copPrefab;
    [SerializeField] private int[] randomList;
    public int maxPlayerCount = 4;

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
        //PhotonNetwork.Instantiate(thisfPrefab.name, randomPos, Quaternion.identity);
        //PhotonNetwork.PlayerList[0].UserId

        ///PhotonNetwork.LocalPlayer.UserId == ;

        /*
        foreach(Player player in PhotonNetwork.PlayerList)
        {
            if(player.CustomProperties == thief)
            {
                PhotonNetwork.Instantiate(thiefPrefab.name, randomPos, Quaternion.identity);
            }
            else if (player.CustomProperties == cop)
            {
                PhotonNetwork.Instantiate(copPrefab.name, randomPos, Quaternion.identity);
            }
            
        }
        */
    }

    public void Assigner()
    {
        int rand = Random.Range(0, maxPlayerCount);

        for (int i = 0; i < maxPlayerCount; i++) 
        {
            if (i != rand)
                randomList[i] = 0;
            else randomList[rand] = 1;
        }
    }
}
