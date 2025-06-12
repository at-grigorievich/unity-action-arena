using System;
using System.Collections.Generic;

namespace ATG.KillCounter
{
    public interface IKillCounter: IDisposable
    {
        IReadOnlyDictionary<string, int> KillByName { get; }
        
        event Action OnTableChanged;
        
        int GetKillCountByName(string name);
    }
}