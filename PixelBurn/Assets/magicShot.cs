using UnityEngine;
using System.Collections;

public class magicShot : MonoBehaviour {

	public int shotNum;
	public bool facingRight;
	public float moveRate;
	public int playerNum;
	SpriteRenderer colorHold;
	Light flashLight;



	// Use this for initialization
	void Start () {
	
		Destroy(this.gameObject, .6f);
		colorHold = this.GetComponent<SpriteRenderer>();
		flashLight = this.GetComponentInChildren<Light>();
		flashLight.color = colorHold.color;

	}
	
	// Update is called once per frame
	void Update () {
	
		float tempX = this.transform.position.x;
		float tempY = this.transform.position.y;

		if(facingRight){
			tempX += moveRate;
		}
		else{
			tempX -= moveRate;
		}

		switch(shotNum){

			case 1:
				tempY += moveRate/4;
				this.transform.position = new Vector3(tempX, tempY, this.transform.position.z);
				break;
			case 2:
				this.transform.position = new Vector3(tempX, tempY, this.transform.position.z);
				break;
			case 3:
				tempY -= moveRate/4;
				this.transform.position = new Vector3(tempX, tempY, this.transform.position.z);
				break;
			default:
				break;

		}

	}

	void OnTriggerStay2D(Collider2D col){

		if(col.tag == "Player 1 Character" && playerNum == 2){
			col.gameObject.GetComponent<furyCharacterController>().knockBack();
		}
		if(col.tag == "Player 2 Character" && playerNum == 1){
			col.gameObject.GetComponent<furyCharacterController>().knockBack();
		}

	}

}
