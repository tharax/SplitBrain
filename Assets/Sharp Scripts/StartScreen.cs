using UnityEngine;
using System.Collections;

public class StartScreen : MonoBehaviour {
	public Texture texture;

	// Use this for initialization
	void Start () {
		
	}
	
	void OnGUI(){
		GUI.DrawTexture(new Rect( 0, 0, Screen.width,  Screen.height), texture, ScaleMode.StretchToFill, true, 10.0F);

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Space)){
			Application.LoadLevel(1);	
		}
	}
}
