using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using ExitGames.Client.Photon;
using Photon.Pun.Demo.Cockpit;
using ExitGames.Client.Photon.StructWrapping;

public class CreateAndJoinRoom : MonoBehaviourPunCallbacks
{
    public TMP_InputField createInput;
    public TMP_InputField joinInput;
    [SerializeField] private int maxPlayer;
    private bool dummy = true;
    public GameObject thiefPrefab;
    public GameObject copPrefab;

    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;
    public float yPos;

    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = maxPlayer;
        PhotonNetwork.CreateRoom(createInput.text, roomOptions);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
    }
    
    /*
    public void Update()
    {
        Debug.Log("Updating");
        if (PhotonNetwork.InRoom) // Checks if the player is in room
        {
            Debug.Log("InRoom");
            if (PhotonNetwork.CurrentRoom.PlayerCount == maxPlayer && dummy) // if current room's player count == maxplayer and dummy var is true
            {
                Debug.Log("MaxPlayers reached");
                Hashtable customPropCop = new Hashtable();
                Hashtable customPropThief = new Hashtable();

                customPropCop.Add("Team", "Cop");
                customPropThief.Add("Team", "Thief");
                
                CopSelector(); // to assign different roles randomly

                for (int i = 0; i < maxPlayer; i++)
                {
                    Vector3 randomPos = new Vector3(Random.Range(minX, maxX), yPos, Random.Range(minZ, maxZ)); // random position
                    
                    if (PhotonNetwork.PlayerList[i].CustomProperties == customPropCop) // if the player has cop assigned
                    {
                        if (PhotonNetwork.LocalPlayer.UserId == PhotonNetwork.PlayerList[i].UserId) // if the local player instance has the same userid as the instance having assigned cop
                        {
                            PhotonNetwork.Instantiate(copPrefab.name, randomPos, Quaternion.identity); // then it instatantiates cop in that specific scene only
                            Debug.Log("Cop Spawned");
                        }       
                    }
                    else if(PhotonNetwork.PlayerList[i].CustomProperties == customPropThief) // works the same as above
                    {
                        if (PhotonNetwork.LocalPlayer.UserId == PhotonNetwork.PlayerList[i].UserId)
                        {
                            PhotonNetwork.Instantiate(thiefPrefab.name, randomPos, Quaternion.identity);
                            Debug.Log("Thief Spawned");
                        }
                    }
                }
                dummy = false;
            }
        }
        
    }
    */

    public override void OnJoinedRoom()
    {
        //PhotonNetwork.LoadLevel("LobbyLevel");
        PhotonNetwork.LoadLevel("Level0");
        //PhotonNetwork.AutomaticallySyncScene = true;
    }
    /*
    public void CopSelector()
    {
        int rand = Random.Range(0, maxPlayer);
        Hashtable customPropCop = new Hashtable();
        Hashtable customPropThief = new Hashtable();

        customPropCop.Add("Team", "Cop");
        customPropThief.Add("Team", "Thief");

        if(PhotonNetwork.InRoom) // Check if connected to a room
        {
            int i = 0;
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                if (i == rand)
                {
                    player.SetCustomProperties(customPropCop);
                }
                else
                {
                    player.SetCustomProperties(customPropThief);
                }
                i++;
            }
        }

    }
    */
}
