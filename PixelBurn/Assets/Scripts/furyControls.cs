using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class furyControls : MonoBehaviour {


	GameObject sR;
	GameObject spotLight;
	public bool furyOn;
	GameObject cam;
	Vector3 defaultCamLoc;
	float defaultCamSize;

	public GameObject slideBar;
	public Slider theSlide;

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

	public int player1track = 1;
	public int player2track = 1;
	
	public Text topText;
	public Text bottomText;

	bool inStruggle;
	bool inReveal;

	// Use this for initialization
	void Start () {
	
		sR = GameObject.FindGameObjectWithTag("furyBackground");
		spotLight = GameObject.FindGameObjectWithTag("Spotlight");
		cam = GameObject.FindGameObjectWithTag ("MainCamera");
		defaultCamLoc = cam.transform.position;
		defaultCamSize = cam.GetComponent<Camera> ().orthographicSize;
		furyOn = false;
		sR.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0);
		spotLight.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0);

		p1Select = GameObject.Find ("Player 1 Select");
		p2Select = GameObject.Find ("Player 2 Select");

		winner = 0;

		inStruggle = false;

		slideBar.gameObject.SetActive(false);

	
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

		playerOneChar = p1;
		playerTwoChar = p2;
		currLoc = furyLoc;

		if (!furyOn) {
			cam.transform.position = currLoc;
			cam.transform.position -= new Vector3 (0, 0, 10);
			cam.GetComponent<Camera> ().orthographicSize = 2.8f;

			spotLight.transform.position = furyLoc;
			spotLight.transform.localScale = new Vector3 (1.8f, 1.8f, 1.8f);
			spotLight.GetComponent<SpriteRenderer> ().color = new Color (255, 255, 255, 255);

			slideBar.gameObject.SetActive(false);

			sR.transform.position = furyLoc;
			sR.GetComponent<SpriteRenderer> ().color = new Color (255, 255, 255, 255);

			topText.color = new Color (255, 255, 255, 255);
			bottomText.color = new Color (255, 255, 255, 255);

			furyOn = true;
		}

	}

	void theFury(){

		if (!inReveal) {

			topText.text = ("GET READY TO FURY!!!");
			bottomText.text = ("Hammer, Magic, or Blade");

		}



		if(!p1HasChosen){
			if(Input.GetKeyDown(KeyCode.Z) && !inStruggle){
			   p1Choice = 1;
				p1HasChosen = true;
			}
			if(Input.GetKeyDown(KeyCode.X) && !inStruggle){
				p1Choice = 2;
				p1HasChosen = true;
			}
			if(Input.GetKeyDown(KeyCode.C) && !inStruggle){
				p1Choice = 3;
				p1HasChosen = true;
			}
		}
		if(!p2HasChosen){
			if(Input.GetKeyDown(KeyCode.J) && !inStruggle){
				p2Choice = 1;
				p2HasChosen = true;
			}
			if(Input.GetKeyDown(KeyCode.K) && !inStruggle){
				p2Choice = 2;
				p2HasChosen = true;
			}
			if(Input.GetKeyDown(KeyCode.L) && !inStruggle){
				p2Choice = 3;
				p2HasChosen = true;
			}
		}



		if(p1HasChosen && p2HasChosen){

			StartCoroutine(theReveal());

		}

	}

	int compareFury(int p1, int p2){

		if ((p1 == 1) && (p2 == 1)) {
			bottomText.text = "TIE!! Player 1 Default Wins";
			return struggle ();
		}
		if ((p1 == 2) && (p2 == 2)) {
						bottomText.text = "TIE!! Player 1 Default Wins";
			return struggle ();
				}
		if ((p1 == 3) && (p2 == 3)) {
						bottomText.text = "TIE!! Player 1 Default Wins";
			return struggle ();
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

	bool countingDown = false;

	int struggle(){

		slideBar.gameObject.SetActive(true);

		int total = player1track + player2track;

		inStruggle = true;

		Debug.Log("Check 1");


		countingDown = true;
		countdown();

		while(countingDown){
			Debug.Log("Check 2");
			// Player 1 Struggle Input
			if((Input.GetKeyDown(KeyCode.Z)) || (Input.GetKeyDown(KeyCode.X)) || (Input.GetKeyDown(KeyCode.C))){

				player1track++;

			}

			// Player 2 Struggle Input
			if((Input.GetKeyDown(KeyCode.J)) || (Input.GetKeyDown(KeyCode.K)) || (Input.GetKeyDown(KeyCode.L))){
				
				player2track++;
				
			}
			Debug.Log("Check 3");

			total = player1track + player2track;
			theSlide.value = total/player1track;
			Debug.Log("Check 4");

		}

	
		inStruggle = false;

		if(player1track >= player2track)
			return 1;
		else
			return 2;
		//return 1;

	}


	// IEnumerator breaking the code!!! Infinite while loop! Coroutine in coroutine?
	IEnumerator countdown(){

		//countingDown = true;
		yield return new WaitForSeconds(7);
		countingDown = false;


	}

	void furyEnd()
	{
		if (furyOn) {
			cam.transform.position = defaultCamLoc;
			cam.GetComponent<Camera> ().orthographicSize = defaultCamSize;
			sR.GetComponent<SpriteRenderer> ().color = new Color (0, 0, 0, 0);
			spotLight.GetComponent<SpriteRenderer> ().color = new Color (0, 0, 0, 0);
			slideBar.gameObject.SetActive(false);
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

		winner = compareFury(p1Choice, p2Choice);

		yield return new WaitForSeconds(3);

		topText.text = "Player " + winner + " wins!!!";


		yield return new WaitForSeconds(2);

		if(winner == 1){
			Destroy(playerTwoChar);
			playerTwoChar = null;
			playerOneChar.transform.position = currLoc;
			playerOneChar.GetComponent<characterScript>().hideWeapons();
		}
		if(winner == 2){
			Destroy(playerOneChar);
			playerOneChar = null;
			playerTwoChar.transform.position = currLoc;
			playerTwoChar.GetComponent<characterScript>().hideWeapons();
		}

		furyEnd();

	}
}
