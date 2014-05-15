<%@ Page Title="Products" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductList.aspx.cs" Inherits="StudentVibes.ProductList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <section>

        <div>
            <hgroup>
                <h2><%: Page.Title %></h2>
            </hgroup>

            <asp:ListView ID="productList" runat="server"
                DataKeyNames="ProductID" GroupItemCount="4"
                ItemType="StudentVibes.Models.Product" SelectMethod="GetProducts">
                <EmptyDataTemplate>
                    <table>
                        <tr>
                            <td>No data was returned.</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <EmptyItemTemplate>
                    <td />
                </EmptyItemTemplate>
                <GroupTemplate>
                    <tr id="itemPlaceholderContainer" runat="server">
                        <td id="itemPlaceholder" runat="server"></td>
                    </tr>

                </GroupTemplate>


                <ItemTemplate>



                  
                        
                        <div class="row">
                            <div class="col-sm-6 col-md-4">
                                <div class="thumbnail">
                                    <img src="/Catalog/Images/Thumbs/<%#:Item.ImagePath %>" alt="...">
                                    <div class="caption">
                                        <h3>Thumbnail label</h3>
                                        <p>...</p>
                                        <p><a href="/AddToCart.aspx?productID=<%#:Item.ProductID %>" class="btn btn-primary" role="button">Add To Cart</a> <a href="#" class="btn btn-default" role="button">Button</a></p>
                                    </div>

                                </div>
                            </div>
                        </div>


                        <!--<table>
                            <tr>
                                <td>
                                    <a href=" <%#: GetRouteUrl("ProductByNameRoute", new {productName = Item.ProductName}) %>">
                                        <image src='/Catalog/Images/Thumbs/<%#:Item.ImagePath %>' width="100" height="75" border="1" />
                                        <a /></td>
                            </tr>
                            <tr>
                                <td>
                                    <a href="<%#: GetRouteUrl("ProductByNameRoute", new {productName = Item.ProductName}) %>">
                                        <%#: Item.ProductName %> </a>
                                    <br />
                                    <span>
                                        <b>Price: </b>
                                        <%#:String.Format("{0:c}", Item.UnitPrice)%> 
                                    </span>
                                    <br />
                                    <a href="/AddToCart.aspx?productID=<%#:Item.ProductID %>">
                                        <span class="ProductListItem">
                                            <b>Add To Cart<b> 
                                        </span></a>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                        </table>-->
                        </p> 
                    

                </ItemTemplate>

          
            </asp:ListView>
        </div>
    </section>
</asp:Content>
