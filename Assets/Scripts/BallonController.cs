﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;

public class BallonController : MonoBehaviour {

    [SerializeField]
    private AudioClip balloonExplode, balloonDeflate, stageClear;
    [SerializeField]
    private GameObject box;
    private AudioSource audioSource;

    private bool isMoving, canMove, isSlow, isDead;

    public bool IsDead
    {
        get { return isDead; }
        set { isDead = value; }
    }
    [SerializeField]
    private float speed, slowMod, rotspeed, moveCd, growSpeed;
    [SerializeField]
    private Image winImage;
    private float elapsedTime, moveOnCd;
    private Animator anim;
    private bool babyBalloon; // true if a baby balloon (and not baboon, that would be akward) is following us
    private float oldSpeed = 0f;
    private float oldGrowSpeed = 0f;
    private List<GameObject> enableList;

    private float speedBonus = 0f;
    private float speedBonusDuration = 0f;


	// Use this for initialization
	void Start () {
       // Cursor.visible = false;
        winImage.enabled = false;
        audioSource = GetComponent<AudioSource>();
        Instantiate(box);
        isMoving = false;
        canMove = false;
        isSlow = false;
        isDead = false;
        elapsedTime = 0;
        anim = GetComponent<Animator>();
        babyBalloon = false;
        elapsedTime = 0;
        speed = 5;
        enableList = new List<GameObject>();
        GetComponent<TrailRenderer>().enabled = true;
        GetComponent<TrailRenderer>().Clear();
        GameManager.instance.niveau = Niveaux.HUB_CENTRAL; // Si le respawn ne bouge pas du hub central
        Camera.main.backgroundColor = hexToColor("96D6EC05");
        if (growSpeed != 0)
        {
            oldGrowSpeed = growSpeed;
            growSpeed = 0f;
        }
	}

    private void init()
    {
        winImage.enabled = false;
        GetComponent<TrailRenderer>().enabled = true;
        GetComponent<TrailRenderer>().Clear();
        GetComponent<TrailRenderer>().time = 0.15f;
        transform.localScale = new Vector3(.5f, .5f, 0);
        transform.position = new Vector3(1.14f, -18.92f, 0);
        Instantiate(box);
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
        anim.SetTrigger("Respawn");
        moveOnCd = 0;
        isDead = false;
        speed = 5;
        GameManager.instance.niveau = Niveaux.HUB_CENTRAL; // Si le respawn ne bouge pas du hub central
        Camera.main.backgroundColor = hexToColor("96D6EC05");
        if (growSpeed != 0)
        {
            oldGrowSpeed = growSpeed;
            growSpeed = 0f;
        }
        ResetAllDisabledGameObjects();

        speedBonus = 0f;
        speedBonusDuration = 0f;
    }

    void Update()
    {
        if (speedBonusDuration > 0)
            speedBonusDuration -= Time.deltaTime;
        else
            speedBonus = 0f;
    }

	// Update is called once per frame
	void FixedUpdate () {

        elapsedTime += Time.deltaTime;

        /*if (Input.GetMouseButton(0))
            isMoving = true;
        else
            isMoving = false;*/

        if (!GameManager.instance.isPause && canMove && (Input.GetMouseButton(0) || Input.GetKey(KeyCode.LeftArrow)))
            transform.Rotate(Vector3.forward, rotspeed);
        if (canMove && (Input.GetMouseButton(1)|| Input.GetKey(KeyCode.RightArrow)))
            transform.Rotate(Vector3.forward, -rotspeed);

        if (isDead && !canMove && ((Input.GetMouseButton(0) && Input.GetMouseButton(1)) || (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))))
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

