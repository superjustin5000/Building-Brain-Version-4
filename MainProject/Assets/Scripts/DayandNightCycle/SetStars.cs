using UnityEngine;
using System.Collections;

public class SetStars : MonoBehaviour {

	public Renderer lightwall;			//reference to TV

	Material sky;				//ref to skybox			

	public Renderer water;		//ref to water

	public Transform worldProbe; // ref to reflection probe.

	bool lighton = false;
	// Use this for initialization
	void Start () 
	{

		sky = RenderSettings.skybox;

	}



	// Update is called once per frame
	void Update () 
	{


		//toggle bool light on
		if (Input.GetKeyDown(KeyCode.T))
		{

			lighton = !lighton;

		}


		//if light on, get color final(tv emits) convert intensity; set color of the render material to change global lighting
		if (lighton)
		{
			Color final = Color.white * Mathf.LinearToGammaSpace(5);
			lightwall.material.SetColor("_EmissionColor", final);
			DynamicGI.SetEmissive(lightwall, final);
		}

		//else change back to normals 
		else
		{
			Color final = Color.white * Mathf.LinearToGammaSpace(0);
			lightwall.material.SetColor("_EmissionColor", final);
			DynamicGI.SetEmissive(lightwall, final);
		}

	//position of the camera is the same postiion of the 
		Vector3 tvec = Camera.main.transform.position;
		worldProbe.transform.position = tvec;

		//affects the water. 
		water.material.mainTextureOffset = new Vector2(Time.time / 100, 0);
		water.material.SetTextureOffset("_DetailAlbedoMap", new Vector2(0, Time.time / 80));

	}
}