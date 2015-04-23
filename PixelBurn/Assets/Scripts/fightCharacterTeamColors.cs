using UnityEngine;
using System.Collections;

public class characterTeamColors : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(this.tag == "Team 1")
			this.GetComponent<SpriteRenderer>().color = GameObject.Find("World").GetComponent<WorldRules>().teamColors [PlayerPrefs.GetInt ("Player 1 Color")]; 
		if(this.tag == "Team 2")
			this.GetComponent<SpriteRenderer>().color = GameObject.Find("World").GetComponent<WorldRules>().teamColors [PlayerPrefs.GetInt ("Player 2 Color")]; 

	}
}
