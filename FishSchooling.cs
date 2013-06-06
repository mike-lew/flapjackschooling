using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CapsuleCollider))]
[RequireComponent (typeof (Rigidbody))]
public class FishSchooling : MonoBehaviour {
	public float speed;
	public float turnSpeed = 15.0f;
	
	public static float separationDensity = 0.4f;
	public static float cohesionDensity = 0.3f;
	public static float alignmentDensity = 0.5f;
	
	public static float separationDistance = 10.0f;
	public static float cohesionMaxDistance = 20.0f;
	public static float cohesionMinDistance = 5.0f;
	public static float alignmentDistance = 10.0f;
	
	private float wallSeparationDensity = 1.0f;
	private float wallSeparationDistance = 10.0f;
	private float wallLocations = 25.0f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 separation = Vector3.zero;
		Vector3 cohesion = Vector3.zero;
		Vector3 alignment = Vector3.zero;
		Vector3 walls = Vector3.zero;
		
		foreach (FishSchooling fish in FishSpawner.fish) {
			GameObject f = fish.gameObject;
			Vector3 distanceVector = transform.position - f.transform.position;
			float distanceMagnitude = distanceVector.sqrMagnitude;
			Vector3 betweenVector = distanceVector.normalized;
			char ally = FishSpawner.getFishTypeDiplomacy(gameObject, f);
			if (distanceMagnitude < separationDistance) {
				separation += betweenVector * distanceMagnitude / separationDistance;
			}
			if (distanceMagnitude > cohesionMinDistance && distanceMagnitude < cohesionMaxDistance && 
				(ally == 'f')) {
				cohesion += betweenVector * -1 * distanceMagnitude / (cohesionMaxDistance - cohesionMinDistance);
			}
			if (distanceMagnitude < alignmentDistance && (ally == 'n' || ally == 'f')) {
				alignment += f.transform.forward / separationDistance;
			}
		}
		if (transform.position.x > wallLocations - wallSeparationDistance) {
			walls += new Vector3(-1.0f / (wallLocations - transform.position.x), 0.0f, 0.0f);
		}
		else if (transform.position.x < -1.0f * wallLocations + wallSeparationDistance) {
			walls += new Vector3(-1.0f / (-1.0f * wallLocations + transform.position.x), 0.0f, 0.0f);
		}
		if (transform.position.y > wallLocations - wallSeparationDistance) {
			walls += new Vector3(0.0f, -1.0f / (wallLocations - transform.position.y), 0.0f);
		}
		else if (transform.position.y < -1.0f * wallLocations + wallSeparationDistance) {
			walls += new Vector3(0.0f, -1.0f / (-1.0f * wallLocations + transform.position.y), 0.0f);
		}
		if (transform.position.z > wallLocations - wallSeparationDistance) {
			walls += new Vector3(0.0f, 0.0f, -1.0f / (wallLocations - transform.position.z));
		}
		else if (transform.position.z < -1.0f * wallLocations + wallSeparationDistance) {
			walls += new Vector3(0.0f, 0.0f, -1.0f / (-1.0f * wallLocations + transform.position.z));
		}
		
		separation = separation.normalized * separationDensity;
		cohesion = cohesion.normalized * cohesionDensity;
		alignment = alignment.normalized * alignmentDensity;
		walls = walls.normalized * wallSeparationDensity;
		
		Vector3 schoolingVector = (transform.forward) + (separation + cohesion + alignment + walls) * 
			Time.fixedDeltaTime * turnSpeed;
		transform.forward = (schoolingVector).normalized;
		
		rigidbody.velocity = transform.forward * speed;
	}
}
