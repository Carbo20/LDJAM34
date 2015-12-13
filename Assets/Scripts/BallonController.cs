using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEditor.Animations;

public class BallonController : MonoBehaviour {

    private bool isMoving, canMove, isSlow, isDead;
    [SerializeField]
    private float speed, slowMod, rotspeed, moveCd, growSpeed;
    private float elapsedTime, moveOnCd;
    private Animator anim;
	// Use this for initialization
	void Start () {
        isMoving = false;
        canMove = false;
        isSlow = false;
        isDead = false;
        elapsedTime = 0;
        anim = GetComponent<Animator>();
	}

    private void init()
    {
        GetComponent<TrailRenderer>().enabled = true;
        GetComponent<TrailRenderer>().Clear();
        GetComponent<TrailRenderer>().time = 0.15f;
        transform.localScale = new Vector3(.5f, .5f, 0);
        transform.position = new Vector3(0, 0, 0);
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        moveOnCd = 0;
        isDead = false;
        anim.Play("Idle");
    }

    void Update()
    {
        
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

        if (isDead && Input.GetMouseButton(0))
        {
            init();
        }

        if (canMove)
        {
            Grow();
            Move();
        }

        

        if (!isDead && !canMove)
        {
            if (moveOnCd < moveCd)
            {
                moveOnCd += Time.deltaTime;
                GetComponent<CircleCollider2D>().enabled = false;
            }
            else
            {
                canMove = true;
                GetComponent<CircleCollider2D>().enabled = true;
            }
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
           

            Die();
            
        }
        if (c.gameObject.tag == "Slow")
        {
            isSlow = true;
        }
        if (c.gameObject.tag == "ModifyScale")
        {
            ModifyScale(c.gameObject.GetComponent<Obstacles>().GetScaleAmountToModify());
            Destroy(c.gameObject);
        }
    }
    void OnTriggerExit2D(Collider2D c)
    {
        if (c.gameObject.tag == "Slow")
        {
            isSlow = false;
        }
    }

    private void Die()
    {
        isDead = true;
        anim.SetTrigger("Die");
        canMove = false;
        GetComponent<TrailRenderer>().enabled = false;
    }

    #region Ajout-luc

    public void ModifyScale(float amount)
    {
        transform.localScale = new Vector3(transform.localScale.x + amount, transform.localScale.y + amount, 0);
    }

    #endregion

}
