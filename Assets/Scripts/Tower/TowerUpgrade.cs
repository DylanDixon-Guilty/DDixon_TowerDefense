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
    private BoxCollider buttonBoxCollider;
    private BoxCollider TowerboxCollider;
    private bool isButtonActive = false;
    private int camRayLength = 100;
    private int layerMask11 = 11;

    private void Start()
    {
        tower = GetComponentInParent<Tower>();
        TowerboxCollider = GetComponent<BoxCollider>();
        buttonBoxCollider = GetComponentInChildren<BoxCollider>();
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
    private void UpgradeButtonVisible() //TODO: fix bug of not being able to click Button
    {
        if (Input.GetMouseButtonDown(0))
        {
            int layerMask = 1 << layerMask11;
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitInfo, camRayLength, layerMask))
            {
                if (hitInfo.collider == TowerboxCollider && !isButtonActive)
                {
                    isButtonActive = true;
                    UpgradeButton.SetActive(true);
                }
                else if (hitInfo.collider == TowerboxCollider && isButtonActive)
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
