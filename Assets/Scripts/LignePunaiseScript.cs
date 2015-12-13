using UnityEngine;
using System.Collections;

public class LignePunaiseScript : MonoBehaviour {

    [SerializeField]
    private Transform start, end;
    [SerializeField]
    private int nbPin;
    [SerializeField]
    private GameObject pin;
    [SerializeField]
    private bool randomColor;
    

	// Use this for initialization
	void Start () {
        for (int i = 0; i < nbPin; i++)
        {
            GameObject gao = Instantiate(pin);
            gao.transform.position = Vector2.Lerp(start.position, end.position, (float)i /  (float)nbPin);
            if(randomColor)
                gao.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        }
	}

    // Update is called once per frame
    void Update()
    {

	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(start.position, end.position);
    }
}
