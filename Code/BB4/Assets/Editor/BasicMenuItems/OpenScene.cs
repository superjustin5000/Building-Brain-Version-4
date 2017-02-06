using UnityEngine;
using System.Collections;

using UnityEditor;

public class OpenScene : Editor {

	[MenuItem("Open Scene/Main Menu")]
	public static void OpenMainMenu() {
		MyEditor.OpenScene(SimulationManager.SceneMainMenu);
	}

	[MenuItem("Open Scene/Simulation")]
	public static void OpenSimulatin() {
		MyEditor.OpenScene(SimulationManager.SceneSimulation);
	}

	[MenuItem("Open Scene/End Stats")]
	public static void OpenEndStats() {
		MyEditor.OpenScene(SimulationManager.SceneEndStats);
	}
	                                
}
