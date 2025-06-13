using UnityEngine;

namespace ATG.KillCounter
{
    //TODO: make more progressive
    [CreateAssetMenu(fileName = "murder-rates-config", menuName = "Configs/New Murder Rates Config", order = 0)]
    public sealed class MurderRatesConfig: ScriptableObject
    {
        [field: SerializeField] public int KillCost { get; private set; }
    }
}