using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemGenerator : MonoBehaviour {
	public bool Generate;
	static public bool StartTheGame;

	static public bool Player2Active;
	static public bool Player3Active;
	static public bool Player4Active;
	static public bool Player5Active;
	static public bool Player6Active;
	static public bool Player7Active;
	static public bool Player8Active;

	static public string Player1Race;
	static public string Player2Race;
	static public string Player3Race;
	static public string Player4Race;
	static public string Player5Race;
	static public string Player6Race;
	static public string Player7Race;
	static public string Player8Race;

	static public bool Player1IsEnemy;
	static public bool Player2IsEnemy;
	static public bool Player3IsEnemy;
	static public bool Player4IsEnemy;
	static public bool Player5IsEnemy;
	static public bool Player6IsEnemy;
	static public bool Player7IsEnemy;
	static public bool Player8IsEnemy;

	static public bool Player1IsAlly;
	static public bool Player2IsAlly;
	static public bool Player3IsAlly;
	static public bool Player4IsAlly;
	static public bool Player5IsAlly;
	static public bool Player6IsAlly;
	static public bool Player7IsAlly;
	static public bool Player8IsAlly;

	static public Color32 Player1Color;
	static public Color32 Player2Color;
	static public Color32 Player3Color;
	static public Color32 Player4Color;
	static public Color32 Player5Color;
	static public Color32 Player6Color;
	static public Color32 Player7Color;
	static public Color32 Player8Color;

	static public string StarColor;
	static public string StarSize;
	static public bool IsBlackHole;

	static public string NebulaColor;
	static public string NebulaSize;
	static public bool NebulaActive;

	public GameObject AI2;
	public GameObject AI3;
	public GameObject AI4;
	public GameObject AI5;
	public GameObject AI6;
	public GameObject AI7;
	public GameObject AI8;

	public GameObject SmallStarSpawnPoint;
	public GameObject MediumStarSpawnPoint;
	public GameObject BigStarSpawnPoint;
	public GameObject HugeStarSpawnPoint;

	public GameObject Player1SpawnPoint;
	public GameObject Player2SpawnPoint;
	public GameObject Player3SpawnPoint;
	public GameObject Player4SpawnPoint;
	public GameObject Player5SpawnPoint;
	public GameObject Player6SpawnPoint;
	public GameObject Player7SpawnPoint;
	public GameObject Player8SpawnPoint;

	public GameObject SmallRed;
	public GameObject SmallYellow;
	public GameObject SmallGreen;
	public GameObject SmallBlue;
	public GameObject SmallWhite;
	public GameObject SmallDark;

	public GameObject SmallBlackHole;

	public GameObject MediumRed;
	public GameObject MediumYellow;
	public GameObject MediumGreen;
	public GameObject MediumBlue;
	public GameObject MediumWhite;
	public GameObject MediumDark;

	public GameObject MeduimBlackHole;

	public GameObject BigRed;
	public GameObject BigYellow;
	public GameObject BigGreen;
	public GameObject BigBlue;
	public GameObject BigWhite;
	public GameObject BigDark;

	public GameObject BigBlackHole;

	public GameObject HugeRed;
	public GameObject HugeYellow;
	public GameObject HugeGreen;
	public GameObject HugeBlue;
	public GameObject HugeWhite;
	public GameObject HugeDark;

	public GameObject HugeBlackHole;

	public GameObject FedStart;
	public GameObject KliStart;
	public GameObject RomStart;
	public GameObject CarStart;
	public GameObject BorStart;
	public GameObject UndStart;

	public GameObject FedStartAI;
	public GameObject KliStartAI;
	public GameObject RomStartAI;
	public GameObject CarStartAI;
	public GameObject BorStartAI;
	public GameObject UndStartAI;

	public GameObject FedStartAlly;
	public GameObject KliStartAlly;
	public GameObject RomStartAlly;
	public GameObject CarStartAlly;
	public GameObject BorStartAlly;
	public GameObject UndStartAlly;

	// Use this for initialization
	void Start () {
		if (!Generate) {
			Player1Race = "UFP";
			Player2Race = "UFP";
			Player3Race = "UFP";
			Player4Race = "UFP";
			Player5Race = "UFP";
			Player6Race = "UFP";
			Player7Race = "UFP";
			Player8Race = "UFP";

			Player2IsEnemy = true;
			Player3IsEnemy = true;
			Player4IsEnemy = true;
			Player5IsEnemy = true;
			Player6IsEnemy = true;
			Player7IsEnemy = true;
			Player8IsEnemy = true;

			Player1Color = Color.white;
			Player2Color = Color.white;
			Player3Color = Color.white;
			Player4Color = Color.white;
			Player5Color = Color.white;
			Player6Color = Color.white;
			Player7Color = Color.white;
			Player8Color = Color.white;
			StarColor = "Red";
			StarSize = "Small";
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Generate){
		if (StartTheGame) {
			if (Player1Race == "UFP") {
					Instantiate (FedStart, Player1SpawnPoint.transform.position, Player1SpawnPoint.transform.rotation);
			}
			if (Player1Race == "KE") {
					Instantiate (KliStart, Player1SpawnPoint.transform.position, Player1SpawnPoint.transform.rotation);
			}
			if (Player1Race == "RE") {
					Instantiate (RomStart, Player1SpawnPoint.transform.position, Player1SpawnPoint.transform.rotation);
			}
			if (Player1Race == "CU") {
					Instantiate (CarStart, Player1SpawnPoint.transform.position, Player1SpawnPoint.transform.rotation);
			}
			if (Player1Race == "BC") {
					Instantiate (BorStart, Player1SpawnPoint.transform.position, Player1SpawnPoint.transform.rotation);
			}
			if (Player1Race == "8472") {
					Instantiate (UndStart, Player1SpawnPoint.transform.position, Player1SpawnPoint.transform.rotation);
			}


			if (Player2Active) {
				if (Player2IsAlly) {
					AI2.GetComponent<GlobalAI> ().FreandAI = true;
					if (Player2Race == "UFP") {
					    	GameObject start = Instantiate (FedStartAlly, Player2SpawnPoint.transform.position, Player2SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 2;
					}
					if (Player2Race == "KE") {
							GameObject start = Instantiate (KliStartAlly, Player2SpawnPoint.transform.position, Player2SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 2;
					}
					if (Player2Race == "RE") {
							GameObject start = Instantiate (RomStartAlly, Player2SpawnPoint.transform.position, Player2SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 2;
					}
					if (Player2Race == "CU") {
							GameObject start = 	Instantiate (CarStartAlly, Player2SpawnPoint.transform.position, Player2SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 2;
					}
					if (Player2Race == "BC") {
							GameObject start = 	Instantiate (BorStartAlly, Player2SpawnPoint.transform.position, Player2SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 2;
					}
					if (Player2Race == "8472") {
							GameObject start = 	Instantiate (UndStartAlly, Player2SpawnPoint.transform.position, Player2SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 2;
					}
				}
				if (Player2IsEnemy) {
					AI2.GetComponent<GlobalAI> ().AI = true;
					if (Player2Race == "UFP") {
							GameObject start = 	Instantiate (FedStartAI, Player2SpawnPoint.transform.position, Player2SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 2;
					}
					if (Player2Race == "KE") {
							GameObject start = Instantiate (KliStartAI, Player2SpawnPoint.transform.position, Player2SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 2;
					}
					if (Player2Race == "RE") {
							GameObject start = Instantiate (RomStartAI, Player2SpawnPoint.transform.position, Player2SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 2;
					}
					if (Player2Race == "CU") {
							GameObject start = 	Instantiate (CarStartAI, Player2SpawnPoint.transform.position, Player2SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 2;
					}
					if (Player2Race == "BC") {
							GameObject start = 	Instantiate (BorStartAI, Player2SpawnPoint.transform.position, Player2SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 2;
					}
					if (Player2Race == "8472") {
							GameObject start = 	Instantiate (UndStartAI, Player2SpawnPoint.transform.position, Player2SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 2;
					}
				}
			}
			if (Player3Active) {
				if (Player3IsAlly) {
					AI3.GetComponent<GlobalAI> ().FreandAI = true;
					if (Player3Race == "UFP") {
							GameObject start = 	Instantiate (FedStartAlly, Player3SpawnPoint.transform.position, Player3SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 3;
					}
					if (Player3Race == "KE") {
							GameObject start = 	Instantiate (KliStartAlly, Player3SpawnPoint.transform.position, Player3SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 3;
					}
					if (Player3Race == "RE") {
							GameObject start = 	Instantiate (RomStartAlly, Player3SpawnPoint.transform.position, Player3SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 3;
					}
					if (Player3Race == "CU") {
							GameObject start = Instantiate (CarStartAlly, Player3SpawnPoint.transform.position, Player3SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 3;
					}
					if (Player3Race == "BC") {
							GameObject start = 	Instantiate (BorStartAlly, Player3SpawnPoint.transform.position, Player3SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 3;
					}
					if (Player3Race == "8472") {
							GameObject start = 	Instantiate (UndStartAlly, Player3SpawnPoint.transform.position, Player3SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 3;
					}
				}
				if (Player3IsEnemy) {
					AI3.GetComponent<GlobalAI> ().AI = true;
					if (Player3Race == "UFP") {
							GameObject start = 	Instantiate (FedStartAI, Player3SpawnPoint.transform.position, Player3SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 3;
					}
					if (Player3Race == "KE") {
							GameObject start = 	Instantiate (KliStartAI, Player3SpawnPoint.transform.position, Player3SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 3;
					}
					if (Player3Race == "RE") {
							GameObject start = 	Instantiate (RomStartAI, Player3SpawnPoint.transform.position, Player3SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 3;
					}
					if (Player3Race == "CU") {
							GameObject start = 	Instantiate (CarStartAI, Player3SpawnPoint.transform.position, Player3SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 3;
					}
					if (Player3Race == "BC") {
							GameObject start = 	Instantiate (BorStartAI, Player3SpawnPoint.transform.position, Player3SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 3;
					}
					if (Player3Race == "8472") {
							GameObject start = 	Instantiate (UndStartAI, Player3SpawnPoint.transform.position, Player3SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 3;
					}
				}
			}
			if (Player4Active) {
				if (Player4IsAlly) {
					AI4.GetComponent<GlobalAI> ().FreandAI = true;
					if (Player4Race == "UFP") {
							GameObject start = Instantiate (FedStartAlly, Player4SpawnPoint.transform.position, Player4SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 4;
					}
					if (Player4Race == "KE") {
							GameObject start = Instantiate (KliStartAlly, Player4SpawnPoint.transform.position, Player4SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 4;
					}
					if (Player4Race == "RE") {
							GameObject start = Instantiate (RomStartAlly, Player4SpawnPoint.transform.position, Player4SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 4;
					}
					if (Player4Race == "CU") {
							GameObject start = 	Instantiate (CarStartAlly, Player4SpawnPoint.transform.position, Player4SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 4;
					}
					if (Player4Race == "BC") {
							GameObject start = 	Instantiate (BorStartAlly, Player4SpawnPoint.transform.position, Player4SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 4;
					}
					if (Player4Race == "8472") {
							GameObject start = 	Instantiate (UndStartAlly, Player4SpawnPoint.transform.position, Player4SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 4;
					}
				}
				if (Player4IsEnemy) {
					AI4.GetComponent<GlobalAI> ().AI = true;
					if (Player4Race == "UFP") {
							GameObject start = Instantiate (FedStartAI, Player4SpawnPoint.transform.position, Player4SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 4;
					}
					if (Player4Race == "KE") {
							GameObject start = 	Instantiate (KliStartAI, Player4SpawnPoint.transform.position, Player4SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 4;
					}
					if (Player4Race == "RE") {
							GameObject start = 	Instantiate (RomStartAI, Player4SpawnPoint.transform.position, Player4SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 4;
					}
					if (Player4Race == "CU") {
							GameObject start = 	Instantiate (CarStartAI, Player4SpawnPoint.transform.position, Player4SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 4;
					}
					if (Player4Race == "BC") {
							GameObject start = 	Instantiate (BorStartAI, Player4SpawnPoint.transform.position, Player4SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 4;
					}
					if (Player4Race == "8472") {
							GameObject start = 	Instantiate (UndStartAI, Player4SpawnPoint.transform.position, Player4SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 4;
					}
				}
			}
			if (Player5Active) {
				if (Player5IsAlly) {
					AI5.GetComponent<GlobalAI> ().FreandAI = true;
					if (Player5Race == "UFP") {
							GameObject start = 	Instantiate (FedStartAlly, Player5SpawnPoint.transform.position, Player5SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 5;
					}
					if (Player5Race == "KE") {
							GameObject start = 	Instantiate (KliStartAlly, Player5SpawnPoint.transform.position, Player5SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 5;
					}
					if (Player5Race == "RE") {
							GameObject start = 	Instantiate (RomStartAlly, Player5SpawnPoint.transform.position, Player5SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 5;
					}
					if (Player5Race == "CU") {
							GameObject start = 	Instantiate (CarStartAlly, Player5SpawnPoint.transform.position, Player5SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 5;
					}
					if (Player5Race == "BC") {
							GameObject start = Instantiate (BorStartAlly, Player5SpawnPoint.transform.position, Player5SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 5;
					}
					if (Player5Race == "8472") {
							GameObject start = 	Instantiate (UndStartAlly, Player5SpawnPoint.transform.position, Player5SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 5;
					}
				}
				if (Player5IsEnemy) {
					AI5.GetComponent<GlobalAI> ().AI = true;
					if (Player5Race == "UFP") {
							GameObject start = 	Instantiate (FedStartAI, Player5SpawnPoint.transform.position, Player5SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 5;
					}
					if (Player5Race == "KE") {
							GameObject start = 	Instantiate (KliStartAI, Player5SpawnPoint.transform.position, Player5SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 5;
					}
					if (Player5Race == "RE") {
							GameObject start = 	Instantiate (RomStartAI, Player5SpawnPoint.transform.position, Player5SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 5;
					}
					if (Player5Race == "CU") {
							GameObject start = 	Instantiate (CarStartAI, Player5SpawnPoint.transform.position, Player5SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 5;
					}
					if (Player5Race == "BC") {
							GameObject start = 	Instantiate (BorStartAI, Player5SpawnPoint.transform.position, Player5SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 5;
					}
					if (Player5Race == "8472") {
							GameObject start = 	Instantiate (UndStartAI, Player5SpawnPoint.transform.position, Player5SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 5;
					}
				}
			}
			if (Player6Active) {
				if (Player6IsAlly) {
					AI6.GetComponent<GlobalAI> ().FreandAI = true;
					if (Player6Race == "UFP") {
							GameObject start = 	Instantiate (FedStartAlly, Player6SpawnPoint.transform.position, Player6SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 6;
					}
					if (Player6Race == "KE") {
							GameObject start = 	Instantiate (KliStartAlly, Player6SpawnPoint.transform.position, Player6SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 6;
					}
					if (Player6Race == "RE") {
							GameObject start = 	Instantiate (RomStartAlly, Player6SpawnPoint.transform.position, Player6SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 6;
					}
					if (Player6Race == "CU") {
							GameObject start = 	Instantiate (CarStartAlly, Player6SpawnPoint.transform.position, Player6SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 6;
					}
					if (Player6Race == "BC") {
							GameObject start = 	Instantiate (BorStartAlly, Player6SpawnPoint.transform.position, Player6SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 6;
					}
					if (Player6Race == "8472") {
							GameObject start = 	Instantiate (UndStartAlly, Player6SpawnPoint.transform.position, Player6SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 6;
					}
				}
				if (Player6IsEnemy) {
					AI6.GetComponent<GlobalAI> ().AI = true;
					if (Player6Race == "UFP") {
							GameObject start = Instantiate (FedStartAI, Player6SpawnPoint.transform.position, Player6SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 6;
					}
					if (Player6Race == "KE") {
							GameObject start = 	Instantiate (KliStartAI, Player6SpawnPoint.transform.position, Player6SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 6;
					}
					if (Player6Race == "RE") {
							GameObject start = 	Instantiate (RomStartAI, Player6SpawnPoint.transform.position, Player6SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 6;
					}
					if (Player6Race == "CU") {
							GameObject start = 	Instantiate (CarStartAI, Player6SpawnPoint.transform.position, Player6SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 6;
					}
					if (Player6Race == "BC") {
							GameObject start = Instantiate (BorStartAI, Player6SpawnPoint.transform.position, Player6SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 6;
					}
					if (Player6Race == "8472") {
							GameObject start = 	Instantiate (UndStartAI, Player6SpawnPoint.transform.position, Player6SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 6;
					}
				}
			}
			if (Player7Active) {
				if (Player7IsAlly) {
					AI7.GetComponent<GlobalAI> ().FreandAI = true;
					if (Player7Race == "UFP") {
							GameObject start = Instantiate (FedStartAlly, Player7SpawnPoint.transform.position, Player7SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 7;
					}
					if (Player7Race == "KE") {
							GameObject start = Instantiate (KliStartAlly, Player7SpawnPoint.transform.position, Player7SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 7;
					}
					if (Player7Race == "RE") {
							GameObject start = 	Instantiate (RomStartAlly, Player7SpawnPoint.transform.position, Player7SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 7;
					}
					if (Player7Race == "CU") {
							GameObject start = 	Instantiate (CarStartAlly, Player7SpawnPoint.transform.position, Player7SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 7;
					}
					if (Player7Race == "BC") {
							GameObject start = 	Instantiate (BorStartAlly, Player7SpawnPoint.transform.position, Player7SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 7;
					}
					if (Player7Race == "8472") {
							GameObject start = 	Instantiate (UndStartAlly, Player7SpawnPoint.transform.position, Player7SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 7;
					}
				}
				if (Player7IsEnemy) {
					AI7.GetComponent<GlobalAI> ().AI = true;
					if (Player7Race == "UFP") {
							GameObject start = 	Instantiate (FedStartAI, Player7SpawnPoint.transform.position, Player7SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 7;
					}
					if (Player7Race == "KE") {
							GameObject start = 	Instantiate (KliStartAI, Player7SpawnPoint.transform.position, Player7SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 7;
					}
					if (Player7Race == "RE") {
							GameObject start = 	Instantiate (RomStartAI, Player7SpawnPoint.transform.position, Player7SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 7;
					}
					if (Player7Race == "CU") {
							GameObject start = 	Instantiate (CarStartAI, Player7SpawnPoint.transform.position, Player7SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 7;
					}
					if (Player7Race == "BC") {
							GameObject start = 	Instantiate (BorStartAI, Player7SpawnPoint.transform.position, Player7SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 7;
					}
					if (Player7Race == "8472") {
							GameObject start = 	Instantiate (UndStartAI, Player7SpawnPoint.transform.position, Player7SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 7;
					}
				}
			}
			if (Player8Active) {
				if (Player8IsAlly) {
					AI8.GetComponent<GlobalAI> ().FreandAI = true;
					if (Player8Race == "UFP") {
							GameObject start = 	Instantiate (FedStartAlly, Player8SpawnPoint.transform.position, Player8SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 8;
					}
					if (Player8Race == "KE") {
							GameObject start = 	Instantiate (KliStartAlly, Player8SpawnPoint.transform.position, Player8SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 8;
					}
					if (Player8Race == "RE") {
							GameObject start = 	Instantiate (RomStartAlly, Player8SpawnPoint.transform.position, Player8SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 8;
					}
					if (Player8Race == "CU") {
							GameObject start = 	Instantiate (CarStartAlly, Player8SpawnPoint.transform.position, Player8SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 8;
					}
					if (Player8Race == "BC") {
							GameObject start = 	Instantiate (BorStartAlly, Player8SpawnPoint.transform.position, Player8SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 8;
					}
					if (Player8Race == "8472") {
							GameObject start = 	Instantiate (UndStartAlly, Player8SpawnPoint.transform.position, Player8SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 8;
					}
				}
				if (Player8IsEnemy) {
					AI8.GetComponent<GlobalAI> ().AI = true;
					if (Player8Race == "UFP") {
							GameObject start = 	Instantiate (FedStartAI, Player8SpawnPoint.transform.position, Player8SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 8;
					}
					if (Player8Race == "KE") {
							GameObject start = 	Instantiate (KliStartAI, Player8SpawnPoint.transform.position, Player8SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 8;
					}
					if (Player8Race == "RE") {
							GameObject start = 	Instantiate (RomStartAI, Player8SpawnPoint.transform.position, Player8SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 8;
					}
					if (Player8Race == "CU") {
							GameObject start = 	Instantiate (CarStartAI, Player8SpawnPoint.transform.position, Player8SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 8;
					}
					if (Player8Race == "BC") {
							GameObject start = 	Instantiate (BorStartAI, Player8SpawnPoint.transform.position, Player8SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 8;
					}
					if (Player8Race == "8472") {
							GameObject start = 	Instantiate (UndStartAI, Player8SpawnPoint.transform.position, Player8SpawnPoint.transform.rotation);
						start.GetComponent<AISelecter> ().PlayerNum = 8;
					}
				}
			}

			if (StarSize == "Small") {
				if (StarColor == "Red") {
					Instantiate (SmallRed, SmallStarSpawnPoint.transform.position, SmallStarSpawnPoint.transform.rotation);
				}
				if (StarColor == "Yellow") {
					Instantiate (SmallYellow, SmallStarSpawnPoint.transform.position, SmallStarSpawnPoint.transform.rotation);
				}
				if (StarColor == "Green") {
					Instantiate (SmallGreen, SmallStarSpawnPoint.transform.position, SmallStarSpawnPoint.transform.rotation);
				}
				if (StarColor == "Blue") {
					Instantiate (SmallBlue, SmallStarSpawnPoint.transform.position, SmallStarSpawnPoint.transform.rotation);
				}
				if (StarColor == "White") {
					Instantiate (SmallWhite, SmallStarSpawnPoint.transform.position, SmallStarSpawnPoint.transform.rotation);
				}
				if (StarColor == "Dark") {
					Instantiate (SmallDark, SmallStarSpawnPoint.transform.position, SmallStarSpawnPoint.transform.rotation);
				}
			}
			if (StarSize == "Medium") {
				if (StarColor == "Red") {
					Instantiate (MediumRed, MediumStarSpawnPoint.transform.position, MediumStarSpawnPoint.transform.rotation);
				}
				if (StarColor == "Yellow") {
					Instantiate (MediumYellow, MediumStarSpawnPoint.transform.position, MediumStarSpawnPoint.transform.rotation);
				}
				if (StarColor == "Green") {
					Instantiate (MediumGreen, MediumStarSpawnPoint.transform.position, MediumStarSpawnPoint.transform.rotation);
				}
				if (StarColor == "Blue") {
					Instantiate (MediumBlue, MediumStarSpawnPoint.transform.position, MediumStarSpawnPoint.transform.rotation);
				}
				if (StarColor == "White") {
					Instantiate (MediumWhite, MediumStarSpawnPoint.transform.position, MediumStarSpawnPoint.transform.rotation);
				}
				if (StarColor == "Dark") {
					Instantiate (MediumDark, MediumStarSpawnPoint.transform.position, MediumStarSpawnPoint.transform.rotation);
				}
			}
			if (StarSize == "Big") {
				if (StarColor == "Red") {
					Instantiate (BigRed, BigStarSpawnPoint.transform.position, BigStarSpawnPoint.transform.rotation);
				}
				if (StarColor == "Yellow") {
					Instantiate (BigYellow, BigStarSpawnPoint.transform.position, BigStarSpawnPoint.transform.rotation);
				}
				if (StarColor == "Green") {
					Instantiate (BigGreen, BigStarSpawnPoint.transform.position, BigStarSpawnPoint.transform.rotation);
				}
				if (StarColor == "Blue") {
					Instantiate (BigBlue, BigStarSpawnPoint.transform.position, BigStarSpawnPoint.transform.rotation);
				}
				if (StarColor == "White") {
					Instantiate (BigWhite, BigStarSpawnPoint.transform.position, BigStarSpawnPoint.transform.rotation);
				}
				if (StarColor == "Dark") {
					Instantiate (BigDark, BigStarSpawnPoint.transform.position, BigStarSpawnPoint.transform.rotation);
				}
			}
			if (StarSize == "Huge") {
				if (StarColor == "Red") {
					Instantiate (HugeRed, HugeStarSpawnPoint.transform.position, HugeStarSpawnPoint.transform.rotation);
				}
				if (StarColor == "Yellow") {
					Instantiate (HugeYellow, HugeStarSpawnPoint.transform.position, HugeStarSpawnPoint.transform.rotation);
				}
				if (StarColor == "Green") {
					Instantiate (HugeGreen, HugeStarSpawnPoint.transform.position, HugeStarSpawnPoint.transform.rotation);
				}
				if (StarColor == "Blue") {
					Instantiate (HugeBlue, HugeStarSpawnPoint.transform.position, HugeStarSpawnPoint.transform.rotation);
				}
				if (StarColor == "White") {
					Instantiate (HugeWhite, HugeStarSpawnPoint.transform.position, HugeStarSpawnPoint.transform.rotation);
				}
				if (StarColor == "Dark") {
					Instantiate (HugeDark, HugeStarSpawnPoint.transform.position, HugeStarSpawnPoint.transform.rotation);
				}
			}
			if (!Player1Color.Equals(Color.clear)) {
				gameObject.GetComponent<PlayerColour> ().PlayerColor1 = Player1Color;
			} else {
				gameObject.GetComponent<PlayerColour> ().PlayerColor1 = Color.white;
			}
			if (!Player2Color.Equals(Color.clear)) {
				gameObject .GetComponent<PlayerColour> ().PlayerColor2 = Player2Color;
			} else {
				gameObject.GetComponent<PlayerColour> ().PlayerColor2 = Color.white;
			}
			if (!Player3Color.Equals(Color.clear)) {
				gameObject.GetComponent<PlayerColour> ().PlayerColor3 = Player3Color;
			} else {
				gameObject.GetComponent<PlayerColour> ().PlayerColor3 = Color.white;
			}
			if (!Player4Color.Equals(Color.clear)) {
				gameObject.GetComponent<PlayerColour> ().PlayerColor4 = Player4Color;
			} else {
				gameObject.GetComponent<PlayerColour> ().PlayerColor4 = Color.white;
			}
			if (!Player5Color.Equals(Color.clear)) {
				gameObject.GetComponent<PlayerColour> ().PlayerColor5 = Player5Color;
			} else {
				gameObject.GetComponent<PlayerColour> ().PlayerColor5 = Color.white;
			}
			if (!Player6Color.Equals(Color.clear)) {
				gameObject.GetComponent<PlayerColour> ().PlayerColor6 = Player6Color;
			} else {
				gameObject.GetComponent<PlayerColour> ().PlayerColor6 = Color.white;
			}
			if (!Player7Color.Equals(Color.clear)) {
				gameObject.GetComponent<PlayerColour> ().PlayerColor7 = Player7Color;
			} else {
				gameObject.GetComponent<PlayerColour> ().PlayerColor7 = Color.white;
			}
			if (!Player8Color.Equals(Color.clear)) {
				gameObject.GetComponent<PlayerColour> ().PlayerColor8 = Player8Color;
			} else {
				gameObject.GetComponent<PlayerColour> ().PlayerColor8 = Color.white;
			}
			StartTheGame = false;
		}
	}
	}
	public void StarGame(){
		SystemGenerator.StartTheGame = true;
	}

	public void SetPlayer1(string Race){
		SystemGenerator.Player1Race = Race;
	}
	public void SetPlayer2(string Race){
		SystemGenerator.Player2Race = Race;
	}
	public void SetPlayer3(string Race){
		SystemGenerator.Player3Race = Race;
	}
	public void SetPlayer4(string Race){
		SystemGenerator.Player4Race = Race;
	}
	public void SetPlayer5(string Race){
		SystemGenerator.Player5Race = Race;
	}
	public void SetPlayer6(string Race){
		SystemGenerator.Player6Race = Race;
	}
	public void SetPlayer7(string Race){
		SystemGenerator.Player7Race = Race;
	}
	public void SetPlayer8(string Race){
		SystemGenerator.Player8Race = Race;
	}

	public void SetPlayer1Enemy(bool Ok){
		SystemGenerator.Player1IsEnemy = Ok;
	}
	public void SetPlayer2Enemy(bool Ok){
		SystemGenerator.Player2IsEnemy = Ok;
	}
	public void SetPlayer3Enemy(bool Ok){
		SystemGenerator.Player3IsEnemy = Ok;
	}
	public void SetPlayer4Enemy(bool Ok){
		SystemGenerator.Player4IsEnemy = Ok;
	}
	public void SetPlayer5Enemy(bool Ok){
		SystemGenerator.Player5IsEnemy = Ok;
	}
	public void SetPlayer6Enemy(bool Ok){
		SystemGenerator.Player6IsEnemy = Ok;
	}
	public void SetPlayer7Enemy(bool Ok){
		SystemGenerator.Player7IsEnemy = Ok;
	}
	public void SetPlayer8Enemy(bool Ok){
		SystemGenerator.Player8IsEnemy = Ok;
	}

	public void SetPlayer1Ally(bool Ok){
		SystemGenerator.Player1IsAlly = Ok;
	}
	public void SetPlayer2Ally(bool Ok){
		SystemGenerator.Player2IsAlly = Ok;
	}
	public void SetPlayer3Ally(bool Ok){
		SystemGenerator.Player3IsAlly = Ok;
	}
	public void SetPlayer4Ally(bool Ok){
		SystemGenerator.Player4IsAlly = Ok;
	}
	public void SetPlayer5Ally(bool Ok){
		SystemGenerator.Player5IsAlly = Ok;
	}
	public void SetPlayer6Ally(bool Ok){
		SystemGenerator.Player6IsAlly = Ok;
	}
	public void SetPlayer7Ally(bool Ok){
		SystemGenerator.Player7IsAlly = Ok;
	}
	public void SetPlayer8Ally(bool Ok){
		SystemGenerator.Player8IsAlly = Ok;
	}

	public void SetPlayer1Color(string color){
		if (color == "Red") {
			SystemGenerator.Player1Color = Color.red;
		}
		if (color == "Yellow") {
			SystemGenerator.Player1Color = Color.yellow;
		}
		if (color == "Green") {
			SystemGenerator.Player1Color = Color.green;
		}
		if (color == "Blue") {
			SystemGenerator.Player1Color = Color.blue;
		}
		if (color == "Violet") {
			SystemGenerator.Player1Color = new Color32(90,1,156,1);
		}
		if (color == "White") {
			SystemGenerator.Player1Color = Color.white;
		}
		if (color == "Black") {
			SystemGenerator.Player1Color = Color.black;
		}
		if (color == "Blood") {
			SystemGenerator.Player1Color = new Color32(255,118,118,1);
		}
	}
	public void SetPlayer2Color(string color){
		if (color == "Red") {
			SystemGenerator.Player2Color = Color.red;
		}
		if (color == "Yellow") {
			SystemGenerator.Player2Color = Color.yellow;
		}
		if (color == "Green") {
			SystemGenerator.Player2Color = Color.green;
		}
		if (color == "Blue") {
			SystemGenerator.Player2Color = Color.blue;
		}
		if (color == "Violet") {
			SystemGenerator.Player2Color = new Color32(90,1,156,1);
		}
		if (color == "White") {
			SystemGenerator.Player2Color = Color.white;
		}
		if (color == "Black") {
			SystemGenerator.Player2Color = Color.black;
		}
		if (color == "Blood") {
			SystemGenerator.Player2Color = new Color32(255,118,118,1);
		}
	}
	public void SetPlayer3Color(string color){
		if (color == "Red") {
			SystemGenerator.Player3Color = Color.red;
		}
		if (color == "Yellow") {
			SystemGenerator.Player3Color = Color.yellow;
		}
		if (color == "Green") {
			SystemGenerator.Player3Color = Color.green;
		}
		if (color == "Blue") {
			SystemGenerator.Player3Color = Color.blue;
		}
		if (color == "Violet") {
			SystemGenerator.Player3Color = new Color32(90,1,156,1);
		}
		if (color == "White") {
			SystemGenerator.Player3Color = Color.white;
		}
		if (color == "Black") {
			SystemGenerator.Player3Color = Color.black;
		}
		if (color == "Blood") {
			SystemGenerator.Player3Color = new Color32(255,118,118,1);
		}
	}
	public void SetPlayer4Color(string color){
		if (color == "Red") {
			SystemGenerator.Player4Color = Color.red;
		}
		if (color == "Yellow") {
			SystemGenerator.Player4Color = Color.yellow;
		}
		if (color == "Green") {
			SystemGenerator.Player4Color = Color.green;
		}
		if (color == "Blue") {
			SystemGenerator.Player4Color = Color.blue;
		}
		if (color == "Violet") {
			SystemGenerator.Player4Color = new Color32(90,1,156,1);
		}
		if (color == "White") {
			SystemGenerator.Player4Color = Color.white;
		}
		if (color == "Black") {
			SystemGenerator.Player4Color = Color.black;
		}
		if (color == "Blood") {
			SystemGenerator.Player4Color = new Color32(255,118,118,1);
		}
	}
	public void SetPlayer5Color(string color){
		if (color == "Red") {
			SystemGenerator.Player5Color = Color.red;
		}
		if (color == "Yellow") {
			SystemGenerator.Player5Color = Color.yellow;
		}
		if (color == "Green") {
			SystemGenerator.Player5Color = Color.green;
		}
		if (color == "Blue") {
			SystemGenerator.Player5Color = Color.blue;
		}
		if (color == "Violet") {
			SystemGenerator.Player5Color = new Color32(90,1,156,1);
		}
		if (color == "White") {
			SystemGenerator.Player5Color = Color.white;
		}
		if (color == "Black") {
			SystemGenerator.Player5Color = Color.black;
		}
		if (color == "Blood") {
			SystemGenerator.Player5Color = new Color32(255,118,118,1);
		}
	}
	public void SetPlayer6Color(string color){
		if (color == "Red") {
			SystemGenerator.Player6Color = Color.red;
		}
		if (color == "Yellow") {
			SystemGenerator.Player6Color = Color.yellow;
		}
		if (color == "Green") {
			SystemGenerator.Player6Color = Color.green;
		}
		if (color == "Blue") {
			SystemGenerator.Player6Color = Color.blue;
		}
		if (color == "Violet") {
			SystemGenerator.Player6Color = new Color32(90,1,156,1);
		}
		if (color == "White") {
			SystemGenerator.Player6Color = Color.white;
		}
		if (color == "Black") {
			SystemGenerator.Player6Color = Color.black;
		}
		if (color == "Blood") {
			SystemGenerator.Player6Color = new Color32(255,118,118,1);
		}
	}
	public void SetPlayer7Color(string color){
		if (color == "Red") {
			SystemGenerator.Player7Color = Color.red;
		}
		if (color == "Yellow") {
			SystemGenerator.Player7Color = Color.yellow;
		}
		if (color == "Green") {
			SystemGenerator.Player7Color = Color.green;
		}
		if (color == "Blue") {
			SystemGenerator.Player7Color = Color.blue;
		}
		if (color == "Violet") {
			SystemGenerator.Player7Color = new Color32(90,1,156,1);
		}
		if (color == "White") {
			SystemGenerator.Player7Color = Color.white;
		}
		if (color == "Black") {
			SystemGenerator.Player7Color = Color.black;
		}
		if (color == "Blood") {
			SystemGenerator.Player7Color = new Color32(255,118,118,1);
		}
	}
	public void SetPlayer8Color(string color){
		if (color == "Red") {
			SystemGenerator.Player8Color = Color.red;
		}
		if (color == "Yellow") {
			SystemGenerator.Player8Color = Color.yellow;
		}
		if (color == "Green") {
			SystemGenerator.Player8Color = Color.green;
		}
		if (color == "Blue") {
			SystemGenerator.Player8Color = Color.blue;
		}
		if (color == "Violet") {
			SystemGenerator.Player8Color = new Color32(90,1,156,1);
		}
		if (color == "White") {
			SystemGenerator.Player8Color = Color.white;
		}
		if (color == "Black") {
			SystemGenerator.Player8Color = Color.black;
		}
		if (color == "Blood") {
			SystemGenerator.Player8Color = new Color32(255,118,118,1);
		}
	}

	public void SetStarColor(string ColorName){
		SystemGenerator.StarColor = ColorName;
	}
	public void SetStarSize(string SizeTypeName){
		SystemGenerator.StarSize = SizeTypeName;
	}
	public void SetIsBlackHole(){
		SystemGenerator.IsBlackHole = !SystemGenerator.IsBlackHole;
	}

	public void SetNebulaColor(string ColorName){
		SystemGenerator.NebulaColor = ColorName;
	}
	public void SetNebulaSize(string SizeTypeName){
		SystemGenerator.NebulaSize = SizeTypeName;
	}
	public void SetNebulaActive(){
		SystemGenerator.NebulaActive = !SystemGenerator.NebulaActive;
	}

	public void SetPlayer2Active(){
		SystemGenerator.Player2Active = !SystemGenerator.Player2Active;
	}
	public void SetPlayer3Active(){
		SystemGenerator.Player3Active = !SystemGenerator.Player3Active;
	}
	public void SetPlayer4Active(){
		SystemGenerator.Player4Active = !SystemGenerator.Player4Active;
	}
	public void SetPlayer5Active(){
		SystemGenerator.Player5Active = !SystemGenerator.Player5Active;
	}
	public void SetPlayer6Active(){
		SystemGenerator.Player6Active = !SystemGenerator.Player6Active;
	}
	public void SetPlayer7Active(){
		SystemGenerator.Player7Active = !SystemGenerator.Player7Active;
	}
	public void SetPlayer8Active(){
		SystemGenerator.Player8Active = !SystemGenerator.Player8Active;
	}
}
