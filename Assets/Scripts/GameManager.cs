using UnityEngine;
using System.Collections;


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
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OneMoreChildBalloonSaved()
    {
        numberOfBalloonSaved++;
        if (numberOfBalloonSaved == maxNumberOfChildBalloon)
            OpenSesame();
    }

    // Open the last door to paradise
    private void OpenSesame()
    {

    }
}
