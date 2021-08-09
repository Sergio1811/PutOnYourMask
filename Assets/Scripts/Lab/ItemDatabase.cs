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
            new Item(1, "Amarillo", "simple item.", Resources.Load<Sprite>("Sprites/Lab/Items/Amarillo"), new Color32 (240, 230, 68, 255)),

            new Item(2, "Azul", "simple item.", Resources.Load<Sprite>("Sprites/Lab/Items/Azul"), new Color32(78, 179, 215, 255)),

            new Item(3, "Rojo", "simple item.", Resources.Load<Sprite>("Sprites/Lab/Items/Rojo"), new Color32(218, 100, 107, 255)),

            new Item(4, "Naranja", "combo item.", Resources.Load<Sprite>("Sprites/Lab/Items/Naranja"), new Color32 (225,153,41,255)),

            new Item(5, "Verde", "combo item.", Resources.Load<Sprite>("Sprites/Lab/Items/Verde"),  new Color32(90,196,115,255)),

            new Item(6, "Lila", "combo item.", Resources.Load<Sprite>("Sprites/Lab/Items/Lila"), new Color32(166,124,211,255)),

            new Item(7, "Amarillo_Burbuja", "warmed item.", Resources.Load<Sprite>("Sprites/Lab/Items/Amarillo_Burbuja"), new Color32 (240, 230, 68, 255)),

            new Item(8, "Azul_Burbuja", "warmed item.", Resources.Load<Sprite>("Sprites/Lab/Items/Azul_Burbuja"), new Color32(78, 179, 215, 255)),

            new Item(9, "Rojo_Burbuja", "warmed item.", Resources.Load<Sprite>("Sprites/Lab/Items/Rojo_Burbuja"), new Color32(218, 100, 107, 255)),

            new Item(10, "Naranja_Burbuja", "warmed item.", Resources.Load<Sprite>("Sprites/Lab/Items/Naranja_Burbuja"), new Color32 (225,153,41,255)),

            new Item(11, "Verde_Burbuja", "warmed item.", Resources.Load<Sprite>("Sprites/Lab/Items/Verde_Burbuja"), new Color32(90,196,115,255)),

            new Item(12, "Lila_Burbuja", "warmed item.", Resources.Load<Sprite>("Sprites/Lab/Items/Lila_Burbuja"), new Color32(166,124,211,255)),

            new Item(0, "Error", "simple item.", Resources.Load<Sprite>("Sprites/Lab/Items/Marron"), Color.grey),
        };
        
    }
}
