using System.Collections;
using TMPro;
using UnityEngine;

public class GoblinTextManagement : MonoBehaviour
{
    [SerializeField] Transform goblinDialog;
    [SerializeField] Transform target;
    [SerializeField] bool rightGoblin;
    private string[] goblinSentences;
    private bool startTalking;
    private float secondsToChangeText;
    
    void Start()
    {
        startTalking = false;
        goblinSentences = new string[6];

        SentencesConfiguration();
    }

    void Update()
    {
        if (target.position.y >= -40 && !startTalking)
        {
            startTalking = true;
            StartCoroutine(TextGoblinProgression());
        }
    }

    private IEnumerator TextGoblinProgression()
    {
        TextMeshProUGUI goblinText = goblinDialog.gameObject.GetComponent<TextMeshProUGUI>();
        Rigidbody2D goblinRigidBody = gameObject.GetComponent<Rigidbody2D>();

        goblinDialog.gameObject.SetActive(true);

        yield return new WaitForSeconds(secondsToChangeText);

        for (int i = 0; i < goblinSentences.Length; i++)
        {
            goblinText.text = goblinSentences[i];

            yield return new WaitForSeconds(secondsToChangeText);
        }

        goblinDialog.gameObject.SetActive(false);

        goblinRigidBody.bodyType = RigidbodyType2D.Dynamic;
    }

    private void SentencesConfiguration()
    {
        if (rightGoblin)
        {
            goblinSentences[0] = "Dfafs ar´es re´asc ghre!!";
            goblinSentences[1] = "Frrrac´to waeas uhóeren!!!";
            goblinSentences[2] = "Emmm...";
            goblinSentences[3] = "Creo que no nos entiende...";
            goblinSentences[4] = "Bueno, da igual.";
            goblinSentences[5] = "En resumen: si entras, mueres.";
            secondsToChangeText = 2f;
        }
        else
        {
            goblinSentences[0] = "Dfafs dfafs!!";
            goblinSentences[1] = "Frrrac´frrrac´!!!";
            goblinSentences[2] = "Mmm...";
            goblinSentences[3] = "No entiende, no entiende!!";
            goblinSentences[4] = "Da bueno, digual!!";
            goblinSentences[5] = "Muere, muere!!!";
            secondsToChangeText = 2.2f;
        }
    }

}
