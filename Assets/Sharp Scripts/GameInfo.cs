using UnityEngine;
using System.Collections;

public class GameInfo : MonoBehaviour {
		
	void Awake(){
		DontDestroyOnLoad (transform.gameObject);
	}
}
