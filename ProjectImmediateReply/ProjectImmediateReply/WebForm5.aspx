<%@ Page Title="" Language="C#" MasterPageFile="~/ImmediateReplayInSide.Master" AutoEventWireup="true" CodeBehind="WebForm5.aspx.cs" Inherits="ProjectImmediateReply.WebForm5" %>

<%@ Register Src="~/ucCrud.ascx" TagPrefix="uc1" TagName="ucCrud" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ucCrud runat="server" ID="ucCrud" />
</asp:Content>
