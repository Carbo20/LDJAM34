using UnityEngine;
using System.Collections;

public class Obstacles : MonoBehaviour {
    [SerializeField] float m_RotationSpeed = 5f;
    [SerializeField] float m_MoveX = 0.001f;
    [SerializeField] float m_MoveY = 0.001f;
    [SerializeField] float lifeTime;
    Transform myTransform;
    [SerializeField] private float m_ScaleAmountToModify = 0.20f; //between 0 and 1 (not 1)

    [SerializeField] bool isProjectile = false;
    float deltaTime;
    [SerializeField] private bool delta = true;

    [SerializeField]
    private bool canon;
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip bouletExplosion;
    private bool soundHasBeenPlayed;

    public float m_SpeedBoost = 0f;
    public float m_SpeedBoostDuration = 0f;
   
    // Use this for initialization
    void Start () {
        myTransform = this.transform;
        deltaTime = 0f;

        if (canon)
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = bouletExplosion;
            soundHasBeenPlayed = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
	}

    void FixedUpdate()
    {
        if (!GameManager.instance.isPause)
        {
            transform.Rotate(Vector3.forward * RotationSpeed);
            transform.Translate(new Vector3(MoveX, MoveY));
        }

        if (!delta)
        {
            transform.Rotate(Vector3.forward * RotationSpeed);
            transform.Translate(new Vector3(MoveX, MoveY));
            
        }
        else
        {
            transform.Rotate(Vector3.forward * RotationSpeed * Time.deltaTime);
            transform.Translate(new Vector3(MoveX * Time.deltaTime, MoveY * Time.deltaTime));
        }

        if (canon)
        {
            deltaTime += Time.deltaTime;
            if (deltaTime > LifeTime && !soundHasBeenPlayed)
            {
                audioSource.Play();
                soundHasBeenPlayed = true;
            }
            if (deltaTime > LifeTime && soundHasBeenPlayed && !audioSource.isPlaying)
            {
                Destroy(gameObject);
            }

        }
        else if (IsProjectile)
        {
            deltaTime += Time.deltaTime;
            if (deltaTime > LifeTime)
            {
                Destroy(gameObject);
            }
        }
    }

    public float GetScaleAmountToModify()
    {
        return m_ScaleAmountToModify;
    }

    public void SetScaleAmountToModify(float newScaleAmountToModify)
    {
        m_ScaleAmountToModify = newScaleAmountToModify;
    }

    public float RotationSpeed
    {
        get
        {
            return m_RotationSpeed;
        }

        set
        {
            m_RotationSpeed = value;
        }
    }

    public float MoveX
    {
        get
        {
            return m_MoveX;
        }

        set
        {
            m_MoveX = value;
        }
    }

    public float MoveY
    {
        get
        {
            return m_MoveY;
        }

        set
        {
            m_MoveY = value;
        }
    }

    public float LifeTime
    {
        get
        {
            return lifeTime;
        }

        set
        {
            lifeTime = value;
        }
    }

    public bool IsProjectile
    {
        get
        {
            return isProjectile;
        }

        set
        {
            isProjectile = value;
        }
    }
    
}
