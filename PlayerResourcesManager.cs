using UnityEngine;


[System.Serializable]
public class PlayerResources
{
    public int gold;
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

        Debug.Log("I'm DontDestroyOnLoad object", gameObject);
        DontDestroyOnLoad(gameObject);
        AwakeJob();
    }
    //-------------------------------------------------------------
    #endregion

    [SerializeField] PlayerResources playerResources;
    const string RESOURCES_KEY = ConstantsKeys.PLAYER_RESOURSES;


    public int Gold { get => playerResources.gold; }
    public int Gems { get => playerResources.gems; }
    public int Butterfiles { get => playerResources.butterflies; }


    void AwakeJob() => LoadData();
    void LoadData() => playerResources = PlayerPrefs.HasKey(RESOURCES_KEY) ? JsonUtility.FromJson<PlayerResources>(PlayerPrefs.GetString(RESOURCES_KEY)) : new PlayerResources();
    void SaveData() => PlayerPrefs.SetString(RESOURCES_KEY, JsonUtility.ToJson(playerResources));


    public void GoldAdd(int value) => GoldChange(value);
    public void GoldSubtract(int value) => GoldChange(-value);
    void GoldChange(int value)
    {
        playerResources.gold += value;
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
        playerResources.gold += gold;
        playerResources.gems += gem;
        playerResources.butterflies += butterflies;
        SaveData();
    }


}
