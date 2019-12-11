# <Center>FSMStateMachine
#### 抽象类State

```
    public interface State//定义状态接口
    {
        void Init();//初始化

        int GetCurrentStateId();//返回当前状态Id

        void ComeEvent(State state);//进入状态

        void LeaveEvent(State state);//离开状态
    }
```

#### 抽象类StateMachine

```
    public interface StateMachine<T>//定义状态机接口
    {
        T GetCurrentState();//返回当前状态

        T GetPreviousState();//返回上个状态

        void Register(T t);//注册状态

        void RemoveState(T t);//移除状态

        void SwitchState(T t);//切换状态
        
    }
```

#### 状态机GameStateMachine

```
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
```

#### 状态机状态GameState

```
    public enum GameState
    {
        GAME_CONNECT_STATE,//开始
        GAME_CONNECT_ING,
        GAME_CONNECT_ERROR,
        GAME_CONNECT_END,
    }
```

#### GameConnectStart状态

```
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
```

#### GameConnectIng状态

```
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
```

#### GameConnectERROR状态

```
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
```

#### GameConnectEnd状态

```
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
```

#### 创建一个单利来执行这个状态机    （怎么执行都行~自己看着方便就行）

#### SingletonPattern(单利)

```
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
```

#### 在 主程序中运行

```
	static void Main(string[] args)
        {
            SingletonPattern.GetInstace().Init();//注册

            SingletonPattern.GetInstace().SwithcStateMachine(0);
            SingletonPattern.GetInstace().SwithcStateMachine((GameState)1);
            SingletonPattern.GetInstace().SwithcStateMachine((GameState)2);

            Console.ReadKey();
        }
```

