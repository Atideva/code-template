using GameManager;
using UnityEngine;
using UnityEngine.UI;

namespace Meta.UI.Controllers
{
    public class PlayButtonController : MonoBehaviour
    {
        [SerializeField] Button playButton;
        void Start()
        {
            playButton.onClick.AddListener(Play);
        }

        void Play()
        {
            Game.Instance.LoadGameplay();
        }
    }
}
