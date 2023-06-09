using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.UI.Perks
{
    public class StarsPanelUI : MonoBehaviour
    {
        [SerializeField] List<StarUI> perkStars = new();

        public void Set(int lvl)
        {
            foreach (var star in perkStars)
                star.StopAnimation();

            for (var i = 0; i < lvl; i++)
                perkStars[i].Enable();

            for (var i = lvl; i < perkStars.Count; i++)
            {
                if (i == lvl && lvl >0)
                    perkStars[i].PlayAnimation();
                else
                    perkStars[i].Disable();
            }
        }

        public void Hide()
        {
            foreach (var star in perkStars)
             star.Disable();   
        }
    }
}