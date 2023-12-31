using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowGenerator : MonoBehaviour
{
     public List<Arrow> arrows;
    [SerializeField] bool looping = false;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllArrows());
        }
        while (looping);
    }

    private IEnumerator SpawnAllArrows()
    {
        for (int i = 0; i < arrows.Count; i++)
        {
           var currentArrow = arrows[i];
            yield return new WaitForSeconds(0.3f);
            currentArrow.gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(1f);
        arrows.ForEach(x => x.gameObject.SetActive(false));
    }
}
