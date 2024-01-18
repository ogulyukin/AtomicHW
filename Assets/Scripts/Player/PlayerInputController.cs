using System;
using UnityEngine;
using Zenject;

namespace Player
{
    public sealed class PlayerInputController : IDisposable, ITickable
    {
        private readonly InputManager inputManager;
        private UserCommands lastUserCommand = UserCommands.Stop;
        private bool fireRequired;
        private readonly PlayerModel playerModel;

        public PlayerInputController(InputManager iManager, PlayerModel model)
        {
            inputManager = iManager;
            inputManager.onUserCommand += GetNewCommand;
            playerModel = model;
        }

        private void GetNewCommand(UserCommands command)
        {
            if (command == UserCommands.Fire)
            {
                fireRequired = true;
            }

            lastUserCommand = command;
        }
        
        public void Tick()
        {
            playerModel.moveDirection.Value = GetDirection(); 
            if (fireRequired)
            {
                fireRequired = false;
                playerModel.onFireRequested?.Invoke();
            }
        }

        public void Dispose()
        {
            inputManager.onUserCommand -= GetNewCommand;
        }

        private Vector3 GetDirection()
        {
            switch (lastUserCommand)
            {
                case UserCommands.Left:
                    return -playerModel.transform.right;
                case UserCommands.Right:
                    return playerModel.transform.right;
                case UserCommands.Forward:
                    return playerModel.transform.forward;
                case UserCommands.Backward:
                    return -playerModel.transform.forward;
                default:
                    return Vector3.zero;
            }
        }
    }
}
