using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;


[System.Serializable]
public enum DeckType
{
    Common,
    GoodAbilities,
    EvilAbilities
}


[CreateAssetMenu(fileName = "Deck_", menuName = "Cards/New Deck")]
public class DeckData : ScriptableObject
{
    [Header("Type")]
    [SerializeField] DeckType deckType;

    [Header("Style")]
    [SerializeField] Sprite cardFrame;
    [SerializeField] Sprite cardBackImage;

    [Header("Sound")]
    [SerializeField] AudioEvent rotateSound;

    [Header("Cards")]
    [SerializeField] List<CardData> cardsInDeck = new List<CardData>();


    public DeckType DeckType { get => deckType; }
    public Sprite CardFrame { get => cardFrame; }
    public Sprite CardBackImage { get => cardBackImage; }
    public AudioEvent RotateSound { get => rotateSound; }
    public ReadOnlyCollection<CardData> CardsInDeck => cardsInDeck.AsReadOnly();


    public void AddCard(CardData card)
    {
        if (!cardsInDeck.Contains(card))
            cardsInDeck.Add(card);
        else
            Debug.LogError("List already contains such card");
    }
}
