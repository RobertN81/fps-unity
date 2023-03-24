using TMPro;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public TMP_Text nickName;
    public TMP_Text HPBar;
    private Transform mainCam;

    void Start()
    {
        mainCam = Camera.main.transform;

        if (PlayerPrefs.HasKey("Nickname"))
        {
            nickName.text = PlayerPrefs.GetString("Nickname");
        }
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + mainCam.rotation * Vector3.forward, mainCam.rotation * Vector3.up);
    }
}