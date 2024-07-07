
using UnityEngine;

public class SheepIdleState : StateMachineBehaviour
{
    Transform player;
    SheepAttribute sa;



    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = player != null ? player : GameObject.FindGameObjectWithTag("Player").transform;

        sa = sa != null ? sa : animator.transform.GetComponent<SheepAttribute>();

        //sa.state = AnimatorState.idle;
        //Debug.Log("eeeeee");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (sa.state == AnimatorState.idle)
        {
            if (sa.isBitedByDog)
            {
                return;
            }

            if (Vector2.Distance(animator.transform.position, player.position) < sa.warnRange)//是否看到玩家
            {
                Debug.Log(sa.state);
                Debug.Log("sheep to walk");
                Vector2 runPoint = 10 * (animator.transform.position - player.position).normalized + animator.transform.position;
                sa.SetTheDestination(runPoint);
                Debug.Log(runPoint);
                animator.SetBool("IsWalk", true);
                sa.state = AnimatorState.walk;
            }
        }
    }
}
