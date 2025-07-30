using UnityEngine;
using UnityEngine.InputSystem;

public class TowerPlaceManager : MonoBehaviour
{
    public Camera MainCamera;
    public LayerMask TileLayer;
    public InputAction PlaceTowerAction;

    [SerializeField] private bool isTileSelected;
    [SerializeField] private float towerPlacementHeightOffset = 0.2f;
    [SerializeField] private bool isPlacingTower = false;
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
    public void StartPlacingTower(GameObject towerPrefab)
    {
        if(currentTowerPrefabToSpawn != towerPrefab)
        {
            isPlacingTower = true;
            currentTowerPrefabToSpawn = towerPrefab;
            if(towerPreview != null)
            {
                Destroy(towerPreview);
            }
            towerPreview = Instantiate(towerPrefab);
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
            Instantiate(currentTowerPrefabToSpawn, towerPlacementPosition, Quaternion.identity);
            Destroy(towerPreview);
            currentTowerPrefabToSpawn = null;
            isPlacingTower = false;
        }
    }
}
