using System;
using System.Collections.Generic;
using DanielLochner.Assets.SimpleScrollSnap;
using Meta.Data;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using static Utilities.Extensions.UIExtensions;

namespace Meta.UI.Popups
{
    public class ScenesPopupUI : PopupUI
    {
        [Header("Setup")]
        [SerializeField] Transform container;
        [SerializeField] Button selectButton;
        [SerializeField] Button selectedButton;
        [SerializeField] SimpleScrollSnap scrollSnap;

        [Header("Canvas groups")]
        [SerializeField] CanvasGroup nextButtonCanvas;
        [SerializeField] CanvasGroup previousButtonCanvas;
        [SerializeField] CanvasGroup selectedHintCanvas;
        [SerializeField] CanvasGroup lockedHintCanvas;
        [SerializeField] CanvasGroup selectButtonCanvas;


        [Header("SceneUI")]
        [SerializeField] SceneUI prefab;
        [SerializeField] List<SceneUI> preCreated = new();
        [SerializeField] [ReadOnly] SceneUI centeredUI;
        [SerializeField] [ReadOnly] int selectedID;

        List<SceneUI> sceneUIs = new();
        public event Action<SceneData> OnSelect = delegate { };
        bool IsSelected => selectedID == scrollSnap.StartingPanel;
        
        protected override void OnShowUI()
        {
            scrollSnap.enabled = true;

            selectButton.onClick.AddListener(Select);
            selectedButton.onClick.AddListener(Hide);
            scrollSnap.OnPanelCentered.AddListener(Centered);

            RefreshSwapButtons();
            RefreshHint(IsSelected, sceneUIs[selectedID].IsLock);
        }



        protected override void OnHideUI()
        {
            scrollSnap.enabled = false;

            selectButton.onClick.RemoveListener(Select);
            selectedButton.onClick.RemoveListener(Hide);
            scrollSnap.OnPanelCentered.RemoveListener(Centered);
        }

        void Centered(int id, int unknown)
        {
            selectedID = id;
            centeredUI = sceneUIs[id];
            RefreshSwapButtons();
            RefreshHint(IsSelected, centeredUI.IsLock);
        }

        void RefreshHint(bool selected, bool isLock)
        {
            if (isLock)
                EnableGroup(lockedHintCanvas);
            else
                DisableGroup(lockedHintCanvas);

            if (selected)
                EnableGroup(selectedHintCanvas);
            else
                DisableGroup(selectedHintCanvas);

            if (isLock || selected)
                DisableGroup(selectButtonCanvas);
            else
                EnableGroup(selectButtonCanvas);
        }

        void Select()
        {
            scrollSnap.StartingPanel = selectedID;

            if (centeredUI)
                OnSelect(centeredUI.Data);

            Hide();
        }

        public void Set(IReadOnlyList<SceneData> scenes, int selected)
        {
            // _scenes = scenes.ToList();
            selectedID = selected;
            scrollSnap.StartingPanel = selected;

            sceneUIs = new List<SceneUI>();

            for (var i = 0; i < scenes.Count; i++)
            {
                var sc = scenes[i];
                var ui = i < preCreated.Count
                    ? preCreated[i]
                    : Instantiate(prefab, container);

                ui.transform.localScale = Vector3.one;
                ui.Set(sc, i);

                sceneUIs.Add(ui);
            }

            if (preCreated.Count > scenes.Count)
            {
                for (var i = scenes.Count; i < preCreated.Count; i++)
                {
                    preCreated[i].gameObject.SetActive(false);
                }
            }
        }

        void RefreshSwapButtons()
        {
            if (selectedID == 0)
                DisableGroup(previousButtonCanvas);
            else
                EnableGroup(previousButtonCanvas);

            if (selectedID == sceneUIs.Count - 1)
                DisableGroup(nextButtonCanvas);
            else
                EnableGroup(nextButtonCanvas);
        }
    }
}