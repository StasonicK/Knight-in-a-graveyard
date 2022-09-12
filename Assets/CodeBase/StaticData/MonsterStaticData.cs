using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "MonsterData", menuName = "StaticData/Monster")]
    public class MonsterStaticData : ScriptableObject
    {
        public MonsterTypeId MonsterTypeId;

        [Range(1, 100)] public int Hp;

        [Range(1f, 30f)] public float Damage;

        [Range(0, 10)] public float MoveSpeed;

        [Range(0.5f, 1)] public float EffectiveDistance;

        [Range(0.5f, 1)] public float Cleavage;

        [Range(0.5f, 1)] public float AttackCooldown;

        public int MinLoot;

        public int MaxLoot;

        public AssetReferenceGameObject PrefabReference;
    }
}