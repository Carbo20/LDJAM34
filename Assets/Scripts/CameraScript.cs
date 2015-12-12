using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    [SerializeField]
    private GameObject player;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
	}
}
