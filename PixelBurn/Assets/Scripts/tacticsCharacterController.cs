using UnityEngine;
using System.Collections;

public class tacticsCharacterController : MonoBehaviour {
	
	public int playerNum;

	Transform myTransform;

	public bool isFacingRight;

	public GameObject[] player1Characters;
	public GameObject[] player2Characters;

	public GameObject selectedCharacter;
	public bool playerSelected;

	public bool myTurn;
	public bool onEnemy;
	public GameObject selCharacter;
	public GameObject selEnemy;

	WorldRules theRules;

	Animator anim;

	public GridSetup.TheGrid currSpot;

	static int maxNumSteps = 7;

	public GameObject[] moveArrows;
	GameObject[] arrowsPlaced = new GameObject[maxNumSteps+1];
	public int numArrows;
	public GameObject tempDestroy;
	Vector3 origin = new Vector3(0f, 0f, 0f);

	public bool movingCharacter = false;

	furyControls furyCont;
	checkGrid gridNumCheck;

	public int numSteps;
	public int currSteps;
	public int gridNum;

	public bool headingBack;

	public enum moveTypes{

		None, 
		Up,
		Left,
		Right,
		Down 
	};

	public moveTypes[] theMoves = new moveTypes[maxNumSteps+1];


	// Use this for initialization
	void Start () {
	
		theRules = GameObject.FindGameObjectWithTag ("Rules").GetComponent<WorldRules>();
		numArrows = 0;

		anim = this.GetComponent<Animator>();

		myTransform = this.transform;
		if(transform.localScale.x == 1)
			isFacingRight = true;
		else
			isFacingRight = false;

		player1Characters = GameObject.FindGameObjectsWithTag("Player 1 Character");
		player2Characters = GameObject.FindGameObjectsWithTag("Player 2 Character");

		playerSelected = false;
		theMoves[0] = moveTypes.None;

		furyCont = GameObject.FindGameObjectWithTag("Rules").GetComponent<furyControls>();

		gridNumCheck = this.GetComponent<checkGrid> ();
		numSteps = 7;
		currSteps = 0;

		if(playerNum == 1)
			this.GetComponent<SpriteRenderer> ().color = GameObject.Find ("World").GetComponent<WorldRules> ().teamColors [PlayerPrefs.GetInt ("Player 1 Color")]; 

		if(playerNum == 2)
			this.GetComponent<SpriteRenderer> ().color = GameObject.Find ("World").GetComponent<WorldRules> ().teamColors [PlayerPrefs.GetInt ("Player 2 Color")]; 
		 

		Debug.Log ("Player 1 Color Num: " + PlayerPrefs.GetInt ("Player 1 Color"));
		Debug.Log ("Player 2 Color Num: " + PlayerPrefs.GetInt ("Player 2 Color"));

	}
	
	// Update is called once per frame
	void Update () {

		roundPosition();
		if(playerNum == 1){
			myTurn = theRules.playerOneTurn;
		}
		if(playerNum == 2){
			myTurn = !theRules.playerOneTurn;
		}

		if(!furyCont.furyOn && myTurn){
			this.GetComponent<SpriteRenderer>().enabled = true;

			//Player 1 Controls
			if ((playerNum == 1) && (theRules.playerOneTurn)) {

				if (!playerSelected){
					player1Movement ();
					movingCharacter = false;
				}
				else {
					movingCharacter = true;
					player1SelectedMovement();
				}
				player1Selection ();

			}

			//Player 2 Controls
			if((playerNum == 2) && (!theRules.playerOneTurn)){
				if(!playerSelected){
					player2Movement();
					movingCharacter = false;
				}
				else {
					player2SelectedMovement();
					movingCharacter = true;
				}
				player2Selection();

			}
		}

		if((furyCont.furyOn) || !myTurn){

			this.GetComponent<SpriteRenderer>().enabled = false;

		}
	}

	void Flip(){

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;

	}


