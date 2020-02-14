using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovingObject
{

	public float speed;

    public GameObject cam;
    public Vector3 offset;

    protected override void Start() {
        animator = GetComponent<Animator>();

        base.Start();
    }

    private void Update()
    {
    	// float horizontal = Input.GetAxis("Horizontal");
    	// float vertical = Input.GetAxis("Vertical");

     //    float step = Time.deltaTime * speed;

    	// // GetComponent<Rigidbody2D>().velocity = new Vector2(horizontal * speed, vertical * speed);
     //    if (horizontal > 0) {
     //        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x+1, transform.position.y, transform.position.z), step);
     //    } else if (horizontal < 0) {
     //        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x-1, transform.position.y, transform.position.z), step);
     //    } else if (vertical > 0) {
     //        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y+1, transform.position.z), step);
     //    } else if (vertical < 0) {
     //        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y-1, transform.position.z), step);
     //    }


        int horizontal = (int)Input.GetAxis("Horizontal");
        int vertical = (int)Input.GetAxis("Vertical");

        if (horizontal != 0) {
            vertical = 0;
        }

        if (horizontal != 0 || vertical != 0) {
            AttemptMove<Component>(horizontal, vertical);
        }

        cam.transform.position = transform.position + offset;
    }

}
