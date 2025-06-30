using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;



// Auteur: Stijn Grievink
// User Story: Als speler wil ik een aantal keer geraakt kunnen worden zonder meteen dood te gaan zodat ik de feedback van de hits kan gebruiken om mijn speelstijl aan te passen.
public class Health : MonoBehaviour
{
    public int health = 100;
    [SerializeField] private GameObject _deadVersion;
    private EnemyBehaviour _behaviour;
    [SerializeField] private VolumeProfile _volume;
    private Vignette _vignette;

    // Vignette Colors
    [Header("Vignette Colors")]
    [SerializeField] private ColorParameter _black;
    [SerializeField] private ColorParameter _red;
    [SerializeField] private UnityEngine.UI.Image _playerHealthbar;
    private bool isDead = false;
    private int _maxHealth;

    private void Awake()
    {
        _maxHealth = health;
        _behaviour = GetComponent<EnemyBehaviour>();

        if (_volume != null)
        {
            _volume.TryGet<Vignette>(out _vignette);
            _vignette.color.Override(Color.black);
        }
    }

    public void Damage(float pAmount)
    {
        int amount = Mathf.FloorToInt(pAmount);
        health -= amount;
        // Check if health is on Enemy, then rush
        if (_behaviour != null)
        {
            _behaviour.SetToHunt();
        }
        // Else the health is on the player
        else if (_vignette != null)
        {
            _playerHealthbar.fillAmount = (float)health / (float)_maxHealth;
            _vignette.color.Override(Color.red);
            Invoke("SetVignetteBlack", 0.5f);
        }

        if (health <= 0 && !isDead)
        {
            if (_deadVersion != null)
            {
                GameObject obj = Instantiate(_deadVersion, transform.position, transform.rotation);
                obj.GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), Random.Range(-5, 5));
                Destroy(gameObject);
                isDead = true;
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    public void SetVignetteBlack()
    {
        _vignette.color.Override(Color.black);
    }
}
