using UnityEngine;
using System.Collections;

public class Fire_Platform : MonoBehaviour {
	
	float timer;
	bool fire;
	// Use this for initialization
	void Start () {
		timer = Time.time+1;
		fire = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(timer < Time.time){
			fire = !fire;
			timer += 3;
		}
	}
}
