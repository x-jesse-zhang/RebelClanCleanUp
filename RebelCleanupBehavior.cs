using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Library;

namespace RebelClanCleanUp
{
    internal class RebelCleanupBehavior : CampaignBehaviorBase
    {
        public override void RegisterEvents()
        {
            CampaignEvents.OnSettlementOwnerChangedEvent.AddNonSerializedListener(
                this, 
                new Action<
                    Settlement, 
                    bool, 
                    Hero, 
                    Hero, 
                    Hero, 
                    ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail
                >(this.OnSettlementOwnerChanged)
            );
        }

        private void OnSettlementOwnerChanged(
            Settlement settlement, 
            bool openToClaim, 
            Hero newOwner, 
            Hero oldOwner, 
            Hero capturerHero, 
            ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail detail
        ){
            if (oldOwner.IsRebel) {
                DestroyClanAction.ApplyByFailedRebellion(oldOwner.Clan);
                InformationMessage msg = new InformationMessage(
                                    oldOwner.Clan.GetName() + " has been destroyed for failing their rebellion!"
                                );
                InformationManager.DisplayMessage(msg);
            }
        }

        public override void SyncData(IDataStore dataStore) {}
    }
}
