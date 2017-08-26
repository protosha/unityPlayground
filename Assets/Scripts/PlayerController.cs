using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Direction { Right, Left }

public class DirectionEventArgs : EventArgs {
    private Direction dir;

    public DirectionEventArgs(Direction dir) {
        this.dir = dir;
    }

    public Direction Direction {
        get { return this.dir; }
    }
}

public class PlayerController : MonoBehaviour {
    [SerializeField] private float walkSpeed = 0;
    [SerializeField] private float runSpeed = 0;

    private Direction direction;

    Rigidbody2D playerRB;
    Animator playerAnimator;

    delegate void DirectionChangedHandler(DirectionEventArgs e);
    event DirectionChangedHandler DirectionChanged;

    public Direction FaceDirection {
        get {
            return this.direction;
        }
    }

	// Use this for initialization
	void Start () {
        playerRB = this.GetComponent<Rigidbody2D>();
        playerAnimator = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void FixedUpdate() {
        float moveAxis = Input.GetAxis("Horizontal");
    }
}
