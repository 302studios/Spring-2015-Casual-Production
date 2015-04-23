using UnityEngine;
using System.Collections;

public class furyCharacterController : MonoBehaviour {
	
	public int playerNum;

	Transform myTransform;

	public bool isFlipped;
	public bool thisFlip;
	public bool isFacingRight;

	Transform player1;
	Transform player2;

	public GameObject selectedCharacter;
	public bool playerSelected;

	public float moveSpeed;

	public GameObject[] weapons;
	public int selectedWeapon;

	Animator anim;

	public bool movingCharacter = false;

	furyControls furyCont;
	checkGrid gridNumCheck;

	public int numSteps;
	public int currSteps;
	public int gridNum;

	public AudioClip bloop;

	public bool isAttacking;
	public bool isSpecial;
	public float knockBackPower;
	public bool inRange;

	// Player Stats
	playerInfo stats;
	Light playerLight;

	// Hammer Stats
	public float speedHammer = 0.1f;
	public GameObject waveInst;

	// Staff Stats
	public float speedStaff = 0.15f;
	public GameObject blastU;
	public GameObject blastF;
	public GameObject blastD;

	// Sword Stats
	public float speedSword = 0.2f;

	// Use this for initialization
	void Start () {

		player1 = GameObject.Find("P1 Fight Char").transform;
		player2 = GameObject.Find("P2 Fight Char").transform;

		stats = this.GetComponent<playerInfo>();
	
		anim = this.GetComponent<Animator>();

		myTransform = this.transform;
		if(transform.localScale.x == 1)
			isFacingRight = true;
		else
			isFacingRight = false; 


		isFlipped = false;
		thisFlip = false;

		knockBackPower = 1f;

		speedHammer = 0.05f;
		speedStaff = 0.1f;
		speedSword = 0.15f;

		selectedWeapon = stats.weaponSel;

		switch (selectedWeapon){

			case (0):
				moveSpeed = speedHammer;
				break;
			case (1):
				moveSpeed = speedStaff;
				break;
			case (2):
				moveSpeed = speedSword;
				break;
			default:
				  break;
		}
		/*
		 * Players Colored Lights
		 * 
		 * playerLight = this.GetComponentInChildren<Light>();
		if(playerNum == 1)
			playerLight.color = GameObject.Find("World").GetComponent<FuryRules>().teamColors [PlayerPrefs.GetInt ("Player 1 Color")]; 
		else
			playerLight.color = GameObject.Find("World").GetComponent<FuryRules>().teamColors [PlayerPrefs.GetInt ("Player 2 Color")]; 
		*/
		weapons[selectedWeapon].GetComponent<SpriteRenderer>().enabled = true;

	}
	
	// Update is called once per frame
	void Update () {

		//roundPosition();

		//Player 1 Controls
		if (playerNum == 1) {

			player1Movement ();
			movingCharacter = false;
			if(thisFlip != isFlipped){
				thisFlip = isFlipped;
				Flip();
			}
		}

		//Player 2 Controls
		if(playerNum == 2){
		
			player2Movement();
			movingCharacter = false;
			if(thisFlip != isFlipped){
				thisFlip = isFlipped;
				Flip();
			}
		}

		if(transform.localScale.x == 1)
			isFacingRight = true;
		else
			isFacingRight = false;

		if(player1.position.x > player2.position.x){
			isFlipped = true;
		}
		else{
			isFlipped = false;
		}

		switch (selectedWeapon){
			
		case (0):
			moveSpeed = speedHammer;
			break;
		case (1):
			moveSpeed = speedStaff;
			break;
		case (2):
			moveSpeed = speedSword;
			break;
		default:
			break;
		}
		
	}
	
	void Flip(){

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;

	}
	

