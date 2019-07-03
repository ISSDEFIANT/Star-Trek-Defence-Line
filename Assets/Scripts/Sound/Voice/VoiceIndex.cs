using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceIndex : MonoBehaviour
{
    public int CapNum;

    public enum enRace
    {
        Federation,
        Borg,
        Klingon,
        Romulan,
        Cardassian,
        S8472
    }

    public enRace race;
    // Use this for initialization
    void Start ()
    {
        initRace();
    }

    public void initRace()
    {
        switch (race)
        {
            case enRace.Borg:
                CapNum = Random.Range(0, VoiceSystem.Instance.BorgVoice.Length);
                break;
            case enRace.Federation:
                CapNum = Random.Range(0, VoiceSystem.Instance.FedVoice.Length);
                break;
            case enRace.Klingon:
                CapNum = Random.Range(0, VoiceSystem.Instance.KliVoice.Length);
                break;
            case enRace.Romulan:
                CapNum = Random.Range(0, VoiceSystem.Instance.RomVoice.Length);
                break;
            case enRace.Cardassian:
                CapNum = Random.Range(0, VoiceSystem.Instance.CarVoice.Length);
                break;
            case enRace.S8472:
                CapNum = Random.Range(0, VoiceSystem.Instance.SpiVoice.Length);
                break;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}