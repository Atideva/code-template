using UnityEngine;

public class ShopVendor : MonoBehaviour
{

    #region Singleton
    //-------------------------------------------------------------
    public static ShopVendor Instance;
    void Awake()
    {
        if (Instance == null) Instance = this;
        else gameObject.SetActive(false);

        Debug.LogWarning("Make me DontDestroyOnLoad please, to prevent load huge data for all levels at each OnLevelLoad");
    }
    //-------------------------------------------------------------
    #endregion

    PlayerResourcesManager playerResourcesManager;
    void Start()
    {
        playerResourcesManager = PlayerResourcesManager.Instance;
    }


    public bool TryBuy_UseGold(int value)
    {
        if (playerResourcesManager.Gold >= value)
        {
            playerResourcesManager.GoldSubtract(value);
            return true;
        }
        else
            return false;
    }
    public bool TryBuy_UseGem(int value)
    {
        if (playerResourcesManager.Gems >= value)
        {
            playerResourcesManager.GemSubtract(value);
            return true;
        }
        else
            return false;
    }
    public bool TryBuy_UseButterflies(int value)
    {
        if (playerResourcesManager.Butterfiles >= value)
        {
            playerResourcesManager.ButterfliesSubtract(value);
            return true;
        }
        else
            return false;
    }
    public bool TryBuy_UseMultiResources(int valueGold, int valueGem, int valueButterflies)
    {
        if (playerResourcesManager.Gold >= valueGold &&
            playerResourcesManager.Gems >= valueGem &&
            playerResourcesManager.Butterfiles >= valueButterflies)
        {
            playerResourcesManager.MultiResourcesSubtract(valueGold, valueGem, valueButterflies);
            return true;
        }
        else
            return false;
    }

}
