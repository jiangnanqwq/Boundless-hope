using UnityEngine;
using UnityEngine.AI;

public class DogIdleState : StateMachineBehaviour
{

    Transform player;
    DogAttribute da;
    readonly float radius = 10;
    readonly float idleStateStayTime = 10;//һ��Idle��ʱ�䣬ʱ���ȥ��walk
    float enterIdleState = 0;
    readonly float idleStateChangeTime = 3;//�л�Idle�ķ���
    float enterIdleState2 = 0;

    //����ҧסʱ�� da.biteTime
    float enterIdleState3 = 0;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = player != null ? player : GameObject.FindGameObjectWithTag("Player").transform;

        da = da != null ? da : animator.transform.GetComponent<DogAttribute>();

        enterIdleState = 0;
        enterIdleState2 = 0;
        enterIdleState3 = 0;


        da.state = AnimatorState.idle;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (da.state == AnimatorState.idle)
        {
            if (da.isBiteSheep)
            {
                enterIdleState3 += Time.deltaTime;
                if (enterIdleState3>da.biteTime)
                {
                    da.isBiteSheep = false;
                    da.ResetAttackCD();
                }
                return;
            }





            enterIdleState += Time.deltaTime;
            enterIdleState2 += Time.deltaTime;

            if (enterIdleState > idleStateStayTime)//���л�״̬
            {
                if (AttributeBase.RandomPoint(player.position, radius, out Vector2 point))
                {
                    enterIdleState = 0;
                    //Debug.Log("dog to walk");

                    da.SetTheDestination(point);

                    animator.SetBool("IsWalk", true);
                    da.state = AnimatorState.walk;
                }

            }
            if (enterIdleState2 > idleStateChangeTime)
            {

                //Debug.Log("dog turn");
                enterIdleState2 = 0;
                animator.SetFloat("Idle", Random.Range(0, 4));
            }
        }
    }
}
