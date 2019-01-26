using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeAnimMenu : MonoBehaviour {

    public Animation menu_swipe;
    private bool tap2;

	// Use this for initialization
	void Start () {
        tap2 = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RightSwipe()
    {
        if (tap2)
        {
            menu_swipe.Play("MenuUIRight");
            tap2 = false;
        }
    }

    public void LeftSwipe()
    {
        if (!tap2)
        {
            menu_swipe.Play("MenuUILeft");
            tap2 = true;
        }
    }

    public void Tap()
    {
        if (tap2)
        {
            menu_swipe.Play("MenuUIRight");
            tap2 = false;
        }
        else
        {
            menu_swipe.Play("MenuUILeft");
            tap2 = true;
        }
    }
}
