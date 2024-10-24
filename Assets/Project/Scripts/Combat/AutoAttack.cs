using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AutoAttack : MonoBehaviour
{
    [SerializeField] LayerMask layers;
    public bool HasImpacted;
    public int Level = 1;
    public IMagicBase magicBase {  get; private set; }

    private void Start()
    {
        StartCoroutine(IEDestroyProyectile());
        magicBase = GameObject.Find("AttacksAndAbilities").GetComponent<IMagicBase>();
    }

    private void Update()
    {
        if (!HasImpacted)
        {
            transform.position += transform.forward * 20 * Time.deltaTime;
        }

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, .5f, layers) && !HasImpacted)
        {
            Debug.Log("Hit Enemy");
            HasImpacted = true;
            GameObject enemy = hit.transform.gameObject;
            switch (Level)
            {
                case 1:
                    Level1(enemy); break;
                case 2:
                    Level2(enemy); break;
                case 3:
                    Level3(enemy); break;
                case 4:
                    Level4(enemy); break;
                case 5:
                    Level5(enemy); break;
                default:
                    Level1(enemy); break;
            }
        }
    }

    public abstract void Level1(GameObject enemy);
    public abstract void Level2(GameObject enemy);
    public abstract void Level3(GameObject enemy);
    public abstract void Level4(GameObject enemy);
    public abstract void Level5(GameObject enemy);

    private IEnumerator IEDestroyProyectile()
    {
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }

    public  void DestroyProyectile()
    {
        Destroy(gameObject);
    }
        
}
