using UnityEngine;
using System.Collections;

public class IAInLine : MonoBehaviour {

    [SerializeField]
    float distance;
    [SerializeField]
    float speed;
    [SerializeField]
    bool bSwitchXToYAxis;

    Vector3 dir;
    private float curPos;
    private bool bReturnMove;

    private void Move()
    {
        //Debug.Log("IA is moving in line");

        if (!bSwitchXToYAxis)
        {
            curPos = transform.localPosition.x;
            dir.x = Time.deltaTime * speed;
            dir.y = 0;
        }
        else
        {
            curPos = transform.localPosition.y;
            dir.x = 0;
            dir.y = Time.deltaTime * speed;
        }

        if (curPos < distance)
        {
            //Debug.Log("Aller pos: "+ dir.x);
            transform.Translate(dir.x, dir.y, 0);
        }
        else
        {
            bReturnMove = true;
        }
       
    }

    private void ReturnMove()
    {
        //Debug.Log("IA is moving in line");

        if (!bSwitchXToYAxis)
        {
            curPos = transform.localPosition.x;
            dir.x = -Time.deltaTime * speed;
            dir.y = 0;
        }
        else
        {
            curPos = transform.localPosition.y;
            dir.x = 0;
            dir.y = -Time.deltaTime * speed;
        }

        if (curPos > -distance)
        {
            //Debug.Log("Aller pos: " + dir.x);
           // dir.x = -Time.deltaTime * speed;
            transform.Translate(dir.x, dir.y, 0);
        }
        else
        {
            bReturnMove = false;
        }
        
    }

    // Use this for initialization
    void Start () {
        dir = new Vector3(0, 0, 0);
        bReturnMove = false;
        
    }
	
	// Update is called once per frame
	void Update () {
        if (bReturnMove == false)
        {
            Move();
        }
        else
        {
            ReturnMove();
        }
    }
}
