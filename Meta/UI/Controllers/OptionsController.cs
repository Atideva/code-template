using System;
using System.Linq;
using GameManager;
using Meta.Enums;
using Meta.UI.Popups;
using UnityEngine;
using UnityEngine.UI;

namespace Meta.UI.Controllers
{
    public class OptionsController : MonoBehaviour
    {
        [SerializeField] OptionsPopupUI optionsPopup;
        [SerializeField] Button openButton;

        void OnEnable()
        {
            openButton.onClick.AddListener(Open);
        }

        void OnDisable()
        {
            openButton.onClick.RemoveListener(Open);
        }

        void OnToggle(OptionToggleEnum type, bool toggle)
        {
            Debug.Log("Options toggle: " + type + " (" + toggle + ")");
            EventsUI.Instance.OptionToggle(type, toggle);
        }

        void Refresh()
        {
            var optionData = Game.Instance.Storage.Options.Toggles;
            foreach (var data in optionData)
            {
                var find
                    = optionsPopup.Toggles.FirstOrDefault(t => t.Enum == data.type);
                if (find)
                    find.SetToggle(data.toggle);
            }
        }

        void Open()
        {
            Refresh();
            optionsPopup.Show();
            optionsPopup.OnToggle += OnToggle;
        }
    }
}