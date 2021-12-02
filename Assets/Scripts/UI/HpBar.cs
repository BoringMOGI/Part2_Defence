using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// hp : hit point.
// hp : health point.
public interface IHealth
{
    bool IsAlive();
    float GetCurrentHP();
    float GetMaxHp();
    Vector3 GetPosition();
}

public class HpBar : MonoBehaviour
{
    [SerializeField] Image hpImage;


    IHealth target;

    public void Setup(IHealth target)           // ü�¹� ����.
    {
        this.target = target;                   // �������̽� ����.
    }

    private void Update()
    {
        if (target.IsAlive())
        {
            hpImage.fillAmount = target.GetCurrentHP() / target.GetMaxHp();             // ü�¹��� fillAmount ����.
            transform.position = Camera.main.WorldToScreenPoint(target.GetPosition());  // Ÿ���� ��ġ(���� ��ǥ) -> UI�� ��ġ(��ũ�� ��ǥ)
        }
        else
            Destroy(gameObject);                                                        // Ÿ���� ������� �� �� ������Ʈ ����.
    }
}
