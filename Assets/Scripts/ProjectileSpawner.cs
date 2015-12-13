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
    private Transform myTransform;

	// Use this for initialization
	void Start () {
        deltaTime = 0f;
        myTransform = this.transform;
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
            instance.GetComponent<Obstacles>().MoveX = speedX;
            instance.GetComponent<Obstacles>().MoveY = speedY;
            instance.GetComponent<Obstacles>().LifeTime = lifeTime;
            //instance.GetComponent<Obstacles>().transform.position = transform.position;
            instance.GetComponent<Obstacles>().transform.position = new Vector3(myTransform.position.x, myTransform.position.y, myTransform.position.z + 1);

            deltaTime = 0f;
        }
        deltaTime += Time.deltaTime;
    }

}
