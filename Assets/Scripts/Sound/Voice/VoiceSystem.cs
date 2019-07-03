using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Utils;

public class VoiceSystem : Singleton<VoiceSystem>
{
    public ComputerVoice BorgSystemVoice;

    public ComputerVoice FedSystemVoice;

    public ComputerVoice KliSystemVoice;

    public ComputerVoice RomSystemVoice;

    public ComputerVoice CarSystemVoice;

    public ComputerVoice SpiSystemVoice;

    public CapAudio[] BorgVoice;

    public CapAudio[] FedVoice;

    public CapAudio[] KliVoice;

    public CapAudio[] RomVoice;

    public CapAudio[] CarVoice;

    public CapAudio[] SpiVoice;

    public string mainPath;

    public bool LoadingCompleat = false;

    protected VoiceSystem() { }
    
    // Use this for initialization
    void Awake()
    {
        mainPath = Path.Combine(Application.dataPath, "Resources/Sounds/Voices/InGame/");
        
        FedVoice = new CapAudio[4];
        if (FedVoice.Length > 0)
        {
            for (int i = 0; i < FedVoice.Length; i++) FedVoice[i] = new CapAudio();
            InitialRace(FedVoice, "Federation/Ships/");
            ClearFromNulls(FedVoice);
        }
        
        BorgVoice = new CapAudio[1];
        if (BorgVoice.Length > 0)
        {
            for (int i = 0; i < BorgVoice.Length; i++) BorgVoice[i] = new CapAudio();
            InitialRace(BorgVoice, "Borg/Ships/");
            ClearFromNulls(BorgVoice);
        }

        /*KliVoice = new CapAudio[Directory.GetFiles(Path.Combine(mainPath, "Klingon/Ships")).Length];
        if (KliVoice.Length > 0)
        {
            for (int i = 0; i < KliVoice.Length; i++) KliVoice[i] = new CapAudio();
            InitialRace(KliVoice, "Klingon/Ships/");
            ClearFromNulls(KliVoice);
        }

        RomVoice = new CapAudio[Directory.GetFiles(Path.Combine(mainPath, "Romulan/Ships")).Length];
        if (RomVoice.Length > 0)
        {
            for (int i = 0; i < RomVoice.Length; i++) RomVoice[i] = new CapAudio();
            InitialRace(RomVoice, "Romulan/Ships/");
            ClearFromNulls(RomVoice);
        }

        CarVoice = new CapAudio[Directory.GetFiles(Path.Combine(mainPath, "Cardassian/Ships")).Length];
        if (CarVoice.Length > 0)
        {
            for (int i = 0; i < CarVoice.Length; i++) CarVoice[i] = new CapAudio();
            InitialRace(CarVoice, "Cardassian/Ships/");
            ClearFromNulls(CarVoice);
        }

        SpiVoice = new CapAudio[Directory.GetFiles(Path.Combine(mainPath, "8472/Ships")).Length];
        if (SpiVoice.Length > 0)
        {
            for (int i = 0; i < SpiVoice.Length; i++) SpiVoice[i] = new CapAudio();
            InitialRace(SpiVoice, "8472/Ships/");
            ClearFromNulls(SpiVoice);
        }*/

        InitComputerVoice(BorgSystemVoice, "Sounds/Voices/InGame/Borg/Computer/");
        InitComputerVoice(FedSystemVoice, "Sounds/Voices/InGame/Federation/Computer/");
        /*InitComputerVoice(KliSystemVoice, "Klingon/Computer/");
        InitComputerVoice(RomSystemVoice, "Romulan/Computer/");
        InitComputerVoice(CarSystemVoice, "Cardassian/Computer/");
        InitComputerVoice(SpiSystemVoice, "8472/Computer/");*/

        LoadingCompleat = true;
    }

    private void InitialRace(CapAudio[] target, string racePath)
    {
        for (int i = 0; i < target.Length; i++)
        {
            LoadSound(target[i].Attack, racePath, i, "Attack");
            LoadSound(target[i].LocationInvalid, racePath, i, "WrongArea");
            LoadSound(target[i].Move, racePath, i, "Move");
            LoadSound(target[i].Fix, racePath, i, "Fix");
            LoadSound(target[i].Select, racePath, i, "Select");
        }
    }

    private void ClearFromNulls(CapAudio[] target)
    {
        for (int i = 0; i < target.Length; i++)
        {
            ClearList(target[i].Attack);
            ClearList(target[i].LocationInvalid);
            ClearList(target[i].Move);
            ClearList(target[i].Fix);
            ClearList(target[i].Select);
        }
    }

    private void ClearList(List<AudioClip> target)
    {
        while (STDLCMethods.FindInList(null, target))
        {
            target.Remove(null);
        }
    }

