using System;
using System.Collections.Generic;
using System.Text;

namespace FSMStatemachineProject
{
    public class GameStateMachine:StateMachine<State>//状态机
    {
        public State CurrentState = null; //当前状态
        public State PrevisousState = null;//上一个状态

        public Dictionary<int, State> StateDt;//状态存储

        public GameStateMachine()
        {
            StateDt = new Dictionary<int ,State>();
        }

        public State GetCurrentState()
        {
            return CurrentState;//返回当前状态
        }

        public State GetPreviousState()
        {
            return PrevisousState;//返回上一个状态
        }

        public void Register(State t)
        {
            if (StateDt.ContainsKey(t.GetCurrentStateId()))//判断注册状态是不是已经注册  注册过return
                return;

            StateDt.Add(t.GetCurrentStateId(),t);//添加状态id和状态信息
        }

        public void RemoveState(State t)
        {
            if (!StateDt.ContainsKey(t.GetCurrentStateId()))//判断注册状态是不是已经注册  没注册过return
                return;
            
            StateDt.Remove(t.GetCurrentStateId());//移除状态id和状态信息
        }

        public void SwitchState(State t)
        {
            if (!StateDt.ContainsKey(t.GetCurrentStateId())) //判断当前状态有没有注册
                return;

            if (CurrentState !=null)//判断是不是第一次
            {
                if (CurrentState.GetCurrentStateId() == t.GetCurrentStateId())//判断要切换的状态和当前状态是不是相同状态
                    return;

                CurrentState.LeaveEvent(t);//当前状态的离开方法
                PrevisousState = CurrentState;//赋值给上一个状态
            }
            t.Init();//执行初始化
            CurrentState = t;//当前状态赋值
            CurrentState.ComeEvent(t);//进入状态
        }

        public void SwitchState(int id)
        {
            if (!StateDt.ContainsKey(id))//判断当前状态有没有注册
                return;

            State t = StateDt[id];//通过Id找到这个状态

            if (CurrentState != null)
            {
                if (CurrentState.GetCurrentStateId() == id)
                    return;
                CurrentState.LeaveEvent(t);
                PrevisousState = CurrentState;
            }
            t.Init();//执行初始化
            CurrentState = t;
            CurrentState.ComeEvent(t);

        }

    }
}
