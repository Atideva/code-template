using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public enum CardState
{
    back,
    isFlip,
    face
}
[System.Serializable]
public enum CardStatus
{
    close,
    open
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
    Dictionary<GameObject, CardStatus> cardStatus = new Dictionary<GameObject, CardStatus>();
    List<GameObject> onceFacedCards = new List<GameObject>();


    public void SetCardState(GameObject card, CardState state)
    {
        if (cardState.ContainsKey(card))
            cardState[card] = state;
        else
            cardState.Add(card, state);

        if (state == CardState.face)
        {
            if (!onceFacedCards.Contains(card)) onceFacedCards.Add(card);
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


    public void SetCardStatus(GameObject card, CardStatus status)
    {
        if (cardStatus.ContainsKey(card))
            cardStatus[card] = status;
        else
            cardStatus.Add(card, status);
    }
    public CardStatus GetCardStatus(GameObject card)
    {
        if (!cardStatus.ContainsKey(card))
        {
            cardStatus.Add(card, CardStatus.close);
            Debug.LogError("Requested card isnt in status dictionary!");
        }
        return cardStatus[card];
    }




    public void SetState_Back(GameObject card) => SetCardState(card, CardState.back);
    public void SetState_IsFlip(GameObject card) => SetCardState(card, CardState.isFlip);
    public void SetState_Face(GameObject card) => SetCardState(card, CardState.face);

    public void SetStatus_Open(GameObject card) => SetCardStatus(card, CardStatus.open);
    public void SetStatus_Close(GameObject card) => SetCardStatus(card, CardStatus.close);

    public bool IsOnceFaced(GameObject card) => onceFacedCards.Contains(card);


}
