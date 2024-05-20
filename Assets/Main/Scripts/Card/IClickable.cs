using UnityEngine;

public class IClickable : MonoBehaviour
{
    Card card;

    private void OnMouseDown()
    {
        card = GetComponent<Card>();

        if (GameManager.Instance.IsClickable && !card.IsMatch)
            GameManager.Instance.AddClickedCard(card);
    }
}