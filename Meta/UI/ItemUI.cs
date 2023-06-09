using System;
using GameManager;
using Meta.Data;
using Meta.Facade;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities.Pools;

namespace Meta.UI
{
    public class ItemUI : PoolObject
    {
        public Image icon;
        public Image back;
        public Image categoryBack;
        public Image categoryIcon;
        public Image levelUpAble;
        public TextMeshProUGUI lvlText;
        public Button clickButton;
        public CanvasGroup canvasGroup;
        public event Action<ItemUI> OnClick = delegate { };

        [field: SerializeField, ReadOnly]
        public EquipmentData Data { get; private set; }

        public void Empty() => Data = null;
        
        void Click()
        {
            Log.UIClick(gameObject.name, gameObject);
            EventsUI.Instance.ShowItemScan(Data);
            OnClick(this);
        }

        public void SetLevelUpAble(bool isAble) => levelUpAble.enabled = isAble;

        public void Refresh(ItemUIData data)
        {
            Data = data.Equip;
            icon.sprite = data.Icon;
            back.sprite = data.ItemBackground;
            // categoryBack.sprite = data.CategoryBack;
            categoryIcon.sprite = data.CategoryIcon;
            lvlText.text = "Lv. " + data.Lvl;
        }

        protected override void OnEnabled()
        {
            clickButton.onClick.AddListener(Click);
        }

        protected override void OnDisabled()
        {
            clickButton.onClick.RemoveListener(Click);
        }

        public void Show()
        {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }

        public void Hide()
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }
}