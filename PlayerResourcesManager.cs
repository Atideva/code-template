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


    public int Gold { get => playerResources.goldCoins; }
    public int Gems { get => playerResources.gems; }
    public int Butterfiles { get => playerResources.butterflies; }


    void AwakeJob() => LoadData();
    void LoadData() => playerResources = PlayerPrefs.HasKey(RESOURCES) ? JsonUtility.FromJson<PlayerResources>(PlayerPrefs.GetString(RESOURCES)) : new PlayerResources();
    void SaveData() => PlayerPrefs.SetString(RESOURCES, JsonUtility.ToJson(playerResources));


    public void GoldAdd(int value) => GoldChange(value);
    public void GoldSubtract(int value) => GoldChange(-value);
    void GoldChange(int value)
    {
        playerResources.goldCoins += value;
        SaveData();
    }

  
    public void GemAdd(int value) => GemChange(value);
    public void GemSubtract(int value) => GemChange(-value);
    void GemChange(int value)
    {
        playerResources.gems += value;
        SaveData();
    }


    public void ButterfliesAdd(int value) => ButterfliesChange(value);
    public void ButterfliesSubtract(int value) => ButterfliesChange(-value);
    void ButterfliesChange(int value)
    {
        playerResources.butterflies += value;
        SaveData();
    }


    public void MultiResourcesAdd(int gold, int gem, int butterflies) => MultiResourcesChange(gold, gem, butterflies);
    public void MultiResourcesSubtract(int gold, int gem, int butterflies) => MultiResourcesChange(-gold, -gem, -butterflies);
    void MultiResourcesChange(int gold, int gem, int butterflies)
    {
        playerResources.goldCoins += gold;
        playerResources.gems += gem;
        playerResources.butterflies += butterflies;
        SaveData();
    }


}
