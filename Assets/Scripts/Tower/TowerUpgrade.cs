using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TowerUpgrade : MonoBehaviour
{

    [SerializeField] private GameObject UpgradeButton;
    private Tower tower;
    private bool isButtonActive = false;
    private int camRayLength = 100;

    private void Start()
    {
        tower = GetComponentInParent<Tower>();
        UpgradeButton.SetActive(false);
    }

    /// <summary>
    /// When the mouse button is pressed, look for the 11th layer mask
    /// </summary>
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            int layerMask = 1 << 11;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit towerHit, camRayLength, layerMask))
            {
                if(!isButtonActive && tower.IsTowerPlaced)
                {
                    isButtonActive = true;
                    UpgradeButton.SetActive(true);
                }
                else if(isButtonActive && tower.IsTowerPlaced)
                {
                    isButtonActive = false;
                    UpgradeButton.SetActive(false);
                }
            }
        }
    }
}
