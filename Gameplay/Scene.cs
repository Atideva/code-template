using GameManager;
using Gameplay.Animations;
using Gameplay.Drop;
using Gameplay.Spawn;
using UnityEngine;
using Utilities.MonoCache.System;

namespace Gameplay
{
    public class Scene : Singleton<Scene>
    {
        [SerializeField] ScenePlayer player;
        [SerializeField] SceneUnits units;
        [SerializeField] SceneBullets bullets;
        [SerializeField] SceneExperienceDrop experienceDrop;
        [SerializeField] CameraFollow cameraFollow;
        [SerializeField] SceneSave saving;
        [SerializeField] EnemySpawner enemySpawner;
        [SerializeField] EnemyWaves enemyWaves;
        [SerializeField] BlockSpawner blockSpawner;
        [SerializeField] BonusChestsSpawn chestSpawner;
        [SerializeField] PlayTime playTime;
        [SerializeField] MagnetSpawner magnetSpawner;
        [SerializeField] NukeSpawner nukeSpawner;
        [SerializeField] DamageText damageText;
        [SerializeField] BossWarning bossWarning;

        public ScenePlayer Player => player;
        public SceneUnits Units => units;
        public EnemySpawner EnemySpawner => enemySpawner;
        public PlayTime PlayTime => playTime;
        public SceneBullets Bullets => bullets;

        public SceneExperienceDrop ExperienceDrop => experienceDrop;

        public BlockSpawner BlocksSpawner => blockSpawner;

        void Start()
        {
            var currentHero = Game.Instance.Storage.Heroes.Current;
            var so = currentHero.so;
            var lvl = currentHero.lvl;
            player.Init(so, lvl);

            cameraFollow.SetTarget(player.Hero.transform);
            saving.Init(this);

            var magnetTrigger = Game.Instance.Config.Settings.MagnetSpawnTrigger;
            var magnetDistance = Game.Instance.Config.Settings.MagnetSpawnDistanceFromHero;
            var hero = player.Hero;
            magnetSpawner.Init(experienceDrop, magnetTrigger, magnetDistance, hero);

            var bombTrigger = Game.Instance.Config.Settings.BombSpawnTrigger;
            var bombCooldown = Game.Instance.Config.Settings.BombSpawnCooldown;
            var bombDistance = Game.Instance.Config.Settings.BombSpawnDistanceFromHero;
            nukeSpawner.Init(units, bombTrigger, bombCooldown, bombDistance);

            enemySpawner.Init(player, units, blockSpawner);
            enemyWaves.Init(playTime, units);
            damageText.Init(player);
            bossWarning.Init(playTime);

            var campaign = Game.Instance.Storage.Campaign;
            playTime.Init(campaign, player);

            units.Register(player.Hero);
        }
    }
}