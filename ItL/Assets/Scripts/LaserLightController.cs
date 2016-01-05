using UnityEngine;
using System.Collections;

public class LaserLightController : MonoBehaviour {
	Light pointLight;
	float laserIntensity;

	// Use this for initialization
	void Awake () {
		pointLight = gameObject.GetComponent<Light> ();
		laserIntensity = pointLight.intensity;
	}
	
	// Update is called once per frame
	void Update () {
		if (pointLight.intensity > 0) {
			pointLight.intensity -= 9 * Time.deltaTime;
		}
	}

	void OnEnable() {
		pointLight.intensity = laserIntensity;
	}
}
