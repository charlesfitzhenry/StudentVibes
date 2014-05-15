<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Delivery.aspx.cs" Inherits="StudentVibes.Delivery" %>

<asp:Content ID="Delivery" ContentPlaceHolderID="MainContent" runat="server">

    <script>
        $().ready(function () {
            var radios = document.getElementsByName("addrs");
            var txtbox = document.getElementById("autocomplete");
            var addBtn = document.getElementById("MainContent_AddBtn");
            // document.getElementById("orderSummary").style.display = "none";
            txtbox.disabled = true;
            addBtn.disabled = true;

            for (var i = 0, max = radios.length; i < max; i++) {
                radios[i].onclick = function () {
                    if (this.id == "prevAddNewSelect") {
                        document.getElementById("newD").style.display = "";
                        document.getElementById("existD").style.display = "none"
                        txtbox.disabled = false;
                        addBtn.disabled = false;
                        // document.getElementById("orderSummary").style.display = "none";

                        //displaySummary();
                    }
                    else {
                        document.getElementById("newD").style.display = "none";
                        document.getElementById("existD").style.display = "";
                        txtbox.disabled = true;
                        addBtn.disabled = true;
                        // document.getElementById("orderSummary").style.display = "";

                        //displaySummary();

                    };
                }
            }
        }
        );

        function displaySummary() {
            //OnClientClick = "/*displaySummary();*/"
            //Display order summary
            //alert("Display");
            //Call C# code to calculate distance

            var txtbox = document.getElementById("autocomplete");
            var addBtn = document.getElementById("MainContent_AddBtn");

            if (destinationA != "") {

                txtbox.disabled = true;

                document.getElementById("orderSummary").style.display = "";
            } else {
                alert("Please enter an address!");
            }

        }

    </script>

    <div id="deliveryAdd" style="margin-top: 20px">
        <h2>Delivery Address</h2>
        <br />
        <% 
            string[] adds = new String[6];
            adds = getStoredAddress();
            
        %>

        <div class="well">
            <%
                if (adds[6] == "true")
                { %>
            <div id="existDiv">
                <span>
                    <label>
                        <input type="radio" id="prevAddSelect" name="addrs" />
                        Select Previous Address</label></span><br />
                <div id="existD" style="font-size: 14px; padding-left: 20px; padding-top: 10px">
                    <p>
                        <%: adds[0] + " " +adds[1]%><br />
                        <%: adds[2] %><br />
                        <%: adds[3] %><br />
                        <%: adds[4] %><br />
                        <%: adds[5] %><br />
                    </p>
                </div>

            </div>
            <%} %>
            <div id="newDiv">
                <span>
                    <label>
                        <input type="radio" id="prevAddNewSelect" name="addrs" />
                        Add a New Address</label></span><br />

                <div id="newD" style="font-size: 14px; padding-left: 20px;">
                    <div id="locationField" style="margin-top: 20px">
                        <input id="autocomplete" placeholder="Enter your address"
                            onfocus="geolocate()" type="text">
                    </div>


                    <table id="address" style="margin-top: 20px">


                        <tr>
                            <td class="label">Street address</td>
                            <td class="slimField">
                                <input name="googletxt" class="field" id="street_number"
                                    disabled="true"></td>
                            <td class="wideField" colspan="2">
                                <input name="googletxt" class="field" id="route"
                                    disabled="true"></td>
                        </tr>
                        <tr>
                            <td class="label">City</td>
                            <td class="wideField" colspan="3">
                                <input name="googletxt" class="field" id="locality"
                                    disabled="true"></td>
                        </tr>
                        <tr>
                            <td class="label">Province</td>
                            <td class="slimField">
                                <input name="googletxt" class="field"
                                    id="administrative_area_level_1" disabled="true"></td>
                            <td class="label">Zip code</td>
                            <td class="wideField">
                                <input name="googletxt" class="field" id="postal_code"
                                    disabled="true"></td>
                        </tr>
                        <tr>
                            <td class="label">Country</td>
                            <td class="wideField" colspan="3">
                                <input name="googletxt" class="field"
                                    id="country" disabled="true"></td>
                        </tr>
                    </table>
                    <div id="content-pane">

                        <div id="outputDiv"></div>


                        <asp:HiddenField ID="dist" runat="server" />

                    </div>
                    <div id="map-canvas"></div>

                    <asp:UpdatePanel UpdateMode="Conditional" ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <br />
                            <asp:Button ID="AddBtn" OnClick="checkOut" Text="Add Address" runat="server" />
                            <br />
                            <asp:Label ID="Label1" runat="server"></asp:Label>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>


    <div id="outOfRange">
    </div>

    <h2>Order Summary</h2>
    <asp:UpdatePanel UpdateMode="Conditional" ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:Table ID="Table1" runat="server"
                
                 Style="width:30%;"
                GridLines="Both"
                HorizontalAlign="Left">
                <asp:TableRow>
                    <asp:TableCell>
                <b>Description</b>
                    </asp:TableCell>
                    <asp:TableCell>
                <b>Price</b>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
               Order total
                    </asp:TableCell>
                    <asp:TableCell>
                <asp:Label ID="label3" runat="server"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                    <asp:TableRow>
                    <asp:TableCell>
               Delivery total
                    </asp:TableCell>
                    <asp:TableCell>
                <asp:Label ID="label2" runat="server"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                 <asp:TableRow>
                    <asp:TableCell>
               <b>Grand Total</b>
                    </asp:TableCell>
                    <asp:TableCell>
                <asp:Label ID="label4" runat="server"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            
        </ContentTemplate>
    </asp:UpdatePanel>

    <!--<div id="orderSummary">

        <h2>Order Summary</h2>
        <table style="width: 300px">
            <tr>
                <th>Description</th>
                <th>Price</th>

            </tr>
            <tr>
                <td>Order Total</td>
                <td>
            </tr>
            <tr>
                <td></td>
                <td></td>

            </tr>
            <tr>
                <td><b>Grand Total</b></td>
                <td><b>R---</b></td>

            </tr>
        </table>

    </div>-->








</asp:Content>
