using System.Collections.Generic;
using UnityEngine;

public class LevelUpMenu : MonoBehaviour
{
    public List<ItemCard> ItemCards;

    // TODO On LevelUp Signal show menu
    // TODO On Item Selected hide menu

    // TODO request items for each card.
    public void ShowMenu(bool active)
    {
        if (active)
            GenerateItems();

        gameObject.SetActive(active);
    }

    private void GenerateItems()
    {
        foreach (var card in ItemCards)
        {
            var item = new PlayerItem();
            card.ConnectItem(item);
        }
    }
}
