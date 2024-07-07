using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Ŀǰû������
public class DogAttackState : StateMachineBehaviour
{
    Transform player;
    DogAttribute da;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = player != null ? player : GameObject.FindGameObjectWithTag("Player").transform;

        da = da != null ? da : animator.transform.GetComponent<DogAttribute>();
    }
}
