using UnityEngine;
using System.Collections.Generic;

public class WorldHolder : MonoBehaviour 
{
	public delegate void WorldHolderHandler();
	public event WorldHolderHandler OnWorldHorderHandlerInitialized;

	[SerializeField]
	private List<GameObject> _worldsList = new List<GameObject> ();

	private Transform _curTransform = null;

	public GameObject GetWorld(int i)
	{
		GameObject returnGameObject = null;

		if (i > 0 && i < _worldsList.Count)
			returnGameObject = _worldsList[i];

		return returnGameObject;
	}
		
	public int WorldsCount
	{
		get 
		{
			return _worldsList.Count;
		}
	}

	void Start () 
	{
		_curTransform = transform;

		for (int i = 0; i < _curTransform.childCount; i++) 
		{
			_worldsList.Add (_curTransform.GetChild (i).gameObject);
		}

		if (OnWorldHorderHandlerInitialized != null)
			OnWorldHorderHandlerInitialized ();
	}
}
