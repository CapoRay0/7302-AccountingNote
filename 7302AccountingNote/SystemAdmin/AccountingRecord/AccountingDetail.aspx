<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/SystemAdmin.Master" AutoEventWireup="true" CodeBehind="AccountingDetail.aspx.cs" Inherits="_7302AccountingNote.SystemAdmin.AccountingRecord.AccountingDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
            <tr>
                <td colspan="2">
                    <h3>流水帳管理</h3>
                </td>
            </tr>
            <tr>
                <td>收 / 支
                    <asp:DropDownList ID="ddlActType" runat="server">
                        <asp:ListItem Value="0">支出</asp:ListItem>
                        <asp:ListItem Value="1">收入</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    金額
                        <asp:TextBox ID="txtAmount" runat="server" TextMode="Number"></asp:TextBox>
                    <br />
                    標題
                        <asp:TextBox ID="txtCaption" runat="server"></asp:TextBox>
                    <br />
                    說明
                        <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine"></asp:TextBox>
                    <br />
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
