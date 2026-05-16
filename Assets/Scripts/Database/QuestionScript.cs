using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "QuestionScript", menuName = "Scriptable Objects/QuestionScript")]
public class QuestionScript : ScriptableObject
{
    public List<QuestionData> questionList;

    [System.Serializable]
    public class QuestionData
    {
        [TextArea]
        public string question;
        public string[] options = new string[4];
        public int questionIndex;
        public int correctAnswerIndex;

        public Sprite image;
    }
}
