using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEditor;

[CustomEditor(typeof(PlugLoadController))]
public class PlugLoadEditor : Editor {

	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();

		PlugLoadController p = (PlugLoadController)target;



		EditorGUILayout.LabelField("STATE", EditorStyles.boldLabel);

		string onLabel = "Turn On";

		if (p.getIsOn()) {
			onLabel = "Turn Off";
		}

		if (GUILayout.Button(onLabel)) {
			p.toggleOnOff();
		}




		EditorGUILayout.LabelField("CONNECTED OBJECTS", EditorStyles.boldLabel);



		List<EnergyUsingObject> euoHierarchy = new List<EnergyUsingObject>();
		//look at all active scene objects and if they have the script. add them to the list.
		foreach (EnergyUsingObject o in FindObjectsOfType<EnergyUsingObject>()) {
			//only add if it's not connected to a plug load.
			if (o.getConnectedPlugLoad() == null)
				euoHierarchy.Add(o);
		}


		//get connected objects.
		List<EnergyUsingObject> euoList = p.getEuoList();
		int remIndex = -1;
		int replaceIndex = -1;
		EnergyUsingObject replaceEuo = null;

		if (euoList.Count > 0) {
			for (int i=0; i<euoList.Count; i++) {

				EnergyUsingObject euo = euoList[i];

				EditorGUILayout.BeginHorizontal();

				EnergyUsingObject newEuo = (EnergyUsingObject)EditorGUILayout.ObjectField("EUO: ", euo, typeof(EnergyUsingObject), true);
				if (newEuo != euo) {
					//trying to add. make sure it doesn't exist in the list already.
					if (p.isEuoConnected(newEuo)) {
						//already connected.
						EditorUtility.DisplayDialog("Error", "This object is already connected to this Plug Load", "OK");
					}
					else {
						replaceIndex = i;
						replaceEuo = newEuo;
						break;
					}
				}

				if (euo != null) {

					string buttonLabel = "Supply";
					if (euo.getIsEnergySupplied())
						buttonLabel = "Deny";
					if (GUILayout.Button(buttonLabel)) {
						euo.toggleSupplyingEnergy();
					}


				}

				if (GUILayout.Button("Unplug")) {
					remIndex = i;
					break;
				}

				EditorGUILayout.EndHorizontal();

			}
		}

		if (remIndex != -1)
			p.remEuo(remIndex);


		if (replaceIndex != -1) {
			p.replaceEuo(replaceEuo, replaceIndex);
		}

		if (GUILayout.Button("Add Object")) {
			p.addEuo(null);
		}



		EditorGUILayout.Space();
		EditorGUILayout.LabelField("USAGE", EditorStyles.boldLabel);

		EditorGUI.BeginDisabledGroup(true);

		EditorGUILayout.FloatField("Total Usage", p.getTotalEnergyUsed());
		EditorGUILayout.FloatField("Usage Per Sec ~", p.getTotalUsagePerSec());

		EditorGUI.EndDisabledGroup();



	}





}
