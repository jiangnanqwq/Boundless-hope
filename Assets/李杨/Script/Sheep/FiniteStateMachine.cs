using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//有限状态机类 负责描述游戏中某个状态集合以及切换逻辑  类似于一个动画控制器管理状态
public class FiniteStateMachine
{
    //玩家可能有的几个状态
    public enum PlayerFiniteState
    {
        Move,
        Rotate
    }

    //状态集合
    public Dictionary<int, StateBase> StateDic = new Dictionary<int, StateBase>();
    //当前处于哪一个状态
    public StateBase CurState { set; get; }

    //根据状态编号 获取某个状态
    public StateBase GetState(int id)
    {
        if (StateDic.ContainsKey(id))
        {
            return StateDic[id];
        }
        Debug.Log("没有找到该状态");
        return null;
    }

    //增加一个状态
    public void AddState(StateBase newState)
    {
        if (!StateDic.ContainsKey(newState.ID))
        {
            StateDic.Add(newState.ID, newState);
        }
    }
    //删除一个状态
    public void RemoveState(int id)
    {
        if (StateDic.ContainsKey(id))
        {
            StateDic.Remove(id);
        }
    }

    //设置状态机的初始状态
    public void SetInitState(int id)
    {
        if (CurState == null)
        {
            CurState = StateDic[id];
            CurState.Enter();
        }
    }
    //切换状态
    public void ChangeState(int id, string extra)
    {
        if (CurState.ID != id || CurState.ExtraData != extra)
        {
            //退出当前状态
            CurState.Exit();
            //切换
            CurState = StateDic[id];
            CurState.ExtraData = extra;
            //进入状态
            CurState.Enter();
        }
    }
    public void ChangeState(int id)
    {
        ChangeState(id, string.Empty);
    }
    //更新回调
    public void ExecuteState()
    {
        CurState.Execute();
    }
    //检测当前状态是否是指定的某个状态
    public bool CheckState(int id)
    {
        return CurState.ID == id;
    }
}