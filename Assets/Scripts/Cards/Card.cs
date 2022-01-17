using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public string type;
    public int dmg;
    public int supportCost;
    public int manaCost;

    public int dragPlayDistance;

    private Vector3 startPosition;

    private string state;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        state = "HAND";
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Drag Begin");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
        GetComponentInParent<Hand>().showLine(transform.position);
        //transform.position += (Vector3)eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Drag Ended");
        GetComponentInParent<Hand>().hideLine();
        if(Vector3.Distance(startPosition, (Vector3)eventData.position) > dragPlayDistance)
        {
            //play card
            if (FindObjectOfType<Director>().addTower((Vector3)eventData.position))
            {
                Object.Destroy(gameObject);
            }
            
        }
        transform.position = startPosition;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse Exit");
    }
}
