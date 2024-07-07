using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBase
{
    public int ID { get; set; }//״̬���

    public string ExtraData { get; set; }//��صĲ������� ��������move�е���������

    public Transform Target { get; set; }//���״̬�����Ķ��� 

    public FiniteStateMachine FSM { get; set; }//��״̬������״̬��

    public virtual void Enter() { }//����״̬
    public virtual void Execute() { }//����״̬
    public virtual void Exit() { }//�˳�״̬
}