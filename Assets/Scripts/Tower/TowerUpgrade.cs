using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TowerUpgrade : MonoBehaviour
{

    [SerializeField] private GameObject UpgradeButton;
    private bool isButtonActive = false;
    private int camRayLength = 100;

    private void Start()
    {
        UpgradeButton.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            int layerMask = 1 << 11;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit towerHit, camRayLength, layerMask))
            {
                if(!isButtonActive)
                {
                    isButtonActive = true;
                    UpgradeButton.SetActive(true);
                }
            }
            else if(isButtonActive)
            {
                isButtonActive = false;
                UpgradeButton.SetActive(false);
            }
        }
    }
}
