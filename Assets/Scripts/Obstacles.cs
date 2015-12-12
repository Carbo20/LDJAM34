using UnityEngine;
using System.Collections;

public class Obstacles : MonoBehaviour {
    [SerializeField] float m_RotationSpeed = 5f;
    [SerializeField] float m_MoveX = 0.001f;
    [SerializeField] float m_MoveY = 0.001f;
    Transform myTransform;

	// Use this for initialization
	void Start () {
        myTransform = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.forward * m_RotationSpeed);
        transform.Translate(new Vector3(m_MoveX, m_MoveY));
	}
}
