﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{

	// private float speed;

 //    private Transform target;

 //    private List<Path> bestPath = new List<Path>();

 //    private Vector2 currentDestination;

 //    // Start is called before the first frame update
 //    void Start()
 //    {
 //        // speed = Random.Range(2f, 10f);
 //        speed = 2f;
 //        target = GameObject.Find("Player").transform;
 //        bestPath = HappyPath();
 //        currentDestination = new Vector3(bestPath[0].x, bestPath[0].y, 0);
 //    }

 //    void FixedUpdate()
 //    {
        
 //    }

 //    private float nextUpdate = 0.5f;
 //    void Update() {
 //        // float step = speed * Time.deltaTime;
 //        // transform.position = Vector3.MoveTowards(transform.position, currentDestination, step);

 //        // if (Time.time >= nextUpdate) {
 //        //     nextUpdate = Time.time + 0.5f;
 //        //     bestPath = HappyPath();
 //        //     currentDestination = new Vector3(bestPath[0].x, bestPath[0].y, 0);
 //        // }
 //    }

 //    private List<Path> GetAdjacentSquares(Path p) {
 //        List<Path> retVal = new List<Path>();
        
 //        int _x = p.x;
 //        int _y = p.y;
 //        for (int x = -1; x <= 1; x++) {
 //            for (int y = -1; y <= 1; y++) {
 //                int __x = _x + x;
 //                int __y = _y + y;
 //                if ((x == 0 && y == 0) || (x != 0 && y != 0)) { // Skip self and diagonals
 //                    continue;
 //                } else if (!CheckForCollision(new Vector2(_x, _y), new Vector2(__x, __y))) {
 //                    retVal.Add(new Path(p.g+1, BlocksToTarget(new Vector2(__x, __y), target.position), p, __x, __y));
 //                }
 //            }
 //        }
        
 //        return retVal;
 //    }

 //    private int BlocksToTarget(Vector2 start, Vector2 end) {
 //        return (int)(Vector2.Distance(start, end));
 //    }

 //    private bool CheckForCollision(Vector2 start, Vector2 end) {
 //        this.GetComponent<BoxCollider2D>().enabled = false;
 //        RaycastHit2D hit = Physics2D.Linecast(start, end);
 //        this.GetComponent<BoxCollider2D>().enabled = true;

 //        //trying to hit wall, change direction
 //        if (hit.transform != null && !hit.collider.tag.Equals("Player")) {
 //            return true;
 //        } else {
 //            return false;
 //        }
 //    }

 //    private List<Path> HappyPath() {
 //        Path destinationSquare = new Path(0, 0, null, (int)target.position.x, (int)target.position.y);

 //        List<Path> evaluationList = new List<Path>();
 //        evaluationList.Add(new Path(0, 0, null, (int)transform.position.x, (int)(transform.position.y)));

 //        List<Path> closedPathList = new List<Path>();
        
 //        Path currentSquare = null;

 //        while (evaluationList.Count > 0) {
 //            currentSquare = itemWithLowestFScore(evaluationList);
            
 //            closedPathList.Add(currentSquare);
 //            evaluationList.Remove(currentSquare);

 //            if (doesPathListContain(closedPathList, destinationSquare)) {
 //                return buildPath(currentSquare);
 //            }

 //            List<Path> adjacentSquares = GetAdjacentSquares(currentSquare);

 //            foreach (Path p in adjacentSquares) {
 //                if (doesPathListContain(closedPathList, p)) {
 //                    continue; //Skip since we already know about it
 //                }
 //                if (!doesPathListContain(evaluationList, p)) {
 //                    evaluationList.Add(p);
 //                } else if (p.h + currentSquare.g + 1 < p.f) {
 //                    p.parent = currentSquare;
 //                }
 //            }
 //        }
 //        return bestPath;
 //    }

 //    private List<Path> buildPath(Path p) {
 //        List<Path> bestPath = new List<Path>();
 //        Path currentLoc = p;
 //        bestPath.Insert(0, currentLoc);
 //        while (currentLoc.parent != null) {
 //            currentLoc = currentLoc.parent;
 //            if (currentLoc.parent != null) {
 //                bestPath.Insert(0, currentLoc);
 //            } else {
                
 //            }
 //        }
 //        return bestPath;
 //    }

 //    private Path itemWithLowestFScore(List<Path> paths) {
 //        int lowestIdx = 0;
 //        for (int i=0; i < paths.Count; i++) {
 //            if (paths[lowestIdx].f > paths[i].f) {
 //                lowestIdx = i;
 //            }
 //        }
 //        return paths[lowestIdx];
 //    }

 //    private bool doesPathListContain(List<Path> paths, Path path) {
 //        bool contains = false;
 //        foreach (Path p in paths) {
 //            if (p.x == path.x && p.y == path.y) {
 //                contains = true;
 //                break;
 //            }
 //        }
 //        return contains;
 //    }

}
