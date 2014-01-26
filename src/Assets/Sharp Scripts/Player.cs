using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public Texture blood;
	public Texture heart;
	GameObject gameInfo;
	public Fire fire;
	public CharacterMotor cMotor;
	int direction = 0;
	int lives;
	private float maxLives = 3;
	float upTimer; 
	bool invulnerable;
	bool drawBlood = false;
	float bloodTimer;
	// Use this for initialization
	void Start() {
		lives = (int)maxLives;
		bloodTimer = 0f;
		upTimer = Time.time;
		gameInfo = GameObject.Find("GameInfo");
	}
	
	void OnGUI(){
		if(drawBlood){
			GUI.DrawTexture(new Rect(0 , 0, Screen.width,Screen.height), blood, ScaleMode.StretchToFill, true, 10.0F);
		}
		for(int i = 0; i <lives; i++){
			GUI.DrawTexture(new Rect(Screen.width/2 - maxLives*64/2 + i*64 , 50, 64,64), heart, ScaleMode.StretchToFill, true, 10.0F);
		}
	}
	
	// Update is called once per frame
void Update () {
		Normalize();
	}
	
	void OnTriggerEnter(Collider other){
		if(other.gameObject.CompareTag ("Enemy")){
			if(!invulnerable){
				Reset ();
				drawBlood = true;
				bloodTimer = Time.time+0.2f;
				lives-=1;
				invulnerable = true;
				upTimer = Time.time + 0.5f;
				if(lives <= 0 ){
					Die();
				}
//				int random = 1; Random.Range(0, 2);
//				if (random == 1){
//					transform.position =  new Vector3(transform.position.x, transform.position.y+2, transform.position.z);
//				}
			}
			//transform.position = new Vector3(transform.position.y, transform.position.y+0.2f, transform.position.z);
		}
		else if(other.gameObject.CompareTag("Fire")){
			Die();
		}
		else if(other.gameObject.CompareTag("PowerJump")){
			cMotor.jumping.extraHeight = 2;
		}
		else if(other.gameObject.CompareTag("Invulnerability")){
			invulnerable = true;
			upTimer = Time.time + 3;
		}
		else if(other.gameObject.CompareTag("Finish")){
				NextLevel();
		}
	}
	
	void Normalize(){
		transform.position =  new Vector3(1.01f, transform.position.y, transform.position.z);
		if(upTimer < Time.time){
			invulnerable = false;
		}
		if( bloodTimer < Time.time){
			drawBlood = false;	
		}
		//Camera.main.transform.position = new Vector3(0, 200, transform.position.z);
	}
	
	void MakeInvulnerable(){
		//invulnerable = true;
		//	upTimer = Time.time + 3;
	}
	void Die(){
				Application.LoadLevel(8);
	}
	
	void Reset(){
			cMotor.jumping.extraHeight = 1.1f;	
	}
	
	void NextLevel(){
		gameInfo.transform.position = new Vector3(0, 0, gameInfo.transform.position.z+1);
		int nextLevel = (int)gameInfo.transform.position.z;
		nextLevel = nextLevel %8;
		if(nextLevel == 0){
			nextLevel = 1;
		}
		Application.LoadLevel(nextLevel);
	}
	
	void CheckPlatform(){
		RaycastHit hitDown;
		 if (Physics.Raycast(transform.position, Vector3.down, out hitDown)) {
            float distanceDown = hitDown.distance;
			if(hitDown.collider.gameObject.CompareTag("Blue")){
				if(hitDown.distance < 0.5){
					
				}
			}
        }
	}

}
