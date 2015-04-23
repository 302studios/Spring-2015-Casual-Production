using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WorldRules : MonoBehaviour {


	public enum GridValues{

		plain = 1,
		trees = 2,
		mountain = 3

	}

	public bool canPlay;
	public bool playerOneTurn;
	//public Image backPanel;

	public Color[] teamColors;

	public int p1Num;
	public int p2Num;

	GameObject laMusica;

	public Text playAgain;
	public Image againBG;
 

	// Use this for initialization
	void Start () {
	
		playerOneTurn = true;
		Camera.main.backgroundColor = teamColors [PlayerPrefs.GetInt ("Player 1 Color")]; 
		canPlay = true;

		laMusica = GameObject.Find("Menu Music");
	}

	// Update is called once per frame
	void Update () {
	
		if(laMusica != null)
			Destroy(laMusica);

		if(Input.GetKeyDown(KeyCode.Y))
		   changeTurns();
		if(Input.GetKeyDown(KeyCode.Escape))
		   Application.Quit();

		if(canPlay == true){

//			againBG.enabled = false;
			//playAgain.enabled = false;

		}

		if(p1Num == 0|| p2Num == 0){
			canPlay = false;
			//againBG.enabled = true;
			playAgain.enabled = true;

			if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)){

				Application.LoadLevel("Main Menu");

			}

		}



	}

	public void changeTurns(){

		playerOneTurn = !playerOneTurn;
	
		if(playerOneTurn)
			Camera.main.backgroundColor = teamColors [PlayerPrefs.GetInt ("Player 1 Color")]; 
		
		else
			Camera.main.backgroundColor = teamColors [PlayerPrefs.GetInt ("Player 2 Color")]; 

	}
}
