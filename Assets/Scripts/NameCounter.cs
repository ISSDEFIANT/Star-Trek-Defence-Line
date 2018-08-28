using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public static class NameCounter
{
    public static NameSystem Galactica = new NameSystem();

    public static NameSystem Defiant = new NameSystem();
    public static NameSystem Nova = new NameSystem();
    public static NameSystem Saber = new NameSystem();

    public static NameSystem Akira = new NameSystem();
    public static NameSystem Intrepid = new NameSystem();
    public static NameSystem Steamrunner = new NameSystem();

    public static NameSystem Luna = new NameSystem();
    public static NameSystem Prometheuse = new NameSystem();
    public static NameSystem Nebula = new NameSystem();
    public static NameSystem Galaxy = new NameSystem();
    public static NameSystem Sovereign = new NameSystem();
    public static NameSystem Excalibur = new NameSystem();

    static List<string> ReadNamesToList(string fileName)
    {
        string filePath = Path.Combine(Application.dataPath, Path.Combine("Data/Names/", fileName));
        return File.ReadAllLines(filePath).ToList();
    }

    static NameCounter() {
        InitNames(Galactica, "GalacticaNames.txt");

        InitNames(Defiant, "DefiantNames.txt");
        InitNames(Nova, "NovaNames.txt");
        InitNames(Saber, "SaberNames.txt");

        InitNames(Akira, "AkiraNames.txt");
        InitNames(Intrepid, "IntrepidNames.txt");
        InitNames(Steamrunner, "SteamRunnerNames.txt");

        InitNames(Luna, "LunaNames.txt");
        InitNames(Prometheuse, "PrometheuseNames.txt");
        InitNames(Nebula, "NebulaNames.txt");
        InitNames(Galaxy, "GalaxyNames.txt");
        InitNames(Sovereign, "SovereignNames.txt");
        InitNames(Excalibur, "ExcaliburNames.txt");
    }

    static void InitNames(NameSystem _ns, string fileName)
    {
        _ns.Names = ReadNamesToList(fileName);
        _ns.MaxShips = _ns.Names.Count;
        _ns.CurShips = _ns.MaxShips;
    }
}
//Hi
[System.Serializable]
public class NameSystem
{
    public List<string> Names;
    public int MaxShips;
    public int CurShips;
}