using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{
    public int id;
    public string title;
    public string description;
    public Sprite icon;
    public Color32 color;


    public Item (int _id, string _title, string _description, Sprite _icon, Color32 _color)
    {
        this.id = _id;
        this.title = _title;
        this.description = _description;
        this.icon = _icon;
        this.color = _color;
    }

    public Item (Item item)
    {
        this.id = item.id;
        this.title = item.title;
        this.description = item.description;
        this.icon = item.icon;
        this.color = item.color;
    }
}
