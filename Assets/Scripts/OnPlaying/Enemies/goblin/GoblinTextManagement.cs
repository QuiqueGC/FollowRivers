using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoblinTextManagement : MonoBehaviour
{
    [SerializeField] Transform text;
    [SerializeField] Transform target;
    [SerializeField] bool rightGoblin;
    private string[] goblinText;
    private bool startTalking;
    private float secondsToChangeText;
    
    // Start is called before the first frame update
    void Start()
    {

        startTalking = false;

        goblinText = new string[6];

        if (rightGoblin)
        {
            goblinText[0] = "Dfafs ar´es re´asc ghre!!";
            goblinText[1] = "Frrrac´to waeas uhóeren!!!";
            goblinText[2] = "Emmm...";
            goblinText[3] = "Creo que no nos entiende...";
            goblinText[4] = "Bueno, da igual.";
            goblinText[5] = "En resumen: si entras, mueres.";

            secondsToChangeText = 2f;
        }
        else
        {
            goblinText[0] = "Dfafs dfafs!!";
            goblinText[1] = "Frrrac´frrrac´!!!";
            goblinText[2] = "Mmm...";
            goblinText[3] = "No entiende, no entiende!!";
            goblinText[4] = "Da bueno, digual!!";
            goblinText[5] = "Muere, muere!!!";

            secondsToChangeText = 2.2f;
        }
        

    }

    // Update is called once per frame
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
        text.gameObject.SetActive(true);

        yield return new WaitForSeconds(secondsToChangeText);

        for (int i = 0; i < goblinText.Length; i++)
        {
            text.gameObject.GetComponent<TextMeshProUGUI>().text = goblinText[i];

            yield return new WaitForSeconds(secondsToChangeText);
        }

        text.gameObject.SetActive(false);

        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

        
    }

}
