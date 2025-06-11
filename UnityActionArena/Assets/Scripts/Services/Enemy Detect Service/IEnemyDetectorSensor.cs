namespace ATG.EnemyDetector
{
    public enum EnemyDetectorType: byte
    {
        FIND_BY_NEAREST_DISTANCE = 0,
        FIND_WEAKEST = 1
    }
    
    public interface IEnemyDetectorSensor
    {
        bool CheckDetect();
        bool TryDetect(EnemyDetectorType type, out IDetectable detectable);
    }
}