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

    public Item GetItem(Sprite icon)
    {
        return items.Find(item => item.icon.name == icon.name);
    }

    public Item GetRandomItem()
    {
        Item randomItem = items[Random.Range(0, items.Count)];

        while (randomItem.description=="simple item.")
        {
            randomItem = items[Random.Range(0, items.Count)];
        }

        return randomItem;
    }

    void BuildItemDatabase()
    {
        items = new List<Item>()
        {
            new Item(1, "Amarillo", "simple item.", Resources.Load<Sprite>("Sprites/Lab/Items/Amarillo")),

            new Item(2, "Azul", "simple item.", Resources.Load<Sprite>("Sprites/Lab/Items/Azul")),

            new Item(3, "Rojo", "simple item.", Resources.Load<Sprite>("Sprites/Lab/Items/Rojo")),

            new Item(4, "Naranja", "combo item.", Resources.Load<Sprite>("Sprites/Lab/Items/Naranja")),

            new Item(5, "Verde", "combo item.", Resources.Load<Sprite>("Sprites/Lab/Items/Verde")),

            new Item(6, "Lila", "combo item.", Resources.Load<Sprite>("Sprites/Lab/Items/Lila")),

           // new Item(7, "Negro", "warmed item.", Resources.Load<Sprite>("Sprites/Lab/Items/negro")),
        };
        
    }
}
