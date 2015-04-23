using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class writeText : MonoBehaviour {

	public tacticsCharacterController player1;
	public tacticsCharacterController player2;
	int theNum;
	Text theText;



	WorldRules theRules;

	// Use this for initialization
	void Start () {

		theRules = GameObject.Find("World").GetComponent<WorldRules>();
		theText = GetComponent<Text>();
		player1 = GameObject.Find("Player 1 Select").GetComponent<tacticsCharacterController>();
		player2 = GameObject.Find("Player 2 Select").GetComponent<tacticsCharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
	

		if(theRules.canPlay){
			if (player1.myTurn && player1.playerSelected) {
				theNum = (player1.numSteps - player1.currSteps);
				theText.text = ("Number of steps: " + theNum);
			} else if (player2.myTurn && player2.playerSelected) {
				theNum = (player2.numSteps - player2.currSteps);
				theText.text = ("Number of steps: " + theNum);
			} 


			else if (player1.myTurn){
				theText.text = ("It's Player 1's turn");
					
			} else if (player2.myTurn){
				theText.text = ("It's Player 2's turn");
				
			}

		}
		else
		{


			if(theRules.p1Num == 0)
				theText.text = ("Player 2 Wins!!!");
			if(theRules.p2Num == 0)
				theText.text = ("Player 1 Wins!!!");

		}

		//this.guiText.text = ("Number of steps: " + theNum);


	}
}
