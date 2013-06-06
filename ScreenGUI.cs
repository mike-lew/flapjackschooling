using UnityEngine;
using System.Collections;

public class ScreenGUI : MonoBehaviour {
	private FishSpawner fishspawner;
	
	private float sepDen = FishSchooling.separationDensity;
	private float sepDis = FishSchooling.separationDistance;
	private float cohDen = FishSchooling.cohesionDensity;
	private float cohMinDis = FishSchooling.cohesionMinDistance;
	private float cohMaxDis = FishSchooling.cohesionMaxDistance;
	private float aliDen = FishSchooling.alignmentDensity;
	private float aliDis = FishSchooling.alignmentDistance;
	private int numFish = FishSpawner.numFish;
	
	void Start() {
		fishspawner = gameObject.GetComponent<FishSpawner>();
	}

	void OnGUI() {
		sepDen = LabelSlider(new Rect(10, 25, 150, 20), sepDen, 1.0f, "Separation Density");
		sepDis = LabelSlider(new Rect(10, 50, 150, 20), sepDis, 20.0f, "Separation Distance");
		
		cohDen = LabelSlider(new Rect(10, 80, 150, 20), cohDen, 1.0f, "Cohesion Density");
		cohMinDis = LabelSlider(new Rect(10, 105, 150, 20), cohMinDis, 20.0f, "Cohesion Min Distance");
		cohMaxDis = LabelSlider(new Rect(10, 130, 150, 20), cohMaxDis, 30.0f, "Cohesion Max Distance");
		
		aliDen = LabelSlider(new Rect(10, 160, 150, 20), aliDen, 1.0f, "Alignment Density");
		aliDis = LabelSlider(new Rect(10, 185, 150, 20), aliDis, 20.0f, "Alignment Distance");
		
		numFish = LabelSliderInt(new Rect(10, 215, 150, 20), (float)(numFish), 75.0f, "Number of Fish");
		
		if (GUI.changed) {
			FishSchooling.separationDensity = sepDen;
			FishSchooling.separationDistance = sepDis;
			
			FishSchooling.cohesionDensity = cohDen;
			FishSchooling.cohesionMinDistance = cohMinDis;
			FishSchooling.cohesionMaxDistance = cohMaxDis;
			
			FishSchooling.alignmentDensity = aliDen;
			FishSchooling.alignmentDistance = aliDis;
			
			handleNumFishChange();
		}
	}
	
	private float LabelSlider (Rect screenRect, float sliderValue, float sliderMaxValue, string labelText) {
		GUI.Label (screenRect, labelText);
		screenRect.x += screenRect.width;
		screenRect.y += 6;
		sliderValue = GUI.HorizontalSlider (screenRect, sliderValue, 0.0f, sliderMaxValue);
		screenRect.x += screenRect.width;
		screenRect.width = 50;
		GUI.Label (screenRect, sliderValue.ToString());
		return sliderValue;
	}
	
	private int LabelSliderInt (Rect screenRect, float sliderValue, float sliderMaxValue, string labelText) {
		GUI.Label (screenRect, labelText);
		screenRect.x += screenRect.width;
		screenRect.y += 6;
		sliderValue = GUI.HorizontalSlider (screenRect, sliderValue, 0.0f, sliderMaxValue);
		screenRect.x += screenRect.width;
		screenRect.width = 50;
		GUI.Label (screenRect, ((int)sliderValue).ToString());
		return (int)sliderValue;
	}
	
	private void handleNumFishChange() {
		fishspawner.ensureFishNum(numFish);
	}
}
