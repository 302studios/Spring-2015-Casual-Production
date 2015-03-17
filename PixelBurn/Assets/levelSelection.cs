using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class levelSelection : MonoBehaviour {

	public Image[] selectors;
	int selection;


	// Use this for initialization
	void Start () {
	
		selection = 1;

	}
	
	// Update is called once per frame
	void Update () {
	
		controls ();
		whichSelector ();

	}

	void controls(){

		if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)){
			if(selection == 1)
				selection = 4;
			else
				selection--;
		}
		
		if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)){
			if(selection == 4)
				selection = 1;
			else
				selection++;
		}

		if(Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.J)){
			if(selection == 1)
				Application.LoadLevel("Grass 1");
			else if(selection == 2)
				Application.LoadLevel("Ice 1");
			else if(selection == 3)
				Application.LoadLevel("Fire 1");
			else if(selection == 4)
				Application.LoadLevel("Team Color");
		}



	}

	void whichSelector(){

		if (selection == 1){
			selectors [0].enabled = true;
			selectors [1].enabled = false;
			selectors [2].enabled = false;
			selectors [3].enabled = false;
		}
		if (selection == 2){
			selectors [0].enabled = false;
			selectors [1].enabled = true;
			selectors [2].enabled = false;
			selectors [3].enabled = false;
		}
		if (selection == 3){
			selectors [0].enabled = false;
			selectors [1].enabled = false;
			selectors [2].enabled = true;
			selectors [3].enabled = false;
		}
		if (selection == 4){
			selectors [0].enabled = false;
			selectors [1].enabled = false;
			selectors [2].enabled = false;
			selectors [3].enabled = true;
		}
	}
}
