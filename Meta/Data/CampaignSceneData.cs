using System.Collections.Generic;
using Meta.Interface;

namespace Meta.Data
{
    [System.Serializable]
    public class CampaignSceneData: ISave, ISerialize
    {
        public SceneData current;
        public List<SceneData> scenes = new();
    }
}