using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

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
	// Use this for initialization
	void Start()
	{
		string MainWay = Path.Combine(Application.dataPath, "Data/Nemes/");

		string[] Galacticalines = File.ReadAllLines(Path.Combine(MainWay, "GalacticaNames.txt"));
		for (int i = 0; i < Galacticalines.Length; ++i)
		{
			GalactiucaNames.Add(Galacticalines[i]);
		}

		string[] Defiantlines = File.ReadAllLines(Path.Combine(MainWay, "DefiantNames.txt"));
		for (int i = 0; i < Defiantlines.Length; ++i)
		{
			DefiantNames.Add(Defiantlines[i]);
		}
		string[] Novalines = File.ReadAllLines(Path.Combine(MainWay, "NovaNames.txt"));
		for (int i = 0; i < Novalines.Length; ++i)
		{
			NovaNames.Add(Novalines[i]);
		}
		string[] Saberlines = File.ReadAllLines(Path.Combine(MainWay, "SaberNames.txt"));
		for (int i = 0; i < Saberlines.Length; ++i)
		{
			SaberNames.Add(Saberlines[i]);
		}

		string[] Akiralines = File.ReadAllLines(Path.Combine(MainWay, "AkiraNames.txt"));
		for (int i = 0; i < Akiralines.Length; ++i)
		{
			AkiraNames.Add(Akiralines[i]);
		}
		string[] Intrepidlines = File.ReadAllLines(Path.Combine(MainWay, "IntrepidNames.txt"));
		for (int i = 0; i < Intrepidlines.Length; ++i)
		{
			IntrepidNames.Add(Intrepidlines[i]);
		}
		string[] SteamRunnerlines = File.ReadAllLines(Path.Combine(MainWay, "SteamRunnerNames.txt"));
		for (int i = 0; i < SteamRunnerlines.Length; ++i)
		{
			SteamRunnerNames.Add(SteamRunnerlines[i]);
		}

		string[] Lunalines = File.ReadAllLines(Path.Combine(MainWay, "LunaNames.txt"));
		for (int i = 0; i < Lunalines.Length; ++i)
		{
			LunaNames.Add(Lunalines[i]);
		}
		string[] Prometheuselines = File.ReadAllLines(Path.Combine(MainWay, "PrometheuseNames.txt"));
		for (int i = 0; i < Prometheuselines.Length; ++i)
		{
			PrometheuseNames.Add(Prometheuselines[i]);
		}
		string[] Nebulalines = File.ReadAllLines(Path.Combine(MainWay, "NebulaNames.txt"));
		for (int i = 0; i < Nebulalines.Length; ++i)
		{
			NebulaNames.Add(Nebulalines[i]);
		}
		string[] Galaxylines = File.ReadAllLines(Path.Combine(MainWay, "GalaxyNames.txt"));
		for (int i = 0; i < Galaxylines.Length; ++i)
		{
			GalaxyNames.Add(Galaxylines[i]);
		}
		string[] Sovereignlines = File.ReadAllLines(Path.Combine(MainWay, "SovereignNames.txt"));
		for (int i = 0; i < Sovereignlines.Length; ++i)
		{
			SovereignNames.Add(Sovereignlines[i]);
		}
		string[] Excaliburlines = File.ReadAllLines(Path.Combine(MainWay, "ExcaliburNames.txt"));
		for (int i = 0; i < Excaliburlines.Length; ++i)
		{
			ExcaliburNames.Add(Excaliburlines[i]);
		}
	}

	// Update is called once per frame
	void Update()
	{

	}
}