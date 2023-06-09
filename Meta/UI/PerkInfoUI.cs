using DG.Tweening;
using Sirenix.OdinInspector;
using SO.PerksSO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Meta.UI
{
    public class PerkInfoUI : MonoBehaviour
    {
        [SerializeField] Image icon;
        [SerializeField] TextMeshProUGUI nameText;
        [SerializeField] TextMeshProUGUI requireText;
        [SerializeField] TextMeshProUGUI requireLvl;
        [SerializeField] Material ownedMaterial;
        [SerializeField] Material notOwnedMaterial;
        [SerializeField] [ReadOnly] PerkSO perk;

        public void Refresh(PerkSO so, int lvlRequire, bool isOwned)
        {
            perk = so;

            icon.sprite = so.Icon;
            icon.material = isOwned ? ownedMaterial : notOwnedMaterial;

            nameText.text = so.Name;
            nameText.DOFade(isOwned ? 1 : 0.35f, 0);

            requireText.enabled = !isOwned;
            requireLvl.enabled = !isOwned;
            
            var txt = requireText.text;
            requireText.text = "(" + txt + ")";
            requireLvl.text = lvlRequire.ToString();
 
            requireText.DOFade(0.35f, 0);
            requireLvl.DOFade(0.35f, 0);
        }
    }
}