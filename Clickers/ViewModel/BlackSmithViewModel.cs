using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Clickers.Models.Items;
using Clickers.Views;
using Clickers.Views.ElementViews;
using Clickers.Models.Buildings;

namespace Clickers.ViewModel
{
    public class BlackSmithViewModel
    {
        private BlackSmithView view;
        public BlackSmithView View
        {
            get { return view; }
            set { view = value; }
        }

        private Blacksmith blacksmith;
        public Blacksmith Blacksmith
        {
            get { return blacksmith; }
            set { blacksmith = value; }
        }


        public BlackSmithViewModel(BlackSmithView view)
        {
            this.Blacksmith = GameViewModel.Instance.MainCastle.Blacksmith;
            this.View = view;

            this.View.BuyEquipmentListingUC.Controller.Blacksmith = this.Blacksmith;
            this.View.BuyEquipmentListingUC.Controller.initBuyView();

        }


    }
}