	void player1Movement(){

		if(!isSpecial){
			if (Input.GetKey (KeyCode.RightArrow) && this.transform.position.x <= 15f) {
				transform.Translate (moveSpeed, 0, 0);
				//audio.PlayOneShot(bloop);
				//moveRight();//transform.position = Vector3.Lerp(transform.position, new Vector3((transform.position.x) + 1, transform.position.y),  
			}
			if (Input.GetKey (KeyCode.LeftArrow) && this.transform.position.x >= 0f) {

				transform.Translate (-(moveSpeed), 0, 0);
				//audio.PlayOneShot(bloop);

			}
			if (Input.GetKey (KeyCode.UpArrow) && this.transform.position.y <= 5.75f){
				transform.Translate (0, moveSpeed, 0);
				//audio.PlayOneShot(bloop);		
			}
			if (Input.GetKey (KeyCode.DownArrow) && this.transform.position.y >= -3.8f){
				transform.Translate (0, -(moveSpeed), 0);
				//audio.PlayOneShot(bloop);
			}
			if (Input.GetKeyDown (KeyCode.U) && !isAttacking){
				anim.SetTrigger("Attack");
				StartCoroutine(attacking());
			}
			if (Input.GetKeyDown (KeyCode.I) && !isAttacking){
				anim.SetTrigger("Attack");
				StartCoroutine(specialAttack());
			}
		}
	}

	void player2Movement(){
		if(!isSpecial){
			if (Input.GetKey (KeyCode.D) && this.transform.position.x <= 15f) {
				transform.Translate (moveSpeed, 0, 0);
				//audio.PlayOneShot(bloop);
				//moveRight();//transform.position = Vector3.Lerp(transform.position, new Vector3((transform.position.x) + 1, transform.position.y),  
			}
			if (Input.GetKey (KeyCode.A) && this.transform.position.x >= 0f) {
				transform.Translate (-(moveSpeed), 0, 0);
				//audio.PlayOneShot(bloop);
				
			}
			if (Input.GetKey (KeyCode.W) && this.transform.position.y <= 5.75f){
				transform.Translate (0, moveSpeed, 0);
				//audio.PlayOneShot(bloop);		
			}
			if (Input.GetKey (KeyCode.S) && this.transform.position.y >= -3.8f){
				transform.Translate (0, -(moveSpeed), 0);
				//audio.PlayOneShot(bloop);
			}
			if (Input.GetKeyDown (KeyCode.R) && !isAttacking){
				anim.SetTrigger("Attack");
				StartCoroutine(attacking());
			}
			if (Input.GetKeyDown (KeyCode.T) && !isAttacking){
				anim.SetTrigger("Attack");
				StartCoroutine(specialAttack());
			}
		}
	}

	public void knockBack(){

		Debug.Log("Knockback");

		if(isFacingRight)
			transform.Translate(-(knockBackPower), 0f, 0f);
		else
			transform.Translate(knockBackPower, 0f, 0f);

		stats.health -= 3;

	}

	IEnumerator attacking(){

		isAttacking = true;
		yield return new WaitForSeconds(0.3f);
		if(playerNum == 1 && inRange)
			player2.gameObject.GetComponent<furyCharacterController>().knockBack();
		if(playerNum == 2 && inRange)
		   player1.gameObject.GetComponent<furyCharacterController>().knockBack();
		isAttacking = false;

	}

