<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/SystemAdmin.Master" AutoEventWireup="true" CodeBehind="UserInfo.aspx.cs" Inherits="_7302AccountingNote.SystemAdmin.UserInfo.UserInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
            <tr>
                <td colspan="2">
                    <h3>個人資訊</h3>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <th>帳號</th>
                            <td>
                                <asp:Literal runat="server" ID="ltAccount"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th>姓名</th>
                            <td>
                                <asp:Literal runat="server" ID="ltName"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th>Email</th>
                            <td>
                                <asp:Literal runat="server" ID="ltEmail"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th>目前帳號等級</th>
                            <td>
                                <asp:Literal runat="server" ID="ItUserLevel"></asp:Literal>
                            </td>
                        </tr>
                    </table><br />
                    <asp:Button runat="server" ID="btnLogout" Text="登出" OnClick="btnLogout_Click"/>
                </td>
            </tr>
        </table>
</asp:Content>
