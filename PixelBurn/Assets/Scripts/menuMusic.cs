using UnityEngine;
using System.Collections;

public class menuMusic : MonoBehaviour {

	bool stayAlive;

	// Use this for initialization
	void Start () {
	
		stayAlive = true;

	}

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}
	
	// Update is called once per frame
	void Update () {


	}
}
