using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Coins : MonoBehaviour
{
    // UI
    public Text coinNumber;
    public Text question;
    public Text timerText;
    public Text answerCorrect;
    public Text questionsDoneText;
    public InputField answerField;

    // Nummers
    int number1;
    int number2;
    int plusOrMinus;
    int answer = 0;
    int questionsDone;
    float timer;

    void Start() 
    {
        // Bij opstart maakt het spel een vraag aan en wordt de timer op 60 seconde gezet
        GenerateQuestion();
        timer = 60;
    }

    void Update() 
    {
        // Bij elke frame...
        coinNumber.text = "Coins: " + MenuManager.coins.ToString(); // wordt de coinnumber tekst bijgewerkt
        timerText.text = "Time: " + timer.ToString("F0"); // wordt de tijd aangepast. de "F0" zorgt ervoor dat het wordt afgerond
        questionsDoneText.text = questionsDone + "/3"; // wordt de text aangepast voor hoeveel vragen je hebt gedaan.
        
        if (timer > 0) // als timer meer dan 0 is...
        {
            timer -= Time.deltaTime; // gaat de timer naar beneden
        }

        if (timer < 1) // als de timer minder dan 1 is...
        {
            timer = 60; // wordt de timer op 60 gezet
            questionsDone = 0; // questionsDone wordt weer gezet op 0
            GenerateQuestion(); // een vraag wordt aangemaakt
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            CheckAnswer();
        }
    }
    void GenerateQuestion()
    {
        answerField.text = "";

        // Het programma maakt twee willekeurige getallen binnen 1 een 200.
        number1 = Random.Range(1, 200);  
        number2 = Random.Range(1, 200);
        
        // Het programma kiest 1 of 2.
        plusOrMinus = Random.Range(1, 3); 
        
        if (plusOrMinus == 1) 
        {
            // Als 1 is gekozen wordt een plus som gemaakt.
            answer = number1 + number2;
            question.text = number1 + " + " + number2;
        }
        
        else 
        {
            // Als 2 wordt gekozen dan wordt een min som gemaakt.
            answer = number1 - number2;
            question.text = number1 + " - " + number2;
        }
    }
    public void CheckAnswer()
    {
        // Checkt of de tekst vak leeg is. Zo ja, doe dan niks. Als hij niet leeg is dan checkt hij of het antwoordt goed is.
        if (answerField.text != "")
        {
            // Checkt als het antwoordt die in het Answerfield gezet is overeenkomt met het antwoord.
            if (answer == int.Parse(answerField.text))
            {
                if (questionsDone == 2) { // Dit checkt als er al twee vragen zijn gedaan. En als er al twee vragen zijn gedaan wordt een coin gegeven. Timer op 60. questionsDone op 0 en een wordt er een nieuwe vraag gemaakt.
                    MenuManager.coins++;
                    timer = 60;
                    questionsDone = 0;
                    answerCorrect.text = "Correct answer";
                    GenerateQuestion();
                }
                else if (questionsDone < 2) // Als er minder dan twee vragen zijn voltooid dan voegt het spel een toe aan questionsDone en wordt een nieuwe vraag gemaakt.
                {
                    questionsDone++;
                    answerCorrect.text = "Correct answer";
                    GenerateQuestion();
                }
            }
            else // Als het antwoord niet goed is staat het dat het antwoord fout is en wordt een nieuwe vraag gemaakt.
            {
                answerCorrect.text = "Wrong answer";
                GenerateQuestion();
            }
        }   
    }
    public void Menu() // Het menu knop stuurt je terug naar het menu.
    {
        SceneManager.LoadScene("Menu");
    }
}
