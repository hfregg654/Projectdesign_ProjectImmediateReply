<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebFormHTMLTest.aspx.cs" Inherits="ProjectImmediateReply.WebFormHTMLTest" %>

<%@ Register Src="~/ucCrud.ascx" TagPrefix="uc1" TagName="ucCrud" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%-- <link href="https://fonts.googleapis.com/css?family=Roboto:100,300,400,500,700,900" rel="stylesheet">
    <link href="https://cdn.jsdelivr.net/npm/@mdi/font@4.x/css/materialdesignicons.min.css" rel="stylesheet">
    <link href="https://cdn.jsdelivr.net/npm/vuetify@2.x/dist/vuetify.min.css" rel="stylesheet">
    <meta name="viewport"
        content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no, minimal-ui">--%>
    <link href="https://fonts.googleapis.com/css?family=Roboto:100,300,400,500,700,900" rel="stylesheet"/>
    <link href="https://cdn.jsdelivr.net/npm/@mdi/font@4.x/css/materialdesignicons.min.css" rel="stylesheet"/>
    <link href="https://cdn.jsdelivr.net/npm/vuetify@2.x/dist/vuetify.min.css" rel="stylesheet"/>
    <meta name="viewport"
        content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no, minimal-ui"/>
</head>
<body>
    <form id="form1" runat="server">
        <%--  <div id="app">
            <v-app>
				<v-content>
					<v-container>Hello world</v-container>
					<v-container fluid>
						<v-row align="center">
							<v-col class="d-flex" cols="12" sm="6">
								<v-select :items="items" label="Standard"></v-select>
							</v-col>

							<v-col class="d-flex" cols="12" sm="6">
								<v-select :items="items" filled label="Filled style"></v-select>
							</v-col>

							<v-col class="d-flex" cols="12" sm="6">
								<v-select :items="items" label="Outlined style" outlined></v-select>
							</v-col>

							<v-col class="d-flex" cols="12" sm="6">
								<v-select :items="items" label="Solo field" solo></v-select>
							</v-col>
						</v-row>
					</v-container>
					<v-container>
						<div class="text-center">
							<v-btn class="ma-2" outlined color="indigo">
								Outlined Button
							</v-btn>
							<v-btn class="ma-2" outlined fab color="teal">
								<v-icon>mdi-format-list-bulleted-square</v-icon>
							</v-btn>
							<v-btn class="ma-2" outlined large fab color="indigo">
								<v-icon>mdi-pencil</v-icon>
							</v-btn>
						</div>
					</v-container>
					<v-container>
					</v-container>
				</v-content>
			</v-app>
        </div>




        <script src="https://cdn.jsdelivr.net/npm/vue@2.x/dist/vue.js"></script>
        <script src="https://cdn.jsdelivr.net/npm/vuetify@2.x/dist/vuetify.js"></script>
        <script>
            new Vue({
                el: '#app',
                vuetify: new Vuetify(),
                data: () => ({
                    items: ['Foo', 'Bar', 'Fizz', 'Buzz'],
                }),
            })
        </script>--%>


        <uc1:ucCrud runat="server" id="ucCrud" />

      
    </form>
</body>
</html>
