using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour
{

	public float speed = 2.0f;
    public Animator animator;

    protected BoxCollider2D bc;
    public Vector3 movingTo;

    protected virtual void Start() {
        animator = GetComponent<Animator>();
        bc = GetComponent<BoxCollider2D>();
        movingTo = transform.position;
    }

    protected void StartMoving(Vector3 towards) {
    	movingTo += towards;
    	animator.SetBool("Walk", true);
    }

    protected void Turn(string direction) {
    	animator.SetTrigger("Turn" + direction);
    }

    protected void MoveLeft() {
    	Turn("Left");
    	RaycastHit2D hit;
        if (CanMove(Vector3.left, out hit)) {
            StartMoving(Vector3.left);
        } else {

        }
    }

    protected void MoveRight() {
    	Turn("Right");
    	RaycastHit2D hit;
        if (CanMove(Vector3.right, out hit)) {
            StartMoving(Vector3.right);
        } else {

        }
    }

    protected void MoveDown() {
    	Turn("Down");
    	RaycastHit2D hit;
        if (CanMove(Vector3.down, out hit)) {
            StartMoving(Vector3.down);
        } else {

        }
    }

    protected void MoveUp() {
		Turn("Up");
		RaycastHit2D hit;
        if (CanMove(Vector3.up, out hit)) {
            StartMoving(Vector3.up);
        } else {

        }
    }

    protected virtual void StopMoving() {
    	animator.SetBool("Walk", false);
    }

    protected void Move() {
		transform.position = Vector3.MoveTowards(transform.position, movingTo, speed * Time.deltaTime);
        animator.SetBool("Walk", true);
    }

    protected virtual void Update()
    {
        Move();

        if (movingTo == transform.position) {
            StopMoving();
        }
    }

    protected bool CanMove(Vector3 direction, out RaycastHit2D hit) {
        bc.enabled = false;
        hit = Physics2D.Linecast(transform.position, transform.position + direction);
        bc.enabled = true;

        return hit.transform == null;
    }

}
