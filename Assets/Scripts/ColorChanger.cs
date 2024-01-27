using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public Material changerMaterial, criminalMaterial, nonCriminalMaterial;
    Color lastColor;
    int randomIndex;
    private void Start()
    {
        ChangeMatColor();
    }

    void ChangeMatColor()
    {
        randomIndex = Random.Range(0, 6);
        switch (randomIndex)
        {
            case 0:
                changerMaterial.color = Color.green;
                break;
            case 1:
                changerMaterial.color = Color.blue;
                break;
            case 2:
                changerMaterial.color = Color.yellow;
                break;
            case 3:
                changerMaterial.color = Color.red;
                break;
            case 4:
                changerMaterial.color = Color.cyan;
                break;
            case 5:
                changerMaterial.color = Color.magenta;
                break;
        }
    }

    void NonCriminalColor()
    {
        while (lastColor == criminalMaterial.color)
        {
            lastColor = Random.ColorHSV();
            nonCriminalMaterial.color = lastColor;
        }
        nonCriminalMaterial.color = lastColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            lastColor = criminalMaterial.color;
            criminalMaterial.color = changerMaterial.color;
            Destroy(this.gameObject);
            NonCriminalColor();
            ChangeMatColor();
        }
    }
}
