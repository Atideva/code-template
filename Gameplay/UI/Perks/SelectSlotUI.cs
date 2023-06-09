using System;
using System.Collections.Generic;
using I2.Loc;
using Meta.Data;
using SO.PerksSO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI.Perks
{
    public class SelectSlotUI : MonoBehaviour
    {
        [SerializeField] Image icon;
        [SerializeField] TextMeshProUGUI description;
        [SerializeField] TextMeshProUGUI nameLabel;
        //  [SerializeField] Localize nameLoc;
        [SerializeField] Image headerImage;
        [SerializeField] Image headerGradient;
        [SerializeField] Color headerActive;
        [SerializeField] Color headerPassive;
        
        public event Action<SelectSlotUI> OnClick = delegate { };

        [SerializeField] Button button;
        [SerializeField] StarsPanelUI perkStars;
        public PerkData Perk { get; private set; }

        public void Enable() => gameObject.SetActive(true);
        public void Disable() => gameObject.SetActive(false);

        void Awake()
            => button.onClick.AddListener(Click);

        void Click()
            => OnClick(this);

        public void Set(PerkData data)
        {
            gameObject.SetActive(true);
            Perk = data;
            nameLabel.text = data.so.Name;
            description.text = data.so.Description;
            icon.sprite = data.so.Icon;
            perkStars.Set(data.Level);
           
            headerImage.color = data.so switch
            {
                ActivePerkSO => headerActive,
                PassivePerkSO => headerPassive,
                _ => Color.white
            };
            headerGradient.color = data.so switch
            {
                ActivePerkSO => headerActive,
                PassivePerkSO => headerPassive,
                _ => Color.white
            };
            
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            // perkStars.Hide();
        }
    }
}