using UnityEngine;
using System.Collections;

public class Lighting : MonoBehaviour {
    [SerializeField] private float m_TimeBetweenTic = 1f;
    [SerializeField] private float m_TimeBetweenAnimation = 0.5f;
    [SerializeField] private float m_TicDuration = 2f;
    [SerializeField] private bool m_ModifyX = false;
    [SerializeField] private bool m_ModifyY = false;
    private Transform myTransform;
    public bool lethal = true;
    [SerializeField]
    private float ticActualDuration;
    private float timeBetweenAnimation;
    private SpriteRenderer mySpriteRenderer;

	// Use this for initialization
	void Start () {
        myTransform = this.transform;
        ticActualDuration = 0f;
        timeBetweenAnimation = 0f;
        mySpriteRenderer = GetComponent<SpriteRenderer>(); 
	}
	
	// Update is called once per frame
	void Update () {

        if (ticActualDuration <= m_TimeBetweenTic) // disabled 
        {
            ticActualDuration += Time.deltaTime;
            lethal = false;
            gameObject.tag = "Untagged";
            mySpriteRenderer.enabled = false;

        }
        else if (ticActualDuration <= m_TicDuration) // enabled and animated
        {
            ticActualDuration += Time.deltaTime;
            lethal = true;
            gameObject.tag = "Kill";
            mySpriteRenderer.enabled = true;

            // Animation
            if (timeBetweenAnimation > 0)
                timeBetweenAnimation -= Time.deltaTime;
            else
            {
                timeBetweenAnimation = m_TimeBetweenAnimation;
                if (m_ModifyX)
                    myTransform.localScale = new Vector3(-myTransform.localScale.x, myTransform.localScale.y, myTransform.localScale.z);
                else if (m_ModifyY)
                    myTransform.localScale = new Vector3(myTransform.localScale.x, -myTransform.localScale.y, myTransform.localScale.z);
            }  
        }
        else
            ticActualDuration = 0f;
	}
}
