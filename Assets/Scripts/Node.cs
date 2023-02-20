using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hoverColor;

    private GameObject turret;
    public Vector3 positionOffset;

    private Renderer rend;
    private Color startColor;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    private void OnMouseEnter()
    {
        rend.material.color = hoverColor;
    }

    private void OnMouseDown()
    {
        if (turret != null)
        {
            Debug.LogError("Can't build there! - TODO: Display on screen.");
            return;
        }

        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
        turret = Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