    /*static IEnumerable<WWW> AwaitFile(string path)
    {
        yield return (new WWW(path));
    }*/

    private void LoadSound(List<AudioClip> Target, string RacePath, int voiceNum, string FolderName)
    {   
        string[] _as = Directory.GetFiles(Path.Combine(mainPath, Path.Combine(RacePath, Path.Combine(voiceNum.ToString(), FolderName))));

        foreach (string path in _as)
        {
            int i = Path.Combine(Application.dataPath, "Resources/").Length;

            string finalPath = path.Remove(0, i);

            finalPath = finalPath.Remove(finalPath.Length - 4, 4);

            Target.Add(Resources.Load<AudioClip>(finalPath));
           // foreach (var newWWW in AwaitFile(path))
           // {
            //    Target.Add(newWWW.GetAudioClip(false, true, AudioType.WAV));
           // }
        }
    }




    private void InitComputerVoice(ComputerVoice target, string racePath)
    {
        //WWW www;
        //if (Directory.Exists(Path.Combine(mainPath, Path.Combine(racePath, "lowResources.wav"))))
        //{
            //www = new WWW(Path.Combine(mainPath, Path.Combine(racePath, "lowResources.wav")));
            target.lowResources = Resources.Load<AudioClip>(racePath + "lowResources");
        //}

        //if (Directory.Exists(Path.Combine(mainPath, Path.Combine(racePath, "lowCrew.wav"))))
        //{
            //www = new WWW(Path.Combine(mainPath, Path.Combine(racePath, "lowCrew.wav")));
            target.lowCrew = Resources.Load<AudioClip>(racePath + "lowCrew");
        //}

        //if (Directory.Exists(Path.Combine(mainPath, Path.Combine(racePath, "stationConstructingBegan.wav"))))
        //{
            //www = new WWW(Path.Combine(mainPath, Path.Combine(racePath, "stationConstructingBegan.wav")));
            target.stationConstructingBegan = Resources.Load<AudioClip>(racePath + "stationConstructingBegan");
        //}

        //if (Directory.Exists(Path.Combine(mainPath, Path.Combine(racePath, "shipConstructingBegan.wav"))))
        //{
            //www = new WWW(Path.Combine(mainPath, Path.Combine(racePath, "shipConstructingBegan.wav")));
            target.shipConstructingBegan = Resources.Load<AudioClip>(racePath + "shipConstructingBegan");
        //}

        //if (Directory.Exists(Path.Combine(mainPath, Path.Combine(racePath, "stationConstructingEnd.wav"))))
        //{
            //www = new WWW(Path.Combine(mainPath, Path.Combine(racePath, "stationConstructingEnd.wav")));
            target.stationConstructingEnd = Resources.Load<AudioClip>(racePath + "stationConstructingEnd");
        //}

        //if (Directory.Exists(Path.Combine(mainPath, Path.Combine(racePath, "shipConstructingEnd.wav"))))
        //{
            //www = new WWW(Path.Combine(mainPath, Path.Combine(racePath, "shipConstructingEnd.wav")));
            target.shipConstructingEnd = Resources.Load<AudioClip>(racePath + "shipConstructingEnd");
        //}

        //if (Directory.Exists(Path.Combine(mainPath, Path.Combine(racePath, "constructingCanseled.wav"))))
        //{
            //www = new WWW(Path.Combine(mainPath, Path.Combine(racePath, "constructingCanseled.wav")));
            target.constructingCanseled = Resources.Load<AudioClip>(racePath + "constructingCanseled");
        //}

        //if (Directory.Exists(Path.Combine(mainPath, Path.Combine(racePath, "isUnderAttack.wav"))))
        //{
            //www = new WWW(Path.Combine(mainPath, Path.Combine(racePath, "isUnderAttack.wav")));
            target.isUnderAttack = Resources.Load<AudioClip>(racePath + "isUnderAttack");
        //}
    }
}

[System.Serializable]
public class CapAudio
{
    public List<AudioClip> Attack = new List<AudioClip>();
    public List<AudioClip> LocationInvalid= new List<AudioClip>();
    public List<AudioClip> Move = new List<AudioClip>();
    public List<AudioClip> Fix = new List<AudioClip>();
    public List<AudioClip> Select = new List<AudioClip>();
}
[System.Serializable]
public class ComputerVoice
{
    public AudioClip lowResources;
    public AudioClip lowCrew;
    public AudioClip stationConstructingBegan;
    public AudioClip shipConstructingBegan;
    public AudioClip stationConstructingEnd;
    public AudioClip shipConstructingEnd;
    public AudioClip constructingCanseled;
    public AudioClip isUnderAttack;
}