using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class MusiqueManager : MonoBehaviour {

    [SerializeField]
    private AudioClip MainTheme, Final;

    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = MainTheme;
        audioSource.loop = true;
        audioSource.Play();
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager.instance.niveau == Niveaux.SOLEIL && audioSource.clip != Final)
        {
            audioSource.clip = Final;
            audioSource.Play();
        }
        if (GameManager.instance.niveau != Niveaux.SOLEIL && audioSource.clip != MainTheme)
        {
            audioSource.clip = MainTheme;
            audioSource.Play();
        }
	}
}
