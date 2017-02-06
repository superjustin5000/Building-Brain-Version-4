using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RoomView : MonoBehaviour 
{
	[SerializeField]CanvasGroup roomNameDisplay;

	public void EnterRoom()
	{
		StartCoroutine(FadeInOut());	
	}

	IEnumerator FadeInOut()
	{
		while(roomNameDisplay.alpha < 1)
		{
			roomNameDisplay.alpha += Time.deltaTime;
			yield return null;
		}

		yield return new WaitForSeconds(1f);

		while(roomNameDisplay.alpha > 0 )
		{
			roomNameDisplay.alpha -= Time.deltaTime;
			yield return null;
		}
	}

	public void ChangeText(string name)
	{
		roomNameDisplay.GetComponent<Text>().text = name;
	}
}
