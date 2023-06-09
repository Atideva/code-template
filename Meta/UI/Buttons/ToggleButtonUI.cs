using System;
using UnityEngine;
using UnityEngine.UI;

namespace Meta.UI.Buttons
{
 
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Animator))]
    public class ToggleButtonUI : MonoBehaviour
    {
        [SerializeField] Button button;
        [SerializeField] Animator animator;
        [SerializeField] Image bgEnabledImage;
        [SerializeField] Image bgDisabledImage;
        [SerializeField] Image handleEnabledImage;
        [SerializeField] Image handleDisabledImage;
        bool _toggle;
        public event Action<bool> OnToggle = delegate { };
        public bool IsToggled => _toggle;

        void Awake()
        {
            // _button = GetComponent<Button>();
            // _animator = GetComponent<Animator>();

            // _bgEnabledImage = transform.GetChild(0).GetChild(0).GetComponent<Image>();
            // _bgDisabledImage = transform.GetChild(0).GetChild(1).GetComponent<Image>();
            // _handleEnabledImage = transform.GetChild(1).GetChild(0).GetComponent<Image>();
            // _handleDisabledImage = transform.GetChild(1).GetChild(1).GetComponent<Image>();

            _toggle = true;
        }

        void OnEnable()
        {
            button.onClick.AddListener(Toggle);
        }

        void OnDisable()
        {
            button.onClick.RemoveListener(Toggle);
        }

        public void Toggle()
        {
            _toggle = !_toggle;
            if (_toggle)
            {
                bgDisabledImage.gameObject.SetActive(false);
                bgEnabledImage.gameObject.SetActive(true);
                handleDisabledImage.gameObject.SetActive(false);
                handleEnabledImage.gameObject.SetActive(true);
            }
            else
            {
                bgEnabledImage.gameObject.SetActive(false);
                bgDisabledImage.gameObject.SetActive(true);
                handleEnabledImage.gameObject.SetActive(false);
                handleDisabledImage.gameObject.SetActive(true);
            }

            animator.SetTrigger(_toggle ? "Enable" : "Disable");

            OnToggle(_toggle);
        }
    }
}