using UnityEngine;
using System.Collections;

public class checkGrid : MonoBehaviour {

	tacticsCharacterController theController;

	public int gridNum;
	public int checkSum;

	// Use this for initialization
	void Start () {
	

		theController = this.GetComponent<tacticsCharacterController> ();

	}
	
	// Update is called once per frame
	void Update () {
	


	}

	void OnTriggerEnter2D(Collider2D col){

		if (col.tag == "Grid") {
			theController.currSpot = col.GetComponent<gridTile> ().tileType;
			gridNum = (int)theController.currSpot;

			checkSum = theController.currSteps + gridNum;

			if (theController.movingCharacter && !theController.headingBack) {
				Debug.Log("Num Steps " + theController.numSteps);
					if (checkSum <= theController.numSteps) {
							Debug.Log ("Did the move");
							theController.currSteps += gridNum;
					} else {
							Debug.Log ("Dafuq?!");
							theController.pushBack ();
					}
			}
		}

	}

}
