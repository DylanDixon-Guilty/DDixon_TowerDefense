using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TowerPlaceManager : MonoBehaviour
{
    [SerializeField] private bool isTileSelected;
    [SerializeField] private bool isPlacingTower = false;
    [SerializeField] private GameObject notEnoughCurrencyText;
    [SerializeField] private float towerPlacementHeightOffset;
    [SerializeField] private InputAction placeTowerAction;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask tileLayer;
    [SerializeField] private LayerMask upgradeTowerLayer = 1 << 11;
    private float lifeTime = 1.5f;
    private GameObject currentTowerPrefabToSpawn;
    private GameObject towerPreview;
    private Vector3 towerPlacementPosition;

    void Update()
    {
        if(isPlacingTower)
        {
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if(Physics.Raycast(ray, Mathf.Infinity, upgradeTowerLayer))
            {
                towerPreview.SetActive(false);
                isTileSelected = false;
            }
            else if(Physics.Raycast(ray, out RaycastHit hitInfoRay, Mathf.Infinity, tileLayer))
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
        placeTowerAction.Enable();
        placeTowerAction.performed += OnPlaceTower;
    }

    private void OnDisable()
    {
        placeTowerAction.performed -= OnPlaceTower;
        placeTowerAction.Disable();
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
                }
            }
            else
            {
                isPlacingTower = false;
                currentTowerPrefabToSpawn = null;
                notEnoughCurrencyText.SetActive(true);
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
            Tower tower = towerInstance.GetComponent<Tower>();
            if(tower.TowerCost <= CurrencyManager.CurrentCurrency)
            {
                TowerPurchased(tower.TowerCost);
            }
            Destroy(towerPreview);
            tower.IsTowerPlaced = true;
            currentTowerPrefabToSpawn = null;
            isPlacingTower = false;
        }
    }

    /// <summary>
    /// Take away the amount it cost purchase this Tower from the player's Currency
    /// </summary>
    private void TowerPurchased(int cost)
    {
        CurrencyManager.CurrentCurrency -= cost;
    }

    /// <summary>
    /// To hide the NotEnoughCurrency Text after it appears 
    /// </summary>
    IEnumerator HideNotEnoughCurrencyText()
    {
        yield return new WaitForSeconds(lifeTime);
        notEnoughCurrencyText.SetActive(false);
    }
}
