using System;
using System.Collections.Generic;
using System.Text;

namespace FSMStatemachineProject
{
    public enum GameState
    {
        GAME_CONNECT_STATE,//开始
        GAME_CONNECT_ING,//进行中
        GAME_CONNECT_ERROR,//错误
        GAME_CONNECT_END,//结束
    }

    public class GameConnectStart : State
    {
        public void ComeEvent(State state)
        {
            Console.WriteLine("进入Start状态");
        }

        public int GetCurrentStateId()
        {
            return (int)GameState.GAME_CONNECT_STATE;
        }

        public void Init()
        {
            Console.WriteLine("Start状态进行初始化");
        }

        public void LeaveEvent(State state)
        {
            Console.WriteLine("Start状态结束" +(GameState)state.GetCurrentStateId() + "状态进来");
        }
    }
    
    public class GameConnectIng : State
    {
        public void ComeEvent(State state)
        {
            Console.WriteLine("进入ING状态");
        }

        public int GetCurrentStateId()
        {
            return (int)GameState.GAME_CONNECT_ING;
        }

        public void Init()
        {
            Console.WriteLine("ING状态进行初始化");
        }

        public void LeaveEvent(State state)
        {
            Console.WriteLine("ING状态结束" + (GameState)state.GetCurrentStateId() + "状态进来");
        }
    }

    public class GameConnectERROR : State
    {
        public void ComeEvent(State state)
        {
            Console.WriteLine("进入ERROR状态   开始切换End状态");
            SingletonPattern.GetInstace().SwithcStateMachine(GameState.GAME_CONNECT_END);//切换状态结束
        }

        public int GetCurrentStateId()
        {
            return (int)GameState.GAME_CONNECT_ERROR;
        }

        public void Init()
        {
            Console.WriteLine("ERROR状态进行初始化,抛出错误");
        }

        public void LeaveEvent(State state)
        {
            Console.WriteLine("ERROR状态结束" + (GameState)state.GetCurrentStateId() + "状态进来");
        }
    }

    public class GameConnectEnd : State
    {
        public void ComeEvent(State state)
        {
            Console.WriteLine("进入END状态");
        }

        public int GetCurrentStateId()
        {
            return (int)GameState.GAME_CONNECT_END;
        }

        public void Init()
        {
            Console.WriteLine("END状态进行初始化");
        }

        public void LeaveEvent(State state)
        {
            Console.WriteLine("END状态结束" + (GameState)state.GetCurrentStateId() + "状态进来 /n");
        }
    }

}
