<%@ Page Language="C#"  MasterPageFile="~/TemplateMasterAdmin_PMS.master" MaintainScrollPositionOnPostback="true"  AutoEventWireup="true"  CodeFile="ParkDetailsAndFascility.aspx.cs" Inherits="ParkDetailsAndFascility"  EnableEventValidation="false" ValidateRequest="false"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="assets/css/CalendarStyle.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/chartjs-plugin-datalabels"></script>
    <div class="main-content">
        <div class="page-content">
            <div class="container-fluid">
                 <div class="row">
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Adopted Park Detail and Fascilities</h4>
                                    </div>
                                    <!-- end card header -->
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-12">
                                                   <div class="clearfix" id="dtOptions" runat="server">
                                                        <div class="pull-right tableTools-container"></div>
                                                    </div>
                                                 <div style="overflow: auto">
                                                <asp:GridView runat="server" ID="grdPost" CssClass="display table table-bordered"   AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPost_PreRender">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr. No.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="District" DataField="Circle_Name" />
                                                        <asp:BoundField HeaderText="ULB Name" DataField="Division_Name" />
                                                        <asp:BoundField HeaderText="Adopted Park Name" DataField="AdoptedParkName" />
                                                        <asp:BoundField HeaderText="Name of Tree Planted" DataField="Name_of_Tree_Planted" />
                                                        <asp:BoundField HeaderText="Species of Planted Trees" DataField="Species_of_Planted_Trees" />
                                                        <asp:BoundField HeaderText="Facility Added" DataField="Facility_Added" />
                                                        <asp:BoundField HeaderText="Facility Available " DataField="Facility_Available" />                                            
                                                        <asp:BoundField HeaderText="No of Gardener " DataField="Noof_Gardener" />                                            
                                                        <asp:BoundField HeaderText="Frequency of Maintenance " DataField="Frequency_of_Maintenance" />                                            
                                                        <asp:BoundField HeaderText="Events Organised in Parks " DataField="Events_Organised_in_Parks" />                                            
                                                        <%--<asp:BoundField HeaderText="GeotaggedPhotographs " DataField="GeotaggedPhotographs" />--%>   
                                                        <asp:TemplateField HeaderText="Geotagged Photographs">
                                                             <ItemTemplate>
                                                                 <asp:Label ID="lblNoDocument" runat="server" Text="No Document" Visible='<%# string.IsNullOrEmpty(Eval("Geotagged_Photos") as string) %>'></asp:Label>
                                                                 <asp:HyperLink ID="hlDocument" runat="server" NavigateUrl='<%# Eval("Geotagged_Photos") %>' Text="Doc" Visible='<%# !string.IsNullOrEmpty(Eval("Geotagged_Photos") as string) %>' Target="_blank"></asp:HyperLink>
                                                             </ItemTemplate>
                                                         </asp:TemplateField>
                                                           <asp:TemplateField HeaderText="Edit">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btnEdit" Width="20px" Height="20px" 
                                                                    OnClick="btnEdit_Click" 
                                                                    ImageUrl="~/assets/images/edit_btn.png" 
                                                                    runat="server" />
                                                                <asp:HiddenField ID="hdnParkDetailFacilityId" 
                                                                    Value='<%# Eval("ParkDetailFacilityId") %>' 
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                       
<%--                                                        <asp:TemplateField HeaderText="Edit">
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnEdit" runat="server" Text='Edit' CommandName="EditAdoptedPark" OnCommand="Edit_Command" CommandArgument='<%# Eval("AdoptedParkId") %>' CssClass="btn btn-primary drill_btn" />
                                                            </ItemTemplate>                                                             
                                                        </asp:TemplateField>--%>
                                                       <%--<asp:TemplateField HeaderText="Delete">
                                                        <ItemTemplate>
                                                             <asp:Button ID="btnDelete" OnClientClick="return confirm('Are You Sure You Want to Delete this Item?')" runat="server" Text='Delete' CommandName="DeleteAnnualAction" OnCommand="btnDelete_Command" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-danger drill_btn" />
                                                         </ItemTemplate>

                                                        </asp:TemplateField>--%>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <tr>
                                                            <td colspan="12" style="text-align: center; font-weight: bold; color: red;">No records found</td>
                                                        </tr>
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </div>
                                         </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
             </div>
        </div>
    </div>
</asp:Content>