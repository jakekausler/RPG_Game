using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

	[SerializeField]
	private float speed;

    [SerializeField]
    private GameObject camera;

    [SerializeField]
    private Vector3 offset;

    void FixedUpdate()
    {
    	float horizontal = Input.GetAxis("Horizontal");
    	float vertical = Input.GetAxis("Vertical");

    	GetComponent<Rigidbody2D>().velocity = new Vector2(horizontal * speed, vertical * speed);

        camera.transform.position = transform.position + offset;
    }
}
