using UnityEngine;
using System.Collections;

public class ProjectileSpawner : MonoBehaviour {

    [SerializeField]
    private float timeBetweenTwoProjectile;
    [SerializeField]
    private float speedX, speedY;
    [SerializeField]
    private float lifeTime;

    [SerializeField]
    private GameObject projectile;

    private float deltaTime;

	// Use this for initialization
	void Start () {
        deltaTime = 0f;
        projectile.GetComponent<Obstacles>().MoveX = speedX;
        projectile.GetComponent<Obstacles>().MoveY = speedY;
        projectile.GetComponent<Obstacles>().LifeTime = lifeTime;
        projectile.transform.position = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        GenerateProjectile();
	}

    private void GenerateProjectile()
    {
        if(deltaTime > timeBetweenTwoProjectile)
        {
            GameObject instance = Instantiate(projectile);
            instance.GetComponent<Obstacles>().IsProjectile = true;
            instance.GetComponent<Obstacles>().transform.position = transform.position;
            deltaTime = 0f;
        }
        deltaTime += Time.deltaTime;
    }

}
