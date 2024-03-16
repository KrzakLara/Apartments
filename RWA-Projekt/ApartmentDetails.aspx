<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPage.Master" AutoEventWireup="true" CodeBehind="ApartmentDetails.aspx.cs" Inherits="RWA_Projekt.ApartmentDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">

        <h3>Apartment details</h3>

        <div class="mb-3">
            <asp:Label ID="lblName" class="form-label" runat="server" Text="Name:"></asp:Label>
            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
        </div>

        <div class="mb-3">
            <asp:Label ID="lblOwnerName" class="form-label" runat="server" Text="Owner name:"></asp:Label>
            <asp:TextBox ID="txtOwnerName" runat="server"></asp:TextBox>
        </div>

        <div class="mb-3">
            <asp:Label ID="lblPrice" class="form-label" runat="server" Text="Price:"></asp:Label>
            <asp:TextBox ID="txtPrice" runat="server"></asp:TextBox>
        </div>

        <div class="mb-3">
            <asp:Label ID="lbMaxAdults" class="form-label" runat="server" Text="Max adults:"></asp:Label>
            <asp:TextBox ID="txtMaxAdults" runat="server"></asp:TextBox>
        </div>

        <div class="mb-3">
            <asp:Label ID="lbMaxChildren" class="form-label" runat="server" Text="Max children:"></asp:Label>
            <asp:TextBox ID="txtMaxChildren" runat="server"></asp:TextBox>
        </div>

        <div class="mb-3">
            <asp:Label ID="lblTotalRooms" class="form-label" runat="server" Text="Total rooms:"></asp:Label>
            <asp:TextBox ID="txtTotalRooms" runat="server"></asp:TextBox>
        </div>

        <div class="mb-3">
            <asp:Label ID="lblBeachDistance" class="form-label" runat="server" Text="Beach distance:"></asp:Label>
            <asp:TextBox ID="txtBeachDistance" runat="server"></asp:TextBox>
        </div>

        <div class="mb-3"><%-- Triba popravit list item--%>
            <asp:Label ID="lblStatus" class="form-label" runat="server" Text="Status:"></asp:Label>
            <asp:DropDownList ID="ddltype" AutoPostBack="true" runat="server">
                <asp:ListItem Value="0">--odaberite status--</asp:ListItem>
                <asp:ListItem Value="1">Zauzeto</asp:ListItem>
                <asp:ListItem Value="2">Rezervirano</asp:ListItem>
                <asp:ListItem Value="3">Slobodno</asp:ListItem>
            </asp:DropDownList>
            <br />
        </div>
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
                        <asp:Button Text="Representative" ID="btnRepresentative" OnClick="btnRepresentative_Click" CommandArgument="<%# Eval(nameof(RwaLib.Models.ApartmentPicture.Id)) %>" CssClass="btn btn-primary" BackColor="#FFBFCD" BorderColor="#FFBFCD" ForeColor="Black"  runat="server" />
                        <asp:Button Text="Delete" ID="btnShowDeleteImageModal" OnClick="btnShowDeleteImageModal_Click" CommandArgument="<%# Eval(nameof(RwaLib.Models.ApartmentPicture.Id)) %>" CssClass="btn btn-danger" BackColor="#FFBFCD" BorderColor="#FFBFCD" ForeColor="Black"  runat="server" />
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
                                <h5 class="modal-title">Picture delete</h5>
                                <button type="button" class="close" data-dismiss="modal" BackColor="#FFBFCD" BorderColor="#FFBFCD" ForeColor="Black"  aria-label="Close" onclick="CloseModal()">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <p>Do you want to delete this image?</p>
                            </div>
                            <div class="modal-footer">
                                <asp:Button class="btn btn-primary" ID="btnDeleteImage" runat="server" Text="Delete" BackColor="#FFBFCD" BorderColor="#FFBFCD" ForeColor="Black"  OnClick="btnDeleteImage_Click" />
                                <button type="button" onclick="CloseModal()" BackColor="#FFBFCD" BorderColor="#FFBFCD" ForeColor="Black"  class="btn btn-secondary" data-dismiss="modal">Close</button>
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
        <!--tagovi-->
        <asp:Repeater ID="rptApartmentDetailsTags" runat="server">
            <HeaderTemplate>
                <table id="myTable" class="table">
                    <thead>
                        <tr>
                            <th scope="col">Name</th>
                            <th scope="col">Type</th>
                            <th scope="col"></th>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><%# Eval(nameof(RwaLib.Models.Tag.Name)) %></td>
                    <td><%# Eval(nameof(RwaLib.Models.Tag.Type)) %></td>
                    <td>
                        <asp:Button ID="btnShowApartmetDetaisDeleteTagModal" CommandArgument="<%# Eval(nameof(RwaLib.Models.Tag.Id)) %>" OnClick="btnShowApartmetDetaisDeleteTagModal_Click" BackColor="#FFBFCD" BorderColor="#FFBFCD" runat="server" Text="Delete" ForeColor="Black" Font-Bold="True" />
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </tbody>
                   </table>
            </FooterTemplate>
        </asp:Repeater>

        <%--potvrda za brisanje taga (pop-up)--%>
        <asp:Panel ID="pnlShowApartmentDetailDeleteTagModal" runat="server" Visible="false">
            <div class="modal" style="display: block" tabindex="-1" role="dialog">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Tag delete</h5>
                            <button type="button" backcolor="#FFBFCD" bordercolor="#FFBFCD" forecolor="Black" class="close" data-dismiss="modal" aria-label="Close" onclick="ClosePopupModal()">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <p>Are You sure You want to delete this tag?</p>
                        </div>
                        <div class="modal-footer">
                            <asp:Button class="btn btn-primary" ID="btnDeleteApartmentDetailTag" runat="server" BackColor="#FFBFCD" BorderColor="#FFBFCD" Text="Delete" OnClick="btnDeleteApartmentDetailTag_Click" />
                            <button type="button" onclick="ClosePopupModal()" class="btn btn-secondary" backcolor="#FFBFCD" bordercolor="#FFBFCD" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>

    <%--provjera jel tag postoji--%>
    <asp:Panel ID="pnlTagPostoji" runat="server" Visible="false">
        <div role="alert" class="alter alert-danger d-flex justify-content-center">
            Tag exists!
        </div>
    </asp:Panel>

    <br />
    <%--funkcija pomocu koje zatvaram pop-up model (close)--%>
    <script>
        function ClosePopupModal() {
            $('.modal').hide();
        }
    </script>


    <%--</asp:Panel>--%>

    <asp:Button ID="btnSaveApartment" OnClick="btnSaveApartment_Click" runat="server" Text="Save" CssClass="btn btn-primary" BackColor="#FFBFCD" BorderColor="#FFBFCD" ForeColor="Black" />
    <asp:Button ID="btnReturnBack" OnClick="btnReturnBack_Click" runat="server" Text="Return to previous page" Height="34px" Width="173px" BackColor="#FFBFCD" BorderColor="#FFBFCD" ForeColor="Black" />
    <asp:Button ID="btnDelete" OnClick="btnDelete_Click" runat="server" Text="Obriši apartman" Height="34px" Width="173px" BackColor="#FFBFCD" BorderColor="#FFBFCD" ForeColor="Black" />

</asp:Content>
