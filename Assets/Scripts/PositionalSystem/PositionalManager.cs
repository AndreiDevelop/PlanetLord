using UnityEngine;

public class PositionalManager
{
	 public static void ReplaceObjectOnPlanet (ref GameObject objectToReplace, GameObject planet) 
	 {
		Transform objectToPlaceTransform = objectToReplace.transform;

		Vector3 newPosition = Random.onUnitSphere * ((planet.transform.localScale.x/2) + objectToPlaceTransform.localScale.y * 0.5f) + planet.transform.position;
		Quaternion newRotation = Quaternion.identity;

		objectToPlaceTransform.position = newPosition;
		objectToPlaceTransform.rotation = newRotation;

		objectToPlaceTransform.LookAt(planet.transform);
		objectToPlaceTransform.Rotate(-90, 0, 0);
     }
}
