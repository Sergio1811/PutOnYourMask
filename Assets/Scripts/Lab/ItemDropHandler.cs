using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDropHandler : MonoBehaviour, IDropHandler
{
    Sprite sprite;

    private void Start()
    {
        sprite = GetComponent<Image>().sprite;
    }
    public void OnDrop(PointerEventData eventData)
    {
        RectTransform invPanel = transform as RectTransform;

        if (!RectTransformUtility.RectangleContainsScreenPoint(invPanel, Input.mousePosition))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Warmer"))
                {
                    Debug.Log("InWarmer");
                    print(LabManager.instance.itemDB.GetItem(sprite));

                    LabManager.instance.warmer.AddObject(LabManager.instance.itemDB.GetItem(sprite));
                    InventoryLab.instance.DeletePosition(int.Parse(transform.parent.name));
                    Destroy(gameObject);

                }
                else if (hit.collider.CompareTag("Centrifugator"))
                {
                    Debug.Log("InCentrifugator");

                    LabManager.instance.centifugator.AddObject(LabManager.instance.itemDB.GetItem(sprite));
                    InventoryLab.instance.DeletePosition(int.Parse(transform.parent.name));
                    Destroy(gameObject);
                }
                else if (hit.collider.CompareTag("Bin"))
                {
                    Debug.Log("Inbin");
                    InventoryLab.instance.DeletePosition(int.Parse(transform.parent.name));
                    Destroy(gameObject);
                }
                else if (hit.collider.CompareTag("Character"))
                {
                    hit.collider.GetComponent<ClientControl>().ItemDropped(LabManager.instance.itemDB.GetItem(sprite));
                    InventoryLab.instance.DeletePosition(int.Parse(transform.parent.name));
                    Destroy(gameObject);
                }
            }
        }
    }
}
