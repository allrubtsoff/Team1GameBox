using UnityEngine;

public class SpellCaster : MonoBehaviour
{

    [SerializeField] private AttackMarkersController attackMarkersController;

    private int _lastSpell;
    private const int _clowdSpell = 0;
    private const int _multyConeSpell = 1;
    private const int _pondSpell = 2;
    private const int _ConesNPondSpell = 3;

    private const int _spellCount = 4;



    public void GetNewRandomSpell()
    {
        int r = Random.Range(0, _spellCount);
        while (r == _lastSpell) r = Random.Range(0, _spellCount);
        _lastSpell = r;
        switch (_lastSpell)
        {
            case _clowdSpell:

                break;
            case _pondSpell:

                break;
            case _ConesNPondSpell:

                break;
            case _multyConeSpell:

                break;
        }
    }

    private void ChanceOfCalling()
    {
        //шанс дополнительного вызова ракет\избы\саммона
    }
}
