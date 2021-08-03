<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/SystemAdmin.Master" AutoEventWireup="true" CodeBehind="UserDetail.aspx.cs" Inherits="_7302AccountingNote.SystemAdmin.AccountManagement.UserDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td colspan="2">
                <h3>會員管理</h3>
            </td>
        </tr>
        <tr>
            <td>帳號
                    <asp:Literal ID="ltAccount" runat="server"></asp:Literal>
                <br />
                姓名
                        <asp:TextBox ID="txtName" runat="server" TextMode="Number"></asp:TextBox>
                <br />
                Email
                        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                <br />
                等級
                        <asp:Literal ID="ltUserLevel" runat="server"></asp:Literal>
                <br />
                建立時間
                        <asp:Literal ID="ltCreateDate" runat="server"></asp:Literal>

                <asp:Button ID="btnSave" runat="server" Text="儲存" OnClick="btnSave_Click" />
                &nbsp;&nbsp;
                    <asp:Button ID="btnDelete" runat="server" Text="刪除" OnClick="btnDelete_Click" />
                <%--CommandArgument='<%# Eval("ID") %>' CommandName="DeleteAccounting--%>
                <br />
                <asp:Literal ID="ltmsg" runat="server"></asp:Literal>

            </td>
        </tr>
    </table>
</asp:Content>
