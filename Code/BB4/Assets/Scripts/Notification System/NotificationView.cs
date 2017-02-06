using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NotificationView : MonoBehaviour 
{

	[SerializeField]CanvasGroup notificationPanel;
	[SerializeField]GameObject questPanel;
	[SerializeField]Text questDetails;
	[SerializeField]Button[] questInfo;
	bool enabledWindow;
	NotificationController ctrl;

	#region Unity 
	// Use this for initialization
	void Start () {
	
		enabledWindow = false;
		notificationPanel.alpha = 0;
		ctrl = GetComponent<NotificationController>();
	}
	#endregion

	#region Custom Methods
	public void ViewQuestInfo(int i )
	{
		//questInfo[i]
		ctrl.UpdateQuestInfo(i);
	}

	public void EnableNotificationPanel()
	{
		enabledWindow = true;
		StartCoroutine(PanelFade());
	}

	public void DisableNotificationPanel()
	{
		enabledWindow = false;
		StartCoroutine(PanelFade());
	}

	IEnumerator PanelFade()
	{
		float targetAlpha = enabledWindow ? 1 : 0;
		int modifier = enabledWindow ? 1: -1;

		while((notificationPanel.alpha < targetAlpha && enabledWindow)
			|| (notificationPanel.alpha > targetAlpha && !enabledWindow))
		{
			notificationPanel.alpha += Time.deltaTime * modifier;
			yield return null;
		}

		yield return null;
	}

	public void UpdateButton(int i)
	{
		questInfo[i].GetComponentInChildren<Text>().text = "Quest #" + i;
		questInfo[i].gameObject.SetActive(true);
	}

	public void ToggleQuestScreen()
	{
		questPanel.SetActive(!questPanel.activeSelf);
	}
	#endregion

	#region Getters/Setters
	public bool WindowState
	{
		get{return enabledWindow;}
		set{enabledWindow = value;}
	}

	public string QuestDetails
	{
		get{return questDetails.text;}
		set{questDetails.text = value;}
	}
	#endregion
}
