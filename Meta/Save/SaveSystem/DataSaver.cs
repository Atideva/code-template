using UnityEngine;

namespace Meta.Save.SaveSystem
{
    public static class DataSaver
    {
        public static void Save<T>(T data, string key)
            => PlayerPrefs.SetString(key, JsonUtility.ToJson(data));

        public static T Load<T>(string key) where T : new()
            => PlayerPrefs.HasKey(key)
                ? JsonUtility.FromJson<T>(PlayerPrefs.GetString(key))
                : new T();
    }
}