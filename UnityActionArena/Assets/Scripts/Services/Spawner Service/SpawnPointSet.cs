using System.Collections.Generic;
using UnityEngine;

namespace ATG.Spawn
{
    public class SpawnPointSet: MonoBehaviour
    {
        private ISpawnPoint[] _set;

        private void Awake()
        {
            _set = GetComponentsInChildren<ISpawnPoint>();
        }

        public ISpawnPoint GetRandomPoint() => GetRandomFreePoint();

        private ISpawnPoint GetRandomFreePoint()
        {
            List<ISpawnPoint> freePoints = new List<ISpawnPoint>();
            
            foreach (var spawnPoint in _set)
            {
                if (spawnPoint.IsFree == true)
                {
                    freePoints.Add(spawnPoint);
                }
            }
            
            return GetRndPoint(freePoints.Count == 0 ? _set : freePoints.ToArray());
        }
        
        private ISpawnPoint GetRndPoint(ISpawnPoint[] spawnPoints)
        {
            int rndIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
            return spawnPoints[rndIndex];
        }
    }
}