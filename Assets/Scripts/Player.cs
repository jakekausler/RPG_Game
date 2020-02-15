using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovingObject
{
    public GameObject cam;
    public Vector3 offset;

    protected override void Update()
    {
    	float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal < 0 && movingTo == transform.position) {
            MoveLeft();
        } else if (horizontal > 0 && movingTo == transform.position) {
            MoveRight();
        } else if (vertical < 0 && movingTo == transform.position) {
            MoveDown();
        } else if (vertical > 0 && movingTo == transform.position) {
            MoveUp();
        }

        base.Update();

        cam.transform.position = transform.position + offset;
    }

}
