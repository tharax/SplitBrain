using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {
	GameObject gameInfo;
	float speed = 1f;
	int direction = -1;
	float smoothFactor = 1;

	// Use this for initialization
	void Start () {
		gameInfo = GameObject.Find("GameInfo");
		Debug.Log (gameInfo.transform.position.z);
		speed = gameInfo.transform.position.z*0.2f+1;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 targetPosition = new Vector3(1.01f, transform.position.y, transform.position.z+ direction*speed);
		transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothFactor);
		
	}
	
	void OnTriggerEnter(Collider other){
		if(other.gameObject.CompareTag("Player")){
			Destroy(other.gameObject);
		}
	}
}