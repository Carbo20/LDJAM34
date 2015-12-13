using UnityEngine;
using System.Collections;

public class LittleBalloonScript : MonoBehaviour {

    private bool isAttached;
    [SerializeField]
    private int levelID;
    [SerializeField]
    private GameObject Player, Backdoor;
    [SerializeField]
    private float attachRange;

    private Vector3 initPos;

	// Use this for initialization
	void Start () {
        isAttached = false;
        initPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (!isAttached && Vector2.Distance(transform.position, Player.transform.position) < attachRange)
        {
            isAttached = true;
            transform.parent = Player.transform;            
            transform.localScale = new Vector3(0.15f / Player.transform.localScale.x, 0.15f / Player.transform.localScale.y, 0);
            transform.position = new Vector3(.5f + Player.transform.position.x, 2.5f + Player.transform.position.y, 0);
            transform.localRotation = Quaternion.identity;
            Backdoor.SetActive(false);
        }

        if (isAttached && Player.GetComponent<BallonController>().IsDead)
        {
            isAttached = false;
            transform.parent = null;
            transform.position = initPos;
            Backdoor.SetActive(true);
        }
	}
}
