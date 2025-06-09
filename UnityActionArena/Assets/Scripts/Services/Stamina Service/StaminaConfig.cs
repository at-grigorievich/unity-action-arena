using UnityEngine;

namespace ATG.Stamina
{
    [CreateAssetMenu(fileName = "stamina-config", menuName = "Configs/New Stamina Config", order = 0)]
    public class StaminaConfig: ScriptableObject
    {
        [field: SerializeField] public int DefaultReduceAmount { get; private set; }
        [field: SerializeField] public int DefaultResetAmount { get; private set; }
        [field: SerializeField] public float DelayAfterReduce { get; private set; }
    }
}