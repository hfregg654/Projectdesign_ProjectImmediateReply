<%@ Page Title="" Language="C#" MasterPageFile="~/ImmediateReplyInSide.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="ProjectImmediateReply.Index1" %>

<%@ Register Src="~/ucCrud.ascx" TagPrefix="uc1" TagName="ucCrud" %>
<%@ Register Src="~/ucCreateClass.ascx" TagPrefix="uc1" TagName="ucCreateClass" %>
<%@ Register Src="~/ucUpdateInfo.ascx" TagPrefix="uc1" TagName="ucUpdateInfo" %>
<%@ Register Src="~/ucCreateProject.ascx" TagPrefix="uc1" TagName="ucCreateProject" %>
<%@ Register Src="~/ucSeeGrade.ascx" TagPrefix="uc1" TagName="ucSeeGrade" %>
<%@ Register Src="~/ucAssignTeam.ascx" TagPrefix="uc1" TagName="ucAssignTeam" %>








<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div runat="server" id="divinnerplace">
        
        <uc1:ucCrud runat="server" ID="ucCrud" Visible="false" />
        <uc1:ucCreateClass runat="server" ID="ucCreateClass" Visible="false" />
        <uc1:ucUpdateInfo runat="server" ID="ucUpdateInfo" Visible="false" />
        <uc1:ucCreateProject runat="server" ID="ucCreateProject" Visible="false"/>
        <uc1:ucSeeGrade runat="server" ID="ucSeeGrade" Visible="false" />
        <uc1:ucAssignTeam runat="server" ID="ucAssignTeam" Visible="false" />
    </div>
   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div runat="server" id="divJS">  <%--插入JS用--%>
    </div>
</asp:Content>
