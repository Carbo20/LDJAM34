using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum Niveaux
{
    HUB_CENTRAL, EAU, FACILE, BULLET_HELL, HARDCORE, PUZZLE, SOLEIL
}

public class GameManager : MonoBehaviour {

    static GameManager _instance;

    public Niveaux niveau = Niveaux.HUB_CENTRAL;
    private int numberOfBalloonSaved = 0;
    [SerializeField] private int maxNumberOfChildBalloon = 5;
    public float waterSpeedModification = 2f;
    public bool isPause;

    //[SerializeField]
    SpriteRenderer spriteOpaquePause;
    private Image sprite;

    static public bool isActive
    {
        get
        {
            return _instance != null;
        }
    }

    static public GameManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Object.FindObjectOfType(typeof(GameManager)) as GameManager;

                if (_instance == null)
                {
                    GameObject go = new GameObject("_gamemanager");
                    DontDestroyOnLoad(go);
                    _instance = go.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }


	// Use this for initialization
	void Start () {

        isPause = false;
     
        sprite = GameObject.FindGameObjectWithTag("UI").GetComponent<Image>();
        sprite.enabled = false;
        
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(2))
        {
            isPause = !isPause;
            if (isPause)
            {
                Time.timeScale = 0;
                // sprite.color = new Color(0f, 0f, 0f, .5f); //5 is about 50 % transparent
                sprite.enabled = true;
            }
            else
            {
                Time.timeScale = 1;
                //    spriteOpaquePause.color = new Color(1f, 1f, 1f, .5f);
                sprite.enabled = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

    }

    public void OneMoreChildBalloonSaved()
    {
        BallonController bC = GameObject.Find("Balloon").GetComponent<BallonController>();
        bC.PlayStageClearSound();

        numberOfBalloonSaved++;
        if (numberOfBalloonSaved == maxNumberOfChildBalloon)
            OpenSesame();
    }

    // Open the last door to paradise
    private void OpenSesame()
    {

    }
}
