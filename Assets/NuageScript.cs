using UnityEngine;
using System.Collections;

public class NuageScript : MonoBehaviour {

    private Vector2 speed;

	// Use this for initialization
	void Start () {
        speed = new Vector2(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f));
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(-speed * Time.deltaTime);
	}
}
