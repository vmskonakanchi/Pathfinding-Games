using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;

public class SurveyTaker : MonoBehaviour
{
    [SerializeField] private GameObject holder;
    [SerializeField] private List<User> users = new List<User>();

    [SerializeField] private Slider ratingSlider;
    [SerializeField] private TMP_InputField changeField;
    [SerializeField] private TMP_InputField otherField;


    [SerializeField] private int rating;
    [SerializeField] private string changeText;
    [SerializeField] private string otherText;

    private string pathToFile;

    private void Awake()
    {
        pathToFile = Application.dataPath + "/SurveyData.csv";
        if (File.Exists(Application.dataPath + "/SurveyData.csv")) return;
        CSVWriter.CreateCSV(pathToFile, "Rating, ChangeToSee, Feedback");
    }


    public void SumbitSurvey()
    {
        if (ratingSlider == null || changeField == null || otherField == null) return;
        rating = (int)ratingSlider.value;
        changeText = changeField.text;
        otherText = otherField.text;
        if (!users.Contains(new User(rating, changeText, otherText)))
        {
            users.Add(new User(rating, changeText, otherText));
        }
        else
        {
            throw new Exception("You Have Aldready Entered Your Response");
        }
        if (rating != 0 && changeText != "" && otherText != "")
        {
            CSVWriter.WriteToCsv(pathToFile, users);
        }
    }

}

[System.Serializable]
public class User
{
    public int rating;
    public string changeText;
    public string otherText;
    public User(int _rating, string _changeText, string _otherText)
    {
        rating = _rating;
        changeText = _changeText;
        otherText = _otherText;
    }
}
