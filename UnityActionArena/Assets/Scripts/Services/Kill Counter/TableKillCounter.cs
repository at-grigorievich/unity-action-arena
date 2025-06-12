using System;
using System.Collections.Generic;
using ATG.Attack;
using UnityEngine;

namespace ATG.KillCounter
{
    public sealed class TableKillCounter : IKillCounter
    {
        private readonly IEnumerable<IDieCountable> _allDieCountables;
        private readonly Dictionary<string, int> _killsByNameDic;
        
        public IReadOnlyDictionary<string, int> KillByName => _killsByNameDic;
        
        public event Action OnTableChanged;

        public TableKillCounter(IEnumerable<IDieCountable> allDieCountables)
        {
            _allDieCountables = allDieCountables;
            _killsByNameDic = new Dictionary<string, int>();

            foreach (var e in _allDieCountables)
            {
                e.OnDieCountRequired += OnAddDieToTableHandler;
            }
        }

        public int GetKillCountByName(string name)
        {
            return _killsByNameDic.GetValueOrDefault(name, 0);
        }
        
        public void Dispose()
        {
            foreach (var e in _allDieCountables)
            {
                e.OnDieCountRequired -= OnAddDieToTableHandler; 
            }
        }
        
        private void OnAddDieToTableHandler(AttackDamageData obj)
        {
            if (_killsByNameDic.TryAdd(obj.AttackerName, 1) == false)
                _killsByNameDic[obj.AttackerName]++;
            
            OnTableChanged?.Invoke();
        }
    }
}