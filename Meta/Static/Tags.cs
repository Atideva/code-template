using UnityEngine;

namespace Meta.Static
{
    public static class Tags 
    {
 
        public const string PLAYER = "Player";
        public const string ENEMY = "Enemy";
        public static string GetEnemy(string myTeamTag)
        {
            switch (myTeamTag)
            {
                case PLAYER:
                    return ENEMY;
                case ENEMY:
                    return PLAYER;
                default:
                    Debug.LogWarning("Not defined team tag");
                    return ENEMY;
            }
        }

 
    }
}
