using UnityEngine;
using System.Collections;

public class WorldRules : MonoBehaviour {


	public enum GridValues{

		plain = 1,
		trees = 2,
		mountain = 3

	}

	public bool playerOneTurn;
	public Animator guiAnim;
 

	// Use this for initialization
	void Start () {
	
		playerOneTurn = true;
		guiAnim.SetBool ("Player 1 Turn", playerOneTurn);

	}

	// Update is called once per frame
	void Update () {
	
		if(Input.GetKeyDown(KeyCode.Y))
		   changeTurns();

	}

	public void changeTurns(){

		playerOneTurn = !playerOneTurn;
		guiAnim.SetBool ("Player 1 Turn", playerOneTurn);
	}
}
