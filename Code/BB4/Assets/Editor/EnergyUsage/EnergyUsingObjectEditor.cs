using UnityEngine;
using System.Collections;

using UnityEditor;

[CustomEditor(typeof(EnergyUsingObject))]
public class EnergyUsingObjectEditor : Editor {

	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();


		EnergyUsingObject euo = (EnergyUsingObject)target;

		//show the plug load.

		EditorGUILayout.Space();
		EditorGUILayout.Space();

		EditorGUILayout.LabelField("PLUG LOAD", EditorStyles.boldLabel);


		PlugLoad pl = euo.getConnectedPlugLoad();
	
		PlugLoad newPl = (PlugLoad)EditorGUILayout.ObjectField("Plug Load Connection: ", pl, typeof(PlugLoad), true);

		if (newPl != pl) {

			//remove from previous plug load.
			if (pl != null)
				pl.GetComponent<PlugLoadController>().remEuo(euo);

			//add to newPl.
			newPl.GetComponent<PlugLoadController>().addEuo(euo);

		}

		if (pl != null) {
		
			if (GUILayout.Button("Disconnect From Plug Load")) {
				if (EditorUtility.DisplayDialog("Confirm", "Are you sure you want to disconnect from the Plug Load?", "YES", "NO")) {
					pl.GetComponent<PlugLoadController>().remEuo(euo);
				}

			}

		}


	}
}
