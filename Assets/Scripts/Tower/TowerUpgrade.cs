using TMPro;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TowerUpgrade : MonoBehaviour
{
    public int CostToUpgrade; //The cost to Upgrade a Tower
    public GameObject levelTwoTower;

    [SerializeField] private GameObject UpgradeButton;
    [SerializeField] private TextMeshProUGUI upgradeButtonText;
    private Tower tower;
    private BoxCollider buttonBoxCollider;
    private BoxCollider towerboxCollider;
    private bool isButtonActive = false;
    private int camRayLength = 100;
    private int layerMask11 = 11;

    private void Start()
    {
        tower = GetComponentInParent<Tower>();
        towerboxCollider = GetComponent<BoxCollider>();
        buttonBoxCollider = GetComponentInChildren<BoxCollider>();
        UpgradeButton.SetActive(false);
    }

    private void Update()
    {
        upgradeButtonText.text = "Upgrade: " + CostToUpgrade;
        UpgradeButtonVisible();
    }

    /// <summary>
    /// When the mouse button is pressed, look for the 11th layer mask.
    /// Then if the mouse is on an object with the 11th layer and hits the Tower's Box Collider, show the Upgrade button above the Tower selected.
    /// If the mouse is on the 11th layer and hits the UpgradeButton's Box Collider, upgrade the Tower (if they have enough Currency)
    /// </summary>
    private void UpgradeButtonVisible()
    {
        if (Input.GetMouseButtonDown(0))
        {
            int layerMask = 1 << layerMask11;
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitInfo, camRayLength, layerMask))
            {
                if (hitInfo.collider == towerboxCollider && !isButtonActive)
                {
                    isButtonActive = true;
                    UpgradeButton.SetActive(true);
                }
                else if (hitInfo.collider == towerboxCollider && isButtonActive)
                {
                    isButtonActive = false;
                    UpgradeButton.SetActive(false);
                }
                else if (hitInfo.collider == buttonBoxCollider && isButtonActive)
                {
                    tower.UpgradeTower();
                }
            }
        }
    }

}
