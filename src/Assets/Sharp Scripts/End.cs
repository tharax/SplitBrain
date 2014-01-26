using UnityEngine;
using System.Collections;

public class End : MonoBehaviour {
	GameObject gameInfo;
	// Use this for initialization
	void Start () {
		gameInfo = GameObject.Find("GameInfo");
		Destroy(gameInfo);
	}
	
	void OnGUI(){
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Space)){
			Application.LoadLevel(0);
		}
	}
}
