using UnityEngine;
using System.Collections;

public class PortailManager : MonoBehaviour {
    [SerializeField]
    GameObject[] lightObject;
    [SerializeField]
    Sprite spritePortailOuvert;
    [SerializeField]
    Sprite spritePortailFerme;
    private bool isOpened;

    // Use this for initialization
    void Start () {
        isOpened = false;
        GetComponent<SpriteRenderer>().sprite = spritePortailOuvert;

        for (int i = 0; i < 5; i++)
        {
            lightObject[i].SetActive(false);
        }

    }
	
	// Update is called once per frame
	void Update () {
	if(!isOpened && GameManager.instance.NbBalloonsSaved() == 5)
        {
            isOpened = true;
            GetComponent<SpriteRenderer>().sprite = spritePortailFerme;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    for(int i=0; i<5; i++)
        {
            if (PlayerPrefs.GetInt("Level" + i, 0) == 1 && !lightObject[i].activeSelf)
            {
                lightObject[i].SetActive(true);
            }
        }
	}
}
