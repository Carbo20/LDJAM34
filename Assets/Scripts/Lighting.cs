using UnityEngine;
using System.Collections;

public class Lighting : MonoBehaviour {
    [SerializeField] private float m_TimeBetweenTic = 1f;
    [SerializeField] private float m_TimeBetweenAnimation = 0.5f;
    [SerializeField] private float m_TicDuration = 2f;
    [SerializeField] private bool m_ModifyX = false;
    [SerializeField] private bool m_ModifyY = false;
    [SerializeField]
    private Sprite energie0, energie1, energie2, energie3;
    [SerializeField]
    private GameObject tesla1, tesla2;

    private Transform myTransform;
    public bool lethal = true;
    [SerializeField]
    private float ticActualDuration;
    private float timeBetweenAnimation;
    private float ticEnergieChange;
    private SpriteRenderer mySpriteRenderer;
    private AudioSource myAudioSource;


    private float timeChangeEnergie;

    private Animator animator;

	// Use this for initialization
	void Start () {
        myAudioSource = GetComponent<AudioSource>();
        myTransform = this.transform;
        ticActualDuration = 0f;
        timeBetweenAnimation = 0f;
        ticEnergieChange = 0f;
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        timeChangeEnergie =( m_TicDuration)/ 4;
        animator = tesla1.GetComponent<Animator>();
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
            if (!myAudioSource.isPlaying)
                myAudioSource.Play();
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

        ticEnergieChange += Time.deltaTime;
        if (ticEnergieChange >= timeChangeEnergie)
        {
            ticEnergieChange = 0f;
            AnimTesla(tesla1);
            AnimTesla(tesla2);
        }
    }

    private void AnimTesla(GameObject tesla)
    {
        tesla.GetComponent<Animator>().SetTrigger("energieUp");
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Empty"))
        {
            tesla.GetComponent<SpriteRenderer>().sprite = energie0;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Energie1"))
        {
            tesla.GetComponent<SpriteRenderer>().sprite = energie1;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Energie2"))
        {
            tesla.GetComponent<SpriteRenderer>().sprite = energie2;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Energie3"))
        {
            tesla.GetComponent<SpriteRenderer>().sprite = energie3;
        }
    }

}
