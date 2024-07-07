using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepWalkState : StateMachineBehaviour
{
    Transform player;
    SheepAttribute sa;

    float updateDestinationTime = 0;
    float runTime = 0;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = player != null ? player : GameObject.FindGameObjectWithTag("Player").transform;

        sa = sa != null ? sa : animator.transform.GetComponent<SheepAttribute>();
        runTime = 0;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (sa.state == AnimatorState.walk)
        {
            if (sa.isBitedByDog)
            {
                sa.agent.isStopped = true;

                Debug.Log("sheep to idle");
                sa.state = AnimatorState.idle;
                animator.SetFloat("SpeedX", 0);
                animator.SetFloat("SpeedY", 0);
                animator.SetBool("IsWalk", false);
                return;
            }



            //Debug.Log("sheep in walk");
            Vector2 nextPointDirection = sa.GetMoveDirection();

            animator.SetFloat("SpeedX", nextPointDirection.x);
            animator.SetFloat("SpeedY", nextPointDirection.y);

            runTime += Time.deltaTime;
            updateDestinationTime += Time.deltaTime;
            if (Vector2.Distance(animator.transform.position, player.position) < sa.warnRange)
            {
                runTime = 0;
                
            }
            if (updateDestinationTime>0.4f)
            {
                updateDestinationTime = 0;
                Vector2 runPoint = 2 * (animator.transform.position - player.position).normalized + animator.transform.position;
                sa.SetTheDestination(runPoint);
            }

            if (runTime > 10)
            {
                Debug.Log("sheep to idle");
                sa.state = AnimatorState.idle;
                animator.SetFloat("SpeedX", 0);
                animator.SetFloat("SpeedY", 0);
                animator.SetBool("IsWalk", false);
            }
        }
    }
}
