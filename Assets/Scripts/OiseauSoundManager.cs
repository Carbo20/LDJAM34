using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class OiseauSoundManager : MonoBehaviour {

    [SerializeField]
    private AudioClip oiseauSon;
    [SerializeField]
    private Vector2 randomTimeBetweenSoundRange;

    private float elapsedTime, timeForNextSound;
    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = oiseauSon;
        audioSource.playOnAwake = false;
        SetNewTime();
	}
	
	// Update is called once per frame
	void Update () {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= timeForNextSound)
        {
            audioSource.Play();
            SetNewTime();
        }
	}

    private void SetNewTime()
    {
        elapsedTime = 0;
        timeForNextSound = Random.Range(randomTimeBetweenSoundRange.x, randomTimeBetweenSoundRange.y);
    }
}
