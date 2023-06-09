using Gameplay.Animations;
using UnityEngine;


namespace Gameplay.Units
{
    public class BossModel : UnitModel
    {
        SpineAnimator _spineAnimator;

        bool Error
        {
            get
            {
                if (_spineAnimator) return false;
                if (Animator is not SpineAnimator s) return true;
                _spineAnimator = s;
                return false;
            }
        }

        public void PlayAnimation(string animName, float animTime)
        {
            if (Error) return;
            _spineAnimator.PlayAnimation(animName,animTime);
        }
    }
}