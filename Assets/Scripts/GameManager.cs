using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum Niveaux
{
    HUB_CENTRAL, EAU, FACILE, BULLET_HELL, HARDCORE, PUZZLE, SOLEIL, SIZE
}

public class GameManager : MonoBehaviour {

    static GameManager _instance;

    public Niveaux niveau = Niveaux.HUB_CENTRAL;
    private int numberOfBalloonSaved = 0;
    [SerializeField] private int maxNumberOfChildBalloon = 5;
    public float waterSpeedModification = 2f;
    public bool isPause;
    private bool isUnlockedDoor;
    private Image spritePause;

 
    GameObject starGateOpened; 
    GameObject starGateClosed;

    GameObject []lightObject;

    static public bool isActive
    {
        get
        {
            return _instance != null;
        }
    }

    public int NbBalloonsSaved()
    {
        return numberOfBalloonSaved;
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
        isUnlockedDoor = false;

        spritePause = GameObject.FindGameObjectWithTag("UI").GetComponent<Image>();
        spritePause.enabled = false;

       
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(2))
        {
            isPause = !isPause;
            if (isPause)
            {
                Time.timeScale = 0;
                spritePause.enabled = true;
            }
            else
            {
                Time.timeScale = 1;
                spritePause.enabled = false;
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
   
    }

    // Open the last door to paradise
    private void OpenSesame()
    {
        
       
    }
}
