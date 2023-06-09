using System;
using GameManager;
using MenuTabsUI;
using SO.ShopSO;
using UnityEngine;

namespace Meta.UI.Controllers
{
    
    public class ShopController : MonoBehaviour
    {
        [SerializeField] Tab shopTab;
        [SerializeField] ShopListSO shopList;
        
        void Start()
        {
            EventsUI.Instance.OnOpenShop += OpenShopTab;
        }

        void OnDisable()
        {
            EventsUI.Instance.OnOpenShop -= OpenShopTab;
        }

        void OpenShopTab()
        {
            Debug.Log("Force open shop");
            shopTab.Click();
        }
    }
}