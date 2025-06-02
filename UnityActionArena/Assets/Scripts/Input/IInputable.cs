using System;
using UnityEngine;

namespace ATG.Input
{
    public interface IInputable
    {
        event Action<bool> OnLMBClicked;
        
        Vector2 GetDirection();
    }
}