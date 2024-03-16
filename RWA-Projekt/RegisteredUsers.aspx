<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPage.Master" AutoEventWireup="true" CodeBehind="RegisteredUsers.aspx.cs" Inherits="RWA_Projekt.RegisteredUsers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">

    <asp:Repeater ID="rptUsers" runat="server" >
                    <HeaderTemplate>
                        <table id="myTable" class=" table table-striped" style="background-color:#FFBFCD">
                            <thead>
                                <tr>
                                    <th scope="col">Id</th>
                                    <th scope="col">Username</th>
                                    <th scope="col">Phone number</th>
                                    <th scope="col">E-mail</th>
                                     <th scope="col">Address</th>
                                    <th scope="col">CreatedAt</th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <th scope="row"><%# Eval(nameof(RwaLib.Models.User.Id)) %></th>
                            <td><%# Eval(nameof(RwaLib.Models.User.UserName)) %></td>
                            <td><%# Eval(nameof(RwaLib.Models.User.PhoneNumber)) %></td>
                            <td><%# Eval(nameof(RwaLib.Models.User.Email)) %></td>
                            <td><%# Eval(nameof(RwaLib.Models.User.Address)) %></td>
                            <td><%# Eval(nameof(RwaLib.Models.User.CreatedAt)) %></td>
                            
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                    </table>
                    </FooterTemplate>
                </asp:Repeater>
</asp:Content>
