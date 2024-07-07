using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//����״̬���� ����������Ϸ��ĳ��״̬�����Լ��л��߼�  ������һ����������������״̬
public class FiniteStateMachine
{
    //��ҿ����еļ���״̬
    public enum PlayerFiniteState
    {
        Move,
        Rotate
    }

    //״̬����
    public Dictionary<int, StateBase> StateDic = new Dictionary<int, StateBase>();
    //��ǰ������һ��״̬
    public StateBase CurState { set; get; }

    //����״̬��� ��ȡĳ��״̬
    public StateBase GetState(int id)
    {
        if (StateDic.ContainsKey(id))
        {
            return StateDic[id];
        }
        Debug.Log("û���ҵ���״̬");
        return null;
    }

    //����һ��״̬
    public void AddState(StateBase newState)
    {
        if (!StateDic.ContainsKey(newState.ID))
        {
            StateDic.Add(newState.ID, newState);
        }
    }
    //ɾ��һ��״̬
    public void RemoveState(int id)
    {
        if (StateDic.ContainsKey(id))
        {
            StateDic.Remove(id);
        }
    }

    //����״̬���ĳ�ʼ״̬
    public void SetInitState(int id)
    {
        if (CurState == null)
        {
            CurState = StateDic[id];
            CurState.Enter();
        }
    }
    //�л�״̬
    public void ChangeState(int id, string extra)
    {
        if (CurState.ID != id || CurState.ExtraData != extra)
        {
            //�˳���ǰ״̬
            CurState.Exit();
            //�л�
            CurState = StateDic[id];
            CurState.ExtraData = extra;
            //����״̬
            CurState.Enter();
        }
    }
    public void ChangeState(int id)
    {
        ChangeState(id, string.Empty);
    }
    //���»ص�
    public void ExecuteState()
    {
        CurState.Execute();
    }
    //��⵱ǰ״̬�Ƿ���ָ����ĳ��״̬
    public bool CheckState(int id)
    {
        return CurState.ID == id;
    }
}