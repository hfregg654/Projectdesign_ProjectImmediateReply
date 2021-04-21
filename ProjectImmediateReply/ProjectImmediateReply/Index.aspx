<%@ Page Title="" Language="C#" MasterPageFile="~/ImmediateReplyInSide.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="ProjectImmediateReply.Index1" %>

<%@ Register Src="~/ucCrud.ascx" TagPrefix="uc1" TagName="ucCrud" %>
<%@ Register Src="~/ucForgetpassword.ascx" TagPrefix="uc1" TagName="ucForgetpassword" %>
<%@ Register Src="~/ucRegistered.ascx" TagPrefix="uc1" TagName="ucRegistered" %>
<%@ Register Src="~/ucCreateClass.ascx" TagPrefix="uc1" TagName="ucCreateClass" %>
<%@ Register Src="~/ucUpdateInfo.ascx" TagPrefix="uc1" TagName="ucUpdateInfo" %>





<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div runat="server" id="divinnerplace">
        
        <uc1:ucCrud runat="server" ID="ucCrud" Visible="false" />
        <uc1:ucCreateClass runat="server" ID="ucCreateClass" Visible="false" />
        <uc1:ucUpdateInfo runat="server" ID="ucUpdateInfo" Visible="false" />
    </div>
   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script src="assets/js/jquery-3.6.0.min.js"></script>
    <script src="assets/js/ImmediateReplyAJAX.js"></script>
    <div runat="server" id="divJS">
    </div>
</asp:Content>
