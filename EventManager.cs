using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{

    #region Singleton
    //-------------------------------------------------------------
    public static EventManager Instance;
    void Awake()
    {
        if (Instance == null) Instance = this;
        else gameObject.SetActive(false);
    }
    //-------------------------------------------------------------
    #endregion


    #region Cards actions
    // 
    //
    // Flip
    public event Action<GameObject> OnCard_actions_Flip = delegate { };
    public void Card_actions_Flip(GameObject card) => OnCard_actions_Flip(card);
    //
    //
    // Flip Back
    public event Action<GameObject> OnCard_actions_FlipBack = delegate { };
    public void Card_actions_FlipBack(GameObject card) => OnCard_actions_FlipBack(card);
    //
    //
    // Flip ENDED
    public event Action<GameObject> OnCard_actions_Flip_ENDED = delegate { };
    public void Card_actions_Flip_ENDED(GameObject card) => OnCard_actions_Flip_ENDED(card);
    //
    //
    // FlipBack ENDED
    public event Action<GameObject> OnCard_actions_FlipBack_ENDED = delegate { };
    public void Card_actions_FlipBack_ENDED(GameObject card) => OnCard_actions_FlipBack_ENDED(card);
    #endregion
}
