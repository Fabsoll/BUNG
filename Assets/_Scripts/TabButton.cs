using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public TabGroup tabGroup;
    public int tabIndex;
    public Decorator decorator;
    public Image background;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (tabGroup.selectedTab == this) { return; }

        tabGroup.OnTabSelected(this);
        GeneratePanel(tabIndex);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (tabGroup.selectedTab == this) { return; }
        tabGroup.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (tabGroup.selectedTab == this) { return; }
        tabGroup.OnTabExit(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        background = GetComponent<Image>();
        tabGroup.Subscribe(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GeneratePanel(int tabIndex)
    {
        switch (tabIndex)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                decorator.GenerateMaterialWindowOuter();
                break;
            case 4:
                decorator.GenerateMaterialWindowInner();
                break;
        }
    }
}
