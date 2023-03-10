using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    public GameObject standardTurretPrefab;

    private GameObject turretToBuild;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        turretToBuild = standardTurretPrefab;
    }

    /*// Update is called once per frame
    void Update()
    {
        
    }*/

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }
}
