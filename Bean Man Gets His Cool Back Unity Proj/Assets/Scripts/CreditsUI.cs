using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsUI : MonoBehaviour
{
    public Button CloseBTN;

    // Start is called before the first frame update
    private void Start()
    {
        CloseBTN.onClick.AddListener(CreditsCloseClicked);
    }


    private void OnDestroy()
    {
        CloseBTN.onClick.RemoveListener(CreditsCloseClicked);
    }

    private void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                gameObject.SetActive(false);
            }
        }
        
    }
    public void CreditsCloseClicked()
    {
        gameObject.SetActive(false);
    }
}
