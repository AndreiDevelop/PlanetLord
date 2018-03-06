using UnityEngine;
using UnityEngine.UI;

public class PhotonNetworkingManager : MonoBehaviour
{
	public delegate void NetworkStateHandler();
	public static event NetworkStateHandler OnJoinRoom;
	public static event NetworkStateHandler OnCreateRoom;

	private const string CONNECTED_TO = "Andrei01";
	private const string ROOM_NAME = "New room";

	[SerializeField]
	private Text _textInfo = null;

	[SerializeField]
	private GameObject _lobbyCamera = null;

	[SerializeField]
	private GameObject _player = null;
	[SerializeField]
	private Transform _playerSpawnPoint = null;
	[SerializeField]
	private GameObject _world = null;

	void Start () 
	{
		PhotonNetwork.ConnectUsingSettings (CONNECTED_TO);
	}

	public virtual void OnJoinedLobby()
	{
		//PhotonNetwork.autoCleanUpPlayerObjects = false;
		PhotonNetwork.automaticallySyncScene = true;
		Debug.Log ("We joined lobby");
		PhotonNetwork.JoinOrCreateRoom (ROOM_NAME, null, null);
	}

	public virtual void OnCreatedRoom()
	{
		if (OnCreateRoom != null)
			OnCreateRoom ();
	}

	public virtual void OnJoinedRoom()
	{
		GameObject player = PhotonNetwork.Instantiate (_player.name, _playerSpawnPoint.position, _playerSpawnPoint.rotation, 0);

		Vector3 newPosition = PositionalManager.CalculatePosionReplaceObjectOnPlanet (player.transform, _world.transform);
		PositionalManager.ReplaceObjectOnPlanet (ref player, _world, newPosition);

		_lobbyCamera.SetActive (false);

		if (OnJoinRoom != null)
			OnJoinRoom ();
	}

	// Update is called once per frame
//	void Update () 
//	{
//		_textInfo.text = PhotonNetwork.connectionStateDetailed.ToString ();
//	}
}
