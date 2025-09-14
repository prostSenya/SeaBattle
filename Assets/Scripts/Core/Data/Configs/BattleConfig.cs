using UnityEngine;

namespace Core.Data.Configs
{
    [CreateAssetMenu(fileName = nameof(BattleConfig), menuName = "ScriptableObject/" + nameof(BattleConfig))]
    public class BattleConfig : ScriptableObject
    {
        public int GridSize;
        public ShipData[] ShipDatas;
    }
}