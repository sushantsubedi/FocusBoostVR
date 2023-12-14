using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SimpleArithmeticQuiz : MonoBehaviour
{
    // References to UI elements
    public TMP_Text questionText;
    public Button optionButton1;
    public Button optionButton2;
    public Button optionButton3;
    public Button optionButton4;
    public GameObject StartMenu;
    public GameObject PomodoroTimer;
    public GameObject QuizPanel;
    public GameObject Video;
    public TMP_Text scoreText;

    // Quiz-related variables
    private int score = 0;
    private int questionCount = 0;

    void Start()
    {
        // Initialization logic can be added here if needed
    }

    // Method to start the quiz with a given difficulty
    public void StartQuiz(int diffValue)
    {
        // Disable the video and show the quiz panel
        PomodoroTimer.SetActive(false);
        Video.SetActive(false);
        QuizPanel.SetActive(true);

        // Display the first question based on the difficulty level
        DisplayNextQuestion(diffValue);
    }

    void Update()
    {
        // Keyboard shortcuts to start the quiz with different difficulty levels
        if (Input.GetKeyDown(KeyCode.Z))
        {
            StartQuiz(1);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            StartQuiz(2);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            StartQuiz(3);
        }

        // Keyboard shortcuts to simulate button clicks for options
        if (Input.GetKeyDown(KeyCode.J))
        {
            HandleKeyPress(0); // Assuming J corresponds to the first option
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            HandleKeyPress(1); // Assuming K corresponds to the second option
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            HandleKeyPress(2); // Assuming L corresponds to the third option
        }
        else if (Input.GetKeyDown(KeyCode.Semicolon))
        {
            HandleKeyPress(3); // Assuming ; corresponds to the fourth option
        }
    }

    // Coroutine to restart the quiz after a delay
    IEnumerator RestartQuizCoroutine()
    {
        yield return new WaitForSeconds(5f);

        // Reset variables and UI elements
        score = 0;
        questionCount = 0;
        scoreText.text = "Score: 0";
        optionButton1.gameObject.SetActive(true);
        optionButton2.gameObject.SetActive(true);
        optionButton3.gameObject.SetActive(true);
        optionButton4.gameObject.SetActive(true);

        // Start the quiz with the desired difficulty (adjust as needed)
        StartQuiz(1);
    }

    // Method to handle keypress and simulate button click for a specific option
    public void HandleKeyPress(int optionIndex)
    {
        // Check if the option index is valid
        if (optionIndex >= 0 && optionIndex < 4) // Assuming you have four options
        {
            // Simulate button click for the corresponding option
            GetOptionButton(optionIndex).onClick.Invoke();
        }
    }

    // Method to display the next question based on the difficulty level
    public void DisplayNextQuestion(int diffValue)
    {
        int operand1 = 0;
        int operand2 = 0;
        int correctAnswer = 0;

        // Set up the question based on the difficulty level
        if (diffValue == 1)
        {
            // Addition of two numbers (1 to 10)
            operand1 = UnityEngine.Random.Range(1, 11);
            operand2 = UnityEngine.Random.Range(1, 11);
            correctAnswer = operand1 + operand2;
            questionText.text = $"{operand1} + {operand2} = ?";
        }
        else if (diffValue == 2)
        {
            // Multiplication of two one-digit numbers (1 to 9)
            operand1 = UnityEngine.Random.Range(1, 10);
            operand2 = UnityEngine.Random.Range(1, 10);
            correctAnswer = operand1 * operand2;
            questionText.text = $"{operand1} × {operand2} = ?";
        }
        else if (diffValue == 3)
        {
            // Division of a two-digit number by a one-digit number with an integer result
            operand2 = UnityEngine.Random.Range(1, 10); // Denominator (1 to 9)

            // Generate a numerator that is an integer multiple of the denominator
            operand1 = UnityEngine.Random.Range(operand2, 10) * operand2;

            correctAnswer = operand1 / operand2; // Ensure that the division result is an integer
            questionText.text = $"{operand1} ÷ {operand2} = ?";
        }
        else
        {
            // Handle invalid difficulty level
            return;
        }

        List<int> usedOptions = new List<int>();
        usedOptions.Add(correctAnswer);

        // Shuffle the option indices to ensure randomness
        List<int> optionIndices = new List<int> { 0, 1, 2, 3 };
        ShuffleList(optionIndices);

        // Set the correct option for buttons
        SetButtonLabel(GetOptionButton(optionIndices[0]), correctAnswer.ToString());

        // Set incorrect options for other buttons
        for (int i = 1; i < 4; i++) // Assuming you have four options
        {
            int incorrectOption;
            do
            {
                incorrectOption = UnityEngine.Random.Range(1, 101); // Adjust the range based on your requirements
            } while (usedOptions.Contains(incorrectOption));

            usedOptions.Add(incorrectOption);
            SetButtonLabel(GetOptionButton(optionIndices[i]), incorrectOption.ToString());
        }

        // Attach listeners directly to buttons
        for (int i = 0; i < 4; i++) // Assuming you have four options
        {
            int currentIndex = i; // Capture the current index in the loop
            GetOptionButton(optionIndices[i]).onClick.RemoveAllListeners();
            bool isCorrect = currentIndex == 0; // The first option is always the correct one
            GetOptionButton(optionIndices[i]).onClick.AddListener(() => AnswerClicked(isCorrect, diffValue));
        }
    }

    // Method to handle button clicks and evaluate the answer
    public void AnswerClicked(bool isCorrect, int diffValue)
    {
        if (isCorrect)
        {
            score++;
        }

        questionCount++;

        // Display the score and question count after all questions have been answered
        if (questionCount == 10)
        {
            UnityEngine.Debug.Log("Game Over. Final Score: " + score);
            scoreText.text = $"Quiz Over. Final Score: {score}/10";

            // Disable the option buttons or perform other end-of-quiz actions

            optionButton1.gameObject.SetActive(false);
            optionButton2.gameObject.SetActive(false);
            optionButton3.gameObject.SetActive(false);
            optionButton4.gameObject.SetActive(false);

            // Start the coroutine to restart the quiz after 5 seconds
            StartCoroutine(RestartQuizCoroutine());
        }
        else
        {
            // Continue to the next question if there are more questions
            DisplayNextQuestion(diffValue);
        }
    }

    // Method to set the label text for a button
    void SetButtonLabel(Button button, string text)
    {
        if (button.GetComponentInChildren<TMP_Text>() != null)
        {
            button.GetComponentInChildren<TMP_Text>().text = text;
        }
        else
        {
            // Add any other necessary logic for non-TextMeshPro text components
        }
    }

    // Fisher-Yates Shuffle algorithm to shuffle a list
    void ShuffleList(List<int> list)
    {
        int n = list.Count;
        for (int i = n - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            int temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }

    // Method to get the reference to an option button based on index
    Button GetOptionButton(int index)
    {
        switch (index)
        {
            case 0: return optionButton1;
            case 1: return optionButton2;
            case 2: return optionButton3;
            case 3: return optionButton4;
            default: return null;
        }
    }
}
