using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartWallPlace : MonoBehaviour
{
    public GraphicRaycaster _graphicRaycaster;
    public PlaceOnPlane _placeOnPlane;
    public Image loader;

    public Animation animComplate;
    public Animation wRLDAnim;
    public Animation loaderAnim;

    private bool work;
    private bool playStartAnim;

    //timer
    private float time;
    public float timeStart;
    public float delayTime = 1f;

    // Use this for initialization
    void Start()
    {
        work = true;
        playStartAnim = true;
        _graphicRaycaster.enabled = false;
        time = timeStart;
    }

    public void ResetPos()
    {
        work = true;
        playStartAnim = true;
        _graphicRaycaster.enabled = false;
        time = timeStart;
    }

    // Update is called once per frame
    void Update()
    {
        if (work)
        {
            if (Camera.main.transform.rotation.eulerAngles.x > 85f && Camera.main.transform.rotation.eulerAngles.x < 95f)
            {
                if (time > 0)
                {
                    if (delayTime <= 0)
                    {
                        time = time - Time.deltaTime;
                        loader.fillAmount += Time.deltaTime / timeStart;
                        if (playStartAnim)
                        {
                            wRLDAnim.Play("anim_HideElement");
                            loaderAnim.Play("LoaderAnimPrew");
                            playStartAnim = false;
                        }
                    }
                    if (delayTime >= 0)
                    {
                        delayTime = delayTime - Time.deltaTime;
                    }

                }
                if (time <= 0)
                {
                    loaderAnim.Play("LoaderAnim");
                    animComplate.Play();
                    _placeOnPlane.PlaceOBJ();
                    _graphicRaycaster.enabled = true;
                    if (!animComplate.isPlaying)
                    {
                        loader.gameObject.SetActive(false);
                    }

                    work = false;
                }

            }
        }


    }

}
