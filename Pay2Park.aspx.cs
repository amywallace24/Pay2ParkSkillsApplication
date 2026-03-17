using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project3
{
    public partial class Pay2Park : System.Web.UI.Page
    {
        private int clickCount = 0;

        //This makes the code easier to maintain if the parking price changes.
        private const int ParkingRate = 5;

        private int ClickCount
        {
            get
            {
                return ViewState["clickCount"] != null ? (int)ViewState["clickCount"] : 0;
            }
            set
            {
                ViewState["clickCount"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //*Note: for logic simplification this can be changed to
                //if(Request.QueryString["showPayButton"] == "true")
                string showPayButton = Request.QueryString["showPayButton"];
                if (showPayButton == "true")
                {
                    SetPaymentUI(true);
                }
            }
        }
        //Instead of writting 10+ lines of .Visible = true/false in every button, I
        //created this "source of truth" method to handle UI states.
        private void SetPaymentUI(bool isPaymentMode)
        {
            PayButton.Visible = isPaymentMode;
            CancelPaymentButton.Visible = isPaymentMode;

            //Toggle groups
            bool showMainControls = !isPaymentMode;
            PaymentSelectionLabel.Visible = showMainControls;
            OrLabel.Visible = showMainControls;
            CashButton.Visible = showMainControls;
            CardButton.Visible = showMainControls;
            WelcomeLabel.Visible = showMainControls;
            InsertTicketButton.Visible = showMainControls;
        }

        protected void InsertTicketButton_Click(object sender, EventArgs e)
        {
            AmountDueLabel.Text = $"${ParkingRate}.00";
            PaidAmountLabel.Text = "The amount paid:";

            ClickCount = 0;
            SetPaymentUI(true); //Replaces the manual toggling
            ParkingTicketImage.Visible = false;
            ThankYouLabel.Visible = false;
            
        }

        protected void CashButton_Click(object sender, EventArgs e)
        {
            SetPaymentUI(false);
            DollarButton.Visible = true;

        }

        protected void CardButton_Click(object sender, EventArgs e)
        {
            //Note: Response.Redirect stops execution, so PayButton.Visible never runs
            Response.Redirect("Login.aspx");
            PayButton.Visible = true;
        }

        //prevent dead code by removing the "if(ClickCount > 5)" check because of the first if
        protected void DollarButton_Click(object sender, EventArgs e)
        {
            if (ClickCount < ParkingRate)
            {
                ClickCount++;
            }
            PaidAmountLabel.Visible = true;
            DispAmountPaidLabel.Visible = true;
            DollarLabel.Visible = true;
            DispAmountPaidLabel.Text = ClickCount.ToString();
            
            CancelPaymentButton.Visible = (ClickCount > 0);
            PayButton.Visible =(ClickCount == ParkingRate);
        }

        protected void CancelPaymentButton_Click(object sender, EventArgs e)
        {
            AmountDueLabel.Text = "";
            ClickCount = 0;
            SetPaymentUI(false); //Reset back to main screen
            ParkingTicketImage.Visible = true;
            DollarButton.Visible = false;
            PaidAmountLabel.Text = "Refunded Payment";
            
        }

        protected void PayButton_Click(object sender, EventArgs e)
        {
            SetPaymentUI(false); //Hide payment button
            DollarButton.Visible = false;
            ThankYouLabel.Visible = true;
            ParkingTicketImage.Visible= true;
        }
    }
}