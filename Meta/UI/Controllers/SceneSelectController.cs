using GameManager;
using Meta.Data;
using Meta.UI.Popups;
using UnityEngine;
using UnityEngine.UI;

namespace Meta.UI.Controllers
{
    public class SceneSelectController : MonoBehaviour
    {
        [SerializeField] ScenesPopupUI popupUI;
        [SerializeField] SceneUI currentSceneUI;
        [SerializeField] Button openButton;

        void Start()
        {
            var campaign = Game.Instance.Storage.Campaign;
            var scenes = campaign.Scenes;

            var current = campaign.CurrentScene;
            var currentID = scenes.GetID(current.so);
            currentSceneUI.Set(current, currentID);

            var list = campaign.Data;
            popupUI.Set(list, currentID);

            EventsUI.Instance.OnRefreshLocalization += Refresh;
        }

        void Refresh()
        {
            var campaign = Game.Instance.Storage.Campaign;
            var scenes = campaign.Scenes;
            var current = campaign.CurrentScene;
            var currentID = scenes.GetID(current.so);
            currentSceneUI.Set(current, currentID);

            var list = campaign.Data;
            popupUI.Set(list, currentID);
        }

        void OnEnable()
        {
            openButton.onClick.AddListener(Show);
        }

        void OnDisable()
        {
            openButton.onClick.RemoveListener(Show);
            EventsUI.Instance.OnRefreshLocalization -= Refresh;
        }

        void Show()
        {
            popupUI.Show();
            popupUI.OnSelect += Select;
            popupUI.OnHide += OnHide;
        }

        void OnHide()
        {
            popupUI.OnSelect -= Select;
            popupUI.OnHide -= OnHide;
        }

        void Select(SceneData scene)
        {
            var campaign = Game.Instance.Storage.Campaign;
            var currentID = campaign.Scenes.GetID(scene.so);

            campaign.SetCurrent(scene);
            currentSceneUI.Set(scene, currentID);
        }
    }
}