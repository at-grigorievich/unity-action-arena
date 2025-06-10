using System.Collections.Generic;

namespace ATG.EnemyDetector
{
    public interface IEnemyDetector
    {
        IReadOnlyCollection<IDetectable> Detect();
    }
}