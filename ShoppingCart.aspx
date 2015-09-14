<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ShoppingCart.aspx.cs" Inherits="Pages_ShoppingCart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="pnlShoppingCart" runat="server">

    </asp:Panel>

    <table style="width:50%">
        <tr>
            <td style="text-align: left"><b>Total: </b></td>
            <td><asp:Literal ID="litTotal" runat="server" Text="" /></td>
        </tr>

        <tr>
            <td style="text-align: left"><b>GST: </b></td>
            <td><asp:Literal ID="litVat" runat="server" Text="" /></td>
        </tr>

        <tr>
            <td style="text-align: left"><b>Shipping: </b></td>
            <td>$ 15</td>
        </tr>

        <tr>
            <td style="text-align: left"><b>Total Amount: </b></td>
            <td><asp:Literal ID="litTotalAmount" runat="server" Text="" /></td>
        </tr>

        <tr>
            <td>
                <asp:LinkButton ID="lnkContinue" runat="server" PostBackUrl="~/Index.aspx" Text="Contiue Shopping" CssClass="btn btn-primary btn-sm"/>
                OR
                <asp:Button ID="btnCheckOut" runat="server" Text="Continue to Checkout" PostBackUrl="Success.aspx"
                    CssClass="btn btn-primary" />
            </td>
        </tr>
    </table>

</asp:Content>

