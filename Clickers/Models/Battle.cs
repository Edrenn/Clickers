using Clickers.Models.Base;
using Clickers.ViewModel.ArmyFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clickers.Models
{
    public class Battle : BaseEntity
    {
        private int battleId;

        public int BattleId
        {
            get { return battleId; }
            set { battleId = value; }
        }

        private BattleViewModel controller;
        public BattleViewModel Controller
        {
            get { return controller; }
            set { controller = value; }
        }

        public Battle( Castle defendingCastle, Castle attackingCastle, bool isEnnemyAttack)
        {
            this.Controller = new BattleViewModel(attackingCastle, defendingCastle, isEnnemyAttack);
        }

        

        
    }
}
