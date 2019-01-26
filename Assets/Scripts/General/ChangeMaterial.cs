using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeMaterial : MonoBehaviour {

    public Material mat1, mat2, mat3, mat4;
    public Material UseNow;
    public int usenowNumber;
    public PlaceOnPlane _PlaceOnPlane;
    public bool work;
    // Use this for initialization
    void Start()
    {
        Material1();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Material1()
    {
        work = true;
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Polygon");
        foreach (GameObject go in gos)
        {
            go.GetComponent<MeshRenderer>().material = mat1;
        }
        UseNow = mat1;
        usenowNumber = 1;
        _PlaceOnPlane.priceRoll = 10;
        //if (_PlaceOnPlane.changePlaneMode == false)
        //{
        //    spawnPolygonQuadWallpaper.transform.GetChild(0).GetComponent<VoxelMap>().fillTypeIndex = usenowNumber;
        //}
    }

    public void Material2()
    {
        work = true;
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Polygon");
        foreach (GameObject go in gos)
        {
            go.GetComponent<MeshRenderer>().material = mat2;
        }
        UseNow = mat2;
        usenowNumber = 2;
        _PlaceOnPlane.priceRoll = 12;
        //if (_PlaceOnPlane.changePlaneMode == false)
        //{
        //    spawnPolygonQuadWallpaper.transform.GetChild(0).GetComponent<VoxelMap>().fillTypeIndex = usenowNumber;
        //}
    }

    public void Material3()
    {
        work = true;
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Polygon");
        foreach (GameObject go in gos)
        {
            go.GetComponent<MeshRenderer>().material = mat3;
        }
        UseNow = mat3;
        usenowNumber = 3;
        _PlaceOnPlane.priceRoll = 14;
        //if (_PlaceOnPlane.changePlaneMode == false)
        //{
        //    spawnPolygonQuadWallpaper.transform.GetChild(0).GetComponent<VoxelMap>().fillTypeIndex = usenowNumber;
        //}
    }

    public void Material4()
    {
        work = true;
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Polygon");
        foreach (GameObject go in gos)
        {
            go.GetComponent<MeshRenderer>().material = mat4;
        }
        UseNow = mat4;
        usenowNumber = 4;
        _PlaceOnPlane.priceRoll = 15;
        //if (_PlaceOnPlane.changePlaneMode == false)
        //{
        //    spawnPolygonQuadWallpaper.transform.GetChild(0).GetComponent<VoxelMap>().fillTypeIndex = usenowNumber;
        //}
    }
}
