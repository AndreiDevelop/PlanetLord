using UnityEngine;

[RequireComponent (typeof (PhotonView))]
public class PlayerNetwork : MonoBehaviour 
{
	[SerializeField]
	private GameObject _playerCamera = null;
	[SerializeField]
	private MonoBehaviour []_playerControllScript = null;

	private PhotonView _photonView;

	void Start () 
	{
		_photonView = GetComponent<PhotonView> ();
		Initialize ();
	}

	private void Initialize () 
	{
		if (_photonView.isMine) 
		{
		} 
		else 
		{
			_playerCamera.SetActive (false);

			foreach (MonoBehaviour behavior in _playerControllScript)
				behavior.enabled = false;
		}
	}
}
