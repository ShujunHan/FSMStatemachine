using System;
using System.Collections.Generic;
using System.Text;

namespace FSMStatemachineProject
{
    public interface State//定义状态接口
    {
        void Init();//初始化

        int GetCurrentStateId();//返回当前状态Id

        void ComeEvent(State state);//进入状态

        void LeaveEvent(State state);//离开状态
    }
}
