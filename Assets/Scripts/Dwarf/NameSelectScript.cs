using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class NameSelectScript : MonoBehaviour
{
	public List<string> GalactiucaNames;

	public List<string> DefiantNames;
	public List<string> NovaNames;
	public List<string> SaberNames;

	public List<string> AkiraNames;
	public List<string> IntrepidNames;
	public List<string> SteamRunnerNames;

	public List<string> LunaNames;
	public List<string> PrometheuseNames;
	public List<string> NebulaNames;
	public List<string> GalaxyNames;
	public List<string> SovereignNames;
	public List<string> ExcaliburNames;

	List<string> ReadNamesToList(string fileName)
	{
		string filePath = Path.Combine(Application.dataPath, Path.Combine("Data/Names/", fileName));
        return File.ReadAllLines(filePath).ToList();
    }

	void Start()
	{
        GalactiucaNames =   ReadNamesToList("GalacticaNames.txt");
        DefiantNames =      ReadNamesToList("DefiantNames.txt");
        NovaNames =         ReadNamesToList("NovaNames.txt");
        SaberNames =        ReadNamesToList("SaberNames.txt");
        AkiraNames =        ReadNamesToList("AkiraNames.txt");
        IntrepidNames =     ReadNamesToList("IntrepidNames.txt");
        SteamRunnerNames =  ReadNamesToList("SteamRunnerNames.txt");
        LunaNames =         ReadNamesToList("LunaNames.txt");
        PrometheuseNames =  ReadNamesToList("PrometheuseNames.txt");
        NebulaNames =       ReadNamesToList("NebulaNames.txt");
        GalaxyNames =       ReadNamesToList("GalaxyNames.txt");
        SovereignNames =    ReadNamesToList("SovereignNames.txt");
        ExcaliburNames =    ReadNamesToList("ExcaliburNames.txt");
	}
}