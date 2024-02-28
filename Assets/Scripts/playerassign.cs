using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;


public class Playerassign : MonoBehaviour
{
    public GameObject thiefPrefab;
    public GameObject copPrefab;
    public GameObject[] playerprefabs;
    [SerializeField] private int maxPlayer;
    public int copplayerno;

    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;
    public float yPos;

public void Start(){
copplayerno = Random.Range(1,maxPlayer+1);
for(int j=0 ; j< maxPlayer;j++){
    if(j== copplayerno){
        playerprefabs[j] = copPrefab;
    }
    else{
        playerprefabs[j] = thiefPrefab;
    }
}

assignplayers();
}

public void assignplayers(){
    if(PhotonNetwork.IsConnectedAndReady){
      if(PhotonNetwork.InRoom){
        if (PhotonNetwork.CurrentRoom.PlayerCount == maxPlayer){
            for(int i =0; i<maxPlayer;i++){
            Vector3 randomPos = new Vector3(Random.Range(minX, maxX), yPos, Random.Range(minZ, maxZ));
             PhotonNetwork.Instantiate(playerprefabs[i].name, randomPos, Quaternion.identity);
}
    }
}
else
{assignplayers();
}
    }
    else{
         assignplayers();
    }
}

}


