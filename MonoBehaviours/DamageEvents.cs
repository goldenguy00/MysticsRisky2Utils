using System.Collections.Generic;
using System.Text;
using RoR2;
using UnityEngine;

namespace MysticsRisky2Utils.MonoBehaviours
{
    public class MysticsRisky2UtilsDamageEvents : MonoBehaviour, IOnIncomingDamageServerReceiver, IOnTakeDamageServerReceiver
    {
        public HealthComponent healthComponent;
        public CharacterBody victimBody;

        public void Start()
        {
            healthComponent = GetComponent<HealthComponent>();
            if (!healthComponent)
            {
                Object.Destroy(this);
                return;
            }
            victimBody = healthComponent.body;
        }

        public void OnIncomingDamageServer(DamageInfo damageInfo)
        {
            if (BeforeTakeDamage != null)
            {
                var attackerBody = damageInfo.attacker ? damageInfo.attacker.GetComponent<CharacterBody>() : null;
                var attackerInfo = attackerBody ? new MysticsRisky2UtilsPlugin.GenericCharacterInfo(attackerBody) : default;
                var victimInfo = new MysticsRisky2UtilsPlugin.GenericCharacterInfo(victimBody);

                BeforeTakeDamage(damageInfo, attackerInfo, victimInfo);
            }
        }

        public void OnTakeDamageServer(DamageReport damageReport)
        {
            if (victimBody)
                OnTakeDamage?.Invoke(damageReport);
        }
    }
}
