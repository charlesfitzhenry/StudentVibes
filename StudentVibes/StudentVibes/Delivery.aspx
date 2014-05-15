<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Delivery.aspx.cs" Inherits="StudentVibes.Delivery" %>

<asp:Content ID="Delivery" ContentPlaceHolderID="MainContent" runat="server">
    
    

    <div id="locationField" style="margin-top: 20px">
        <input id="autocomplete" placeholder="Enter your address"
            onfocus="geolocate()" type="text">
    </div>

    <table id="address" style="margin-top: 20px">
        <tr>
            <td class="label">Street address</td>
            <td class="slimField">
                <input class="field" id="street_number"
                    disabled="true"></td>
            <td class="wideField" colspan="2">
                <input class="field" id="route"
                    disabled="true"></td>
        </tr>
        <tr>
            <td class="label">City</td>
            <td class="wideField" colspan="3">
                <input class="field" id="locality"
                    disabled="true"></td>
        </tr>
        <tr>
            <td class="label">Province</td>
            <td class="slimField">
                <input class="field"
                    id="administrative_area_level_1" disabled="true"></td>
            <td class="label">Zip code</td>
            <td class="wideField">
                <input class="field" id="postal_code"
                    disabled="true"></td>
        </tr>
        <tr>
            <td class="label">Country</td>
            <td class="wideField" colspan="3">
                <input class="field"
                    id="country" disabled="true"></td>
        </tr>
    </table>
    <div id="content-pane">
        <!--<div id="inputs">
            <pre>
var origin1 = new google.maps.LatLng(55.930, -3.118);
var origin2 = 'Greenwich, England';
var destinationA = 'Stockholm, Sweden';
var destinationB = new google.maps.LatLng(50.087, 14.421);
        </pre>
            <p>
                <button type="button" onclick="calculateDistances();">
                    Calculate
          distances</button>
            </p>
        </div>-->
        <div id="outputDiv"></div>
    </div>
    <div id="map-canvas"></div>


</asp:Content>
