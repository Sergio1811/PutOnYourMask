using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    private void Awake()
    {
        BuildItemDatabase();
    }

    public Item GetItem(int id)
    {
       return items.Find(item => item.id == id);    
    }

    public Item GetItem(string title)
    {
       return items.Find(item => item.title == title);    
    }

    void BuildItemDatabase()
    {
        items = new List<Item>()
        {
            new Item(1, "Wheat", "simple item.", Resources.Load<Sprite>("Sprites/Lab/Items/Wheat")),

            new Item(2, "Beer", "warmed item.", Resources.Load<Sprite>("Sprites/Lab/Items/Beer")),

            new Item(3, "Box", "combo item.", Resources.Load<Sprite>("Sprites/Lab/Items/Box")),

            new Item(4, "Rabbit", "simple item.", Resources.Load<Sprite>("Sprites/Lab/Items/Rabbit")),

            new Item(5, "Death", "warmed item.", Resources.Load<Sprite>("Sprites/Lab/Items/Death")),

            new Item(6, "Egg", "combo item.", Resources.Load<Sprite>("Sprites/Lab/Items/Egg")),

            new Item(7, "Dinner", "combo item.", Resources.Load<Sprite>("Sprites/Lab/Items/Dinner")),
        };
        
    }
}
