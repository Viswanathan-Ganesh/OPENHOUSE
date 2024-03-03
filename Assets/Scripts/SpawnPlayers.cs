using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject thiefPrefab;
    public GameObject copPrefab;
    [SerializeField] private int maxPlayer;
    private bool dummy = true;

    public Hashtable customPropCop = new Hashtable();
    public Hashtable customPropThief = new Hashtable();

    void Awake(){
        customPropCop.Add("Team", "Cop");
        customPropThief.Add("Team", "Thief");
    }
    
                
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;
    public float yPos;

    
    public void Update()
    {
        Debug.Log("Updating");
        if (PhotonNetwork.InRoom) // Checks if the player is in room
        {
            Debug.LogError("InRoom");
            if (PhotonNetwork.CurrentRoom.PlayerCount == maxPlayer && dummy) // if current room's player count == maxplayer and dummy var is true
            {
                Debug.LogError("MaxPlayers reached");
                
                //  CopSelector(); // to assign different roles randomly
                foreach(Player player in PhotonNetwork.PlayerList)
                {
                    Vector3 randomPos = new Vector3(Random.Range(minX, maxX), yPos, Random.Range(minZ, maxZ)); // random position
                    if (PhotonNetwork.LocalPlayer.UserId == player.UserId) // if the local player instance has the same userid as the instance having assigned cop
                    {
                        PhotonNetwork.Instantiate(copPrefab.name, randomPos, Quaternion.identity); // then it instatantiates cop in that specific scene only
                        Debug.LogError("Player Spawned");
                    }
                    /*
                    
                    if (player.CustomProperties.ContainsKey("Team") && (string)player.CustomProperties["Team"] == "Cop") // if the player has cop assigned
                    {
                        Debug.LogError("Cop Matched");
                        if (PhotonNetwork.LocalPlayer.UserId == player.UserId) // if the local player instance has the same userid as the instance having assigned cop
                        {
                            PhotonNetwork.Instantiate(copPrefab.name, randomPos, Quaternion.identity); // then it instatantiates cop in that specific scene only
                            Debug.LogError("Cop Spawned");
                        }
                        dummy = false;
                    }
                    else if (player.CustomProperties.ContainsKey("Team") && (string)player.CustomProperties["Team"] == "Thief") // works the same as above
                    {
                        Debug.LogError("Thief Matched");
                        if (PhotonNetwork.LocalPlayer.UserId == player.UserId)
                        {
                            PhotonNetwork.Instantiate(thiefPrefab.name, randomPos, Quaternion.identity);
                            Debug.LogError("Thief Spawned");
                        }
                        dummy = false;
                    }
                    */
                }
                /*
                for (int i = 0; i < maxPlayer; i++)
                {
                    Debug.LogError(i);
                    Vector3 randomPos = new Vector3(Random.Range(minX, maxX), yPos, Random.Range(minZ, maxZ)); // random position
                    
                    if (PhotonNetwork.PlayerList[i].CustomProperties == customPropCop) // if the player has cop assigned
                    {
                        Debug.LogError("Cop Matched");
                        if (PhotonNetwork.LocalPlayer.UserId == PhotonNetwork.PlayerList[i].UserId) // if the local player instance has the same userid as the instance having assigned cop
                        {
                            PhotonNetwork.Instantiate(copPrefab.name, randomPos, Quaternion.identity); // then it instatantiates cop in that specific scene only
                            Debug.LogError("Cop Spawned");
                        }
                    }
                    else if (PhotonNetwork.PlayerList[i].CustomProperties == customPropThief) // works the same as above
                    {
                        Debug.LogError("Thief Matched");
                        if (PhotonNetwork.LocalPlayer.UserId == PhotonNetwork.PlayerList[i].UserId)
                        {
                            PhotonNetwork.Instantiate(thiefPrefab.name, randomPos, Quaternion.identity);
                            Debug.LogError("Thief Spawned");
                        }
                    }
                
                 }*/
                dummy = false;
            }
        }

    }
    
    
    public void CopSelector()
    {
        Debug.LogError("CopSelector");
        int rand = Random.Range(0, maxPlayer);

        if (PhotonNetwork.InRoom) // Check if connected to a room
        {
            int i = 0;
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                Debug.LogError(player); 
                if (i == rand)
                {
                    Debug.LogError(customPropCop);
                    player.SetCustomProperties(customPropCop);
                }
                else
                {
                    Debug.LogError(customPropThief);
                    player.SetCustomProperties(customPropThief);
                }
                i++;
                Debug.LogError(player.CustomProperties);
            }
        }

    }
}
