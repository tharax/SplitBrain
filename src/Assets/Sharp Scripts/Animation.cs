using UnityEngine;
using System.Collections;

public class Animation : MonoBehaviour 
{
	private int _uvTieX = 12;
	private int _uvTieY = 1;
	private int _fps = 10;
 
	private Vector2 _size;
	private Renderer _myRenderer;
	private int _lastIndex = -1;
	private float prevZ;
	private float startTime;
	private bool screen = false;
 
	void Start () 
	{
		startTime = Time.time;
		if(gameObject.CompareTag("Player")){
			_uvTieX = 6;	
		}
		else if(gameObject.CompareTag("Enemy")){
			_uvTieX = 8;	
		}
		else if(gameObject.CompareTag("Fire")){
			_uvTieX = 3;	
		}
		else if(gameObject.CompareTag("Death")){
			_uvTieX = 4;
			screen = true;
		}
		_size = new Vector2 (1.0f / _uvTieX , 1.0f / _uvTieY);
		_myRenderer = renderer;
		if(_myRenderer == null){
			enabled = false;
		}
	}

	// Update is called once per frame
	void Update()
	{
		float currentZ = transform.position.z;
		if(prevZ == currentZ){
			Move(0);
		}
		else {
			Move(-1);
		}
		prevZ = currentZ;
	}
	
	void Move(int direction){

				// Calculate index
		//int index = (int)(Time.timeSinceLevelLoad * _fps) % (_uvTieX * _uvTieY);
		int index = (int)((Time.time - startTime)* _fps) % (_uvTieX * _uvTieY);

		//int index = (int)Time.time;


    	if(index != _lastIndex)
		{		
			// split into horizontal and vertical index
			int uIndex = index % _uvTieX;
			int vIndex = index / _uvTieY;

 			if(!screen){
				if(direction == 0){
					index = 0;
					uIndex = 0;
				}
			}
			else{
				if(_lastIndex ==3){
					uIndex =3;
					index = 3;
				}
			}
			// build offset
			// v coordinate is the bottom of the image in opengl so we need to invert.
			Vector2 offset = new Vector2 (uIndex * _size.x, 1.0f - _size.y - vIndex * _size.y);
			_myRenderer.material.SetTextureOffset ("_MainTex", offset);
			_myRenderer.material.SetTextureScale ("_MainTex", _size); 

			_lastIndex = index;
		}
	}
}