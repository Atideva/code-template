using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum CardState
{
    back,
    isFlip,
    face
}


public class CardStateManager : MonoBehaviour
{

    #region Singleton
    //-------------------------------------------------------------
    public static CardStateManager Instance;
    void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Debug.LogError("Did you open it in prefab mode, or there's really 2 instanses of Singleton here?", gameObject);
            //gameObject.SetActive(false);
        }
    }
    //-------------------------------------------------------------
    #endregion

    Dictionary<GameObject, CardState> cardState = new Dictionary<GameObject, CardState>();
    List<GameObject> onceTouchedCards = new List<GameObject>();

    void Start()
    {
        EventManager.Instance.OnCard_State_changed += CardStateChanged;
    }

    void CardStateChanged(GameObject card, CardState state)
    {
        if (cardState.ContainsKey(card))
            cardState[card] = state;
        else
            cardState.Add(card, state);

        if (state == CardState.face)
        {
            if (!onceTouchedCards.Contains(card)) onceTouchedCards.Add(card);
        }
    }
    public CardState GetCardState(GameObject card)
    {
        if (!cardState.ContainsKey(card))
        {
            cardState.Add(card, CardState.back);
            Debug.LogError("Requested card isnt in state dictionary!");
        }
        return cardState[card];
    }


    public bool IsOnceTouched(GameObject card) => onceTouchedCards.Contains(card)


}