	void moveRight(){

		float goal = (transform.position.x) + 1f;

		for(float i = 0f; i<=1f; i += 0.1f)
			transform.position = Vector3.Lerp (transform.position, new Vector3 (goal, transform.position.y), i);

	}

	void roundPosition(){

		int roundedX;
		int roundedY;
		int roundedZ;
		roundedX = Mathf.RoundToInt(this.transform.position.x);
		roundedY = Mathf.RoundToInt(this.transform.position.y);
		roundedZ = Mathf.RoundToInt(this.transform.position.z);
		Vector3 temp = new Vector3 (roundedX, roundedY, roundedZ);
		myTransform.position = temp;
	}

	void player1Movement(){

		if (Input.GetKeyDown (KeyCode.A)) {

			transform.Translate (-1, 0, 0);
		}
		if (Input.GetKeyDown (KeyCode.D)) {

			//transform.Translate (1, 0, 0);
			moveRight();//transform.position = Vector3.Lerp(transform.position, new Vector3((transform.position.x) + 1, transform.position.y),  
		}
		if (Input.GetKeyDown (KeyCode.W))
			transform.Translate (0, 1, 0);
		if (Input.GetKeyDown (KeyCode.S))
			transform.Translate (0, -1, 0);
	}

	void player2Movement(){

		if (Input.GetKeyDown (KeyCode.LeftArrow)) {

			transform.Translate (-1, 0, 0);
		}
		if (Input.GetKeyDown (KeyCode.RightArrow)) {

			//transform.Translate (1, 0, 0);
			moveRight();//transform.position = Vector3.Lerp(transform.position, new Vector3((transform.position.x) + 1, transform.position.y),  
		}
		if (Input.GetKeyDown (KeyCode.UpArrow))
			transform.Translate (0, 1, 0);
		if (Input.GetKeyDown (KeyCode.DownArrow))
			transform.Translate (0, -1, 0);

	}

	void player1SelectedMovement(){

		headingBack = headedBack ();

		if ((currSteps < (numSteps)) || headingBack) {


			if (Input.GetKeyDown (KeyCode.A)) {
	
					//if(lastMove != moveTypes.None)
					dropArrowAndCount (moveTypes.Left);
	
					//theMoves[numArrows] = moveTypes.Left;
					transform.Translate (-1, 0, 0);

			}
			if (Input.GetKeyDown (KeyCode.D)) {
	
					//if(lastMove != moveTypes.None)
					dropArrowAndCount (moveTypes.Right);
	
					//theMoves[numArrows] = moveTypes.Right;
					//transform.Translate (1, 0, 0);
					moveRight ();//transform.position = Vector3.Lerp(transform.position, new Vector3((transform.position.x) + 1, transform.position.y),  

			}
			if (Input.GetKeyDown (KeyCode.W)) {
	
					//if(lastMove != moveTypes.None)
					dropArrowAndCount (moveTypes.Up);
	
					//theMoves[numArrows] = moveTypes.Up;
					transform.Translate (0, 1, 0);


			}
			if (Input.GetKeyDown (KeyCode.S)) {
	
					//if(lastMove != moveTypes.None)
					dropArrowAndCount (moveTypes.Down);
	
					//theMoves[numArrows] = moveTypes.Down;
					transform.Translate (0, -1, 0);

			}

			if (headingBack) {
					currSteps -= gridNumCheck.gridNum;

			}
		}
	}

