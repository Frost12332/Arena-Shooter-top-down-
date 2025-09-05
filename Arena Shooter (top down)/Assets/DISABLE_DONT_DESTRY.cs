using UnityEngine;

public class DISABLE_DONT_DESTRY : MonoBehaviour
{
    private void Start()
    {
        GameObject.Find("ProjectContext(Clone)").SetActive(false);
    }
}
