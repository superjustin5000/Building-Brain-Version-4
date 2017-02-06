using UnityEngine;
using System.Collections;

public class DayNightController : MonoBehaviour {

	public Gradient nightDayColor;			//change in color over time

	public float maxIntensity = 3f;			//
	public float minIntensity = 0f;
	public float minPoint = -0.2f;

	public float maxAmbient = 1f;
	public float minAmbient = 0f;
	public float minAmbientPoint = -0.2f;


	public Gradient nightDayFogColor;
	public AnimationCurve fogDensityCurve;			//curve for fog density
	public float fogScale = 1f;

	public float dayAtmosphereThickness = 0.4f;
	public float nightAtmosphereThickness = 0.87f;

	//rotation speed
	public Vector3 dayRotateSpeed;
	public Vector3 nightRotateSpeed;

	float skySpeed = 2;		//skyspeed controlled with keys

	public Transform stars;		//ref to stars 

	Light mainLight;	//sun
	Skybox sky;
	Material skyMat;

	void Start () 
	{

		mainLight = GetComponent<Light>();	
		skyMat = RenderSettings.skybox;

	}

	void Update () 
	{

		stars.transform.rotation = transform.rotation; // sets stars to rotation of this object 

		float tRange = 1 - minPoint;		//calculate the range of a day
		float dot = Mathf.Clamp01 ((Vector3.Dot (mainLight.transform.forward, Vector3.down) - minPoint) / tRange); 
		float i = ((maxIntensity - minIntensity) * dot) + minIntensity;

		mainLight.intensity = i;

		tRange = 1 - minAmbientPoint;
		dot = Mathf.Clamp01 ((Vector3.Dot (mainLight.transform.forward, Vector3.down) - minAmbientPoint) / tRange);
		i = ((maxAmbient - minAmbient) * dot) + minAmbient;
		RenderSettings.ambientIntensity = i;

		mainLight.color = nightDayColor.Evaluate(dot);
		RenderSettings.ambientLight = mainLight.color;

		RenderSettings.fogColor = nightDayFogColor.Evaluate(dot);
		RenderSettings.fogDensity = fogDensityCurve.Evaluate(dot) * fogScale;

		i = ((dayAtmosphereThickness - nightAtmosphereThickness) * dot) + nightAtmosphereThickness;
		skyMat.SetFloat ("_AtmosphereThickness", i);

		if (dot > 0) 
			transform.Rotate (dayRotateSpeed * Time.deltaTime * skySpeed);
		else
			transform.Rotate (nightRotateSpeed * Time.deltaTime * skySpeed);

		if (Input.GetKeyDown (KeyCode.Q)) skySpeed *= 0.5f;
		if (Input.GetKeyDown (KeyCode.E)) skySpeed *= 2f;


	}
}