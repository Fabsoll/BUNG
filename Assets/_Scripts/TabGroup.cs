using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour
{
    private List<TabButton> tabButtons;

    public Color tabIdle;
    public Color tabSelected;
    public Color tabHovered;
    public TabButton selectedTab;
    public List<GameObject> objectsToSwap;

    public GameObject windowManager;

    private void Start()
    {
        ResetTabs();
        foreach (GameObject obj in objectsToSwap)
        {
            obj.SetActive(false);
        }
    }

    private void Update()
    {
        
    }

    public void Subscribe(TabButton button)
    {
        if (tabButtons == null)
        {
            tabButtons = new List<TabButton>();
        }

        tabButtons.Add(button);
    }

    public void ResetTabs()
    {
        foreach (TabButton button in tabButtons)
        {
            if(selectedTab != null && button == selectedTab) { continue; }
            button.background.color = tabIdle;
        }
    }

    public void OnTabEnter(TabButton button)
    {
        
        ResetTabs();
        /*windowManager.GetComponent<WindowManager>().resetWindows();
        windowManager.GetComponent<WindowManager>().enableSpawnButtons();*/
        if(selectedTab == null || button != selectedTab)
        {
            button.background.color = tabHovered;
        }
        
    }

    public void OnTabExit(TabButton button)
    {
        ResetTabs();
        
    }
    public void OnTabSelected(TabButton button)
    {
        selectedTab = button;
        ResetTabs();
        windowManager.GetComponent<WindowManager>().resetWindows();
        //windowManager.GetComponent<WindowManager>().enableSpawnButtons();
        button.background.color = tabSelected;
        int index = button.transform.GetSiblingIndex();
        for (int i = 0; i < objectsToSwap.Count; i++)
        {
            if (i == index)
            {
                objectsToSwap[i].SetActive(true);
            }
            else
            {
                objectsToSwap[i].SetActive(false);
            }
        }
    }
}
