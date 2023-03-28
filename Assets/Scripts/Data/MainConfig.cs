using Gameplay.Characters.Hero;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "SO/MainConfig", fileName = "MainConfig")]
    public class MainConfig : ScriptableObject
    {
        [field: SerializeField] public HeroController HeroPrefab { get; private set; }
        [field: Space(20f), SerializeField] public PlayerData Player { get; private set; }
        [field: Space(20f), SerializeField] public EnemyManagerData EnemyManager { get; private set; }
        [field: Space(20f), SerializeField] public FactoryData Factory { get; private set; }
        [field: Space(20f), SerializeField] public HeroData Hero { get; private set; }
        [field: Space(20f), SerializeField] public CameraData Camera { get; private set; }
    }
}
