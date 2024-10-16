<%@ Page Language="C#"  MasterPageFile="~/TemplateMasterAdmin_PMS.master" MaintainScrollPositionOnPostback="true"  AutoEventWireup="true" CodeFile="ParkAdoptionStatusReport.aspx.cs" Inherits="ParkAdoptionStatusReport" EnableEventValidation="false" ValidateRequest="false"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="assets/css/CalendarStyle.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/chartjs-plugin-datalabels"></script>
    <div class="main-content">
        <div class="page-content">
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
                    </cc1:ToolkitScriptManager>
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-12">
                                <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                                    <h4 class="mb-sm-0">Park Adoption Status And progress Report</h4>
                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                            <li class="breadcrumb-item">Construction Of Parks</li>
                                            <li class="breadcrumb-item active">Park Adoption Status And progress Report</li>
                                        </ol>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Park Adoption Status And Progress Report</h4>
                                    </div>
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-4">
                                                    <div class="col-xxl-3 col-md-6">
                                                    <div id="divCircle" runat="server">
                                                        <asp:Label ID="lblCircleH" runat="server" Text="District" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlCircle" runat="server" CssClass="form-select" AutoPostBack="true" ></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divFY" runat="server">
                                                        <asp:Label ID="lblFY" runat="server" Text="Financial Year" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlFY" runat="server" CssClass="form-select"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                 <div class="col-xxl-3 col-md-6">
                                                    <div id="divMonth" runat="server">
                                                        <asp:Label ID="lblMonth" runat="server" Text="Month" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-select"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-xxl-3 col-md-6">
                                                    <div>
                                                        <label class="d-block">&nbsp;</label>
                                                        <asp:Button ID="btnSearch" Text="Search" OnClick="btnSearch_Click" runat="server" CssClass="btn bg-success text-white"></asp:Button>
                                                        <asp:Button ID="btnCancel" Text="Cancel / Reset" OnClick="btnCancel_Click" runat="server" CssClass="btn bg-secondary text-white"></asp:Button>
                                                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                                                        <asp:HiddenField ID="hfWorkProposalId" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                           <%-- <div class="row gy-4">
                                                <div class="col-xxl-2 offset-xxl-10 col-md-6">
                                                    <div>
                                                        <label class="d-block">&nbsp;</label>
                                                        <asp:Button ID="btnSearch" Text="Search" OnClick="btnSearch_Click" runat="server" CssClass="btn bg-success text-white"></asp:Button>
                                                        <asp:Button ID="btnCancel" Text="Cancel / Reset" OnClick="btnCancel_Click" runat="server" CssClass="btn bg-secondary text-white"></asp:Button>
                                                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                                                        <asp:HiddenField ID="hfWorkProposalId" runat="server" />
                                                    </div>
                                                </div>
                                            </div>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    
                    <%--<div runat="server" visible="true" id="divData" class="tblheader"  style="overflow: auto">
                        <div class="row">
                            <div class="col-lg-12">
                                
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Park Adoption Status And Progress Report</h4>
                                    </div>
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-12">
                                                
                                                <div class="clearfix" id="dtOptions" runat="server">
                                                    <div class="pull-right tableTools-container">
                                                    </div>
                                                </div>
                                                <div style="overflow: auto">
                                                    <table class="display table table-bordered">
    <thead>
        <tr>
            <th>Sr. No.</th>
            <th>ULB Name</th>
            <th>Ward</th>
            <th>No. of Parks Adopted</th>
            <th>Name of Park Adopted</th>
            <th>NO. of park adoption in process</th>
            <th>Name of CSR/PPP/NGO/RWA others</th>
            <th>Contact Detail of the CSR/PPP/NGO/RWA/Others</th>
            <th>Geo-tagged Photos of the Park</th>
            <th>Download KML</th>
        </tr>
    </thead>
    <tbody>
        <!-- Example static rows -->
        <tr>
            <td>1</td>
            <td>ULB Name A</td>
            <td>Ward Alpha</td>
            <td>5</td>
            <td>Name of Park Adopted</td>
            <td>20</td>
            <td>Name of CSR/PPP/NGO/RWA others</td>
            <td>Contact Detail of the CSR/PPP/NGO/RWA/Others</td>
            <td>Geo-tagged Photos of the Park</td>
            <td><span class="btn btn-primary drill_btn">Download</span></td>
        </tr>
        <tr>
            <td>2</td>
            <td>ULB B</td>
            <td>Ward Beta</td>
            <td>7</td>
            <td>Name of Park Adopted</td>
            <td>15</td>
            <td>Name of CSR/PPP/NGO/RWA others</td>
            <td>Contact Detail of the CSR/PPP/NGO/RWA/Others</td>
            <td>Geo-tagged Photos of the Park</td>
            <td><span class="btn btn-primary drill_btn">Download</span></td>
        </tr>
        <!-- Add more rows as needed -->
    </tbody>
  <%--  <tfoot>
        <tr>
            <td colspan="3" style="text-align: center;">Total:</td>
            <td><label id="lblTotalNoOfProposal">12</label></td>
            <td><label id="lblTotalAmount"></label></td>
            <td><label id="lblApprovedProposal">35</label></td>
            <td><label id="lblHoldProposal"></label></td>
            <td><label id="lblPendingProposal"></label></td>
        </tr>
    </tfoot>
