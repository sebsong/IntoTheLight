using UnityEngine;
using System.Collections;

public class LaserLightController : MonoBehaviour {
	Light pointLight;
	float laserIntensity;
	float fadeSpeed;

	// Use this for initialization
	void Awake () {
		pointLight = gameObject.GetComponent<Light> ();
		laserIntensity = pointLight.intensity;
		fadeSpeed = laserIntensity * 2;
	}
	
	// Update is called once per frame
	void Update () {
		if (pointLight.intensity > 0) {
			pointLight.intensity -= fadeSpeed * Time.deltaTime;
		}
	}

	void OnEnable() {
		pointLight.intensity = laserIntensity;
	}
}
