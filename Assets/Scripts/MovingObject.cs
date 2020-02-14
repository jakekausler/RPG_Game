using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour
{

	public float moveTime = 0.1f;

	public Animator animator;

	private BoxCollider2D bc;
	private Rigidbody2D rb2D;
	private float inverseMoveTime;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        inverseMoveTime = 1f/moveTime;
    }

    //Returns true if able to move, false if not
    protected bool Move(int xDir, int yDir, out RaycastHit2D hit) {
    	Vector2 start = transform.position;
    	Vector2 end = start + new Vector2(xDir, yDir);

    	bc.enabled = false;
    	hit = Physics2D.Linecast(start, end);
    	bc.enabled = true;

    	if (hit.transform == null) {
    		StartCoroutine(SmoothMovement(end));
    		return true;
    	}
    	Debug.Log("Hit");

    	return false;
    }

    //Moves unit from one space to the next.
    protected IEnumerator SmoothMovement(Vector3 end) {
    	animator.SetBool("Walk", true);
    	float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
    	while (sqrRemainingDistance > float.Epsilon) {
    		Vector3 newPosition = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);
    		rb2D.MovePosition(newPosition);
    		sqrRemainingDistance = (transform.position - end).sqrMagnitude;

    		yield return null;
    	}
    	animator.SetBool("Walk", false);
    }

    //Attempt to move toward something
    protected virtual void AttemptMove<T>(int xDir, int yDir)
    where T : Component
    {
    	RaycastHit2D hit;

    	if (xDir > 0) {
    		animator.SetTrigger("TurnRight");
    	} else if (xDir < 0) {
    		animator.SetTrigger("TurnLeft");
    	} else if (yDir > 0) {
    		animator.SetTrigger("TurnUp");
    	} else if (yDir < 0) {
    		animator.SetTrigger("TurnDown");
    	}
    	
    	bool canMove = Move(xDir, yDir, out hit);

    	if (hit.transform == null) {
    		return;
    	}
	}
    // 	T hitComponent = hit.transform.GetComponent<T>();

    // 	if (!canMove && hitComponent != null) {
    // 		OnCantMove(hitComponent);
    // 	}
    // }

    // protected abstract void OnCantMove<T> (T component)
    // where T : Component;

}
