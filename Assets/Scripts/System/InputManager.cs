using UnityEngine;
using Zenject;

namespace System
{
    public enum UserCommands
    {
        Left,
        Right,
        Forward,
        Backward,
        Stop,
        Fire
    }
    public sealed class InputManager : ITickable
    {
        public Action<UserCommands> onUserCommand;

        public void Tick()
        {
            if (Input.GetMouseButtonUp(0))
            {
                onUserCommand?.Invoke(UserCommands.Fire);
            }

            if (Input.GetKey(KeyCode.A))
            {
                onUserCommand?.Invoke(UserCommands.Left);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                onUserCommand?.Invoke(UserCommands.Right);
            }else if (Input.GetKey(KeyCode.W))
            {
                onUserCommand?.Invoke(UserCommands.Forward);
            }else if (Input.GetKey(KeyCode.S))
            {
                onUserCommand?.Invoke(UserCommands.Backward);
            }else
            {
                onUserCommand?.Invoke(UserCommands.Stop);
            }
        }
    }
}