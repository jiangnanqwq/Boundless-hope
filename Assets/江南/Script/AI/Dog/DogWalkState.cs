
using UnityEngine;

public class DogWalkState : StateMachineBehaviour
{
    Transform player;
    DogAttribute da;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = player != null ? player : GameObject.FindGameObjectWithTag("Player").transform;

        da = da != null ? da : animator.transform.GetComponent<DogAttribute>();

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (da.state==AnimatorState.walk)
        {
            Vector2 nextPointDirection = da.GetMoveDirection();

            animator.SetFloat("SpeedX", nextPointDirection.x);
            animator.SetFloat("SpeedY", nextPointDirection.y);
            if (Vector2.Distance(da.agent.destination, da.agent.nextPosition) < 0.1f)
            {
                //Debug.Log("dog to idle"); 
                da.state = AnimatorState.idle;
                animator.SetFloat("SpeedX", 0);
                animator.SetFloat("SpeedY", 0);
            } 
        }
    }
}
