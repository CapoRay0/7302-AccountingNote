﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/SystemAdmin.Master" AutoEventWireup="true" CodeBehind="AccountingList.aspx.cs" Inherits="_7302AccountingNote.SystemAdmin.AccountingRecord.AccountingList" %>

<%@ Register Src="~/UserControls/Pager.ascx" TagPrefix="uc1" TagName="Pager" %>


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
            <td>
                <asp:Button ID="btnCreate" runat="server" Text="Add" OnClick="btnCreate_Click" />
                <asp:Label ID="Label1" runat="server" Text="用來  調距離的" Visible="false"></asp:Label>
                <asp:TextBox ID="TotalAccount" runat="server" ReadOnly="true" Width="190px"></asp:TextBox><br />
                <asp:Button ID="btnAllSpend" runat="server" Text="全部支出" OnClick="btnAllSpend_Click" />
                <asp:Button ID="btnAllReceive" runat="server" Text="全部收入" OnClick="btnAllReceive_Click"/>
                <asp:Label ID="lblTotalAmount" runat="server" Text=""></asp:Label>

                <asp:GridView ID="gvAccountingList" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvAccountingList_RowDataBound" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>

                        <asp:BoundField HeaderText="建立日期" DataField="CreateDate" DataFormatString="{0:yyyy-MM-dd}" />

                        <asp:TemplateField HeaderText="收 / 支">
                            <ItemTemplate>

                                <asp:Label ID="lblActType" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField HeaderText="金額" DataField="Amount" />

                        <asp:BoundField HeaderText="標題" DataField="Caption" />
                        <asp:TemplateField HeaderText="編輯">
                            <ItemTemplate>
                                <a href="/SystemAdmin/AccountingRecord/AccountingDetail.aspx?ID=<%# Eval("ID") %>">修改</a>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>

                <uc1:Pager runat="server" id="Pager" PageSize="5" Url="/SystemAdmin/AccountingRecord/AccountingList.aspx" />

                <asp:PlaceHolder ID="plcNoData" runat="server" Visible="false">
                    <p style="color: red; background-color: cornflowerblue">
                        流水帳管理表目前無資料
                    </p>
                </asp:PlaceHolder>
                <asp:Literal ID="ltmsg" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
</asp:Content>
