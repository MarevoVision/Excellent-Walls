using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GeneralManager : MonoBehaviour
{

    void Start()
    {
        //sizeSlider.maxValue = maxValueSlider;
    }

    void Update()
    {

    }

    /*
    public void ReloadedScene()
    {

        foreach (RectTransform childUI in _UI_Interface.transform)
        {
            childUI.gameObject.SetActive(false);
        }

        btnCMW.SetActive(true);
        btns.SetActive(true);
        btnMenu.SetActive(true);
        btnReset.SetActive(true);
        _menuUI.SetActive(true);
        _swipeAnimMenu.LeftSwipe();

        _toturial.SetActive(false);
        _dark.GetComponent<Image>().enabled = false;
        _dark.GetComponent<Animation>().enabled = false;
        points.Clear();
        lineRenderer.positionCount = 0;
        punkDot.positionCount = 0;
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        _UI_Interface.SetActive(false);
        _WRLDP.SetActive(false);
        _StartWallPlace.delayTime = 1f;
        _StartWallPlace.timeStart = 5f;
        _StartWallPlace.ResetPos();
        loaderFilAmount.fillAmount = 0f;

        //delFirtDot = true;
        changePlaneMode = true;
        //ChangePlaneModeDW();
        //sizeSlider.maxValue = maxValueSlider;

        _WRLDP.SetActive(true);

        particlesystem.SetActive(false);
        start = false;
        punktBox.SetActive(true);

        areaSum = 0;
        txt_areaSum.text = "~ " + areaSum.ToString() + " m2";

        rolls = 0;

        txt_rolls.text = rolls.ToString() + " roll";

        price = 0;
        txt_price.text = "$" + price.ToString();

    }
    */
}

