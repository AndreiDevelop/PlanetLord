using UnityEngine;
using UnityEngine.UI;

public class PhotonNetworkingManager : MonoBehaviour
{
	private const string CONNECTED_TO = "Andrei01";
	private const string ROOM_NAME = "New room";

	[SerializeField]
	private Text _textInfo = null;
	[SerializeField]
	private GameObject _player = null;
	[SerializeField]
	private Transform _playerSpawnPoint = null;
	[SerializeField]
	private GameObject _lobbyCamera = null;


	void Start () 
	{
		PhotonNetwork.ConnectUsingSettings (CONNECTED_TO);
	}

	public virtual void OnJoinedLobby()
	{
		Debug.Log ("We joined lobby");
		PhotonNetwork.JoinOrCreateRoom (ROOM_NAME, null, null);
	}

	public virtual void OnJoinedRoom()
	{
		PhotonNetwork.Instantiate (_player.name, _playerSpawnPoint.position, _playerSpawnPoint.rotation, 0);
		_lobbyCamera.SetActive (false);
	}

	// Update is called once per frame
//	void Update () 
//	{
//		_textInfo.text = PhotonNetwork.connectionStateDetailed.ToString ();
//	}
}
