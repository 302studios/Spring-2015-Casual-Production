using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class writeText : MonoBehaviour {

	public tacticsCharacterController player1;
	public tacticsCharacterController player2;
	int theNum;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (player1.myTurn && player1.playerSelected) {
			theNum = (player1.numSteps - player1.currSteps);
			GetComponent<Text>().text = ("Number of steps: " + theNum);
		} else if (player2.myTurn && player2.playerSelected) {
			theNum = (player2.numSteps - player2.currSteps);
			GetComponent<Text>().text = ("Number of steps: " + theNum);
		} 


		else if (player1.myTurn){
			GetComponent<Text>().text = ("It's Player 1's turn");
				
		} else if (player2.myTurn){
			GetComponent<Text>().text = ("It's Player 2's turn");
			
		}

		//this.guiText.text = ("Number of steps: " + theNum);


	}
}
