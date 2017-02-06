using UnityEngine;
using System.Collections;

public class NotificationController : MonoBehaviour
{
	[SerializeField]NotificationView view;
	[SerializeField]NotificationModel model;
	bool[] activeQuest;

	void Start()
	{
		activeQuest = new bool[model.Size];

	}

	void Update()
	{
		if(Input.GetKeyUp(KeyCode.I))
		{
			view.ToggleQuestScreen();
		}

		if(view.WindowState && Input.GetKeyUp(KeyCode.Space))
		{
			view.DisableNotificationPanel();
		}	
	}

	public void SetActiveQuest(int i)
	{
		if(!activeQuest[i] && !view.WindowState)
		{
			activeQuest[i] = true;
			view.QuestDetails = "The Emission Level in " + model.GetRoomName(i) + " is getting High: " + model.GetEmission(i);
			view.EnableNotificationPanel();
			view.UpdateButton(i);
		}
	}

	public void UpdateQuestInfo(int index)
	{
		view.QuestDetails = "The Emission Level in " + model.GetRoomName(index) + " is getting High: " + model.GetEmission(index);
		view.EnableNotificationPanel();
	}
}
