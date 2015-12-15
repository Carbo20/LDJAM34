using UnityEngine;
using System.Collections;

public class BoxScript : MonoBehaviour {

    [SerializeField]
    private float LifeTime;

    private float timeElapsed;
    //private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
        timeElapsed = 0;
        //spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= LifeTime)
        {
            //spriteRenderer.color = new Color()
            Destroy(gameObject);
        }
	}
}
