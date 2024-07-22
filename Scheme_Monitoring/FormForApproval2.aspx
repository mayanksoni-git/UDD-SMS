<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin_PMS.master" AutoEventWireup="true" CodeFile="FormForApproval2.aspx.cs" Inherits="FormForApproval2" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="assets/css/CalendarStyle.css" rel="stylesheet" />

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
                                    <h4 class="mb-sm-0">Work Plan</h4>
                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                            <li class="breadcrumb-item">Work Plan Management System</li>
                                            <li class="breadcrumb-item active">Work Plan</li>
                                        </ol>
                                    </div>
                                </div>
                            </div>
                            <!--end col-->
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Search  Work Plan</h4>
                                    </div>
                                    <!-- end card header -->
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-4">

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divFY" runat="server">
                                                        <asp:Label ID="lblFY" runat="server" Text="Select Financial Year*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlFY" runat="server" CssClass="form-select"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divZone" runat="server">
                                                        <asp:Label ID="lblZoneH" runat="server" Text="Zone*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlZone" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divCircle" runat="server">
                                                        <asp:Label ID="lblCircleH" runat="server" Text="Circle*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlCircle" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCircle_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divDivision" runat="server">
                                                        <asp:Label ID="lblDivisionH" runat="server" Text="Division*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divScheme" runat="server">
                                                        <asp:Label ID="lblScheme" runat="server" Text="Scheme" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlProjectMaster" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlProjectMaster_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>


                                                <div class="col-xxl-3 col-md-6" style="display:none">
                                                    <div id="divWorkType" runat="server">
                                                        <asp:Label ID="lblWorkType" runat="server" Text="Type of Work*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlWorkType" runat="server" CssClass="form-select"></asp:DropDownList>

                                                    </div>
                                                </div>

                                                <div class="col-xxl-2 col-md-6" id="divParty">
                                                    <asp:Label ID="lblStatus" runat="server" Text=" Work Plan Status*" CssClass="form-label"></asp:Label>
                                                    <asp:DropDownList ID="ddlProposalStatus" runat="server" CssClass="form-select">
                                                        <asp:ListItem Text="--Select Status--" Value="-1"></asp:ListItem>
                                                        <asp:ListItem Text="Pending" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Approved" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Reject" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Hold/Archive" Value="3"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                                <div class="col-xxl-3 offset-xxl-4 col-md-6">
                                                    <div>
                                                        <label class="d-block">&nbsp;</label>
                                                        <asp:Button ID="btnSearch" Text="Search" OnClick="btnSearch_Click" runat="server" CssClass="btn bg-success text-white"></asp:Button>
                                                        <asp:Button ID="btnCancel" Text="Cancel / Reset" OnClick="btnCancel_Click" runat="server" CssClass="btn bg-secondary text-white"></asp:Button>
                                                        <asp:Button ID="btnShowModal" runat="server" Visible="false" CssClass="btn bg-success text-white" Text="Show Fund Status" OnClick="btnShowModal_Click" />
                                                        <asp:Button ID="btnHideModal" runat="server" Visible="false" CssClass="btn bg-black text-white" Text="Hide Fund Status" OnClick="btnHideModal_Click" />
                                                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                                                        <asp:HiddenField ID="hfFormApproval_Id" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            <!--end row-->
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>



                    <div runat="server" visible="false" id="divFundStatus">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Fund Status of ULB</h4>
                                    </div>
                                    <!-- end card header -->
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-12">
                                                <!-- div.table-responsive -->
                                                <div class="clearfix" id="Div1" runat="server">
                                                    <div class="pull-right tableTools-container"></div>
                                                </div>
                                                <!-- div.dataTables_borderWrap -->
                                                <div style="overflow: auto">
                                                    <div id="fundStatusModal" runat="server" class="modal-content">
                                                        <span id="btnClose" runat="server" class="close" onserverclick="btnClose_Click"></span>
                                                        <table class="table">
                                                            <thead>
                                                                <tr>
                                                                    <th>Financial Year</th>
                                                                    <th>
                                                                        <asp:DropDownList ID="ddlFY1" runat="server" CssClass="form-select"></asp:DropDownList>
                                                                    </th>
                                                                    <th>
                                                                        <asp:DropDownList ID="ddlFY2" runat="server" CssClass="form-select"></asp:DropDownList>
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr>
                                                                    <td>Fund Received</td>
                                                                    <td>Yes/No</td>
                                                                    <td>Yes/No</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Utilization Status</td>
                                                                    <td>Utilized/Unutilized</td>
                                                                    <td>Utilized/Unutilized</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>UC Status</td>
                                                                    <td>Received/Not Received</td>
                                                                    <td>Received/Not Received</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Total Proposals</td>
                                                                    <td>2</td>
                                                                    <td>2</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Total Approved Proposals</td>
                                                                    <td>1</td>
                                                                    <td>1</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Total Rejected Proposals</td>
                                                                    <td>1</td>
                                                                    <td>1</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Total Fund Released(In Lakhs)</td>
                                                                    <td>100</td>
                                                                    <td>100</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Total Fund Sanctioned(In Lakhs)</td>
                                                                    <td>85</td>
                                                                    <td>90</td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                            <!--end row-->
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--end col-->
                        </div>
                    </div>



                    <%--place grid here--%>

                    <div runat="server" visible="true" id="divData">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Work Plan Detail</h4>
                                 
                                        <a href="#" id="exportToExcel" runat="server" onclick="ExportToExcel('xlsx')" class="filter-btn" style="float:right"><i class="icon-download"></i> Export To Excel</a>
                                    </div>
                                    <!-- end card header -->
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-12">
                                                <!-- div.table-responsive -->
                                                <div class="clearfix" id="dtOptions" runat="server">
                                                    <div class="pull-right tableTools-container"></div>
                                                </div>
                                                <!-- div.dataTables_borderWrap -->
                                                <div style="overflow: auto">

                                                    <asp:GridView ID="gvRecords" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" AllowPaging="true"
                                                    OnPageIndexChanging="OnPageIndexChanging" PageSize="15">
                                                        <Columns>
                                                            <asp:BoundField DataField="WorkProposalId" HeaderText="Work Proposal Id">
                                                                <HeaderStyle CssClass="displayStyle" />
                                                                <ItemStyle CssClass="displayStyle" />
                                                                <FooterStyle CssClass="displayStyle" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Sr. No.">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <%--<asp:TemplateField HeaderText="Edit">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnEdit" Width="20px" Height="20px" OnClick="btnEdit_Click" ImageUrl="~/assets/images/edit_btn.png" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>--%>
                                                            <asp:BoundField HeaderText="Work Plan Proposal Code" DataField="ProposalCode" />
                                                            <asp:BoundField HeaderText="Financial Year" DataField="FinYear" />
                                                            <%--<asp:BoundField HeaderText="State" DataField="Zone_Name" />--%>
                                                            <asp:BoundField HeaderText="District" DataField="Circle_Name" />
                                                            <asp:BoundField HeaderText="ULB" DataField="Division_Name" />
                                                            <asp:BoundField HeaderText="Zone" DataField="ZoneOfULB" />
                                                            <asp:BoundField HeaderText="Ward" DataField="Ward" />
                                                            <asp:BoundField HeaderText="Scheme" DataField="Project_Name" />
                                                            <asp:BoundField HeaderText="Project Type" DataField="ProjectType_Name" />
                                                            <%--<asp:BoundField HeaderText="Sanctioned Amount in 2nd Last Year" DataField="FY2" />--%>
                                                            <%--<asp:BoundField HeaderText="Sanctioned Amount in Last Year" DataField="FY1" />--%>

                                                            <asp:BoundField HeaderText="Expected Amount" DataField="ExpectedAmount" />
                                                            <asp:BoundField HeaderText="Work Plan Proposer Type" DataField="ProposerType" />
                                                            <asp:BoundField HeaderText="MP/MLA Name" DataField="MPMLAName" />
                                                            <asp:BoundField HeaderText="Work Plan Proposer Name" DataField="ProposerName" />
                                                            <asp:BoundField HeaderText="Mobile" DataField="Mobile" />
                                                            <asp:BoundField HeaderText="Designation" DataField="Designation" />
                                                            <asp:TemplateField HeaderText="Recommendation Letter">
                                                                <ItemTemplate>
                                                                    <asp:HyperLink ID="hypRecommendationLetter" runat="server" Target="_blank" NavigateUrl='<%# Eval("RecomendationLetter") %>' Text="Click To View" Visible='<%# !string.IsNullOrEmpty(Eval("RecomendationLetter").ToString()) %>'>
                                                                        <asp:Image ID="imgViewPDF" runat="server" ImageUrl="~/assets/images/ViewPdf.png" AlternateText="View PDF" Height="30" Width="30" />
                                                                    </asp:HyperLink>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Status" DataField="ProposalStatus" />
                                                            <asp:TemplateField HeaderText="Action">
                                                                <ItemTemplate>
                                                                    <asp:Button ID="btnAction" runat="server" Text="Action" CommandName="Action" CommandArgument='<%# Eval("WorkProposalId") %>' CssClass="btn btn-primary" OnCommand="btnAction_Command" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            
                                                            <%--<asp:BoundField HeaderText="Added On" DataField="AddedOn" DataFormatString="{0:dd/MM/yyyy}" />--%>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            <tr>
                                                                <td colspan="15" style="text-align: center; font-weight: bold; color: red;">No records found</td>
                                                            </tr>
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <!--end row-->
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--end col-->
                        </div>
                    </div>

                    <div runat="server" visible="false" id="mpActionProposal">
                        <div class="row">
                            <div class="col-lg-6">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Action on Proposal</h4> <b><asp:Label runat="server" id="lblWrokProposalId" Text=""></asp:Label></b>
                                    </div>
                                    <!-- end card header -->
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-12">
                                                <!-- div.table-responsive -->
                                                <div class="clearfix" id="Div3" runat="server">
                                                    <div class="pull-right tableTools-container"></div>
                                                </div>
                                                <!-- div.dataTables_borderWrap -->
                                                <div style="overflow: auto">
                                                    <div id="Div4" runat="server" class="modal-content">
                                                        <span id="Span1" runat="server" class="close" onserverclick="btnClose_Click"></span>
                                                        <div class="modal-content">
                                                            <div class="modal-body">
                                                                <div class="form-group">
                                                                    <asp:Label ID="Label1" runat="server" Text="Action"></asp:Label>
                                                                    <asp:DropDownList ID="ddlAction" runat="server" CssClass="form-control">
                                                                        <asp:ListItem Text="Pending" Value="0"></asp:ListItem>
                                                                        <asp:ListItem Text="Approved" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="Reject" Value="2"></asp:ListItem>
                                                                        <asp:ListItem Text="Hold/Archive" Value="3"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="form-group">
                                                                    <asp:Label ID="Label2" runat="server" Text="Remarks"></asp:Label>
                                                                    <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <br />
                                                            <div class="modal-footer">
                                                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmitAction_Click" /> 
                                                                <asp:Button ID="btnCloseAction" runat="server" Text="Close" CssClass="btn btn-secondary" OnClick="btnCloseActionProposal_Click" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!--end row-->
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--end col-->
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSearch" />
                    <asp:PostBackTrigger ControlID="exportToExcel" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.16.2/xlsx.full.min.js"></script>
    <script>
        function ExportToExcel(type, fn, dl) {
          debugger
            const currentDate = new Date();

            // Get the current date
            const year = currentDate.getFullYear();
            const month = currentDate.getMonth() + 1; // Months are zero-based
            const day = currentDate.getDate();

            // Format the date as desired (e.g., YYYY-MM-DD)
            const formattedDate = "Work Plan Detail_" + `${year}-${month}-${day}`;

            var elt = document.getElementById('ctl00_ContentPlaceHolder1_gvRecords');
            var wb = XLSX.utils.table_to_book(elt, { sheet: "sheet1" });
            return dl ?
                XLSX.write(wb, { bookType: type, bookSST: true, type: 'base64' }) :
                XLSX.writeFile(wb, fn || (formattedDate + "." + (type || 'xlsx')));
        }
    </script>
   
    <style>
        .form-control img {
            height: 20px;
            width: 20px;
            vertical-align: middle;
            margin-right: 10px;
        }

        #ctl00_ContentPlaceHolder1_gvRecords tbody tbody td {
            height: 35px;
            width: 35px;
            line-height: 35px;
            display: inline-block;
            background: #dce9f7;
            border: 1px solid #d1e3f7;
            text-align: center;
            margin: 0 2px;
        }

        #ctl00_ContentPlaceHolder1_gvRecords tbody tbody td a {
            height: 35px;
            color: #000;
            width: 35px;
            display: block;
        }

        #ctl00_ContentPlaceHolder1_gvRecords tbody tbody td:hover {
            background: #c5dffb;
            border: 1px solid #bbdbff;
        }
    </style>
</asp:Content>
