using UnityEngine;
using System.Collections;

public class furyWeaponHolder : MonoBehaviour {


	public enum weaponType {

		Hammer,
		Staff,
		Sword

	};

	public weaponType thisWeapon;

	public Sprite[] weapons;

	SpriteRenderer spriteRender;

	int weaponChoice;
	
	// Use this for initialization
	void Start () {

		spriteRender = this.GetComponent<SpriteRenderer> ();

	}
	
	// Update is called once per frame
	void Update () {
	
		if (spriteRender.sprite != weapons [weaponChoice])
			spriteRender.sprite = weapons [weaponChoice];


	}

}
