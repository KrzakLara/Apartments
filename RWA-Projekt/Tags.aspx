<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPage.Master" AutoEventWireup="true" CodeBehind="Tags.aspx.cs" Inherits="RWA_Projekt.Tags" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">


    <%--lista tagova--%>
    <asp:Panel ID="pnlTagsList" runat="server">
        <div class="container">
            <div class="col-md">
                <asp:Repeater ID="rptTags" runat="server">
                    <HeaderTemplate>
                        <table id="myTable" class="table">
                            <thead>
                                <tr>
                                    <th scope="col">Id</th>
                                    <th scope="col">Name</th>
                                    <th scope="col">Tagged apartments</th>
                                    <th scope="col"></th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <th scope="row"><%# Eval(nameof(RwaLib.Models.Tag.Id)) %></th>
                            <td><%# Eval(nameof(RwaLib.Models.Tag.Name)) %></td>
                            <td><%# Eval(nameof(RwaLib.Models.Tag.TaggedApartments)) %> </td>
                            <td>
                                <asp:Button ID="btnShowDeleteModal" Visible='<%#Convert.ToInt32(Eval("TaggedApartments"))==0%>' CommandArgument="<%# Eval(nameof(RwaLib.Models.Tag.Id)) %>" OnClick="btnShowDeleteModal_Click" BackColor="#FFBFCD" BorderColor="#FFBFCD" runat="server" Text="Delete" ForeColor="Black" Font-Bold="True" />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                    </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>

            <%--potvrda za brisanje taga (pop-up)--%>
            <asp:Panel ID="pnlShowDeleteModal" runat="server" Visible="false">
                <div class="modal" style="display: block" tabindex="-1" role="dialog">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title">Tag delete</h5>
                                <button type="button" BackColor="#FFBFCD" BorderColor="#FFBFCD" ForeColor="Black" class="close" data-dismiss="modal" aria-label="Close" onclick="ClosePopupModal()">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <p>Are You sure You want to delete this tag?</p>
                            </div>
                            <div class="modal-footer">
                                <asp:Button class="btn btn-primary" ID="btnDeleteTag" runat="server" BackColor="#FFBFCD" BorderColor="#FFBFCD" Text="Delete" OnClick="btnDeleteTag_Click" />
                                <button type="button" onclick="ClosePopupModal()" class="btn btn-secondary" BackColor="#FFBFCD" BorderColor="#FFBFCD" data-dismiss="modal">Close</button>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>


           <%--dodavanje taga--%>
            <asp:Panel ID="pnlAddTag" runat="server">
                <fieldset>
                    <h3>Add new tag</h3>
                    <div class="mb-3">
                        <asp:Label ID="lblType" class="form-label" runat="server" Text="Tip:"></asp:Label>
                        <asp:DropDownList ID="ddlType" AutoPostBack="true" runat="server"></asp:DropDownList>
                    </div>

                    <div class="mb-3">
                        <asp:Label ID="lblName" class="form-label" runat="server" Text="Ime:"></asp:Label>
                        <asp:TextBox ID="tbName" runat="server"></asp:TextBox>
                    </div>

                    <div class="mb-3">
                        <asp:Button class="btn btn-primary" ID="btnAddTag" runat="server" BackColor="#FFBFCD" BorderColor="#FFBFCD" ForeColor="Black" Text="Dodaj" OnClick="btnAddTag_Click" />
                    </div>
                </fieldset>
            </asp:Panel>

        </div>
    </asp:Panel>

    <%--provjera jel tag postoji--%>
    <asp:Panel ID="pnlTagPostoji" runat="server" Visible="false">
        <div role="alert" class="alter alert-danger d-flex justify-content-center">
            Tag exists!
        </div>
    </asp:Panel>

    <br />

    <%--funkcija pomocu koje zatvaras pop-up model (close botun)--%>
    <script>
        function ClosePopupModal() {
            $('.modal').hide();
        }
    </script>


</asp:Content>
