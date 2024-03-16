<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPage.Master" AutoEventWireup="true" CodeBehind="ApartmentsList.aspx.cs" Inherits="RWA_Projekt.ApartmentsList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">

    <asp:Panel ID="pnlApartmentList" runat="server">
        <div class="container">
            <div class="col-md">

                <div class="mb-3">
                    <!--sortiranje prema statusu-->
                    <asp:Label ID="lblStatus" class="form-label" runat="server" Text="Status:"></asp:Label>
                    <asp:DropDownList ID="ddlStatus" DataValueField="Id" DataTextField="Name" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">                        
                    </asp:DropDownList>
                </div>
                <!--sortiranje prema gradu-->
                    <div class="mb-3">
                <asp:Label ID="lblCities" class="form-label" runat="server" Text="City:"></asp:Label>
                <asp:DropDownList ID="ddlCities" AutoPostBack="true" DataValueField="Id" DataTextField="Name" runat="server"></asp:DropDownList>                    
            </div>

                <!--lista apartmana--> 
                <asp:Repeater ID="rptApartment" runat="server">
                    <HeaderTemplate>
                        <table id="myTable" class=" table table-striped" style="background-color: #FFBFCD">
                            <thead>
                                <tr>
                                    <th scope="col">Name</th>
                                    <th scope="col">City</th>
                                    <th scope="col">Address</th>
                                    <th scope="col">MaxChildren</th>
                                    <th scope="col">MaxAdults</th>
                                    <th scope="col">TotalRooms</th>
                                    <th scope="col">Price</th>
                                    <th scope="col">Status</th>
                                    <th scope="col"></th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <th scope="row"><%# Eval(nameof(RwaLib.Models.Apartment.Name)) %></th>
                            <td><%# Eval(nameof(RwaLib.Models.Apartment.NameCity)) %></td>
                            <td><%# Eval(nameof(RwaLib.Models.Apartment.Address)) %></td>
                            <td><%# Eval(nameof(RwaLib.Models.Apartment.MaxChildren)) %></td>
                            <td><%# Eval(nameof(RwaLib.Models.Apartment.MaxAdults)) %></td>
                            <td><%# Eval(nameof(RwaLib.Models.Apartment.TotalRooms)) %></td>
                            <td><%# Eval(nameof(RwaLib.Models.Apartment.Price)) %></td>
                            <td><%# Eval(nameof(RwaLib.Models.Apartment.StatusName)) %></td>
                            <td><asp:Button ID="btnDetails" runat="server" CommandArgument="<%# Eval(nameof(RwaLib.Models.Apartment.Id)) %>" Text="Open details" ForeColor="Black" OnClick="btnDetails_Click"/>
                            </td>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                    </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>
    </asp:Panel>

    <!--Dodavanje novog apartmana-->
    <asp:Button ID="btnAddNewApartment" OnClick="btnAddNewApartment_Click" BackColor="#FFBFCD" BorderColor="#FFBFCD" ForeColor="Black" runat="server" Text="Add new apartment" />


</asp:Content>
