using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{
    public int id;
    public string title;
    public string description;
    public Sprite icon;


    public Item (int _id, string _title, string _description, Sprite _icon)
    {
        this.id = _id;
        this.title = _title;
        this.description = _description;
        this.icon = _icon;       
    }

    public Item (Item item)
    {
        this.id = item.id;
        this.title = item.title;
        this.description = item.description;
        this.icon = item.icon;
    }
}
