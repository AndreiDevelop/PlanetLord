using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Props : MonoBehaviour 
{
	private PropsManager _propsManager = null;

	void Start()
	{
		_propsManager = transform.parent.GetComponent<PropsManager> ();
	}

	public void OnTriggerEnter(Collider col)
	{
		if(col.tag == TagConstants.PLAYER)
			_propsManager.ActionDeleteProps (gameObject);
	}
}
