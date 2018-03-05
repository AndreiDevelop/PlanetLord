using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class PropsManager : Photon.MonoBehaviour
{
	[SerializeField]
	private GameObject _propsPrefab = null;

	[SerializeField]
	private List<GameObject> _propsList = new List<GameObject> ();

	[SerializeField]
	private int _requireCount = 0;

	[SerializeField]
	private GameObject _world = null;

	private Transform _curTransform = null;

	public int PropsCount
	{
		get
		{
			return _propsList.Count;
		}
	}
		
	void OnEnable()
	{
		PhotonNetworkingManager.OnJoinRoom += CustomStart;
	}

	void OnDisable()
	{
		PhotonNetworkingManager.OnJoinRoom -= CustomStart;
	}

	private void CustomStart () 
	{
		_curTransform = transform;

		Debug.Log (PhotonNetwork.countOfPlayers);

		if (PhotonNetwork.countOfPlayers < 2) 
		{
			while (PropsCount < _requireCount) 
			{
				ActionAddProps ();
			} 
		}
	}

	public virtual void ActionDeleteProps(GameObject props)
	{
		int findIndex = _propsList.FindIndex (item => item.Equals (props));
		RemoveProps(findIndex);
	}

	public virtual void ActionAddProps()
	{
		Vector3 newPosition = PositionalManager.CalculatePosionReplaceObjectOnPlanet (_propsPrefab.transform, _world.transform);

		AddProps(newPosition);
	}
		
	public virtual void AddProps(Vector3 position)
	{
		GameObject newProps = PhotonNetwork.Instantiate(_propsPrefab.name, Vector3.zero, Quaternion.identity, 0);
		newProps.transform.SetParent (_curTransform);

		PositionalManager.ReplaceObjectOnPlanet (ref newProps, _world, position);

		_propsList.Add (newProps);

		Debug.Log ("Add ");
	}
		
	public virtual void RemoveProps(int propNum)
	{
		PhotonNetwork.Destroy (_propsList [propNum].gameObject);

		_propsList.RemoveAt (propNum);

		Debug.Log ("Remove " + propNum);
		ActionAddProps ();

//		photonView.RPC("dfshdftghf",PhotonTargets.AllBuffered)
//		{
//			
//		}

	}
}
