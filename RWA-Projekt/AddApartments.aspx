<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPage.Master" AutoEventWireup="true" CodeBehind="AddApartments.aspx.cs" Inherits="RWA_Projekt.AddImages" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <style type="text/css">
    .auto-style1 {
        height: 20px;
    }
    .auto-style2 {
            height: 20px;
            width: 138px;
        }
    .auto-style3 {
            width: 138px;
        }
    .auto-style4 {
        width: 138px;
        height: 21px;
    }
    .auto-style5 {
        height: 21px;
    }
    .auto-style6 {
            height: 20px;
            width: 220px;
        }
    .auto-style7 {
        height: 21px;
        width: 220px;
    }
    .auto-style8 {
            width: 220px;
        }
    .auto-style9 {
        width: 138px;
        height: 22px;
    }
    .auto-style10 {
        width: 220px;
        height: 22px;
    }
    .auto-style11 {
        height: 22px;
    }
        .auto-style12 {
            width: 138px;
            height: 38px;
        }
        .auto-style13 {
            width: 220px;
            height: 38px;
        }
        .auto-style14 {
            height: 38px;
        }
        .auto-style15 {
            margin-left: 32px;
            margin-top: 0;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    
    <br />
    <table class="nav-justified">
        
        <tr>
            <td class="auto-style2">
                <asp:Label ID="Label1" runat="server" Text="Name"></asp:Label>
            </td>
            <td class="auto-style6">
                <asp:TextBox ID="txtApartmentName" runat="server" Width="247px"></asp:TextBox>
            </td>
            <td class="auto-style1"></td>
        </tr>
        
        <tr>
            <td class="auto-style3">
                <asp:Label ID="lblAddress" runat="server" Text="Adress"></asp:Label>
            </td>
            <td class="auto-style8">
                <asp:TextBox ID="txtApartmentAdress" runat="server" Width="242px"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style9">
                <asp:Label ID="lblPrice" runat="server" Text="Price"></asp:Label>
            </td>
            <td class="auto-style10">
                <asp:TextBox ID="txtApartmentPrice" runat="server" Height="17px" Width="115px"></asp:TextBox>
            </td>
            <td class="auto-style11"></td>
        </tr>
        <tr>
            <td class="auto-style3">
                <asp:Label ID="lblMaxAdults" runat="server" Text="Max adults"></asp:Label>
            </td>
            <td class="auto-style8">
                <asp:TextBox ID="txtMaxAdults" runat="server" Height="21px" Width="117px"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style3">
                <asp:Label ID="lblMaxChildren" runat="server" Text="Max children"></asp:Label>
            </td>
            <td class="auto-style8">
                <asp:TextBox ID="txtMaxChildren" runat="server" Width="117px"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style3">
                <asp:Label ID="lblRooms" runat="server" Text="Ukupno soba"></asp:Label>
            </td>
            <td class="auto-style8">
                <asp:TextBox ID="txtTotalRooms" runat="server" Height="16px" Width="120px"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style3">
                <asp:Label ID="lblBeachDistance" runat="server" Text="Udaljenost od plaže"></asp:Label>
            </td>
            <td class="auto-style8">
                <asp:TextBox ID="txtBeachDistance" runat="server"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>

        <tr>
            <td class="auto-style3">
                <asp:Label ID="lblStatus" runat="server" Text="Status"></asp:Label>
            </td>
            <td class="auto-style8">
                <asp:DropDownList ID="ddlStatus" DataValueField="Id" DataTextField="Name" runat="server"></asp:DropDownList>
            </td>
            <td>&nbsp;</td>
        </tr>

        <tr>
            <td class="auto-style3">
                <asp:Label ID="lblOwner" runat="server" Text="Vlasnik"></asp:Label>
            </td>
            <td class="auto-style8">
                <asp:DropDownList ID="ddlOwner" DataValueField="Id" DataTextField="Name" runat="server"></asp:DropDownList>
            </td>
            <td>&nbsp;</td>
        </tr>

        <tr>
            <td class="auto-style3">
                <asp:Label ID="lblCity" runat="server" Text="Grad"></asp:Label>
            </td>
            <td class="auto-style8">
                <asp:DropDownList ID="ddlCities" DataValueField="Id" DataTextField="Name" runat="server"></asp:DropDownList>
            </td>
            <td>&nbsp;</td>
        </tr>

        <tr>

            <%--odabir slike--%>
            <div class="container">
                <div class="form-group">
                    <asp:FileUpload ID="uplImages" Accept=".jpeg,.png,.jpg" runat="server" />
                    <br />
                    <asp:Button ID="btnAddPicture" Text="Add picture" runat="server" CssClass="btn btn-primary" BackColor="#FFBFCD" BorderColor="#FFBFCD" ForeColor="Black" OnClick="btnAddPicture_Click" />
                </div>
            </div>

            <%--prikazivanje odabrane slike--%>
            <asp:Repeater ID="rptApartmentPictures" runat="server">
                <ItemTemplate>
                    <div>
                        <img src="<%# Eval(nameof(RwaLib.Models.ApartmentPicture.Path)) %>" style="width: 400px; height: 300px; padding:10px">
                        <asp:Button Text="Representative" ID="btnRepresentative" OnClick="btnRepresentative_Click" CommandArgument="<%# Eval(nameof(RwaLib.Models.ApartmentPicture.Id)) %>" CssClass="btn btn-primary" BackColor="#FFBFCD" BorderColor="#FFBFCD" ForeColor="Black" runat="server" />
                        <asp:Button Text="Delete" ID="btnShowDeleteImageModal" OnClick="btnShowDeleteImageModal_Click" CommandArgument="<%# Eval(nameof(RwaLib. Models.ApartmentPicture.Id)) %>" CssClass="btn btn-danger" BackColor="#FFBFCD" BorderColor="#FFBFCD" ForeColor="Black" runat="server" />
                        <br />
                    </div>
                </ItemTemplate>
            </asp:Repeater>



            <%--Popup za brisanje slike--%>

            <asp:Panel ID="pnlShowDeleteImgModal" runat="server" Visible="false">
                <div class="modal" style="display: block" tabindex="-1" role="dialog">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title">Image delete</h5>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="CloseModal()">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <p>Do you want to delete this picture?</p>
                            </div>
                            <div class="modal-footer">
                                <asp:Button class="btn btn-primary" ID="btnDeleteImage" runat="server" Text="Delete" BackColor="#FFBFCD" BorderColor="#FFBFCD" ForeColor="Black" OnClick="btnDeleteImage_Click" />
                                <button type="button" onclick="CloseModal()" class="btn btn-secondary" data-dismiss="modal">Close</button>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <br />
            <br />
            <br />
        </div>

        <div class="col-md-6">

            <td class="auto-style14">
                <asp:Button ID="btnAddApartment" runat="server" Text="Add apartment" BackColor="#FFBFCD" BorderColor="#FFBFCD" ForeColor="Black"  OnClick="btnAddApartment_Click" CssClass="accordion-header" />
            </td>
        </tr>
        <tr>
            <td class="auto-style3">
                <asp:Button ID="btnReturnToApt" runat="server" Text="Return to previous page" Width="197px" BackColor="#FFBFCD" BorderColor="#FFBFCD" ForeColor="Black" OnClick="btnReturnToApt_Click"/>
            </td>
          <%--  <td class="auto-style8">
                <asp:Button ID="btnReturn" runat="server" OnClick="btnReturn_Click" BackColor="#FFBFCD" BorderColor="#FFBFCD" ForeColor="Black"  Text="Page with list of apartments" Width="213px" Height="23px" CssClass="auto-style15" />
            </td>--%>
            <td>
                &nbsp;</td>
        </tr>
    </table>

</asp:Content>
