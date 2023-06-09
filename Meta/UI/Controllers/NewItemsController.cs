using System.Collections;
using System.Collections.Generic;
using GameManager;
using Gameplay.Animations;
using Meta.Data;
using Meta.Facade;
using Meta.Static;
using Meta.UI.Popups;
using Sirenix.OdinInspector;
using UnityEngine;
using Utilities.Pools;
using static Meta.Static.Equip;


namespace Meta.UI.Controllers
{
    public class NewItemsController : WithPool<ItemUIPool, ItemUI>
    {
        [SerializeField] NewItemsPopupUI popup;
        [SerializeField] float itemShowRate = 0.05f;
        [SerializeField] Vfx itemShowVfx;
        [Header("DEBUG")]
        [SerializeField] [ReadOnly] List<ItemUI> items;


        void Start()
        {
            EventsUI.Instance.OnShowNewItems += Show;
            popup.OnHide += OnHide;
        }

        void OnDisable()
        {
            EventsUI.Instance.OnShowNewItems -= Show;
            popup.OnHide -= OnHide;
        }

        void Show(List<EquipmentData> equips)
        {
            StartCoroutine(ShowRoutine(equips));
        }

        void OnHide()
        {
            StopAllCoroutines();

            if (items.Count <= 0) return;
            foreach (var item in items)
            {
                item.ReturnToPool();
            }
        }

        IEnumerator ShowRoutine(List<EquipmentData> equips)
        {
            popup.Show();

            foreach (var equip in equips)
            {
                var item = Pool.Get();
                item.Hide();

                var ui = Item.NewUIData(equip);
                item.Refresh(ui);

#if UNITY_EDITOR
                var itemName = Name(equip) != string.Empty
                    ? Name(equip)
                    : equip.so.name;
                item.gameObject.name = "[NewItem] " + itemName;
#endif

                item.SetLevelUpAble(false);
                items.Add(item);

                yield return null;
            }

            foreach (var item in items)
            {
                item.Show();
                VFX.Play(itemShowVfx, item.transform.position);
                yield return new WaitForSeconds(itemShowRate);
            }
        }
    }
}