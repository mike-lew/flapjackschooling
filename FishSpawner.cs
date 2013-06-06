using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FishSpawner : MonoBehaviour {
	public static List<FishSchooling> fish;
	public static int numFish = 20;
	public GameObject fishModel;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < numFish; i++) {
			createNewFish();
		}
		fish = new List<FishSchooling>(GameObject.FindObjectsOfType(typeof(FishSchooling)) as FishSchooling[]);
	}
	
	private void createNewFish() {
		GameObject capsule = Instantiate(fishModel) as GameObject;
		capsule.transform.position = new Vector3(Random.Range(-20.0f, 20.0f), Random.Range(-20.0f, 20.0f), Random.Range(-20.0f, 20.0f));
		capsule.transform.LookAt(transform);
		capsule = setFishType(capsule);
		capsule.AddComponent("Rigidbody");
		capsule.rigidbody.useGravity = false;
		capsule.AddComponent("FishSchooling");
		FishSchooling schooling = capsule.GetComponent<FishSchooling>();
		schooling.speed = Random.Range(3.0f, 6.0f);
	}
	
	public void ensureFishNum(int num) {
		int diff = num - fish.Count;
		print (diff);
		if (diff < 0) {
			for (int i = 0; i < Mathf.Abs(diff); i++) {
				GameObject f = fish[0].gameObject;
				fish.RemoveAt(0);
				DestroyImmediate(f);
			}
			fish = new List<FishSchooling>(GameObject.FindObjectsOfType(typeof(FishSchooling)) as FishSchooling[]);
		}
		else if (diff > 0) {
			for (int i = 0; i < diff; i++) {
				createNewFish();
			}
			fish = new List<FishSchooling>(GameObject.FindObjectsOfType(typeof(FishSchooling)) as FishSchooling[]);
		}
	}
	
	private GameObject setFishType(GameObject capsule) {
		int r = Random.Range(0, 4);
		switch (r) {
			case 0:
				capsule.tag = "Blue Fish";
				capsule.transform.GetChild(0).renderer.material = Resources.Load("BlueFishMat", typeof(Material)) as Material;
				break;
			case 1:
				capsule.tag = "Green Fish";
				capsule.transform.GetChild(0).renderer.material = Resources.Load("GreenFishMat", typeof(Material)) as Material;
				break;
			case 2:
				capsule.tag = "Yellow Fish";
				capsule.transform.GetChild(0).renderer.material = Resources.Load("YellowFishMat", typeof(Material)) as Material;
				break;
			case 3:
				capsule.tag = "Red Fish";
				capsule.transform.GetChild(0).renderer.material = Resources.Load("RedFishMat", typeof(Material)) as Material;
				break;
		}
		return capsule;
	}
	
	public static char getFishTypeDiplomacy(GameObject fish1, GameObject fish2) {
		switch (fish1.tag) {
		case "Blue Fish":
			if (fish2.tag == "Blue Fish") return 'f';
			if (fish2.tag == "Yellow Fish") return 'e';
			else return 'n';
			break;
		case "Green Fish":
			if (fish2.tag == "Green Fish") return 'f';
			if (fish2.tag == "Red Fish") return 'e';
			else return 'n';
			break;
		case "Yellow Fish":
			if (fish2.tag == "Yellow Fish") return 'f';
			if (fish2.tag == "Blue Fish") return 'e';
			else return 'n';
			break;
		case "Red Fish":
			if (fish2.tag == "Red Fish") return 'f';
			if (fish2.tag == "Green Fish") return 'e';
			else return 'n';
			break;
		}
		return 'n';
	}
	
}