        if (canMove && !isDead)
            anim.SetTrigger("Respawn");



	}

    private void Move()
    {
        transform.Translate(isSlow ? ((speed + speedBonus) - 2) * Time.deltaTime : (speed + speedBonus) * Time.deltaTime, 0, 0);
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
        else if (c.gameObject.tag == "Slow")
        {
            isSlow = true;
        }
        else if (c.gameObject.tag == "ModifyScale")
        {
            ModifyScale(c.gameObject.GetComponent<Obstacles>().GetScaleAmountToModify());
            c.gameObject.SetActive(false);
            enableList.Add(c.gameObject);
            audioSource.clip = balloonDeflate;
            audioSource.Play();
        }
        else if (c.gameObject.tag == "ModifySpeed")
        {
            speedBonus = c.gameObject.GetComponent<Obstacles>().m_SpeedBoost;
            speedBonusDuration = c.gameObject.GetComponent<Obstacles>().m_SpeedBoostDuration;
            c.gameObject.SetActive(false);
            enableList.Add(c.gameObject);
        } 
        else if (c.gameObject.tag == "HubCentral")
        {
            if (GameManager.instance.niveau != Niveaux.HUB_CENTRAL)
            {
                Debug.Log("vitesse hub :" + speed);
                GameManager.instance.niveau = Niveaux.HUB_CENTRAL;
                speed = 5; // retour à une vitesse normale
                oldGrowSpeed = growSpeed;
                growSpeed = 0f;
                transform.localScale = new Vector3(0.5f, 0.5f, 0);
                Camera.main.backgroundColor = hexToColor("96D6EC05");

                LittleBalloonScript[] littleBalloons = GetComponentsInChildren<LittleBalloonScript>();
                for (int i = 0; i < littleBalloons.Length; i++)
                {
                    littleBalloons[i].Validate();
                }
                ResetAllDisabledGameObjects();
                speedBonus = 0f;
                speedBonusDuration = 0f;
            }
        }
        else if (c.gameObject.tag == "NiveauEau")
        {
            if (GameManager.instance.niveau != Niveaux.EAU)
            {
                GameManager.instance.niveau = Niveaux.EAU;
                oldSpeed = speed;
                speed = speed * GameManager.instance.waterSpeedModification;
                growSpeed = oldGrowSpeed;
                Camera.main.backgroundColor = hexToColor("218AB605");
            }
        }
        else if (c.gameObject.tag == "NiveauFacile")
        {
            if (GameManager.instance.niveau != Niveaux.FACILE)
            {
                GameManager.instance.niveau = Niveaux.FACILE;
                oldSpeed = speed;
                growSpeed = oldGrowSpeed;
                Camera.main.backgroundColor = hexToColor("66B49405");
            }
        }
        else if (c.gameObject.tag == "NiveauBulletHell")
        {
            if (GameManager.instance.niveau != Niveaux.BULLET_HELL)
            {
                GameManager.instance.niveau = Niveaux.BULLET_HELL;
                oldSpeed = speed;
                growSpeed = oldGrowSpeed;
                Camera.main.backgroundColor = hexToColor("D8CF8E05");
            }
        }
        else if (c.gameObject.tag == "NiveauHardcore")
        {
            if (GameManager.instance.niveau != Niveaux.HARDCORE)
            {
                GameManager.instance.niveau = Niveaux.HARDCORE;
                oldSpeed = speed;
                growSpeed = oldGrowSpeed;
                Camera.main.backgroundColor = hexToColor("F1673905");
            }
        }
        else if (c.gameObject.tag == "NiveauPuzzle")
        {
            if (GameManager.instance.niveau != Niveaux.PUZZLE)
            {
                GameManager.instance.niveau = Niveaux.PUZZLE;
                oldSpeed = speed;
                growSpeed = oldGrowSpeed;
                Camera.main.backgroundColor = hexToColor("E6C34F05");
            }
        }
        else if (c.gameObject.tag == "NiveauSoleil")
        {
            if (GameManager.instance.niveau != Niveaux.SOLEIL)
            {
                GameManager.instance.niveau = Niveaux.SOLEIL;
                oldSpeed = speed;
                growSpeed = oldGrowSpeed;
                Camera.main.backgroundColor = hexToColor("212E5105");/// 16065605 212E5105
            }
        }
        else if (c.gameObject.tag == "Sun")
        {
            winImage.enabled = true;
            Die();
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
        audioSource.clip = balloonExplode;
        audioSource.Play();
    }

    private void ResetAllDisabledGameObjects()
    {
        foreach (GameObject GO in enableList)
        {
            GO.SetActive(true);
        }
    }

    public void PlayStageClearSound()
    {
        audioSource.clip = stageClear;
        audioSource.Play();
    }

    #region Ajout-luc
    public void ModifyScale(float amount)
    {
        if (transform.localScale.x + amount >= 0.2)
            transform.localScale = new Vector3(transform.localScale.x + amount, transform.localScale.y + amount, 0);
        else
            transform.localScale = new Vector3(0.2f, 0.2f, 0);
    }

     private static Color hexToColor(string hex)
     {
         hex = hex.Replace ("0x", "");//in case the string is formatted 0xFFFFFF
         hex = hex.Replace ("#", "");//in case the string is formatted #FFFFFF
         byte a = 255;//assume fully visible unless specified in hex
         byte r = byte.Parse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber);
         byte g = byte.Parse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber);
         byte b = byte.Parse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber);
         //Only use alpha if the string has enough characters
         if(hex.Length == 8){
             a = byte.Parse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber);
         }
         return new Color32(r,g,b,a);
     }

     
    #endregion
}
