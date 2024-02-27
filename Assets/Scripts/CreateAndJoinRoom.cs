using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using ExitGames.Client.Photon;
using Photon.Pun.Demo.Cockpit;

public class CreateAndJoinRoom : MonoBehaviourPunCallbacks
{
    public TMP_InputField createInput;
    public TMP_InputField joinInput;
    [SerializeField] private int maxPlayer;
    private bool dummy = true;

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
    

    public void Update()
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount == maxPlayer)
        {

        }
        /*
        if (PhotonNetwork.CurrentRoom.PlayerCount == maxPlayer && dummy)
        {
            //PhotonNetwork.SetMasterClient(PhotonNetwork.PlayerList[maxPlayer]);
            CopSelector();
            PhotonNetwork.LoadLevel("Level0");
            dummy = false;
        }
        */
    }
    
    public override void OnJoinedRoom()
    {
        //PhotonNetwork.LoadLevel("LobbyLevel");
        PhotonNetwork.LoadLevel("Level0");
        //PhotonNetwork.AutomaticallySyncScene = true;
    }

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
}
