using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

/// <summary>
/// Listens for touch events and performs an AR raycast from the screen touch point.
/// AR raycasts will only hit detected trackables like feature points and planes.
/// 
/// If a raycast hits a trackable, the <see cref="placedPrefab"/> is instantiated
/// and moved to the hit position.
/// </summary>
[RequireComponent(typeof(ARSessionOrigin))]
public class PlaceOnPlane : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Instantiates this prefab on a plane at the touch location.")]
    GameObject m_PlacedPrefab;

    /// <summary>
    /// The prefab to instantiate on touch.
    /// </summary>
    public GameObject placedPrefab
    {
        get { return m_PlacedPrefab; }
        set { m_PlacedPrefab = value; }
    }

    public GameObject placedPrefabTEST;
    public GameObject point;
    public GameObject focus;

    [Header("Точки для генерации")]
    [Tooltip("Сюда добавляются точки (их координаты) которые ставятся пользователем в реальном мире.")]
    public List<Vector3> points = new List<Vector3>();

    [Tooltip("Здесь собраны все обьекты для того что бы сгенерировать плоскость и управлять растановкой точек в реальном мире.")]
    [Header("Генерация плоскости")]
    public LineRenderer punkDot;
    public LineRenderer lineRenderer;
    private bool delFirtDot;

    [Tooltip("Все для смены материалов на плоскости (так же смена материалов кисти на покраске).")]
    [Header("Смена материалов")]
    public ChangeMaterial _changeMaterial;
    public bool changePlaneMode;
    public SwipeAnim _swipeAnim;
    public SwipeAnimMenu _swipeAnimMenu;

    [Tooltip("Эффект для генерации стены.")]
    [Header("Эффект")]
    public GameObject particlesystem;
    /*
    [Tooltip("Все для роботы с покраской стен.")]
    [Header("Обои (Покраска)")]
    public GameObject planeWallparer;
    public Slider sizeSlider;
    public int maxValueSlider;
    public Button eraserToggle, snaptogridToggle;
    */
    [Tooltip("Управление эддементами интерфейса.")]
    [Header("Интерфейс")]
    public GameObject btn_Generation;
    public GameObject _ToturialUI;
    public GameObject _UIInterface;
    public GameObject _S_WRLD_P;
    public Image loaderProgress;
    private bool restartBool;

    public GameObject _TextBox_1, _TextBox_2, _TextBox_3, _TextBox_4;
    private bool first_Box_1, first_Box_2, first_Box_3, first_Box_4;

    private bool first_Toturial;

    private bool minusDotBox;
    private bool endToturial;

    [Tooltip("Маштаб и цена созданой плоскости")]
    [Header("Маштаб и цена")]
    private float areaSum;
    private float price;
    private float rolls;
    public float priceRoll;
    public Text txt_areaSum, txt_price, txt_rolls;
    private bool start;
    public GameObject punktBox;

    public float xyArea;

    [Tooltip("Скрипты которые используются.")]
    [Header("Доп. скрипты")]
    public StartWallPlace _StartWallPlace;
    public RayCam _RayCam;

    /// <summary>
    /// The object instantiated as a result of a successful raycast intersection with a plane.
    /// </summary>
    public GameObject spawnedObject { get; private set; }

    void Start()
    {
        delFirtDot = true;
        changePlaneMode = true;
        //firstStart = true;
        start = false;
        restartBool = false;

        first_Toturial = true;

        first_Box_1 = false;
        first_Box_2 = false;
        first_Box_3 = false;
        first_Box_4 = false;
        minusDotBox = false;
        endToturial = false;
    }

    //Start m2 площадь 
    public float AreaOfTriangle(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float a = Vector3.Distance(p1, p2);
        float b = Vector3.Distance(p2, p3);
        float c = Vector3.Distance(p3, p1);
        float p = 0.5f * (a + b + c);
        float s = Mathf.Sqrt(p * (p - a) * (p - b) * (p - c));
        return s;
    }

    public float AreaOfMesh(Mesh mesh)
    {
        float area = 0f;

        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;

        for (int i = 0; i < mesh.triangles.Length; i += 3)
        {
            Vector3 p1 = vertices[triangles[i + 0]];
            Vector3 p2 = vertices[triangles[i + 1]];
            Vector3 p3 = vertices[triangles[i + 2]];
            area += AreaOfTriangle(p1, p2, p3);
        }

        return area;
    }
    //End m2



    public void Generation()
    {
        if (points.Count >= 3)
        {

            points.Add(points[0]);

            lineRenderer.positionCount += 1;
            lineRenderer.SetPosition(transform.childCount - 1, lineRenderer.GetPosition(0));

            Poly2Mesh.Polygon poly = new Poly2Mesh.Polygon();
            poly.outside = points;

            // Set up game object with mesh;

            GameObject gob = new GameObject();
            gob.name = "Polygon";
            gob.tag = "Polygon";
            gob.AddComponent(typeof(MeshRenderer));
            MeshFilter filter = gob.AddComponent(typeof(MeshFilter)) as MeshFilter;

            GameObject polyMesh = Poly2Mesh.CreateGameObject(poly);
            filter.mesh = polyMesh.GetComponent<MeshFilter>().mesh;

            if (filter != null)
            {
                Mesh mesh2 = filter.mesh;

                Vector3[] normals = mesh2.normals;
                for (int i = 0; i < normals.Length; i++)
                    normals[i] = -normals[i];
                mesh2.normals = normals;

                for (int m = 0; m < mesh2.subMeshCount; m++)
                {
                    int[] triangles = mesh2.GetTriangles(m);
                    for (int i = 0; i < triangles.Length; i += 3)
                    {
                        int temp = triangles[i + 0];
                        triangles[i + 0] = triangles[i + 1];
                        triangles[i + 1] = temp;
                    }
                    mesh2.SetTriangles(triangles, m);
                }
            }

            //gob.AddComponent<BoxCollider>().size = new Vector3(1f, 1f, 0.001f);

            //m2
            areaSum = AreaOfMesh(polyMesh.GetComponent<MeshFilter>().sharedMesh);
            xyArea = areaSum;
            txt_areaSum.text = "~ " + areaSum.ToString("#.##") + " m2";
            if (areaSum < 1)
            {
                txt_areaSum.text = "~ 0" + areaSum.ToString("#.##") + " m2";
            }
            _changeMaterial.work = true;
            start = true;

            lineRenderer.positionCount = 0;
            punkDot.positionCount = 0;
            ClearAllDots();
            _changeMaterial.Material1();
            _swipeAnim.UpSwipe();


            first_Toturial = false;

            _TextBox_3.SetActive(false);
            _TextBox_1.SetActive(false);
            _TextBox_2.SetActive(false);
            _TextBox_4.SetActive(false);

            first_Box_1 = false;
            first_Box_2 = false;
            first_Box_3 = false;
            first_Box_4 = false;

            _ToturialUI.SetActive(false);

        }
    }
    void Awake()
    {
        m_SessionOrigin = GetComponent<ARSessionOrigin>();
    }

    public void PlaceOBJ()
    {
        Vector3 curentEularAngel = Camera.main.transform.eulerAngles;
        Camera.main.transform.eulerAngles = new Vector3(90f, GameObject.FindGameObjectWithTag("MainCamera").transform.eulerAngles.y, GameObject.FindGameObjectWithTag("MainCamera").transform.eulerAngles.z);
        Instantiate(placedPrefabTEST, GameObject.FindGameObjectWithTag("MainCamera").transform.position, GameObject.FindGameObjectWithTag("MainCamera").transform.rotation);
        Camera.main.transform.eulerAngles = curentEularAngel;

        PlacePS();

        focus.SetActive(true);

        if (first_Toturial == true)
        {
            first_Box_2 = true;
        }
    }

    public void PlacePS()
    {
        Vector3 curentEularAngel = Camera.main.transform.eulerAngles;
        Camera.main.transform.eulerAngles = new Vector3(90f, GameObject.FindGameObjectWithTag("MainCamera").transform.eulerAngles.y, GameObject.FindGameObjectWithTag("MainCamera").transform.eulerAngles.z);
        Instantiate(particlesystem, GameObject.FindGameObjectWithTag("MainCamera").transform.position, GameObject.FindGameObjectWithTag("MainCamera").transform.rotation);
        Camera.main.transform.eulerAngles = curentEularAngel;

        GameObject gos;
        gos = GameObject.FindGameObjectWithTag("particlesystem");
        gos.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void AddPoint()
    {
        Instantiate(point, focus.transform.position, focus.transform.rotation);

        points.Add(focus.transform.position);
        lineRenderer.positionCount += 1;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, focus.transform.position);

        if (first_Toturial == true && minusDotBox == false)
        {
            first_Box_3 = true;
        }

    }

    public void EnablePS()
    {
        GameObject gos;
        gos = GameObject.FindGameObjectWithTag("particlesystem");
        gos.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void Restart()
    {
        restartBool = true;

        GameObject[] gosPS;
        gosPS = GameObject.FindGameObjectsWithTag("particlesystem");
        foreach (GameObject goPS in gosPS)
        {
            Destroy(goPS);
        }

        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Polygon");
        foreach (GameObject go in gos)
        {
            Destroy(go);
        }

        GameObject[] gosPFD;
        gosPFD = GameObject.FindGameObjectsWithTag("ARPlacedPlaneForDot");
        foreach (GameObject goPFD in gosPFD)
        {
            Destroy(goPFD);
        }

        ClearAllDots();

        lineRenderer.positionCount = 0;
        punkDot.positionCount = 0;

        _UIInterface.SetActive(false);
        _StartWallPlace.ResetPos();
        _S_WRLD_P.SetActive(true);
        loaderProgress.fillAmount = 0f;

        areaSum = 0;
        txt_areaSum.text = "~ " + areaSum.ToString() + " m2";

        rolls = 0;

        txt_rolls.text = rolls.ToString() + " roll";

        price = 0;
        txt_price.text = "$" + price.ToString();


        first_Toturial = false;

        _TextBox_3.SetActive(false);
        _TextBox_1.SetActive(false);
        _TextBox_2.SetActive(false);
        _TextBox_4.SetActive(false);

        first_Box_1 = false;
        first_Box_2 = false;
        first_Box_3 = false;
        first_Box_4 = false;

        _ToturialUI.SetActive(false);
        focus.SetActive(false);
    }

    public void ClearAllDots()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Point");
        foreach (GameObject go in gos)
        {
            Destroy(go);
        }
        points.Clear();
    }

    void Update()
    {

        if (points.Count >= 3)
        {
            btn_Generation.SetActive(true);
            if(endToturial == false)
            {
                first_Box_4 = true;
            }

        }
        if (points.Count < 3)
        {
            btn_Generation.SetActive(false);
        }

        //m2
        if (_changeMaterial.work && start)
        {
            rolls = Mathf.Ceil(areaSum / 3);

            if (rolls < 2)
            {
                rolls = 1;

                txt_rolls.text = rolls.ToString() + " roll";
            }
            if (rolls >= 2)
            {
                txt_rolls.text = rolls.ToString() + " rolls";
            }

            price = priceRoll * rolls;
            txt_price.text = "$" + price.ToString();
            _changeMaterial.work = false;
        }

        if(lineRenderer.positionCount == 1)
        {
            punkDot.positionCount = 1;
            punkDot.SetPosition(0, lineRenderer.GetPosition(0));
        }

        if(lineRenderer.positionCount >= 2)
        {
            punkDot.positionCount = 2;
            punkDot.SetPosition(1, lineRenderer.GetPosition(lineRenderer.positionCount - 1));
        }
        if(lineRenderer.positionCount == 0)
        {
            punkDot.positionCount = 0;
        }
        if (first_Toturial == true)
        {

            if(first_Box_2 == true)
            {
                _TextBox_2.SetActive(true);

                _TextBox_1.SetActive(false);
                _TextBox_3.SetActive(false);
                _TextBox_4.SetActive(false);

                first_Box_2 = false;
            }

            if (first_Box_3 == true)
            {
                _TextBox_3.SetActive(true);

                _TextBox_1.SetActive(false);
                _TextBox_2.SetActive(false);
                _TextBox_4.SetActive(false);
                minusDotBox = true;
            }
            if (first_Box_4 == true)
            {
                _TextBox_4.SetActive(true);

                _TextBox_1.SetActive(false);
                _TextBox_2.SetActive(false);
                _TextBox_3.SetActive(false);
                endToturial = true;
            }

            if(points.Count > 1 && minusDotBox == true && first_Box_3 == true || points.Count == 0 && minusDotBox == true && first_Box_3 == true)
            {
                _TextBox_3.SetActive(false);
                first_Box_3 = false;
            }

            if (points.Count > 3 && endToturial == true && first_Box_4 == true || points.Count < 3 && endToturial == true && first_Box_4 == true)
            {
                _TextBox_4.SetActive(false);
                first_Box_4 = false;
            }
        }
        if (Input.touchCount == 0)
            return;

        var touch = Input.GetTouch(0);

        if (m_SessionOrigin.Raycast(touch.position, s_Hits, TrackableType.PlaneWithinPolygon))
        {
            // Raycast hits are sorted by distance, so the first one
            // will be the closest hit.
            var hitPose = s_Hits[0].pose;
            /*
            if (spawnedObject == null)
            {
                spawnedObject = Instantiate(m_PlacedPrefab, hitPose.position, hitPose.rotation);
            }
            else
            {
                spawnedObject.transform.position = hitPose.position;
            }
            */
        }
    }

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    ARSessionOrigin m_SessionOrigin;
}
