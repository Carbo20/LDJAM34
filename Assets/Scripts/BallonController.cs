using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BallonController : MonoBehaviour {

    private bool isMoving, canMove, isSlow;
    [SerializeField]
    private float speed, slowMod, rotspeed, moveCd, growSpeed;
    private float elapsedTime, moveOnCd;

	// Use this for initialization
	void Start () {
        isMoving = false;
        canMove = true;
        isSlow = false;
        elapsedTime = 0;
	}

    private void init()
    {
        GetComponent<TrailRenderer>().Clear();
        GetComponent<TrailRenderer>().time = 0.15f;
        transform.localScale = new Vector3(.5f, .5f, 0);
        transform.position = new Vector3(0, 0, 0);
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }

	
	// Update is called once per frame
	void FixedUpdate () {

        

        elapsedTime += Time.deltaTime;

        /*if (Input.GetMouseButton(0))
            isMoving = true;
        else
            isMoving = false;*/

        if (canMove && Input.GetMouseButton(0))
            transform.Rotate(Vector3.forward, rotspeed);
        if (canMove && Input.GetMouseButton(1))
            transform.Rotate(Vector3.forward, -rotspeed);

        if (canMove)
        {
            Grow();
            Move();
        }

        

        if (!canMove)
        {
            if (moveOnCd < moveCd)
                moveOnCd += Time.deltaTime;
            else
                canMove = true;
        }
	}

    private void Move()
    {
        transform.Translate(isSlow ? (speed - 2) * Time.deltaTime : speed * Time.deltaTime, 0, 0);
    }

    private void Grow()
    {
        transform.localScale = new Vector3(transform.localScale.x + growSpeed * Time.deltaTime, transform.localScale.y + growSpeed * Time.deltaTime, 0);
        GetComponent<TrailRenderer>().time += Time.deltaTime * 0.25f;
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Kill")
        {
            init();
            canMove = false;
            moveOnCd = 0;
        }
        if (c.gameObject.tag == "Slow")
        {
            isSlow = true;
        }
    }
    void OnTriggerExit2D(Collider2D c)
    {
        if (c.gameObject.tag == "Slow")
        {
            isSlow = false;
        }
    }

}
