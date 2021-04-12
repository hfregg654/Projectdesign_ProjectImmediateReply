<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucCrud.ascx.cs" Inherits="ProjectImmediateReply.ucCRUD" %>


<input type="hidden" id="abc" runat='server' />
<div id="app">
    <v-app>
				<v-content>
					<v-container>
						<v-data-table :headers="headers" :items="CrudItem" sort-by="calories" class="elevation-1">
							<template v-slot:top>
								<v-toolbar flat>
									<v-toolbar-title><asp:Literal ID="LiteralCrudName" runat="server" Text=""></asp:Literal></v-toolbar-title>
									<%--<v-divider class="mx-4" inset vertical>DDDDDD</v-divider>--%>
									<v-spacer></v-spacer>
									<v-dialog v-model="dialog" max-width="500px">
										<template v-slot:activator="{ on, attrs }">
											<v-btn color="primary" dark class="mb-2" v-bind="attrs" v-on="on">
												New Item
											</v-btn>
										</template>
										<v-card>
											<v-card-title>
												<span class="headline">{{ formTitle }}</span>
											</v-card-title>

											<v-card-text>
												<v-container>
													<v-row>
														<v-col cols="12" sm="6" md="4">
															<v-text-field v-model="editedItem.Account"
																label="Account"></v-text-field>
														</v-col>
														<v-col cols="12" sm="6" md="4">
															<v-text-field v-model="editedItem.Privilege"
																label="Privilege"></v-text-field>
														</v-col>
														<v-col cols="12" sm="6" md="4">
															<v-text-field v-model="editedItem.ClassNumber" label="ClassNumber">
															</v-text-field>
														</v-col>
														<v-col cols="12" sm="6" md="4">
															<v-text-field v-model="editedItem.Name" label="Name">
															</v-text-field>
														</v-col>
														<v-col cols="12" sm="6" md="4">
															<v-text-field v-model="editedItem.License"
																label="License"></v-text-field>
														</v-col>
													</v-row>
												</v-container>
											</v-card-text>

											<v-card-actions>
												<v-spacer></v-spacer>
												<v-btn color="blue darken-1" text @click="close">
													Cancel
												</v-btn>
												<v-btn color="blue darken-1" text @click="save">
													Save
												</v-btn>
											</v-card-actions>
										</v-card>
									</v-dialog>
									<v-dialog v-model="dialogDelete" max-width="500px">
										<v-card>
											<v-card-title class="headline">Are you sure you want to delete this item?
											</v-card-title>
											<v-card-actions>
												<v-spacer></v-spacer>
												<v-btn color="blue darken-1" text @click="closeDelete">Cancel</v-btn>
												<v-btn color="blue darken-1" text @click="deleteItemConfirm">OK</v-btn>
												<v-spacer></v-spacer>
											</v-card-actions>
										</v-card>
									</v-dialog>
								</v-toolbar>
							</template>
							<template v-slot:item.actions="{ item }">
								<v-icon small class="mr-2" @click="editItem(item)">
									mdi-pencil
								</v-icon>
								<v-icon small @click="deleteItem(item)">
									mdi-delete
								</v-icon>
                                
							</template>
							<template v-slot:no-data>
								<v-btn color="primary" @click="initialize">
									Reset
								</v-btn>
							</template>
						</v-data-table>
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
            dialog: false,
            dialogDelete: false,
            headers: [{
                text: 'Account',
                align: 'start',
                sortable: false,
                value: 'Account',
            },
            {
                text: 'Privilege',
                value: 'Privilege'
            },
            {
                text: 'ClassNumber',
                value: 'ClassNumber'
            },
            {
                text: 'Name',
                value: 'Name'
            },
            {
                text: 'License',
                value: 'License'
            },
            {
                text: 'Actions',
                value: 'actions',
                sortable: false
            },
            ],
            CrudItem: [],
            editedIndex: -1,
            editedItem: {
                Account: '',
                Privilege: '',
                ClassNumber: '',
                Name: '',
                License: '',
            },
            defaultItem: {
                Account: '',
                Privilege: '',
                ClassNumber: '',
                Name: '',
                License: '',
            },

        }),
        computed: {
            formTitle() {
                return this.editedIndex === -1 ? 'New Item' : 'Edit Item'
            },
        },

        watch: {
            dialog(val) {
                val || this.close()
            },
            dialogDelete(val) {
                val || this.closeDelete()
            },
        },

        created() {
            this.initialize()
        },


        methods: {
            initialize() {
                var ar = '<%=GetSomething()%>';
                var ars = ar.split(',');
                for (var i = 0; i < <%=GetDBLength()%>; i += 5) {
                    this.CrudItem.push({
                        Account: String(ars[i]),
                        Privilege: String(ars[i + 1]),
                        ClassNumber: String(ars[i + 2]),
                        Name: String(ars[i + 3]),
                        License: String(ars[i + 4])
                    })
                }
            },

            editItem(item) {
                this.editedIndex = this.CrudItem.indexOf(item)
                this.editedItem = Object.assign({}, item)
                this.dialog = true
            },

            deleteItem(item) {
                this.editedIndex = this.CrudItem.indexOf(item)
                this.editedItem = Object.assign({}, item)
                this.dialogDelete = true
            },

            deleteItemConfirm() {
                this.CrudItem.splice(this.editedIndex, 1)
                this.closeDelete()
            },

            close() {
                this.dialog = false
                this.$nextTick(() => {
                    this.editedItem = Object.assign({}, this.defaultItem)
                    this.editedIndex = -1
                })
            },

            closeDelete() {
                this.dialogDelete = false
                this.$nextTick(() => {
                    this.editedItem = Object.assign({}, this.defaultItem)
                    this.editedIndex = -1
                })
            },

            save() {
                if (this.editedIndex > -1) {
                    Object.assign(this.CrudItem[this.editedIndex], this.editedItem)
                } else {
                    this.CrudItem.push(this.editedItem)
                }
                this.close()
            },
        },
    })




</script>
