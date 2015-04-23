using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class furyControls : MonoBehaviour {


	GameObject sR;
	public GameObject spotLight;
	public bool furyOn;
	GameObject cam;
	Vector3 defaultCamLoc;
	float defaultCamSize;

	public GameObject playerOneChar;
	public GameObject playerTwoChar;
	Vector3 currLoc;

	float smooth = 10f;

	int winner;

	bool p1HasChosen;
	bool p2HasChosen;

	GameObject p1Select;
	GameObject p2Select;

	int p1Choice;
	int p2Choice;

	public float player1Track = 1;
	public float player2Track = 1;
	public float counter = 0;

	public Text topText;
	public Text bottomText;
	public Text controls;
	
	bool inReveal;
	bool canMinus;
	bool countingDown = false;

	public AudioClip furyStart;

	// Use this for initialization
	void Start () {
	
		//sR = GameObject.FindGameObjectWithTag("furyBackground");
		cam = GameObject.FindGameObjectWithTag ("MainCamera");
		defaultCamLoc = cam.transform.position;
		defaultCamSize = cam.GetComponent<Camera> ().orthographicSize;
		furyOn = false;
		//sR.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0);
		//spotLight.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0);

		p1Select = GameObject.Find ("Player 1 Select");
		p2Select = GameObject.Find ("Player 2 Select");

		winner = 0;

		canMinus = true;
	
	}
	
	// Update is called once per frame
	void Update () {
	

		//if(Input.GetKeyDown(KeyCode.U))
			//furyInitialize(new Vector3(8f, 1f, 0f));
		if(furyOn)
			theFury();

		if(Input.GetKeyDown(KeyCode.B))
			furyEnd();

	}

	public void furyInitialize(Vector3 furyLoc, GameObject p1, GameObject p2)
	{

		Application.LoadLevel("Fight Scene");

		playerOneChar = p1;
		playerTwoChar = p2;
		currLoc = furyLoc;

		Vector2 camMove = new Vector2(playerOneChar.transform.position.x + 0.5f, playerOneChar.transform.position.y);

		cam.transform.position = camMove;
		cam.GetComponent<Camera> ().orthographicSize = defaultCamSize;

		if (!furyOn) {
		
			audio.PlayOneShot(furyStart);

			spotLight.SetActive(true);

			p1Choice = 0;
			p2Choice = 0;
			
			//sR.transform.position = furyLoc;
			//sR.GetComponent<SpriteRenderer> ().color = new Color (255, 255, 255, 255);

			//topText.color = new Color (255, 255, 255, 255);
			//bottomText.color = new Color (255, 255, 255, 255);
			canMinus = true;
			furyOn = true;

		}

	}

	void theFury(){

		if (!inReveal) {

			topText.text = ("GET READY TO FURY!!!");
			bottomText.text = ("Hammer, Magic, or Blade");
			controls.enabled = true;
		}



		if(!p1HasChosen){
			if(Input.GetKeyDown(KeyCode.C)){
			   p1Choice = 1;
				p1HasChosen = true;
			}
			if(Input.GetKeyDown(KeyCode.V)){
				p1Choice = 2;
				p1HasChosen = true;
			}
			if(Input.GetKeyDown(KeyCode.B)){
				p1Choice = 3;
				p1HasChosen = true;
			}
		}
		if(!p2HasChosen){
			if(Input.GetKeyDown(KeyCode.I)){
				p2Choice = 1;
				p2HasChosen = true;
			}
			if(Input.GetKeyDown(KeyCode.O)){
				p2Choice = 2;
				p2HasChosen = true;
			}
			if(Input.GetKeyDown(KeyCode.P)){
				p2Choice = 3;
				p2HasChosen = true;
			}
		}



		if(p1HasChosen && p2HasChosen){

			StartCoroutine(theReveal());

		}

	}

	void startFury(int p1, int p2){

		PlayerPrefs.SetInt("Player 1 Weapon", p1);
		PlayerPrefs.SetInt("Player 2 Weapon", p2);
		Application.LoadLevel("Grass Fight Scene");
	
	}

	/*
	int compareFury(int p1, int p2){

		controls.enabled = false;

		Debug.Log("Compare check");

		if ((p1 == 1) && (p2 == 1)) {
			bottomText.text = "FURY!!!";
			//return struggle ();
		}
		if ((p1 == 2) && (p2 == 2)) {
			bottomText.text = "FURY!!!";
			//return struggle ();
				}
		if ((p1 == 3) && (p2 == 3)) {
			bottomText.text = "FURY!!!";
			//return struggle ();
				}

		// Player 1 Victory
		if ((p1 == 2) && (p2 == 1)) {
						bottomText.text = "Magic beats Hammer";
						return 1;
				}
		if ((p1 == 3) && (p2 == 2)) {
						bottomText.text = "Blade beats Magic";
						return 1;
				}
		if ((p1 == 1) && (p2 == 3)) {
						bottomText.text = "Hammer beats Blade";
						return 1;
				}

		// Player 2 Victory
		if ((p1 == 1) && (p2 == 2)) {
						bottomText.text = "Magic beats Hammer";
						return 2;
				}
		if ((p1 == 2) && (p2 == 3)) {
						bottomText.text = "Blade beats Magic";
						return 2;
				}
		if ((p1 == 3) && (p2 == 1)) {
						bottomText.text = "Hammer beats Blade";
						return 2;
				}

		return 0;

	}

	int struggle(){

		slideBar.gameObject.SetActive(true);

		if(!inStruggle){

			player1Track = 1f;
			player2Track = 1f;

		}

		float total = player1Track + player2Track;

		inStruggle = true;

		Debug.Log("Check 1");


		// Player 1 Struggle Input
		if((sliderActive) && ((Input.GetKeyDown(KeyCode.Alpha1)) || (Input.GetKeyDown(KeyCode.Alpha2)) || (Input.GetKeyDown(KeyCode.Alpha3))))
		{

			player1Track++;
			Debug.Log("Player 1 Plus Plus");
		}

		// Player 2 Struggle Input
		if((sliderActive) && (Input.GetKeyDown(KeyCode.J)) || (Input.GetKeyDown(KeyCode.K)) || (Input.GetKeyDown(KeyCode.L)))
		{
			
			player2Track++;
			Debug.Log("Player 2 Plus Plus");
		}


		total = player1Track + player2Track;
		theSlide.value = player1Track/total;


		if(player1Track >= player2Track)
			return 1;
		else
			return 2;

		//return 1;

	}*/


	// IEnumerator breaking the code!!! Infinite while loop! Coroutine in coroutine?
	IEnumerator countdown(){

		//countingDown = true;
		yield return new WaitForSeconds(3);
		countingDown = false;


	}

	void furyEnd()
	{
		if (furyOn) {
			cam.transform.position = defaultCamLoc;
			cam.GetComponent<Camera> ().orthographicSize = defaultCamSize;
			sR.GetComponent<SpriteRenderer> ().color = new Color (0, 0, 0, 0);
			spotLight.GetComponent<SpriteRenderer> ().color = new Color (0, 0, 0, 0);
			topText.color = new Color (0, 0, 0, 0);
			bottomText.color = new Color (0, 0, 0, 0);

			p1Choice = 0;
			p2Choice = 0;
			p1HasChosen = false;
			p2HasChosen = false;

			p1Select.GetComponent<tacticsCharacterController>().onEnemy = false;
			p2Select.GetComponent<tacticsCharacterController>().onEnemy = false;

			furyOn = false;		
			inReveal = false;

			this.GetComponent<WorldRules>().changeTurns();
		}


	}

	IEnumerator theReveal(){

		inReveal = true;

		playerOneChar.GetComponent<characterScript>().showWeapon(p1Choice);
		playerTwoChar.GetComponent<characterScript>().showWeapon(p2Choice);

		//winner = compareFury(p1Choice, p2Choice);

		yield return new WaitForSeconds(3);

		startFury (p1Choice, p2Choice);
		/*
		sliderActive = false;
		topText.text = "Player " + winner + " wins!!!";


		yield return new WaitForSeconds(2);

		if(winner == 1){
			Destroy(playerTwoChar);
			playerTwoChar = null;
			if(canMinus){
				this.GetComponent<WorldRules>().p2Num--;
				canMinus = false;
			}
			playerOneChar.transform.position = currLoc;
			playerOneChar.GetComponent<characterScript>().hideWeapons();
		}
		if(winner == 2){
			Destroy(playerOneChar);
			playerOneChar = null;
			if(canMinus){
				this.GetComponent<WorldRules>().p1Num--;
				canMinus = false;
			}
			playerTwoChar.transform.position = currLoc;
			playerTwoChar.GetComponent<characterScript>().hideWeapons();
		}*/

		furyEnd();

	}
}
