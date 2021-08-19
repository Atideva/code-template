using UnityEngine;


[System.Serializable]
public class PlayerResources
{
    public int goldCoins;
    public int gems;
    public int butterflies;

}


public class PlayerResourcesManager : MonoBehaviour
{

    #region Awake Singleton
    //-------------------------------------------------------------
    public static PlayerResourcesManager Instance;
    void Awake()
    {
        if (Instance == null) Instance = this;
        else gameObject.SetActive(false);

        Debug.LogWarning("Make me DontDestroyOnLoad please, to prevent load huge data for all levels at each OnLevelLoad");
        AwakeJob();
    }
    //-------------------------------------------------------------
    #endregion


    [SerializeField] PlayerResources playerResources;
    const string RESOURCES = ConstantsKeys.PLAYER_RESOURSES;
 

    public int Gold { get => playerResources.goldCoins;   }
    public int Gems { get => playerResources.gems;   }
    public int Special { get => playerResources.butterflies;   }


    void AwakeJob() => LoadData();
    void LoadData() => playerResources = PlayerPrefs.HasKey(RESOURCES) ? JsonUtility.FromJson<PlayerResources>(PlayerPrefs.GetString(RESOURCES)) : new PlayerResources();
    void SaveData() => PlayerPrefs.SetString(RESOURCES, JsonUtility.ToJson(playerResources));


    public bool TryBuy_UseMultiResources(int gold, int gem, int butterlies)
    {
        if (playerResources.goldCoins >= gold &&
            playerResources.gems >= gem &&
            playerResources.butterflies >= butterlies)
        {
            ResourcesSubtract(gold, gem, butterlies);
            SaveData();
            return true;
        }
        else
            return false;
    }
    void ResourcesSubtract(int gold, int gem, int butterlies)
    {
        playerResources.goldCoins -= gold;
        playerResources.gems -= gem;
        playerResources.butterflies -= butterlies;
    }


    public bool TryBuy_UseGold(int value)
    {
        if (playerResources.goldCoins >= value)
        {
            GoldSubtract(value);
            return true;
        }
        else
            return false;
    }
    void GoldChange(int value)
    {
        playerResources.goldCoins += value;
        SaveData();
    }
    public void GoldAdd(int value) => GoldChange(value);
    public void GoldSubtract(int value) => GoldChange(-value);


    public bool TryBuy_UseGem(int value)
    {
        if (playerResources.gems >= value)
        {
            GemSubtract(value);
            return true;
        }
        else
            return false;
    }
    void GemChange(int value)
    {
        playerResources.gems += value;
        SaveData();
    }
    public void GemAdd(int value) => GemChange(value);
    public void GemSubtract(int value) => GemChange(-value);


    public bool TryBuy_UseButterflies(int value)
    {
        if (playerResources.butterflies >= value)
        {
            ButterfliesSubtract(value);
            return true;
        }
        else
            return false;
    }
    void ButterfliesChange(int value)
    {
        playerResources.butterflies += value;
        SaveData();
    }
    public void ButterfliesAdd(int value) => ButterfliesChange(value);
    public void ButterfliesSubtract(int value) => ButterfliesChange(-value);


}
