using Gameplay.UI.Perks;
using Meta.UI.Buttons;
using UnityEngine;
using UnityEngine.UI;

namespace Meta.UI
{
    public class SceneStatUI : MonoBehaviour
    {
        [SerializeField] ButtonUI button;
        [SerializeField] CanvasGroup checkMark;
       
        [Header("Stars")]
        [SerializeField] StarsPanelUI stars;
        [SerializeField] Image starsBackground;
        [SerializeField] Material grayMaterial;
        [SerializeField] Material normalMaterial;

        public void Set(int count)
        {
            stars.Set(count);
            starsBackground.material = count >= 3 ? normalMaterial : grayMaterial;
        }

        public void EnableCheckMark()
        {
            checkMark.alpha = 1;
        }

        public void DisableCheckMark()
        {
            checkMark.alpha = 0;
        }

        public void EnableRewardButton()
        {
            button.Enable();
            button.Show();
        }

        public void DisableRewardButton()
        {
            // button.Disable();
            button.Hide();
        }
    }
}