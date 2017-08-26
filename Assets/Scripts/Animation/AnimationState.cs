using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class AnimationState : StateMachineBehaviour {
    [SerializeField] private string animationName;
    [SerializeField] private bool loop;
    [SerializeField] private float speed = 1;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        SkeletonAnimation anim = animator.GetComponent<SkeletonAnimation>();
        anim.state.SetAnimation(0, this.animationName, this.loop).timeScale = speed;
    }
}
