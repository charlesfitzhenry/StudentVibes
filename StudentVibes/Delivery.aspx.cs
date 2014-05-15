using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Threading;

namespace StudentVibes
{
    public partial class Delivery : System.Web.UI.Page
    {
        // Delivery variables
        public string streetNumber = "36";
        public string streetName = "Main Road";
        public string city = "Cape Town";
        public string province = "WC";
        public string zipCode = "7442";
        public string country = "South Africa";
        public double orderTotal = 500.65;


        protected void Page_Load(object sender, EventArgs e)
        {

            // HtmlGenericControl body = this.Master.FindControl("body") as HtmlGenericControl;

            // body.Attributes.Add("onLoad", "initialize();");



        }
        public double getTotalCost()
        {

            return orderTotal + getDeliveryCost()[4];
        }
        public double[] getDeliveryCost()
        {
            double[] cost = new Double[5];


            //Free delivery within 15km
            if (getDistance() > 15 && !(getDistance() < 0) && getDistance() < 60)
            {
                //Check if free delivery applies order total > R250
                if (orderTotal > 250)
                {
                    switch (getLoyalty())
                    {

                        case "silver":
                            cost[0] = 1;// Loyalty position. 1= Silver, 2=Gold, 3=Platinum
                            cost[1] = 5;//Applicable delivery cost per km over free 15km
                            cost[2] = getDistance(); //total delivery distance
                            cost[3] = cost[2] - 15;//Distance exceeding 15km.
                            cost[4] = ((getDistance() - 15) * 5); //Applicable exceeding distance cost.
                            break;

                        case "gold":
                            cost[0] = 2;// Loyalty position. 1= Silver, 2=Gold, 3=Platinum
                            cost[1] = 3;//Applicable delivery cost per km over free 15km
                            cost[2] = getDistance(); //total delivery distance
                            cost[3] = cost[2] - 15;//Distance exceeding 15km.
                            cost[4] = ((getDistance() - 15) * 3); //Applicable exceeding distance cost.
                            break;

                        case "platinum":
                            cost[0] = 3;// Loyalty position. 1= Silver, 2=Gold, 3=Platinum
                            cost[1] = 0;//120 fixed price for further deliveries
                            cost[2] = getDistance(); //total delivery distance
                            cost[3] = 0;//Distance exceeding 15km.
                            cost[4] = 0; //Applicable exceeding distance cost.
                            break;


                    }

                    return cost;
                }
                else
                { //Cost < R250
                    switch (getLoyalty())
                    {

                        case "silver":
                            cost[0] = 1;// Loyalty position. 1= Silver, 2=Gold, 3=Platinum
                            cost[1] = 5;//Applicable delivery cost per km over free 15km
                            cost[2] = getDistance(); //total delivery distance
                            cost[3] = cost[2];//Distance exceeding 15km.
                            cost[4] = ((getDistance()) * 5); //Applicable exceeding distance cost.
                            break;
                        case "gold":
                            cost[0] = 2;// Loyalty position. 1= Silver, 2=Gold, 3=Platinum
                            cost[1] = 3;//Applicable delivery cost per km over free 15km
                            cost[2] = getDistance(); //total delivery distance
                            cost[3] = cost[2];//Distance exceeding 15km.
                            cost[4] = ((getDistance()) * 3); //Applicable exceeding distance cost.
                            break;

                        case "platinum":
                            cost[0] = 3;// Loyalty position. 1= Silver, 2=Gold, 3=Platinum
                            cost[1] = 1.50;//120 fixed price for further deliveries
                            cost[2] = getDistance(); //total delivery distance
                            cost[3] = cost[2];//Distance exceeding 15km.
                            cost[4] = cost[2] * cost[1]; //Applicable exceeding distance cost.
                            break;


                    }
                    return cost;

                }


            }
            else
            { //Distance > range
                cost[0] = -1;// Loyalty position. 1= Silver, 2=Gold, 3=Platinum
                cost[1] = 0;//Applicable delivery cost per km over free 15km
                cost[2] = getDistance(); //total delivery distance
                cost[3] = 0;//Distance exceeding 15km.
                cost[4] = 0; //Applicable exceeding distance cost.
                return cost;
            }



        }





        private string getLoyalty()
        {
            return "platinum";
        }
        public string[] getStoredAddress()
        {
            //Query the DB with the current customer logged in and populate the address fields if it exists
            //Assign query to local variables 

            //if(Query == success){
            //meaning there is an existing address
            string[] addrs = new string[7];

            addrs[0] = streetNumber;
            addrs[1] = streetName;
            addrs[2] = city;
            addrs[3] = province;
            addrs[4] = zipCode;
            addrs[5] = country;
            addrs[6] = "true";

            return addrs;


            //}else{
            //addrs[6] = "false";
            //}


        }

        public void setAddress()
        {
            String[] tmp;
            tmp = Request.Params["googletxt"].Split(',');
            streetNumber = tmp[0];
            streetName = tmp[1];
            city = tmp[2];
            province = tmp[3];
            zipCode = tmp[4];
            country = tmp[5];

        }

        public double getDistance()
        {
            while (dist.Value == null)
            {
                Thread.Sleep(5000);
            }

            string distance = dist.Value;
            if (dist.Value != "")
            {
                if (dist.Value.Contains(","))
                {
                    distance = distance.Replace(",", ".");
                }

                return Double.Parse(distance);

            }

            return -1;

        }
        protected void checkOut(object sender, EventArgs e)
        {
            setAddress();
            //ONLY ALLOW TO PROCEED IF LOGGED IN

            //PROCEED TO PAYPAL
            // Response.Redirect(dist.Value);
            double ds = getDistance();
            if (ds != -1)
            {

                if (ds < 15)
                {
                    Label1.Text = dist.Value + "km";
                    label3.Text = " Delivery Cost  R" + getDeliveryCost()[4];
                }
                else
                {
                    if (ds > 60)
                    {
                        Label1.Text = dist.Value + "km, you are out of the delivery range of Student Vibes.";
                        //label3.Text = " Delivery Cost  R" + getDeliveryCost()[4];
                    }
                    else
                    {
                        Label1.Text = dist.Value + "km, you are out of the free delivery range.";
                        label3.Text = " Delivery Cost  R" + getDeliveryCost()[4];
                        UpdatePanel2.Update();
                    }
                }
                //Session["payment_amt"]; //"At "
            }

        }
    }
}