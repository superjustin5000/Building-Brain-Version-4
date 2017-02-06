Directory Structure:

->BB4 - The Main project folder, this is folder you select when opening the project in Unity.
	->Assets - The real Code Folder       
		->Editor
			Holds editor scripts that extend the Unity Editor
		->Imports
			Any assets imported from the Asset Store or from other outside sources
		->OVR
			Contains assets for Oculus VR

		->Plugins
			Unused

		->Prefabs
			Any Prefabs that created by us. (Split into feature folders)

		->Resources
			Any resources provided by us.

		->Scenes
			All scene files created by us. (split up into feature folders)

		->Scripts
			All scripts created by us. (split up into feature folders)

		->Standard Assets
			Unity’s standard assets. Primarily for the standard character controller that was later modified.			

	Library	 	 - Used by Unity
	obj     	 - Used by Unity
	ProjectSettings - Used by Unity
	Temp            - Used by Unity

->FinishedFeatures - UnityPackage files for sprints 1,2,and 3.
	-Import them into empty projects to test.