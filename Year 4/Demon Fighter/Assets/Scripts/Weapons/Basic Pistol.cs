//Auteur: Dion Vos
//User Story: Als speler wil ik een basiswapen hebben met modale statistieken en demping zodat ik geluidloos vijanden kan neerhalen zonder anderen te alarmeren.
using System.Collections;
using UnityEngine;

public class BasicPistol : Weapon
{
    [Header("Weapon Mods")]
    public bool hasNoSilencerMod;
    public bool hasFullautoMod;

    [Header("No Silencer Mod Buffs")]
    [SerializeField]
    private int _newDamage;

    private GameObject _supressor;
    private GameObject _gunBody;

    public override void Fire(Vector3 pHitPoint)
    {
        if (_canFire && HasBullets() && !_isReloading)
        {
            StartCoroutine(FireRate());

            // gefixed door: Stijn, Dion en Paulo
            Vector3 direction = pHitPoint - _bulletSpawnpoint.transform.position;
            GameObject bullet = Instantiate(_bulletGameobject, _bulletSpawnpoint.transform.position, Quaternion.LookRotation(direction, Vector3.up), null);
            bullet.GetComponent<Bullet>().bulletDamage = _damage;
            _currentMag -= _bulletConsumption;
        }
    }

    private void SetMod()
    {
        if (hasNoSilencerMod)
        {
            _supressor.SetActive(false);
            _damage = _newDamage;
        }
    }
}
