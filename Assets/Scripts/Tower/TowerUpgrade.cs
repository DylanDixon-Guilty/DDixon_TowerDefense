using TMPro;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TowerUpgrade : MonoBehaviour
{
    public int costToUpgrade; //The cost to Upgrade a Tower
    public GameObject levelTwoTower;

    [SerializeField] private GameObject UpgradeButton;
    [SerializeField] private TextMeshProUGUI upgradeButtonText;
    private Tower tower;
    private bool isButtonActive = false;
    private int camRayLength = 100;

    private void Start()
    {
        tower = GetComponentInParent<Tower>();
        UpgradeButton.SetActive(false);
    }

    
    private void Update()
    {
        upgradeButtonText.text = "Upgrade: " + costToUpgrade;
        UpgradeButtonVisible();
    }

    /// <summary>
    /// When the mouse button is pressed, look for the 11th layer mask.
    /// Then if that is true, show the Upgrade button above the Tower selected
    /// </summary>
    private void UpgradeButtonVisible()
    {
        if (Input.GetMouseButtonDown(0))
        {
            int layerMask = 1 << 11;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit towerHit, camRayLength, layerMask))
            {
                if (!isButtonActive && tower.IsTowerPlaced)
                {
                    isButtonActive = true;
                    UpgradeButton.SetActive(true);
                }
                else if (isButtonActive && tower.IsTowerPlaced)
                {
                    isButtonActive = false;
                    UpgradeButton.SetActive(false);
                }
            }
        }
    }
}
