using System;
using GameManager;
using Gameplay.Spawn;
using Gameplay.UI;
using Gameplay.Units;
using Meta.Save.Storage;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay
{
    public class PlayTime : MonoBehaviour
    {
        [SerializeField] [ReadOnly] int minutes;
        [SerializeField] [ReadOnly] int seconds;
        int _totalSeconds;
        float _timer;
        bool _isPause;
        [SerializeField] [ReadOnly]  bool _bossFight;
        public bool Stop => _isPause || _bossFight;
        public int Minutes => minutes;
        public int Seconds => seconds;
        public int TotalSeconds => _totalSeconds;

        CampaignScenesStorage _storage;
        ScenePlayer _player;
        const int TIME_LIMIT = 900;

        public void Init(CampaignScenesStorage storage, ScenePlayer player)
        {
            _storage = storage;
            _player = player;
            _player.Hero.Hitpoints.OnDeath += PlayerDead;

            GameplayEvents.Instance.OnPause += Pause;
            GameplayEvents.Instance.OnContinue += Continue;
            
            GameplayEvents.Instance.OnBossSpawn += BossSpawn;
            GameplayEvents.Instance.OnBossDeath += BossDead;
        }

        void PlayerDead()
        {
            Pause();
        }


        void BossSpawn(Unit boss) => _bossFight = true;
        void BossDead(Unit boss) => _bossFight = false;


        void OnDisable()
        {
            GameplayEvents.Instance.OnPause -= Pause;
            GameplayEvents.Instance.OnContinue -= Continue;
            GameplayEvents.Instance.OnBossSpawn -= BossSpawn;
            GameplayEvents.Instance.OnBossDeath -= BossDead;
        }

        void Continue() => _isPause = false;
        void Pause() => _isPause = true;


        void FixedUpdate()
        {
            if (Stop) return;
            
            _timer += Time.fixedDeltaTime;

            if (_timer < 1) return;
            _timer -= 1;

            seconds++;
            _totalSeconds++;

            if (seconds == 60)
            {
                seconds -= 60;
                minutes++;
                GameplayEvents.Instance.MinutePassed();
            }

            Text();
            Record();
        }

        void Record()
        {
            if (!_storage || _storage.CurrentScene == null) return;

            var record = _storage.CurrentScene.recordSeconds;
            if (_totalSeconds >= TIME_LIMIT)
                _totalSeconds = TIME_LIMIT;

            if ((_totalSeconds - record) > 10
                || _totalSeconds >= TIME_LIMIT)
                _storage.RefreshCurrentRecord(_totalSeconds);
        }

        void Text() => GameplayUI.Instance.SurviveTime.RefreshText(minutes, seconds);
    }
}