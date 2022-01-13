
public class DefaultWeapon : Weapon
{
    private void Start()
    {
        WeaponSetUp(0.2f, 609, FindObjectOfType<DefaultWeaponProjectile>().gameObject);
    }
}
