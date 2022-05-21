using UnityEngine;
using StarterAssets;

public class SpellCaster : MonoBehaviour
{
    [SerializeField] private AttackMarkersController attackMarkers;

    private int _lastSpell;
    private const int _clowdSpell = 0;
    private const int _multyConeSpell = 1;
    private const int _pondSpell = 2;
    private const int _ConesNPondSpell = 3;

    private const int _spellCount = 4;



    public void GetNewRandomSpell(ThirdPersonController controller ,float timeToDel)
    {
        int r = Random.Range(0, _spellCount);
        while (r == _lastSpell) r = Random.Range(0, _spellCount);
        _lastSpell = r;
        switch (_lastSpell)
        {
            case _clowdSpell:
                Debug.Log("Clowd Cast");
                attackMarkers.CreateClowdSpell(controller, transform.position);
                break;
            case _pondSpell:
                Debug.Log("Pond Cast");
                attackMarkers.CreateBossPondSpell(transform.position, timeToDel);
                break;
            case _ConesNPondSpell:
                Debug.Log("Pond N Cones Cast");
                attackMarkers.CreateBossPondSpell(transform.position, timeToDel);
                attackMarkers.CreateMultyCones(transform.position, timeToDel);
                break;
            case _multyConeSpell:
                Debug.Log("Cones Cast");
                attackMarkers.CreateMultyCones(transform.position, timeToDel);
                break;
        }
    }

    private void ChanceOfCalling()
    {
        
    }
}
