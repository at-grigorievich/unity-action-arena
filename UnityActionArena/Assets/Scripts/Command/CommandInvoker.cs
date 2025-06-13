using System;
using System.Collections.Generic;
using UnityEngine;

namespace ATG.Command
{
    public sealed class CommandInvoker: ICommand, IDisposable
    {
        private readonly ICommand[] _commands;
        private readonly bool _ignoreIfAlreadyExecuted;
        
        public event Action<bool> OnComplete; 
        
        private Queue<ICommand> _commandQueue;
        private ICommand _currentCommand;
        
        public CommandInvoker(bool ignoreIfAlreadyExecuted = false, params ICommand[] commands)
        {
            _commands = commands;
        }

        public event Action<bool> OnCompleted;

        public void Execute()
        {
            if (_ignoreIfAlreadyExecuted == false && _currentCommand != null)
            {
                Debug.LogWarning("Command already executed");
                return;
            }
            
            Reset();
            
            _currentCommand = _commandQueue.Dequeue();
            _currentCommand.OnCompleted += MoveNext;
            _currentCommand.Execute();
        }

        public void Dispose()
        {
            _currentCommand?.Dispose();
            _commandQueue?.Clear();
        }
        
        private void Reset()
        {
            _currentCommand?.Dispose();
            _currentCommand = null;
            _commandQueue = new Queue<ICommand>(_commands);
        }

        private void MoveNext(bool commandExecutedStatus)
        {
            _currentCommand.OnCompleted -= MoveNext;

            if (commandExecutedStatus == false)
            {
                OnCompleted?.Invoke(false);
                Dispose();
                return;
            }

            if (_commandQueue.Count == 0)
            {
                OnCompleted?.Invoke(true);
                Dispose();
                return;
            }
            
            _currentCommand = _commandQueue.Dequeue();
            _currentCommand.OnCompleted += MoveNext;
            _currentCommand.Execute();
        }
    }
}