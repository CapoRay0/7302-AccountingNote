<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="_7302AccountingNote.DefaultPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table border="0" align="center" valign="center">
            <tr>
                <td width="100">
                    <asp:Label ID="lblFstRec" runat="server" Text="初次記帳"></asp:Label><br /><br />
                    <asp:Label ID="lblLstRec" runat="server" Text="最後記帳"></asp:Label><br /><br />
                    <asp:Label ID="lblTotalAcc" runat="server" Text="記帳數量"></asp:Label><br /><br />
                    <asp:Label ID="lblUserNum" runat="server" Text="會員數"></asp:Label><br /><br />
                </td>

                <td width="200" align="center">
                    <asp:Literal ID="ltFstRec" runat="server"></asp:Literal><br /><br />
                    <asp:Literal ID="ltLstRec" runat="server"></asp:Literal><br /><br />
                    <asp:Literal ID="ltTotalAcc" runat="server"></asp:Literal><br /><br />
                    <asp:Literal ID="ltUserNum" runat="server"></asp:Literal><br /><br />
                </td>
            </tr>

            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="btnLogin" runat="server" Text="登入系統" OnClick="btnLogin_Click" />
                </td>
            </tr>
        </table>

</asp:Content>
