using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBase
{
    public int ID { get; set; }//状态编号

    public string ExtraData { get; set; }//相关的参数类型 例如区分move中的上下左右

    public Transform Target { get; set; }//与该状态关联的对象 

    public FiniteStateMachine FSM { get; set; }//该状态从属的状态机

    public virtual void Enter() { }//进入状态
    public virtual void Execute() { }//更新状态
    public virtual void Exit() { }//退出状态
}