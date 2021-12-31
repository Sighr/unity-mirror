using UnityEngine;

public class WinScript : MonoBehaviour
{
    bool isWon = false;
    private void OnTriggerEnter(Collider other)
    {
        if (!isWon)
        {
            isWon = true;
            Win();
        }
    }

    private void Win()
    {
        Debug.Log("Win");
    }
}
