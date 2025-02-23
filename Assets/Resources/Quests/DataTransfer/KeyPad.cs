using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class KeyPad : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI codeText;
    [SerializeField] private Button submitButton;
    [SerializeField] private TextMeshProUGUI buttonText;

	private string codeInput = "";
    private string code = "";
    private int randomNumber;
    private int presses = 0;
    private bool resetTimer = false;
    private float timer = 2;


	void Start()
    {
		for (int i = 0; i < 8; i++)
        {
            randomNumber = Random.Range(1, 9);
            code = code + randomNumber.ToString();
        }
        codeText.text = "CODE: " + code;

        Debug.Log(code);
    }

    void Update()
    {
        if (resetTimer == true)
        {
            timer = timer - Time.deltaTime;
            if (timer < 0)
            {
                ResetButtonEnd();
            }
        }
    }

    public void ButtonPressed(int number)
    {
        presses++;
        if (presses == 9)
        {
            presses = 0;
            codeInput = "";
			ResetButtonStart();
		} else
        {
			codeInput = codeInput + number.ToString();
		}
	}

    public void SubmitPressed()
    {
        if (code == codeInput)
        {
            EventManager.Instance.taskEvents.KeypadCodeCorrect();
        } else
        {
			presses = 0;
			codeInput = "";
			ResetButtonStart();
		}
    }

    private void ResetButtonStart()
    {
		submitButton.image.color = Color.red;
		buttonText.text = "RESET";
		resetTimer = true;
	}

	private void ResetButtonEnd()
	{
		submitButton.GetComponent<Image>().color = new Color32(120, 185, 80, 255);
		buttonText.text = "Submit";
		resetTimer = false;
		timer = 2;
	}
}