	void player2SelectedMovement(){

		headingBack = headedBack ();

		if ((currSteps< (numSteps)) || headingBack) {
			if (Input.GetKeyDown (KeyCode.LeftArrow)) {
	
					//if(lastMove != moveTypes.None)
					dropArrowAndCount (moveTypes.Left);
	
					//theMoves[numArrows] = moveTypes.Left;
					transform.Translate (-1, 0, 0);
			}
			if (Input.GetKeyDown (KeyCode.RightArrow)) {
	
					//if(lastMove != moveTypes.None)
					dropArrowAndCount (moveTypes.Right);
	
					//theMoves[numArrows] = moveTypes.Right;
					//transform.Translate (1, 0, 0);
					moveRight ();//transform.position = Vector3.Lerp(transform.position, new Vector3((transform.position.x) + 1, transform.position.y),  
			}
			if (Input.GetKeyDown (KeyCode.UpArrow)) {
	
					//if(lastMove != moveTypes.None)
					dropArrowAndCount (moveTypes.Up);
	
					//theMoves[numArrows] = moveTypes.Up;
					transform.Translate (0, 1, 0);
			}
			if (Input.GetKeyDown (KeyCode.DownArrow)) {
	
					//if(lastMove != moveTypes.None)
					dropArrowAndCount (moveTypes.Down);
	
					//theMoves[numArrows] = moveTypes.Down;
					transform.Translate (0, -1, 0);
			}

			if (headingBack) {
				currSteps -= gridNumCheck.gridNum;

			}
		}
	}
	
