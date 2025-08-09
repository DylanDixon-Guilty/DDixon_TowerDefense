using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TowerPlaceManager : MonoBehaviour
{
    public Camera MainCamera;
    public LayerMask TileLayer;
    public InputAction PlaceTowerAction;

    [SerializeField] private bool isTileSelected;
    [SerializeField] private bool isPlacingTower = false;
    [SerializeField] private GameObject NotEnoughCurrencyText;
    [SerializeField] private float towerPlacementHeightOffset = 0.2f;
    private float lifeTime = 1.5f;
    private Tower tower;
    private GameObject currentTowerPrefabToSpawn;
    private GameObject towerPreview;
    private Vector3 towerPlacementPosition;
    
    void Update()
    {
        if(isPlacingTower)
        {
            Ray ray = MainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if(Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, TileLayer))
            {
                towerPlacementPosition = hitInfo.transform.position + Vector3.up * towerPlacementHeightOffset;
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
    public void StartPlacingTower(GameObject towerPreview)
    {
        if(currentTowerPrefabToSpawn != towerPreview)
        {
            isPlacingTower = true;
            currentTowerPrefabToSpawn = towerPreview;
            tower = towerPreview.GetComponent<Tower>();
        }

        if (tower.TowerCost <= CurrencyManager.CurrentCurrency)
        {
            TowerPurchased(tower.TowerCost);
            if (this.towerPreview == null)
            {
                Destroy(this.towerPreview);
            }
            this.towerPreview = Instantiate(towerPreview);
        }
        else
        {
            isPlacingTower = false;
            NotEnoughCurrencyText.SetActive(true);
            StartCoroutine(HideNotEnoughCurrencyText());
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
            tower.IsTowerPlaced = true;
        }
    }

    /// <summary>
    /// Take away the amount it cost to make this Tower from the player's Currency
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
        NotEnoughCurrencyText.SetActive(false);
    }
}
