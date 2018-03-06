using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class PropsManager : MonoBehaviour
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
		
	public bool CanAddProps
	{
		get 
		{
			return PropsCount < _requireCount;
		}
	}

	public void InitializePropsInPropsList(GameObject newProps)
	{
		newProps.transform.SetParent (_curTransform);
		_propsList.Add (newProps);
	}

	void OnEnable()
	{
		PhotonNetworkingManager.OnCreateRoom += CreateProps;
	}

	void OnDisable()
	{
		PhotonNetworkingManager.OnCreateRoom -= CreateProps;
	}

	void Start()
	{
		_curTransform = transform;
	}

	private void CreateProps () 
	{
		while (PropsCount < _requireCount) 
		{
			AddProps ();
		} 
	}

	private void SetProps () 
	{
		while (PropsCount < _requireCount) 
		{
			var propsFromCache = PhotonNetwork.PrefabCache.Where (x => string.Equals(x.Key,_propsPrefab.name));

			foreach (var curProps in propsFromCache)
			{
				Debug.Log (curProps.Value.gameObject.name);
				//InitializePropsInPropsList (curProps.Value.gameObject);
			}
		} 
	}

	public virtual void AddProps()
	{
		Vector3 newPosition = PositionalManager.CalculatePosionReplaceObjectOnPlanet (_propsPrefab.transform, _world.transform);

		GameObject newProps = PhotonNetwork.InstantiateSceneObject(_propsPrefab.name, Vector3.zero, Quaternion.identity, 0, null);
		PositionalManager.ReplaceObjectOnPlanet (ref newProps, _world, newPosition);

		InitializePropsInPropsList (newProps);
	}
		
	public virtual void RemoveProps(GameObject props)
	{
		int propsIndex = _propsList.FindIndex (item => item.Equals (props));

		PhotonNetwork.Destroy (_propsList [propsIndex].gameObject);

		_propsList.RemoveAt (propsIndex);

		AddProps ();
	}
}