</table>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>--%>
                        
                        </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSearch" />
                    <asp:PostBackTrigger ControlID="ddlCircle" />
                    <asp:PostBackTrigger ControlID="ddlFY" />
                    <asp:PostBackTrigger ControlID="ddlMonth" />
                </Triggers>
            </asp:UpdatePanel>
            <div class="container-fluid">
                 <div class="row">
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Projects For Annual Action Plan</h4>
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
                                                        <asp:BoundField HeaderText="ULB Name" DataField="Division_Name" />
                                                        <asp:BoundField HeaderText="District" DataField="Circle_Name" />
                                                        <asp:BoundField HeaderText="Inprocess Park" DataField="NoOfAdoptionInprocessPark" />
                                                        <asp:BoundField HeaderText="NoOfParkAdopted" DataField="NoOfParkAdopted" />
                                                        <asp:BoundField HeaderText="NoOfParkInULB" DataField="NoOfParkInULB" />
                                                        <asp:BoundField HeaderText="AdoptedParkName" DataField="AdoptedParkName" />
                                                        <asp:BoundField HeaderText="ParkLatitude" DataField="ParkLatitude" />
                                                        <asp:BoundField HeaderText="ParkLongitude" DataField="ParkLongitude" />
                                                        <asp:BoundField HeaderText="SessionYear" DataField="SessionYear" />
                                                        <asp:BoundField HeaderText="Month_MonthName " DataField="Month_MonthName" />                                            
                                                        <asp:BoundField HeaderText="NameCSR_NGO " DataField="NameCSR_NGO" />                                            
                                                        <asp:BoundField HeaderText="DetailCSR_NGO " DataField="DetailCSR_NGO" />                                            
                                                        <%--<asp:BoundField HeaderText="GeotaggedPhotographs " DataField="GeotaggedPhotographs" />                                            
                                                        <asp:BoundField HeaderText="MOUAttached " DataField="MOUAttached" />  --%>                                          
                                                        <%-- <asp:TemplateField HeaderText="Docs">
                                                             <ItemTemplate>
                                                                 <asp:Label ID="lblNoDocument" runat="server" Text="No Document" Visible='<%# string.IsNullOrEmpty(Eval("Documents") as string) %>'></asp:Label>
                                                                 <asp:HyperLink ID="hlDocument" runat="server" NavigateUrl='<%# Eval("Documents") %>' Text="Doc" Visible='<%# !string.IsNullOrEmpty(Eval("Documents") as string) %>' Target="_blank"></asp:HyperLink>
                                                             </ItemTemplate>
                                                         </asp:TemplateField>--%>
                                                        <%--<asp:TemplateField HeaderText="Edit">
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnEdit" runat="server" Text='Edit' CommandName="EditAnnualAction" OnCommand="Edit_Command" CommandArgument='<%# Eval("planId") %>' CssClass="btn btn-primary drill_btn" />
                                                            </ItemTemplate>                                                             
                                                        </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="Delete">
                                                        <ItemTemplate>
                                                             <asp:Button ID="btnDelete" OnClientClick="return confirm('Are You Sure You Want to Delete this Item?')" runat="server" Text='Delete' CommandName="DeleteAnnualAction" OnCommand="btnDelete_Command" CommandArgument='<%# Eval("planId") %>' CssClass="btn btn-danger drill_btn" />
                                                         </ItemTemplate>

                                                        </asp:TemplateField>--%>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <tr>
                                                            <td colspan="15" style="text-align: center; font-weight: bold; color: red;">No records found</td>
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