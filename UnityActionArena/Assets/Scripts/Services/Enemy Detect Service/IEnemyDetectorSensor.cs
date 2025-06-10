namespace ATG.EnemyDetector
{
    public interface IEnemyDetectorSensor
    {
        bool TryDetect(out IDetectable detectable);
    }
}