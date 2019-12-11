using System;
using System.Collections.Generic;
using System.Text;

namespace FSMStatemachineProject
{
    public interface StateMachine<T>//定义状态机接口
    {
        T GetCurrentState();//返回当前状态

        T GetPreviousState();//返回上个状态

        void Register(T t);//注册状态

        void RemoveState(T t);//移除状态

        void SwitchState(T t);//切换状态
        
    }
}
