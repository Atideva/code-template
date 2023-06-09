using System.Collections.Generic;
using Sirenix.OdinInspector;
using SO.ScenesSO;
using UnityEngine;

namespace SO.ConfigsSO
{
    [CreateAssetMenu(fileName = "Scenes List", menuName = "Config/Scenes List")]
    public class ScenesListSO : ScriptableObject
    {
        [InfoBox("Please, add scenes here manually / Незабудь добавить сюда уровень")]
        [Space(20)]
        [ListDrawerSettings(Expanded = true), InlineEditor]
        [SerializeField] List<SceneSO> list = new();

        public IReadOnlyList<SceneSO> List => list;

        public bool Exist(SceneSO so) => list.Contains(so);
        
        public int GetID(SceneSO scene) 
            => Exist(scene) ? list.IndexOf(scene) :0;
    }
}