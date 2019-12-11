using System;

namespace FSMStatemachineProject
{
    class Program
    {
        static void Main(string[] args)
        {
            

            SingletonPattern.GetInstace().Init();//注册

            SingletonPattern.GetInstace().SwithcStateMachine(0);
            SingletonPattern.GetInstace().SwithcStateMachine((GameState)1);

            try
            {
                object temp2 = null;
                string temp1 = temp2.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("错误原因" + e.Message);
                SingletonPattern.GetInstace().SwithcStateMachine((GameState)2);
            }

            Console.ReadKey();
        }
    }
}
