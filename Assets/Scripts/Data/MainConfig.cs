using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "SO/MainConfig", fileName = "MainConfig")]
    public class MainConfig : ScriptableObject
    {
        [field: SerializeField] public PlayerData Player { get; private set; }
        [field: Space(20f), SerializeField] public EnemyData Enemy { get; private set; }
        [field: Space(20f), SerializeField] public FactoryData Factory { get; private set; }
        [field: Space(20f), SerializeField] public HeroData Hero { get; private set; }
        [field: Space(20f), SerializeField] public CameraData Camera { get; private set; }
    }
}
