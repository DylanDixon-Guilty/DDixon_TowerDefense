using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TowerPlaceManager : MonoBehaviour
{
    [SerializeField] private bool isTileSelected;
    [SerializeField] private bool isPlacingTower = false;
    [SerializeField] private GameObject NotEnoughCurrencyText;
    [SerializeField] private float towerPlacementHeightOffset;
    [SerializeField] private InputAction PlaceTowerAction;
    [SerializeField] private Camera MainCamera;
    [SerializeField] private LayerMask TileLayer;
    [SerializeField] private LayerMask towerPreviewLayer = 1 << 12;
    [SerializeField] private LayerMask upgradeTowerLayer = 1 << 11;
    private float lifeTime = 1.5f;
    private GameObject currentTowerPrefabToSpawn;
    private GameObject towerPreview;
    private Vector3 towerPlacementPosition;

    void Update()
    {
        if(isPlacingTower)
        {
            Ray ray = MainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if(Physics.Raycast(ray, Mathf.Infinity, upgradeTowerLayer))
            {
                towerPreview.SetActive(false);
                isTileSelected = false;
            }
            else if(Physics.Raycast(ray, out RaycastHit hitInfoRay, Mathf.Infinity, TileLayer))
            {
                towerPlacementPosition = hitInfoRay.transform.position + Vector3.up * towerPlacementHeightOffset;
                towerPreview.transform.position = towerPlacementPosition;
                towerPreview.SetActive(true);
                isTileSelected = true;
            }
            else
            {
                towerPreview.SetActive(false);
                isTileSelected = false;
            }
        }
    }

    private void OnEnable()
    {
        PlaceTowerAction.Enable();
        PlaceTowerAction.performed += OnPlaceTower;
    }

    private void OnDisable()
    {
        PlaceTowerAction.performed -= OnPlaceTower;
        PlaceTowerAction.Disable();
    }

    /// <summary>
    /// When the player has clicked on a Tower button, have a Preview of the Tower appear when the player hovers the mouse over a grid
    /// </summary>
    public void StartPlacingTower(GameObject towerPrefab)
    {
        if(currentTowerPrefabToSpawn != towerPrefab)
        {
            isPlacingTower = true;
            currentTowerPrefabToSpawn = towerPrefab;
            
            Tower tower = towerPrefab.GetComponent<Tower>();
            if (tower.TowerCost <= CurrencyManager.CurrentCurrency)
            {
                if (this.towerPreview != null)
                {
                    Destroy(this.towerPreview);
                }
                else
                {
                    this.towerPreview = Instantiate(towerPrefab);
                    this.towerPreview.GetComponentInChildren<BoxCollider>().enabled = false;
                    this.towerPreview.layer = towerPreviewLayer;
                }
            }
            else
            {
                isPlacingTower = false;
                currentTowerPrefabToSpawn = null;
                NotEnoughCurrencyText.SetActive(true);
                StartCoroutine(HideNotEnoughCurrencyText());
            }
        }
    }

    /// <summary>
    /// When the player presses the Left Mouse button and isPlacingTower is true, place a Tower on the current Grid the mouse is on.
    /// Do not place Tower if the player's mouse is either off the platform or on the Path
    /// </summary>
    private void OnPlaceTower(InputAction.CallbackContext context)
    {
        if(isPlacingTower && isTileSelected)
        {
            GameObject towerInstance = Instantiate(currentTowerPrefabToSpawn, towerPlacementPosition, Quaternion.identity);
            Destroy(towerPreview);
            currentTowerPrefabToSpawn = null;
            isPlacingTower = false;
            Tower tower = towerInstance.GetComponent<Tower>();
            CurrencyManager currencyManager = GetComponent<CurrencyManager>();
            if(tower.TowerCost <= CurrencyManager.CurrentCurrency)
            {
                currencyManager.CurrencySpent(tower.TowerCost);
            }
            tower.IsTowerPlaced = true;
        }
    }

    /// <summary>
    /// To hide the NotEnoughCurrency Text after it appears 
    /// </summary>
    IEnumerator HideNotEnoughCurrencyText()
    {
        yield return new WaitForSeconds(lifeTime);
        NotEnoughCurrencyText.SetActive(false);
    }
}