	void player1Selection(){

		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			if (playerSelected) {
				if((origin != selectedCharacter.transform.position) && (!onEnemy)){	
					playerSelected = false;
					currSteps = 0;
					selectedCharacter.transform.position = this.transform.position;
					selectedCharacter.transform.parent = null;
					selectedCharacter.transform.localScale = new Vector3(1f, 1f, 1f);
					anim.SetBool("Flash", false);
					arrowsReset();
					theRules.changeTurns();
				}
				else{
					playerSelected = false;
					selectedCharacter.transform.parent = null;
					selectedCharacter.transform.localScale = new Vector3(1f, 1f, 1f);
					anim.SetBool("Flash", false);
					arrowsReset();
				}

				if(onEnemy){
					playerSelected = false;
					selectedCharacter.transform.position = ((this.transform.position) - new Vector3(0.5f, 0f, 0f));
					selEnemy.transform.position = ((this.transform.position) + new Vector3(0.5f, 0f, 0f));
					furyCont.furyInitialize(this.transform.position, selectedCharacter, selEnemy);
				}
			}

			else if(!playerSelected){
				//foreach(GameObject cObj in player1Characters){
					//if(cObj.transform.position == this.transform.position){
						Debug.Log("Player 1 has selected a character.");
						selCharacter.transform.parent = this.transform;
						origin = this.transform.position;
						selectedCharacter = selCharacter;
						selectedCharacter.transform.localScale = new Vector3(.45f, .45f, 1f);
						playerSelected = true;
						currSteps = 0;
						anim.SetBool("Flash", true);
					

			}
		}

	}

	void player2Selection(){

		if (Input.GetKeyDown (KeyCode.J)){
			if(playerSelected){
				if((origin != this.transform.position) && (!onEnemy)){
					playerSelected = false;
					currSteps = 0;
					selectedCharacter.transform.position = this.transform.position;
					selectedCharacter.transform.parent = null;
					selectedCharacter.transform.localScale = new Vector3(1f, 1f, 1f);
					anim.SetBool("Flash", false);
					arrowsReset();
					theRules.changeTurns();
				}
				else{
					playerSelected = false;
					selectedCharacter.transform.parent = null;
					selectedCharacter.transform.localScale = new Vector3(1f, 1f, 1f);
					anim.SetBool("Flash", false);
					arrowsReset();
				}
				
				if(onEnemy){
					playerSelected = false;
					selectedCharacter.transform.position = ((this.transform.position) + new Vector3(0.5f, 0f, 0f));
					selEnemy.transform.position = ((this.transform.position) - new Vector3(0.5f, 0f, 0f));
					furyCont.furyInitialize(this.transform.position, selEnemy, selectedCharacter);
				}
			}

			else if(!playerSelected){
				//foreach(GameObject cObj in player2Characters){
					//if(cObj.transform.position == this.transform.position){
						Debug.Log("Player 2 has selected a character.");
						selCharacter.transform.parent = this.transform;
						origin = this.transform.position;
						selectedCharacter = selCharacter;
						selectedCharacter.transform.localScale = new Vector3(.45f, .45f, 1f);
						playerSelected = true;
						currSteps = 0;
						anim.SetBool("Flash", true);
					
				
			}
		}
		
	}

	void dropArrowAndCount(moveTypes currMove){

		// The Ups
		if(currMove == moveTypes.Up){	
			switch (theMoves[numArrows]){
				case moveTypes.Up:
					arrowsPlaced[numArrows] = Instantiate (moveArrows [0], this.transform.position, new Quaternion (0, 0, 0, 0)) as GameObject;
					numArrows++;
					theMoves[numArrows] = moveTypes.Up;
					break;

				case moveTypes.Left:
					arrowsPlaced[numArrows] = Instantiate (moveArrows [1], this.transform.position, new Quaternion (0, 0, 0, 0)) as GameObject;
					numArrows++;
					
					theMoves[numArrows] = moveTypes.Up;
					break;

				case moveTypes.Right:
					arrowsPlaced[numArrows] = Instantiate (moveArrows [2], this.transform.position, new Quaternion (0, 0, 0, 0)) as GameObject;
					numArrows++;
					
					theMoves[numArrows] = moveTypes.Up;
					break;

				case moveTypes.Down: 
					//arrowsPlaced[numArrows] = Instantiate (moveArrows [0], this.transform.position, new Quaternion (0, 0, 0, 0));
					numArrows--;
					
					tempDestroy = arrowsPlaced[numArrows];
					arrowsPlaced[numArrows] = null;
					Destroy(tempDestroy.gameObject);
					break;

				case moveTypes.None:
					arrowsPlaced[numArrows] = Instantiate (moveArrows [0], this.transform.position, new Quaternion (0, 0, 0, 0)) as GameObject;
					numArrows++;

					theMoves[numArrows] = moveTypes.Up;
				break;

			default:
				break;
			}
		}
		// The Downs
		if(currMove == moveTypes.Down){	
			switch (theMoves[numArrows]){
			case moveTypes.Up:
				numArrows--;
			
				tempDestroy = arrowsPlaced[numArrows];
				arrowsPlaced[numArrows] = null;
				Destroy(tempDestroy.gameObject);
				break;
				
			case moveTypes.Left:
				arrowsPlaced[numArrows] = Instantiate (moveArrows [2], this.transform.position, Quaternion.Euler(new Vector3(0, 0, 180))) as GameObject;
				numArrows++;
			
				theMoves[numArrows] = moveTypes.Down;
				break;
				
			case moveTypes.Right:
				arrowsPlaced[numArrows] = Instantiate (moveArrows [1], this.transform.position, Quaternion.Euler(new Vector3(0, 0, 180))) as GameObject;
				numArrows++;
			
				theMoves[numArrows] = moveTypes.Down;
				break;
				
			case moveTypes.Down: 
				//arrowsPlaced[numArrows] = Instantiate (moveArrows [0], this.transform.position, new Quaternion (0, 0, 0, 0));
				arrowsPlaced[numArrows] = Instantiate (moveArrows [0], this.transform.position, Quaternion.Euler(new Vector3(0, 0, 180))) as GameObject;
				numArrows++;
			
				theMoves[numArrows] = moveTypes.Down;
				break;
				
			case moveTypes.None:
				arrowsPlaced[numArrows] = Instantiate (moveArrows [0], this.transform.position, Quaternion.Euler(new Vector3(0, 0, 180))) as GameObject;
				numArrows++;
			
				theMoves[numArrows] = moveTypes.Down;
				break;
				
			default:
				break;
			}
		}
		// The Lefts
		if(currMove == moveTypes.Left){	
			switch (theMoves[numArrows]){
			case moveTypes.Up:
				arrowsPlaced[numArrows] = Instantiate (moveArrows [2], this.transform.position, Quaternion.Euler(new Vector3(0, 0, 90))) as GameObject;
				numArrows++;
		
				theMoves[numArrows] = moveTypes.Left;
				break;
				
			case moveTypes.Left:
				arrowsPlaced[numArrows] = Instantiate (moveArrows [0], this.transform.position, Quaternion.Euler(new Vector3(0, 0, 90))) as GameObject;
				numArrows++;
			
				theMoves[numArrows] = moveTypes.Left;
				break;
				
			case moveTypes.Right:
				numArrows--;
			
				tempDestroy = arrowsPlaced[numArrows];
				arrowsPlaced[numArrows] = null;
				Destroy(tempDestroy.gameObject);
				break;
				
			case moveTypes.Down: 
				//arrowsPlaced[numArrows] = Instantiate (moveArrows [0], this.transform.position, new Quaternion (0, 0, 0, 0));
				arrowsPlaced[numArrows] = Instantiate (moveArrows [1], this.transform.position, Quaternion.Euler(new Vector3(0, 0, 90))) as GameObject;
				numArrows++;
			
				theMoves[numArrows] = moveTypes.Left;
				break;
				
			case moveTypes.None:
				arrowsPlaced[numArrows] = Instantiate (moveArrows [0], this.transform.position, Quaternion.Euler(new Vector3(0, 0, 90))) as GameObject;
				//arrowsPlaced[numArrows].transform.rotation = Quaternion.Euler
				numArrows++;
			
				theMoves[numArrows] = moveTypes.Left;
				break;
				
			default:
				break;
			}
		}
		// The Rights
		if(currMove == moveTypes.Right){	
			switch (theMoves[numArrows]){
			case moveTypes.Up:
				arrowsPlaced[numArrows] = Instantiate (moveArrows [1], this.transform.position, Quaternion.Euler(new Vector3(0, 0, 270))) as GameObject;
				numArrows++;
			
				theMoves[numArrows] = moveTypes.Right;
				break;
				
			case moveTypes.Left:
				numArrows--;
			
				tempDestroy = arrowsPlaced[numArrows];
				arrowsPlaced[numArrows] = null;
				Destroy(tempDestroy.gameObject);
				break;
				
			case moveTypes.Right:
				arrowsPlaced[numArrows] = Instantiate (moveArrows [0], this.transform.position, Quaternion.Euler(new Vector3(0, 0, 270))) as GameObject;
				numArrows++;
			
				theMoves[numArrows] = moveTypes.Right;
				break;
				
			case moveTypes.Down: 
				//arrowsPlaced[numArrows] = Instantiate (moveArrows [0], this.transform.position, new Quaternion (0, 0, 0, 0));
				arrowsPlaced[numArrows] = Instantiate (moveArrows [2], this.transform.position, Quaternion.Euler(new Vector3(0, 0, 270))) as GameObject;
				numArrows++;
			
				theMoves[numArrows] = moveTypes.Right;
				break;
				
			case moveTypes.None:
				arrowsPlaced[numArrows] = Instantiate (moveArrows [0], this.transform.position, Quaternion.Euler(new Vector3(0, 0, 270))) as GameObject;
				numArrows++;
			
				theMoves[numArrows] = moveTypes.Right;
				break;
				
			default:
				break;
			}
		}
	}

	void arrowsReset(){

		numArrows = 0;
		for (int i=0; i<arrowsPlaced.Length; i++) {
			tempDestroy = arrowsPlaced [i];
			arrowsPlaced [i] = null;
			if(tempDestroy)
				Destroy (tempDestroy.gameObject);
		}
		for(int j=0; j<theMoves.Length; j++)
			theMoves[j] = moveTypes.None;

	}

	bool headedBack(){

		if (playerNum == 1){
			if (theMoves [numArrows] == moveTypes.Up && Input.GetKeyDown (KeyCode.S))
				return true;
			else if (theMoves [numArrows] == moveTypes.Left && Input.GetKeyDown (KeyCode.D))
				return true;
			else if (theMoves [numArrows] == moveTypes.Right && Input.GetKeyDown (KeyCode.A))
				return true;
			else if (theMoves [numArrows] == moveTypes.Down && Input.GetKeyDown (KeyCode.W))
				return true;
			else
				return false;
		}
		if (playerNum == 2) {
			if (theMoves [numArrows] == moveTypes.Up && Input.GetKeyDown (KeyCode.DownArrow))
					return true;
			else if (theMoves [numArrows] == moveTypes.Left && Input.GetKeyDown (KeyCode.RightArrow))
					return true;
			else if (theMoves [numArrows] == moveTypes.Right && Input.GetKeyDown (KeyCode.LeftArrow))
					return true;
			else if (theMoves [numArrows] == moveTypes.Down && Input.GetKeyDown (KeyCode.DownArrow))
					return true;
			else
					return false;
		} else
			return false;

	}

	public void pushBack(){

		int backNum;

		if (theMoves [numArrows] == moveTypes.Up) {
			transform.Translate (0, -1, 0);
			backNum = gridNumCheck.gridNum;
			numArrows--;
			tempDestroy = arrowsPlaced [numArrows];
			arrowsPlaced [numArrows] = null;
			Destroy (tempDestroy.gameObject);
			currSteps -= backNum;
			//transform.Translate (0, -1, 0);
		}

		if (theMoves [numArrows] == moveTypes.Down) {
			transform.Translate (0, 1, 0);
			backNum = gridNumCheck.gridNum;
			numArrows--;
			tempDestroy = arrowsPlaced [numArrows];
			arrowsPlaced [numArrows] = null;
			Destroy (tempDestroy.gameObject);
			currSteps -= backNum;
			//transform.Translate (0, 1, 0);
		}

		if (theMoves [numArrows] == moveTypes.Left) {
			transform.Translate (1, 0, 0);
			backNum = gridNumCheck.gridNum;
			numArrows--;
			tempDestroy = arrowsPlaced [numArrows];
			arrowsPlaced [numArrows] = null;
			Destroy (tempDestroy.gameObject);
			currSteps -= backNum;
			//transform.Translate (1, 0, 0);
		}

		if (theMoves [numArrows] == moveTypes.Right) {
			transform.Translate (-1, 0, 0);
			backNum = gridNumCheck.gridNum;
			numArrows--;
			tempDestroy = arrowsPlaced [numArrows];
			arrowsPlaced [numArrows] = null;
			Destroy (tempDestroy.gameObject);
			currSteps -= backNum;
			//transform.Translate (-1, 0, 0);
		}

	}

	void OnTriggerEnter2D(Collider2D col){

		if(myTurn){
			if(col.tag == "Player 1 Character"){
				Debug.Log("Player 1 in select");

				if(playerNum == 1 && !playerSelected){
					selCharacter = col.gameObject;
				}

				if(playerNum == 2){
					onEnemy = true;
					selEnemy = col.gameObject;
				}
			}
			if(col.tag == "Player 2 Character")
			{
				if(playerNum == 1){
					onEnemy = true;
					selEnemy = col.gameObject;
				}

				if(playerNum == 2 && !playerSelected){
					selCharacter = col.gameObject;
				}
				Debug.Log("Player 2 in select");
			}
		}


	}

	void OnTriggerExit2D(Collider2D col){

		if(myTurn){
			if(col.tag == "Player 1 Character"){
				if(playerNum == 2){
					onEnemy = false;
					selEnemy = null;
				}
				Debug.Log("Player 1 exit");
			}
			if(col.tag == "Player 2 Character"){
				if(playerNum == 1){
					onEnemy = false;
					selEnemy = null;
				}
				Debug.Log("Player 2 exit");
			}
		}
	}

}
