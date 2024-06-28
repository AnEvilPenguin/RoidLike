using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerItemSelectedEventArgs : System.EventArgs
{
    public PlayerItem Item { get; set; }

    public PlayerItemSelectedEventArgs(PlayerItem item)
    {
        Item = item;
    }
}

public class ItemCard : MonoBehaviour
{
    public static event System.EventHandler<PlayerItemSelectedEventArgs> PlayerItemSelected;

    public delegate void PlayerItemSelectedEventHandler(PlayerItemSelectedEventArgs e);

    public TMP_Text ItemName;
    public TMP_Text ItemDescription;
    public Button SelectButton;
    public PlayerItem Item;

    /*
    private void Start()
    {
        // For whatever reason I am apparently incapable of getting a button to accept clicks
        SelectButton.GetComponent<Button>().onClick.AddListener(() => HandleClick());
    }
    */


    public void ConnectItem(PlayerItem item)
    {
        Item = item;

        ItemName.text = Item.Name;
        ItemDescription.text = Item.Description;
    }

    public void HandleClick()
    {
        var playerItemSelected = PlayerItemSelected;

        if (playerItemSelected != null)
        {
            playerItemSelected(this, new PlayerItemSelectedEventArgs(Item));
        }
    }
}
