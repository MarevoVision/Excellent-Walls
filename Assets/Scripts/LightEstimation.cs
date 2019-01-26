using UnityEngine;
using UnityEngine.XR.ARFoundation;

/// <summary>
/// A component that can be used to access the most
/// recently received light estimation information
/// for the physical environment as observed by an
/// AR device.
/// </summary>
[RequireComponent(typeof(Light))]
public class LightEstimation : MonoBehaviour
{
    /// <summary>
    /// The estimated brightness of the physical environment, if available.
    /// </summary>
    public float? brightness { get; private set; }

    /// <summary>
    /// The estimated color temperature of the physical environment, if available.
    /// </summary>
    public float? colorTemperature { get; private set; }

    /// <summary>
    /// The estimated color correction value of the physical environment, if available.
    /// </summary>
    public Color? colorCorrection { get; private set; }

    public GameObject warrningText, canvasUI;

    public float startTime;
    private float time;
    private bool work;

    public void Start()
    {
        time = startTime;
        work = true;
    }

    void Awake ()
    {
        m_Light = GetComponent<Light>();
    }

    void OnEnable()
    {
        ARSubsystemManager.cameraFrameReceived += FrameChanged;
    }

    void OnDisable()
    {
        ARSubsystemManager.cameraFrameReceived -= FrameChanged;
    }

    private void Update()
    {
        if (brightness < 200f && time > 0)
        {
            time = time - Time.deltaTime;
        }
        if (time <= 0 && work == true)
        {
            warrningText.SetActive(true);
            canvasUI.SetActive(false);
            work = false;
        }
    }

    void FrameChanged(ARCameraFrameEventArgs args)
    {
        if (args.lightEstimation.averageBrightness.HasValue)
        {
            brightness = args.lightEstimation.averageBrightness.Value;
            m_Light.intensity = brightness.Value;

        }

        if (args.lightEstimation.averageColorTemperature.HasValue)
        {
            colorTemperature = args.lightEstimation.averageColorTemperature.Value;
            m_Light.colorTemperature = colorTemperature.Value;
        }
        
        if (args.lightEstimation.colorCorrection.HasValue)
        {
            colorCorrection = args.lightEstimation.colorCorrection.Value;
            m_Light.color = colorCorrection.Value;
        }
    }

    Light m_Light;
}
