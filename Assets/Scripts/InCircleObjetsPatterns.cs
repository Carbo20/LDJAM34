using UnityEngine;
using System.Collections;

public class InCircleObjetsPatterns : MonoBehaviour
{

    // Instantiates a prefab in a circle

    public GameObject prefab;

    public int numberOfObjects = 20;
    public float radius = 5f;

    void Start()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            float angle = i * Mathf.PI * 2 / numberOfObjects;
            Vector3 pos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
            GameObject go = Instantiate(prefab) as GameObject;
            go.transform.position = pos;
            
                        Vector2 dir = (Vector3.zero-go.transform.position).normalized;
                        //transform.position = transform.position;

                        float angleBis = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

                        go.transform.rotation = Quaternion.AngleAxis(angleBis, Vector3.forward);
            
        }
    }
}