	IEnumerator specialAttack(){
		
		isAttacking = true;
		isSpecial = true;
		yield return new WaitForSeconds(0.3f);

		if(selectedWeapon == 0){
			GameObject temp = Instantiate(waveInst, this.transform.position, this.transform.rotation) as GameObject;
			temp.GetComponent<shockwave>().playerNum = playerNum;
			if(playerNum == 1)
				temp.GetComponent<SpriteRenderer>().color = GameObject.Find("World").GetComponent<FuryRules>().teamColors [PlayerPrefs.GetInt ("Player 1 Color")]; 
			else
				temp.GetComponent<SpriteRenderer>().color = GameObject.Find("World").GetComponent<FuryRules>().teamColors [PlayerPrefs.GetInt ("Player 2 Color")]; 
			yield return new WaitForSeconds(0.7f);
			isSpecial = false;
			yield return new WaitForSeconds(3f);
			isAttacking = false;
		}



		else if(selectedWeapon == 1){
			GameObject tempU = Instantiate(blastU, this.transform.position, this.transform.rotation) as GameObject;
			GameObject tempF = Instantiate(blastF, this.transform.position, this.transform.rotation) as GameObject;
			GameObject tempD = Instantiate(blastD, this.transform.position, this.transform.rotation) as GameObject;

			tempU.GetComponent<magicShot>().playerNum = playerNum;
			tempF.GetComponent<magicShot>().playerNum = playerNum;
			tempD.GetComponent<magicShot>().playerNum = playerNum;

			tempU.GetComponent<magicShot>().facingRight = isFacingRight;
			tempF.GetComponent<magicShot>().facingRight = isFacingRight;
			tempD.GetComponent<magicShot>().facingRight = isFacingRight;

			if(playerNum == 1){
				tempU.GetComponent<SpriteRenderer>().color = GameObject.Find("World").GetComponent<FuryRules>().teamColors [PlayerPrefs.GetInt ("Player 1 Color")]; 
				tempF.GetComponent<SpriteRenderer>().color = GameObject.Find("World").GetComponent<FuryRules>().teamColors [PlayerPrefs.GetInt ("Player 1 Color")]; 
				tempD.GetComponent<SpriteRenderer>().color = GameObject.Find("World").GetComponent<FuryRules>().teamColors [PlayerPrefs.GetInt ("Player 1 Color")];

			}
			else{
				tempU.GetComponent<SpriteRenderer>().color = GameObject.Find("World").GetComponent<FuryRules>().teamColors [PlayerPrefs.GetInt ("Player 2 Color")]; 
				tempF.GetComponent<SpriteRenderer>().color = GameObject.Find("World").GetComponent<FuryRules>().teamColors [PlayerPrefs.GetInt ("Player 2 Color")]; 
				tempD.GetComponent<SpriteRenderer>().color = GameObject.Find("World").GetComponent<FuryRules>().teamColors [PlayerPrefs.GetInt ("Player 2 Color")]; 
			}
			yield return new WaitForSeconds(0.7f);
			isSpecial = false;
			yield return new WaitForSeconds(3f);
			isAttacking = false;
		}
		else{
			if(playerNum == 1 && inRange)
				player2.gameObject.GetComponent<furyCharacterController>().knockBack();
			if(playerNum == 2 && inRange)
				player1.gameObject.GetComponent<furyCharacterController>().knockBack();
			isAttacking = false;
			isSpecial = false;
		}
		
	}

	void OnTriggerEnter2D(Collider2D col){

		if (col.tag == "Player 2 Character" && playerNum == 1){
			inRange = true;
		}

		if (col.tag == "Player 1 Character" && playerNum == 2){
			inRange = true;
		}

	}

	void OnTriggerExit2D(Collider2D col){
		
		if (col.tag == "Player 2 Character" && playerNum == 1){
			inRange = false;
		}
		
		if (col.tag == "Player 1 Character" && playerNum == 2){
			inRange = false;
		}
		
	}

	/*void player1Movement(){
		
		if (Input.GetKeyDown (KeyCode.A)) {
			
			transform.Translate (-1, 0, 0);
			audio.PlayOneShot(bloop);
		}
		if (Input.GetKeyDown (KeyCode.D)) {
			audio.PlayOneShot(bloop);
			transform.Translate (1, 0, 0);
			//moveRight();//transform.position = Vector3.Lerp(transform.position, new Vector3((transform.position.x) + 1, transform.position.y),  
		}
		if (Input.GetKeyDown (KeyCode.W)){
			transform.Translate (0, 1, 0);
			audio.PlayOneShot(bloop);		
		}
		if (Input.GetKeyDown (KeyCode.S)){
			transform.Translate (0, -1, 0);
			audio.PlayOneShot(bloop);
		}
	}
	
	void player2Movement(){
		
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			audio.PlayOneShot(bloop);
			transform.Translate (1, 0, 0);
		}
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			audio.PlayOneShot(bloop);
			transform.Translate (-1, 0, 0);
			//moveRight();//transform.position = Vector3.Lerp(transform.position, new Vector3((transform.position.x) + 1, transform.position.y),  
		}
		if (Input.GetKeyDown (KeyCode.UpArrow)){
			audio.PlayOneShot(bloop);
			transform.Translate (0, 1, 0);
		}
		if (Input.GetKeyDown (KeyCode.DownArrow)){
			audio.PlayOneShot(bloop);
			transform.Translate (0, -1, 0);
		}
	}*/

}
