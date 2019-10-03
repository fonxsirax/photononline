using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PhotonLobby : MonoBehaviourPunCallbacks
{
    public static PhotonLobby lobby;
    public GameObject battleButton;

    public GameObject cancelButton;


    private void Awake()
    {
        lobby = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();  // connects with the master server.
    }

    public override void OnConnectedToMaster() {
        Debug.Log("Player has connected to masters server");
        PhotonNetwork.AutomaticallySyncScene = true;
        battleButton.SetActive(true);
    }
    public void OnBattleButtonClicked() {
        Debug.Log("Battle Button has clicked");
        battleButton.SetActive(false);
        cancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Tried to join a random game buy fails. There must be no open games available");
        CreateRoom();
    }
    public void CreateRoom() {
        Debug.Log("Trying to create a new room");
        int randomRoomName = Random.Range(0,10000);
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)MultiplayerSettings.multiplayerSettings.maxPlayers};
        PhotonNetwork.CreateRoom("Room "+randomRoomName,roomOps);  //trying to create a room with the specified values.

    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Tried to create a room but failed. There must be a room with the same name");
        CreateRoom();
    }
    public void OnCancelButtonClicked() {
        Debug.Log("Cancel button clicked");
        cancelButton.SetActive(false);
        battleButton.SetActive(true);
        PhotonNetwork.LeaveRoom();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
