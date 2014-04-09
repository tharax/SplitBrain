using UnityEngine;
using System.Collections;

public class Enemy_AI : MonoBehaviour {
	float speed = 1f;
	int direction = 1;
	float smoothFactor = 1;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 targetPosition = new Vector3(1.01f, transform.position.y, transform.position.z+ direction*speed);
		transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothFactor);
		
	}
	
//	void OnGUI(){
//		GUI.Label (new Rect(10,10, 150, 100), message);	
//	}
	void OnTriggerEnter(Collider other){
		if(other.gameObject.CompareTag("Turn")){
			direction = direction* -1;
		}
		else if(other.gameObject.CompareTag ("Player")){
			Destroy (gameObject);
		}
	}
}
