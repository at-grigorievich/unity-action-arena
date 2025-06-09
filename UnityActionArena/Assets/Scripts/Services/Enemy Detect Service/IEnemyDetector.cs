using System.Collections.Generic;

namespace ATG.EnemyDetector
{
    public interface IEnemyDetector
    {
        IEnumerable<IEnemy> Detect();
    }
}