using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] GameObject _can1, _can2, _can3;
   
    public void CanKontrol(int KalanHak)
    {
        switch (KalanHak)
        {
            case 3:
                _can1.SetActive(true);
                _can2.SetActive(true);
                _can3.SetActive(true);
                break;
            case 2:
                _can1.SetActive(true);
                _can2.SetActive(true);
                _can3.SetActive(false);
                break;
            case 1:
                _can1.SetActive(true);
                _can2.SetActive(false);
                _can3.SetActive(false);
                break;
            case 0:
                _can1.SetActive(false);
                _can2.SetActive(false);
                _can3.SetActive(false);
                break;

        }
    }
}
