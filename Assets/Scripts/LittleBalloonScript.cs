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
    [SerializeField]
    private AudioClip LevelFinish, Rescue;
    private AudioSource audioSource;

    private Vector3 initPos;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();

        isAttached = false;
        initPos = transform.position;

        if (PlayerPrefs.GetInt("Level" + levelID, 0) == 1)
        {
            Validate();
        }
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
            audioSource.clip = Rescue;
            audioSource.Play();
        }

        if (isAttached && Player.GetComponent<BallonController>().IsDead)
        {
            isAttached = false;
            transform.parent = null;
            transform.position = initPos;
            Backdoor.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            for (int i = 0; i < 5; i++)
            {
                PlayerPrefs.SetInt("Level" + i, 1);
            }
        }
        if (Input.GetKeyDown(KeyCode.End))
        {
            for (int i = 0; i < 5; i++)
            {
                PlayerPrefs.SetInt("Level" + i, 0);
            }
        }
	}

    public void Validate()
    {
        Destroy(Backdoor);
        Destroy(gameObject);
        PlayerPrefs.SetInt("Level" + levelID, 1);

        GameManager.instance.OneMoreChildBalloonSaved();
    }
}
