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
                string showPayButton = Request.QueryString["showPayButton"];
                if (showPayButton == "true")
                {
                    PayButton.Visible = true;
                    CancelPaymentButton.Visible = true;
                    PaymentSelectionLabel.Visible = false;
                    OrLabel.Visible = false;
                    CashButton.Visible = false;
                    CardButton.Visible = false;
                    WelcomeLabel.Visible = false;
                    InsertTicketButton.Visible = false;
                }
            }
        }

        protected void InsertTicketButton_Click(object sender, EventArgs e)
        {
            AmountDueLabel.Text = "$5.00";
            PaidAmountLabel.Text = "The amount paid:";
            PaymentSelectionLabel.Visible = true;
            OrLabel.Visible = true;
            CashButton.Visible = true;
            CardButton.Visible = true;
            ClickCount = 0;
            ParkingTicketImage.Visible = false;
            PaidAmountLabel.Visible = false;
            DispAmountPaidLabel.Visible = false;
            DollarLabel.Visible = false;
            ThankYouLabel.Visible = false;
            TicketLabel.Visible = false;
            WelcomeLabel.Visible = false;
            InsertTicketButton.Visible = false;
        }

        protected void CashButton_Click(object sender, EventArgs e)
        {
            PaymentSelectionLabel.Visible = false;
            OrLabel.Visible = false;
            CashButton.Visible = false;
            CardButton.Visible = false;
            DollarButton.Visible = true;

        }

        protected void CardButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
            PayButton.Visible = true;
        }

        protected void DollarButton_Click(object sender, EventArgs e)
        {
            if (ClickCount < 5)
            {
                ClickCount++;
            }
            PaidAmountLabel.Visible = true;
            DispAmountPaidLabel.Visible = true;
            DollarLabel.Visible = true;
            DispAmountPaidLabel.Text = ClickCount.ToString();
            if (ClickCount > 0)
            {
                CancelPaymentButton.Visible = true;
            }
            if (ClickCount == 5)
            {
                PayButton.Visible = true;
            }
            if(ClickCount > 5)
            {
                PayButton.Visible = false;
                WarningLabel.Visible = true;
            }
        }

        protected void CancelPaymentButton_Click(object sender, EventArgs e)
        {
            AmountDueLabel.Text = "";
            ClickCount = 0;
            WelcomeLabel.Visible = true;
            InsertTicketButton.Visible = true;
            TicketLabel.Visible = true;
            ParkingTicketImage.Visible = true;
            DollarButton.Visible = false;
            CancelPaymentButton.Visible = false;
            PayButton.Visible = false;
            PaidAmountLabel.Text = "Refunded Payment";
            DollarLabel.Visible = false;
            DispAmountPaidLabel.Visible = false;
        }

        protected void PayButton_Click(object sender, EventArgs e)
        {
            CancelPaymentButton.Visible = false;
            PayButton.Visible = false;
            DollarButton.Visible = false;
            ThankYouLabel.Visible = true;
            TicketLabel.Visible= true;
            ParkingTicketImage.Visible= true;
        }
    }
}