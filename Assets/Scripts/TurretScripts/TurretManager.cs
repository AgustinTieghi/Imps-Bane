using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    public bool selected = false;    
    private Camera myCamera;
    private Ray cameraRay;
    private RaycastHit hit;
    private Vector3 mousePosition;
    private int sensorLayerMask;
    private int selectLayerMask;

    [Header("Tower Settings")]
    [SerializeField] private Color transparentTower;
    [SerializeField] private GameObject previewTower;
    [SerializeField] private List<GameObject> turretList;
    [SerializeField] private List<TurretScript> turretScriptList;
    [SerializeField] private List<GameObject> upgradedTowers;
    public TurretScript selectedTurretScript;
    public int turretCost;
    public int upgradeCost;
    public int index = 0;
    public bool constructMode = false;
    public GameObject selectedTurret;

    public ManagementScript manager;


    void Start()
    {
        //This makes the layerMask interact with everything except for the layer 7
        sensorLayerMask = 1 << 7;
        sensorLayerMask = ~sensorLayerMask;

        //This makes the layerMask interact only with layer 9
        selectLayerMask = 1 << 9;

        myCamera = Camera.main;
        transparentTower = previewTower.GetComponent<Renderer>().material.color;

        foreach (GameObject t in turretList)
        {
            TurretScript aux = t.GetComponent<TurretScript>();
            turretScriptList.Add(aux);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ActivateConstructMode();
        }

        if (constructMode)
        {
            UpdatePreviewRenderer();
            UpdateTurretCost();

            if (Input.GetKeyDown(KeyCode.E))
            {
                ChangeSelectedTurret();
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (manager.money >= turretCost)
                {
                    Construct();
                }
            }
        }
        else if (constructMode == false)
        {
            previewTower.gameObject.SetActive(false);

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Select();
            }
        }


        if (selected)
        {
            if (manager.money >= upgradeCost && selectedTurretScript.upgradable && Input.GetKeyDown(KeyCode.A))
            {
                Upgrade(selectedTurretScript);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                Sell(selectedTurretScript);
            }
        }
    }
    private void ActivateConstructMode()
    {
        constructMode = !constructMode;
        previewTower.gameObject.SetActive(true);
    }
    private void ChangeSelectedTurret()
    {
        index++;

        if (index > turretList.Count - 1)
        {
            index = 0;
        }
        
    }
    private void UpdateTurretCost()
    {
        turretCost = turretScriptList[index].cost;      
    }

    private void Construct()
    {
        cameraRay = myCamera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(cameraRay, out hit, 1000, sensorLayerMask, QueryTriggerInteraction.Ignore))
        {
            Vector3 spawnPosition = hit.point + (hit.normal /** offset*/);
            if (hit.collider.gameObject.layer != 6)
            {
                GameObject spawnedTurret = Instantiate(turretList[index], spawnPosition, Quaternion.FromToRotation(Vector3.up, hit.normal));
                TurretScript turretScript = spawnedTurret.GetComponent<TurretScript>();
                manager.money -= turretScript.cost;
            }
        }
    }

    private void UpdatePreviewRenderer()
    {
        mousePosition = Input.mousePosition;

        cameraRay = myCamera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(cameraRay, out hit, 100, sensorLayerMask, QueryTriggerInteraction.Ignore))
        {
            if (hit.collider.gameObject.layer == 6)
            {
                previewTower.GetComponent<Renderer>().material.color = Color.red;
            }
            else
            {
                previewTower.GetComponent<Renderer>().material.color = transparentTower;
            }
            Vector3 spawnPosition = hit.point + (hit.normal /** offset*/);
            previewTower.transform.position = spawnPosition;
        }
    }

    private void Select()
    {
        mousePosition = Input.mousePosition;
        cameraRay = myCamera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(cameraRay, out hit, 1000, selectLayerMask))
        {
            if (selectedTurret != null)
            {
                Deselect();
            }
            selectedTurretScript = hit.collider.gameObject.GetComponent<TurretScript>();
            selectedTurretScript.selected.SetActive(true);
            selected = true;
            selectedTurret = hit.collider.gameObject;
        }
        else
        {
            if (selectedTurret != null)
            {
                Deselect();
            }
        }
    }

    private void Upgrade(TurretScript inputSelectedTower)
    {
        int index;
        switch (inputSelectedTower.type)
        {
            case Turret.Type.Cannon:
                index = 0;
                break;
            case Turret.Type.Exploding:
                index = 1;
                break;
            case Turret.Type.Ice:
                index = 2;
                break;
            default:
                index = 0;
                break;
        }
        Instantiate(upgradedTowers[index], inputSelectedTower.transform.position, Quaternion.identity);
        manager.money -= upgradeCost;
        Destroy(selectedTurret);
    }

    private void Deselect()
    {
        selectedTurret.gameObject.GetComponent<TurretScript>().selected.SetActive(false);
        selected = false;
        selectedTurret = null;
        selectedTurretScript = null;
    }
    private void Sell(TurretScript inputSelectedTower)
    {        
        Destroy(inputSelectedTower.gameObject);
        manager.money += inputSelectedTower.sellValue;
    }
}
