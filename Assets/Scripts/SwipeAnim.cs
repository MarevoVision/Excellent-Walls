using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeAnim : MonoBehaviour {

    public Animation menu_swipe;
    private bool tap;

	// Use this for initialization
	void Start () {
        tap = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpSwipe()
    {
        if (tap)
        {
            menu_swipe.Play("Menu_MatSwipe");
            tap = false;
        }
    }

    public void DownSwipe()
    {
        if (!tap)
        {
            menu_swipe.Play("Mat_SwipeDown");
            tap = true;
        }
    }

    public void Tap()
    {
        if (tap)
        {
            menu_swipe.Play("Menu_MatSwipe");
            tap = false;
        }
        else
        {
            menu_swipe.Play("Mat_SwipeDown");
            tap = true;
        }
    }
    public void StaticUpMenu()
    {
        if (!tap)
        {
            tap = true;
        }
        menu_swipe.Play("statikUpMenu");
    }
}
