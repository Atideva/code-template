using System.Collections;
using UnityEngine;

public class CardsFliper : MonoBehaviour
{
    public enum FlipDirection { Left, Right }

    [Header("Speed")]
    [SerializeField] float rotationSpeed;
    [SerializeField] float rotationSpeedBack;

    [Header("Directions")]
    [SerializeField] FlipDirection flipDirection;
    [SerializeField] FlipDirection flipBackDirection;

    [Header("Sounds")]
    [SerializeField] AudioEvent flipSound;
    [SerializeField] AudioEvent flipBackSound;

    const float SPEED_MULT = 100;
    const float ANGLE_ROT_MAX = 180;

    void Start()
    {
        EventManager.Instance.OnCard_actions_Flip += FlipCard;
        EventManager.Instance.OnCard_actions_FlipBack += FlipCardBack;
    }


    void FlipCard(GameObject card) => StartCoroutine(FlipCardAction(card));
    void FlipCardBack(GameObject card) => StartCoroutine(FlipCardBackAction(card));


    IEnumerator FlipCardAction(GameObject card)
    {
        PlaySound(flipSound);
        SetCardState(card, CardState.isFlip);

        Transform cardTransform = card.transform;
        float y = cardTransform.localEulerAngles.y;

        float dir = GetDirection(flipDirection);
        float angleRotated = 0;
        float angleRotatedMax = ANGLE_ROT_MAX;

        while (angleRotated < angleRotatedMax)
        {
            float step = rotationSpeed * SPEED_MULT * dir * Time.deltaTime;
            y += step;
            angleRotated += Mathf.Abs(step);

            cardTransform.localEulerAngles = new Vector3(cardTransform.localEulerAngles.x, y, cardTransform.localEulerAngles.z);
            yield return null;
        }

        if (angleRotated > angleRotatedMax)
        {
            float overAngle = angleRotated - angleRotatedMax;
            y -= overAngle;
            cardTransform.localEulerAngles = new Vector3(cardTransform.localEulerAngles.x, y, cardTransform.localEulerAngles.z);
        }

        SetCardState(card, CardState.flipFace);
        CardFlipENDED_event(cardTransform);
    }
    IEnumerator FlipCardBackAction(GameObject card)
    {
        PlaySound(flipBackSound);
        SetCardState(card, CardState.isFlip);

        Transform cardTransform = card.transform;
        float y = cardTransform.localEulerAngles.y;

        float dir = GetDirection(flipBackDirection);
        float angleRotated = 0;
        float angleRotatedMax = ANGLE_ROT_MAX;

        while (angleRotated < angleRotatedMax)
        {
            float step = rotationSpeedBack * SPEED_MULT * dir * Time.deltaTime;
            y += step;
            angleRotated += Mathf.Abs(step);

            cardTransform.localEulerAngles = new Vector3(cardTransform.localEulerAngles.x, y, cardTransform.localEulerAngles.z);
            yield return null;
        }

        if (angleRotated > angleRotatedMax)
        {
            float overAngle = angleRotated - angleRotatedMax;
            y -= overAngle * dir;
            cardTransform.localEulerAngles = new Vector3(cardTransform.localEulerAngles.x, y, cardTransform.localEulerAngles.z);
        }

        SetCardState(card, CardState.normal);
        CardFlipBackENDED_event(cardTransform);
    }


    void SetCardState(GameObject card, CardState state)
    {
        switch (state)
        {
            case CardState.normal: CardStateManager.Instance.SetState_Normal(card); break;
            case CardState.isFlip: CardStateManager.Instance.SetState_IsFlip(card); break;
            case CardState.flipFace: CardStateManager.Instance.SetState_FlipFace(card); break;
            default: break;
        }
    }
    void CardFlipENDED_event(Transform cardTransform) => EventManager.Instance.Card_actions_Flip_ENDED(cardTransform.gameObject);
    void CardFlipBackENDED_event(Transform cardTransform) => EventManager.Instance.Card_actions_FlipBack_ENDED(cardTransform.gameObject);


    void PlaySound(AudioEvent audioEvent) => AudioManager.Instance.PlaySimpleEvent(audioEvent);
    float GetDirection(FlipDirection dir) => dir == FlipDirection.Left ? 1 : -1;

}
