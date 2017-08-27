using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Spine.Unity;

public enum Direction { Right, Left }

public class MoveDirectionEventArgs : EventArgs {
    private Direction dir;

    public MoveDirectionEventArgs(Direction dir) {
        this.dir = dir;
    }

    public Direction Direction {
        get { return this.dir; }
    }
}

public class PlayerController : MonoBehaviour {
    [SerializeField] private float maxWalkSpeed = 0;
    [SerializeField] private float maxRunSpeed = 0;

    private Direction direction;
    private bool isWalkPressed;

    Rigidbody2D playerRB;
    Animator animator;
    SkeletonAnimation skeletonAnimation;

    delegate void MoveDirectionChangedHandler(PlayerController sender, MoveDirectionEventArgs e);
    event MoveDirectionChangedHandler MoveDirectionChanged;

    public Direction MoveDirection {
        get {
            return this.direction;
        }
        set {
            if (this.direction != value) {
                this.direction = value;

                var e = new MoveDirectionEventArgs(value);
                this.MoveDirectionChanged(this, e);
            }
        }
    }

	// Use this for initialization
	void Start () {
        playerRB = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        skeletonAnimation = this.GetComponent<SkeletonAnimation>();
        this.MoveDirectionChanged += OnMoveDirectionChanged;
	}
	
	// Update is called once per frame
	void Update () {
        this.animator.SetBool("isWalking", this.isWalkPressed);
        this.animator.SetFloat("xSpeed", Mathf.Abs(this.playerRB.velocity.x));
	}


    void FixedUpdate() {
        isWalkPressed = Input.GetAxis("Walk") > 0;
        float horizAxis = Input.GetAxis("Horizontal");
        float currMaxSpeed = (this.isWalkPressed)? this.maxWalkSpeed : this.maxRunSpeed;
        float xSpeed = currMaxSpeed * horizAxis;

        playerRB.velocity = new Vector2(xSpeed, playerRB.velocity.y);
        if (xSpeed > 0) {
            this.MoveDirection = Direction.Right;
        } else if (xSpeed < 0) {
            this.MoveDirection = Direction.Left;
        }
    }

    private void OnMoveDirectionChanged(PlayerController sender, MoveDirectionEventArgs e) {
        sender.skeletonAnimation.skeleton.FlipX = e.Direction == Direction.Left;
    }
}
