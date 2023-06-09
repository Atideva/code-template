using I2.Loc;
using Meta.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Utilities.Extensions.UIExtensions;

namespace Meta.UI
{
    public class SceneUI : MonoBehaviour
    {
        [SerializeField] Image icon;
        [SerializeField] CanvasGroup infoGroup;
        [SerializeField] CanvasGroup statsGroup;

        [Header("Lock")]
        [SerializeField] Image lockIcon;
        [SerializeField] Color lockColor;

        [Header("Text")]
        [SerializeField] TextMeshProUGUI nameTxt;
        [SerializeField] TextMeshProUGUI totalKills;

        [Header("Localize Keys")]
        [SerializeField] LocalizationParamsManager longestParams;
        [SerializeField] LocalizationParamsManager totalTimeParams;
        [SerializeField] LocalizedString totalKillsString;

        [Header("Stats")]
        [SerializeField] SceneStatUI bossStat;
        [SerializeField] SceneStatUI questStat;
        [SerializeField] SceneStatUI pocketStat;
        public SceneData Data { get; private set; }
        public bool IsLock => Data.isLock;

        public void Set(SceneData data, int id)
        {
            Data = data;
            icon.sprite = data.so.Icon;

            nameTxt.text = $"{id + 1}. {data.so.Name}";

            RefreshLongestText(data.recordSeconds);
            RefreshTimeText(data.totalSeconds);
            totalKills.text = $"{totalKillsString}: {data.totalKills}";

            RefreshStat(bossStat, data.bossKills, data.bossCollected);
            RefreshStat(questStat, data.questsComplete, data.questsCollected);
            RefreshStat(pocketStat, data.treasureFound, data.treasureCollected);

            if (data.isLock)
                Lock();
            else
                Unlock();
        }

        void Lock()
        {
            icon.color = lockColor;
            lockIcon.enabled = true;
            DisableGroup(infoGroup);
            DisableGroup(statsGroup);
        }

        void Unlock()
        {
            icon.color = Color.white;
            lockIcon.enabled = false;
            EnableGroup(infoGroup);
            EnableGroup(statsGroup);
        }

        void RefreshLongestText(int seconds)
        {
            var min = seconds / 60;
            var sec = seconds - min * 60;
            longestParams.SetParameterValue("MINUTES", min.ToString());
            longestParams.SetParameterValue("SECONDS", sec.ToString());
        }

        void RefreshTimeText(int seconds)
        {
            var min = seconds / 60;
            var sec = seconds - min * 60;
            totalTimeParams.SetParameterValue("MINUTES", min.ToString());
            totalTimeParams.SetParameterValue("SECONDS", sec.ToString());
        }

        void RefreshStat(SceneStatUI stat, int amount, bool collected)
        {
            stat.Set(amount);

            if (collected)
                stat.EnableCheckMark();
            else
                stat.DisableCheckMark();

            if (amount >= 3 && !collected)
                stat.EnableRewardButton();
            else
                stat.DisableRewardButton();
        }
    }
}