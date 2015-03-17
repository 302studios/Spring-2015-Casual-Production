using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class arrowControls : MonoBehaviour {


	public Text player1Header;
	public Text player2Header;
	public Text p1Tag;
	public Text p2Tag;

	public Color[] colorArray;
	public Color defaultColor;

	public float[] colorsPos;
	public GameObject player1Arrow;
	public GameObject player2Arrow;
	public int player1Pos;
	public int player2Pos;
	bool player1Selected;
	bool player2Selected;

	// Use this for initialization
	void Start () {
	
		setPos(1, player1Pos);
		setPos(2, player2Pos);

		player1Selected = false;
		player2Selected = false;

	}
	
	// Update is called once per frame
	void Update () {


		player1Select ();
		player2Select ();
		yayColors();
		continueCheck ();





	}

	void continueCheck(){

		if (player1Selected && player2Selected) {

			PlayerPrefs.SetInt("Player 1 Color", player1Pos);
			PlayerPrefs.SetInt("Player 2 Color", player2Pos);

			Application.LoadLevel("Level Select");

		}

	}

	void yayColors(){

		if (player1Selected) {

			player1Header.color = colorArray[player1Pos];
			p1Tag.color = colorArray[player1Pos];
		}else{
			player1Header.color = defaultColor;
				p1Tag.color = defaultColor;
		}



		if (player2Selected) {
			
			player2Header.color = colorArray[player2Pos];
			p2Tag.color = colorArray[player2Pos];
		}else{
			player2Header.color = defaultColor;
			p2Tag.color = defaultColor;
		}
	}

	void player1Select(){

		// Player 1 Select
		if (!player1Selected) {
			if (Input.GetKeyDown (KeyCode.A)) {
					if (player1Pos == 0) {
							if (player2Pos == 6)
									player1Pos = 5;
							else	
									player1Pos = 6;
					} else {
							player1Pos--;
							if (player2Pos == player1Pos)
							if (player1Pos == 0) {
									if (player2Pos == 6)
											player1Pos = 5;
									else	
											player1Pos = 6;
							} else
									player1Pos--;
					}
					setPos (1, player1Pos);
			}
			if (Input.GetKeyDown (KeyCode.D)) {
					if (player1Pos == 6) {
							if (player2Pos == 0)
									player1Pos = 1;
							else	
									player1Pos = 0;
					} else {
							player1Pos++;
							if (player2Pos == player1Pos)
							if (player1Pos == 6) {
									if (player2Pos == 0)
											player1Pos = 1;
									else	
											player1Pos = 0;
							} else
									player1Pos++;
					}
					setPos (1, player1Pos);
			}
		}

		if(Input.GetKeyDown(KeyCode.Alpha1)){

			player1Selected = !player1Selected;

		}
	}

	void player2Select(){

		// Player 2 Select
		if (!player2Selected) {
			if (Input.GetKeyDown (KeyCode.LeftArrow)) {
					if (player2Pos == 0) {
							if (player1Pos == 6)
									player2Pos = 5;
							else	
									player2Pos = 6;
					} else {
							player2Pos--;
							if (player1Pos == player2Pos)
							if (player2Pos == 0) {
									if (player1Pos == 6)
											player2Pos = 5;
									else	
											player2Pos = 6;
							} else
									player2Pos--;
					}
					setPos (2, player2Pos);
			}
			if (Input.GetKeyDown (KeyCode.RightArrow)) {
					if (player2Pos == 6) {
							if (player1Pos == 0)
									player2Pos = 1;
							else	
									player2Pos = 0;
					} else {
							player2Pos++;
							if (player1Pos == player2Pos)
							if (player2Pos == 6) {
									if (player1Pos == 0)
											player2Pos = 1;
									else	
											player2Pos = 0;
							} else
									player2Pos++;
					}
					setPos (2, player2Pos);
			}
		}

		if (Input.GetKeyDown (KeyCode.J)) {

			player2Selected = !player2Selected;

		}
		
	}

	void setPos(int player, int color){

		if (player == 1) {

			player1Arrow.transform.localPosition = new Vector3(colorsPos[color], player1Arrow.transform.localPosition.y, player1Arrow.transform.localPosition.z);

		}

		if (player == 2) {
			
			player2Arrow.transform.localPosition = new Vector3(colorsPos[color], player2Arrow.transform.localPosition.y, player2Arrow.transform.localPosition.z);
			
		}
	}
}
