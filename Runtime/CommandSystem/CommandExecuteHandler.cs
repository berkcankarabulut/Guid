using System;
using UnityEngine;

namespace Project.Utility.Runtime.CommandSystem
{
    public abstract class CommandExecuteHandler : MonoBehaviour
    {
        [SerializeField] private Command[] _commands;

        private int _commandIndex;
        protected int CommandCount => _commands.Length; 
        
        public Action onAllCommandsExecuted = () => { }; 
        
        public void ExecuteAllCommands()
        {
            _commandIndex = 0;
            ResetAllCommand(); 
            ExecuteCommand();
        }

        private void ResetAllCommand()
        {
            for(int i = 0; i < _commands.Length-1; i++) 
                _commands[i].Undo();
        }

        private void ExecuteNextCommand()
        {
            _commandIndex++;
            ExecuteCommand();
        }

        private void OnAllCommandsExecuted()
        {
            //Debug.Log("ALL COMMANDS ARE EXECUTED.");
            onAllCommandsExecuted.Invoke();
        }

        private void ExecuteCommand()
        { 
            if (IsThereAnyAvailableCommandOnCurrentIndex())
            {
                Command currentCommand = _commands[_commandIndex];
                currentCommand.OnComplete += OnCommandCompleted;
                currentCommand.Execute();
            }
            else
            {
                OnAllCommandsExecuted();
            }
        }

        private void OnCommandCompleted(Command command)
        {
            //Debug.Log("COMMAND COMPLTED : " + command);
            bool checkedAndClosedConnectionWithCommand = CheckAndCloseConnectionWithCompletedCommand(command);
            if (!checkedAndClosedConnectionWithCommand) return;

            ExecuteNextCommand();
        }

        private bool CheckAndCloseConnectionWithCompletedCommand(Command command)
        {
            bool isCompletedAndCurrentCommandEqual = CheckCompletedAndCurrentCommandEquality(command);
            if (!isCompletedAndCurrentCommandEqual) return false;

            CloseConnectionWithCommand(command);
            return true;
        }

        private bool CheckCompletedAndCurrentCommandEquality(Command command)
        {
            Command currentCommand = _commands[_commandIndex];
            if (currentCommand != command)
            {
                Debug.LogError("CURRENT COMMAND : " + currentCommand.gameObject + " NOT EQUAL TO COMPLETED COMMAND : " + command.gameObject);
                return false;
            }

            return true;
        }

        private void CloseConnectionWithCommand(Command command)
        {
            command.OnComplete -= OnCommandCompleted;
        }


        private bool IsThereAnyAvailableCommandOnCurrentIndex() => _commandIndex < CommandCount;
    }
}