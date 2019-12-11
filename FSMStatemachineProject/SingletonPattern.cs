using System;
using System.Collections.Generic;
using System.Text;

namespace FSMStatemachineProject
{
    public class SingletonPattern
    {
        static SingletonPattern Instance;

        GameStateMachine gameStateMachine = new GameStateMachine();//状态机

        //四种状态
        GameConnectStart gameConnectStart = new GameConnectStart();
        GameConnectIng gameConnectIng = new GameConnectIng();
        GameConnectERROR gameConnectERROR = new GameConnectERROR();
        GameConnectEnd gameConnectEnd = new GameConnectEnd();

        //单利
        public static SingletonPattern GetInstace()
        {
            if (Instance == null)
            {
                Instance = new SingletonPattern();
            }
            return Instance;
        }

        /// <summary>
        /// 状态注册
        /// </summary>
        public void Init()
        {
            gameStateMachine.Register(gameConnectStart);
            gameStateMachine.Register(gameConnectIng);
            gameStateMachine.Register(gameConnectERROR);
            gameStateMachine.Register(gameConnectEnd);
        }

        /// <summary>
        /// 切换状态
        /// </summary>
        /// <param name="state"> 状态类型 </param>
        public void SwithcStateMachine(GameState state)
        {
            gameStateMachine.SwitchState((int)state);
        }
    }
}
