using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Captan : MonoBehaviour
{
	public string Race;
	public int CaptanNum;

	public List<AudioClip> CurAttack;
	public List<AudioClip> CurLocationInvalid;
	public List<AudioClip> CurMove;
	public List<AudioClip> CurIsUnderAttack;
	public List<AudioClip> CurFix;
	public List<AudioClip> CurSelect;
	public List<AudioClip> CurLowResuses;
	public List<AudioClip> CurConstructingBegan;
	public List<AudioClip> CurConstructingEnd;
	public List<AudioClip> CurConstructingCanseled;

	public List<AudioClip> Borg1Attack;
	public List<AudioClip> Borg1LocationInvalid;
	public List<AudioClip> Borg1Move;
	public List<AudioClip> Borg1IsUnderAttack;
	public List<AudioClip> Borg1Fix;
	public List<AudioClip> Borg1Select;
	public List<AudioClip> Borg1LowResuses;
	public List<AudioClip> Borg1ConstructingBegan;
	public List<AudioClip> Borg1ConstructingEnd;
	public List<AudioClip> Borg1ConstructingCanseled;

	public List<AudioClip> Fed1Attack;
	public List<AudioClip> Fed1LocationInvalid;
	public List<AudioClip> Fed1Move;
	public List<AudioClip> Fed1IsUnderAttack;
	public List<AudioClip> Fed1Fix;
	public List<AudioClip> Fed1Select;
	public List<AudioClip> Fed1LowResuses;
	public List<AudioClip> Fed1ConstructingBegan;
	public List<AudioClip> Fed1ConstructingEnd;
	public List<AudioClip> Fed1ConstructingCanseled;

	public List<AudioClip> Fed2Attack;
	public List<AudioClip> Fed2LocationInvalid;
	public List<AudioClip> Fed2Move;
	public List<AudioClip> Fed2IsUnderAttack;
	public List<AudioClip> Fed2Fix;
	public List<AudioClip> Fed2Select;
	public List<AudioClip> Fed2LowResuses;
	public List<AudioClip> Fed2ConstructingBegan;
	public List<AudioClip> Fed2ConstructingEnd;
	public List<AudioClip> Fed2ConstructingCanseled;

	public List<AudioClip> Fed3Attack;
	public List<AudioClip> Fed3LocationInvalid;
	public List<AudioClip> Fed3Move;
	public List<AudioClip> Fed3IsUnderAttack;
	public List<AudioClip> Fed3Fix;
	public List<AudioClip> Fed3Select;
	public List<AudioClip> Fed3LowResuses;
	public List<AudioClip> Fed3ConstructingBegan;
	public List<AudioClip> Fed3ConstructingEnd;
	public List<AudioClip> Fed3ConstructingCanseled;

	public List<AudioClip> Fed4Attack;
	public List<AudioClip> Fed4LocationInvalid;
	public List<AudioClip> Fed4Move;
	public List<AudioClip> Fed4IsUnderAttack;
	public List<AudioClip> Fed4Fix;
	public List<AudioClip> Fed4Select;
	public List<AudioClip> Fed4LowResuses;
	public List<AudioClip> Fed4ConstructingBegan;
	public List<AudioClip> Fed4ConstructingEnd;
	public List<AudioClip> Fed4ConstructingCanseled;

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
				CurAttack = Borg1Attack.ToList();
				CurLocationInvalid = Borg1LocationInvalid.ToList();
				CurMove = Borg1Move.ToList();
				CurIsUnderAttack = Borg1IsUnderAttack.ToList();
				CurFix = Borg1Fix.ToList();
				CurSelect = Borg1Select.ToList();
				CurLowResuses = Borg1LowResuses.ToList();
				CurConstructingBegan = Borg1ConstructingBegan.ToList();
				CurConstructingEnd = Borg1ConstructingEnd.ToList();
				CurConstructingCanseled = Borg1ConstructingCanseled.ToList();
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
				CurAttack = Fed1Attack.ToList();
				CurLocationInvalid = Fed1LocationInvalid.ToList();
				CurMove = Fed1Move.ToList();
				CurIsUnderAttack = Fed1IsUnderAttack.ToList();
				CurFix = Fed1Fix.ToList();
				CurSelect = Fed1Select.ToList();
				CurLowResuses = Fed1LowResuses.ToList();
				CurConstructingBegan = Fed1ConstructingBegan.ToList();
				CurConstructingEnd = Fed1ConstructingEnd.ToList();
				CurConstructingCanseled = Fed1ConstructingCanseled.ToList();
			}
			if (CaptanNum == 2)
			{
				CurAttack = Fed2Attack.ToList();
				CurLocationInvalid = Fed2LocationInvalid.ToList();
				CurMove = Fed2Move.ToList();
				CurIsUnderAttack = Fed2IsUnderAttack.ToList();
				CurFix = Fed2Fix.ToList();
				CurSelect = Fed2Select.ToList();
				CurLowResuses = Fed2LowResuses.ToList();
				CurConstructingBegan = Fed2ConstructingBegan.ToList();
				CurConstructingEnd = Fed2ConstructingEnd.ToList();
				CurConstructingCanseled = Fed2ConstructingCanseled.ToList();
			}
			if (CaptanNum == 3)
			{
				CurAttack = Fed3Attack.ToList();
				CurLocationInvalid = Fed3LocationInvalid.ToList();
				CurMove = Fed3Move.ToList();
				CurIsUnderAttack = Fed3IsUnderAttack.ToList();
				CurFix = Fed3Fix.ToList();
				CurSelect = Fed3Select.ToList();
				CurLowResuses = Fed3LowResuses.ToList();
				CurConstructingBegan = Fed3ConstructingBegan.ToList();
				CurConstructingEnd = Fed3ConstructingEnd.ToList();
				CurConstructingCanseled = Fed3ConstructingCanseled.ToList();
			}
			if (CaptanNum == 4)
			{
				CurAttack = Fed4Attack.ToList();
				CurLocationInvalid = Fed4LocationInvalid.ToList();
				CurMove = Fed4Move.ToList();
				CurIsUnderAttack = Fed4IsUnderAttack.ToList();
				CurFix = Fed4Fix.ToList();
				CurSelect = Fed4Select.ToList();
				CurLowResuses = Fed4LowResuses.ToList();
				CurConstructingBegan = Fed4ConstructingBegan.ToList();
				CurConstructingEnd = Fed4ConstructingEnd.ToList();
				CurConstructingCanseled = Fed4ConstructingCanseled.ToList();
			}
		}
	}
}