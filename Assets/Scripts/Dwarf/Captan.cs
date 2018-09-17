using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Captan : MonoBehaviour
{
	public string Race;
	public int CaptanNum;

    public CapAudio CurCap;

    public CapAudio[] BorgVoice;

    public CapAudio[] FedVoice;

    // Use this for initialization
    void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
	    if (Race == "Borg")
	    {
	        if (CaptanNum == 0)
	        {
	            CaptanNum = Random.Range(1, 1);
	        }

	        if (CaptanNum == 1)
	        {
	            CurCap = BorgVoice[0];
	        }
	    }

	    if (Race == "Federation")
	    {
	        if (CaptanNum == 0)
	        {
	            CaptanNum = Random.Range(1, 5);
	        }

	        if (CaptanNum == 1)
	        {
	            CurCap = FedVoice[0];
	        }

	        if (CaptanNum == 2)
	        {
	            CurCap = FedVoice[1];
	        }

	        if (CaptanNum == 3)
	        {
	            CurCap = FedVoice[2];
	        }

	        if (CaptanNum == 4)
	        {
	            CurCap = FedVoice[3];
	        }
	    }
    }
}
[System.Serializable]
public class CapAudio
{
    public List<AudioClip> Attack;
    public List<AudioClip> LocationInvalid;
    public List<AudioClip> Move;
    public List<AudioClip> IsUnderAttack;
    public List<AudioClip> Fix;
    public List<AudioClip> Select;
    public List<AudioClip> LowResuses;
    public List<AudioClip> ConstructingBegan;
    public List<AudioClip> ConstructingEnd;
    public List<AudioClip> ConstructingCanseled;
}