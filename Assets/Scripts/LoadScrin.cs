using UnityEngine;
using System.Collections;

public class LoadScrin : MonoBehaviour {
	public int Level;
	public Texture2D fiest;
	public Texture2D second;
	public Texture2D third;
	IEnumerator Start()
	{
	AsyncOperation async = Application.LoadLevelAsync(Level);
	
	yield return async;
	}
	void LoadingImage()
	{
		int rndImage = Random.Range(0,9);
		if(rndImage==0)
		{
			GetComponent<GUITexture>().texture = fiest;
		}
		if(rndImage==1)
		{
			GetComponent<GUITexture>().texture = second;
		}
		if(rndImage==2)
		{
			GetComponent<GUITexture>().texture = third;
		}
	}
}




