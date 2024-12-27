using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class EndUI : MonoBehaviour
{
    public string sentence1, sentence2, sentence3, sentence4, sentence5, sentence6, sentence7;
    public bool EndofGame = false;

    public string NextLevel = "Level2";
    public float delayTime = 0.25f;
    public TMP_Text sentence1UI, sentence2UI, sentence3UI, sentence4UI, sentence5UI, sentence6UI, sentence7UI;
    public LevelUI levelUI;

    void Start()
    {
         StartEndOfLevel();
    }

    public void StartEndOfLevel()
    {
        sentence2 = "Captured Fish: " + levelUI.fishInt + "/" + levelUI.fishMax;
        int tempFishTotal = (100 * levelUI.fishInt);
        sentence3 = tempFishTotal.ToString();
        if (levelUI.fishInt - levelUI.fishMax == 0){
            tempFishTotal = tempFishTotal + 200;
            sentence3 = (tempFishTotal).ToString();
        }
        sentence4 = "Escape Attempts: " + ScoreVarPersistent.RestartCt;
        int tempEscScore = (200-(ScoreVarPersistent.RestartCt*25));
        sentence5 = tempEscScore.ToString();
        sentence6 = "Total Score: ";
        sentence7 = (tempEscScore+tempFishTotal).ToString();
       
   
        StartCoroutine(PlaySentencesSequentially());
    }

    IEnumerator PlaySentencesSequentially()
    {

        yield return StartCoroutine(SentenceIter(sentence1, sentence1UI));
        yield return StartCoroutine(SentenceIter(sentence2, sentence2UI));
        yield return StartCoroutine(SentenceIter(sentence3, sentence3UI));
        yield return StartCoroutine(SentenceIter(sentence4, sentence4UI));
        yield return StartCoroutine(SentenceIter(sentence5, sentence5UI));
        yield return StartCoroutine(SentenceIter(sentence6, sentence6UI));
        yield return StartCoroutine(SentenceIter(sentence7, sentence7UI));
        
    }
    public void LevelSwitch(){
        SceneManager.LoadScene(NextLevel);
        ScoreVarPersistent.RestartCt = 0;
        if (EndofGame){
            SceneManager.LoadScene("Title");
        }
    }
    IEnumerator SentenceIter(string sentence, TMP_Text uiSentence)
    {
        string tempStr = ""; 
        foreach (char c in sentence)
        {
            tempStr += c;
            uiSentence.text = tempStr; 
            yield return new WaitForSeconds(delayTime); 
        }
    }
}
