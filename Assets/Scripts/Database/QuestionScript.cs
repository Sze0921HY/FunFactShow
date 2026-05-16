using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "QuestionScript", menuName = "Scriptable Objects/QuestionScript")]
public class QuestionScript : ScriptableObject
{
    public List<QuestionData> questionList;

    public enum Options
    {
        Option_A,
        Option_B,
        Option_C,
        Option_D,
    }

    [System.Serializable]
    public class QuestionData
    {
        [TextArea]
        public string question;
        public string[] options = new string[4];

        public Options correctAnswer;

        public int correctAnswerIndex
        {
            get { return (int)correctAnswer; }
        }

        public Sprite image;
    }
}
