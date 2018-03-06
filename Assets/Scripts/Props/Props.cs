using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Props : MonoBehaviour 
{
	private PropsManager _propsManager = null;

	void Start()
	{
		_propsManager = FindObjectOfType<PropsManager> ();

		if (_propsManager.CanAddProps)
			_propsManager.InitializePropsInPropsList (gameObject);
	}

	public void OnTriggerEnter(Collider col)
	{
		if(col.tag == TagConstants.PLAYER && _propsManager!=null)
			_propsManager.RemoveProps (gameObject);
	}
}
