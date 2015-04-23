using UnityEngine;
using System.Collections;

public class playerInfo : MonoBehaviour {

	public float playerNum;
	public float health;
	public int weaponSel;


	// Use this for initialization
	void Start () {


		if(playerNum == 1)
			weaponSel = PlayerPrefs.GetInt("Player 1 Weapon");
		if(playerNum == 2)
			weaponSel = PlayerPrefs.GetInt("Player 2 Weapon");
		health = 100f;


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
