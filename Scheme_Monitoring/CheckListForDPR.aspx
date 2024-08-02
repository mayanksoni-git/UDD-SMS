<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin_PMS.master" AutoEventWireup="true" CodeFile="CheckListForDPR.aspx.cs" Inherits="CheckListForDPR" EnableEventValidation="false" ValidateRequest="false" %>

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
                                    <h4 class="mb-sm-0">Check List For DPR & Upload DPR</h4>
                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                            <li class="breadcrumb-item">DPR</li>
                                            <li class="breadcrumb-item active">Check List For DPR & Upload DPR</li>
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
                                        <h4 class="card-title mb-0 flex-grow-1">Check List For DPR & Upload DPR</h4>
                                    </div>
                                    <!-- end card header -->
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-4">

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divFY" runat="server">
                                                        <asp:Label ID="lblFY" runat="server" Text="State*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlFY" runat="server" CssClass="form-select" AutoPostBack="true" >
                                                              <asp:ListItem>Select State*</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divDistrict" runat="server">
                                                        <asp:Label ID="lblDistrict" runat="server" Text="District*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-select" AutoPostBack="true" >
                                                             <asp:ListItem>Select District*</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                               
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divULB" runat="server">
                                                        <asp:Label ID="lblULB" runat="server" Text="ULB*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlULB" runat="server" CssClass="form-select">
                                                            <asp:ListItem>Select ULB*</asp:ListItem>

                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divScheme" runat="server">
                                                        <asp:Label ID="lblScheme" runat="server" Text="Scheme" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlScheme" runat="server" CssClass="form-select" AutoPostBack="true" >
                                                              <asp:ListItem>Select Scheme*</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                 <div class="col-xxl-3 col-md-6">
                                                    <div id="divULBType" runat="server">
                                                        <asp:Label ID="lblULBType" runat="server" Text="Project*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlULBType" runat="server" CssClass="form-select" AutoPostBack="true" >
                                                              <asp:ListItem>Select Project*</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                               
                                                <div class="col-xxl-3 offset-xxl-1 col-md-6">
                                                    <div>
                                                       <%-- <label class="d-block">&nbsp;</label>
                                                        <asp:Button ID="btnSave" Text="Save" OnClick="btnSave_Click" runat="server" CssClass="btn bg-success text-white"></asp:Button>
                                                        <asp:Button ID="btnUpdate" Text="Update" Visible="false" OnClick="btnUpdate_Click" runat="server" CssClass="btn bg-success text-white"></asp:Button>
                                                        <asp:Button ID="btnCancel" Text="Cancel / Reset" OnClick="btnCancel_Click" runat="server" CssClass="btn bg-secondary text-white"></asp:Button>
                                                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                                                        <asp:HiddenField ID="hfId" runat="server" />--%>
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


                    <div runat="server" visible="true" id="divData">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">DPR Detail</h4>
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

                                                    <table class="table table-bordered table-responsive">
                                                        <thead>
                                                            <tr>
                                                                <th>Sr No.</th>
                                                                <th>Description</th>
                                                                <th>Yes/No</th>
                                                                 <th>Sr No.</th>
                                                                <th>Description</th>
                                                                <th>Yes/No</th>
                                                                 <th>Sr No.</th>
                                                                <th>Description</th>
                                                                <th>Yes/No</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                           <%-- <tr>
                                                                <td style="background: #f8763d; text-align: center; color: white"></td>
                                                                <td colspan="2" style="background: #f8763d; text-align: center; color: white">Chapters to be incorporated in the DPR	
                                                                </td>
                                                            </tr>--%>
                                                            <tr>

                                                                <td>1</td>
                                                                <td>Executive summary of the project and its proposal
                                                                </td>
                                                                <td>
                                                                    <asp:RadioButton ID="rbYes" runat="server" Text="Yes" GroupName="YesNo" />
                                                                    <asp:RadioButton ID="rbNo" runat="server" Text="No" GroupName="YesNo" />
                                                                </td>

                                                                <td>14</td>
                                                                <td>Expected Sources of funding

                                                                </td>
                                                                <td>
                                                                    <asp:RadioButton ID="RadioButton25" runat="server" Text="Yes" GroupName="YesNo" />
                                                                    <asp:RadioButton ID="RadioButton26" runat="server" Text="No" GroupName="YesNo" />
                                                                </td>

                                                                <td>27</td>
                                                                <td>Proposed 3D view of proposed built form


                                                                </td>
                                                                <td>
                                                                    <asp:RadioButton ID="RadioButton41" runat="server" Text="Yes" GroupName="YesNo" />
                                                                    <asp:RadioButton ID="RadioButton42" runat="server" Text="No" GroupName="YesNo" />
                                                                </td>


                                                            </tr>
                                                            <tr>
                                                                <td>2</td>
                                                                <td>Introduction

                                                                </td>
                                                                <td>
                                                                    <asp:RadioButton ID="RadioButton1" runat="server" Text="Yes" GroupName="YesNo" />
                                                                    <asp:RadioButton ID="RadioButton2" runat="server" Text="No" GroupName="YesNo" />
                                                                </td>

                                                                <td>15</td>

                                                                <td>Possible Revenue streams

                                                                </td>
                                                                <td>
                                                                    <asp:RadioButton ID="RadioButton27" runat="server" Text="Yes" GroupName="YesNo" />
                                                                    <asp:RadioButton ID="RadioButton28" runat="server" Text="No" GroupName="YesNo" />
                                                                </td>

                                                                <td>28</td>
                                                                <td>Structure Framing & Column layout plan


                                                                </td>
                                                                <td>
                                                                    <asp:RadioButton ID="RadioButton51" runat="server" Text="Yes" GroupName="YesNo" />
                                                                    <asp:RadioButton ID="RadioButton52" runat="server" Text="No" GroupName="YesNo" />
                                                                </td>

                                                            </tr>
                                                            <tr>
                                                                <td>3</td>
                                                                <td>About The Project
                                                                </td>
                                                                <td>
                                                                    <asp:RadioButton ID="RadioButton3" runat="server" Text="Yes" GroupName="YesNo" />
                                                                    <asp:RadioButton ID="RadioButton4" runat="server" Text="No" GroupName="YesNo" />
                                                                </td>

                                                                <td>16</td>
                                                                <td>Environmental & sustainability aspects
                                                                </td>
                                                                <td>
                                                                    <asp:RadioButton ID="RadioButton29" runat="server" Text="Yes" GroupName="YesNo" />
                                                                    <asp:RadioButton ID="RadioButton30" runat="server" Text="No" GroupName="YesNo" />
                                                                </td>

                                                                <td>29</td>
                                                                <td>Services layout plan 


                                                                </td>
                                                                <td>
                                                                    <asp:RadioButton ID="RadioButton53" runat="server" Text="Yes" GroupName="YesNo" />
                                                                    <asp:RadioButton ID="RadioButton54" runat="server" Text="No" GroupName="YesNo" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>4</td>
                                                                <td>Project definition, Concept, Objective and scope of work

                                                                </td>
                                                                <td>
                                                                    <asp:RadioButton ID="RadioButton5" runat="server" Text="Yes" GroupName="YesNo" />
                                                                    <asp:RadioButton ID="RadioButton6" runat="server" Text="No" GroupName="YesNo" />
                                                                </td>

                                                                <td>17</td>
                                                                <td>Risk assessment and mitigation measures

                                                                </td>
                                                                <td>
                                                                    <asp:RadioButton ID="RadioButton31" runat="server" Text="Yes" GroupName="YesNo" />
                                                                    <asp:RadioButton ID="RadioButton32" runat="server" Text="No" GroupName="YesNo" />
                                                                </td>

                                                                <td style="background: #f8763d; text-align: center; color: white">30</td>
                                                                <td colspan="2" style="background: #f8763d; text-align: center; color: white">Annexures/ Documents to be added


                                                                </td>
                                                                <%-- <td>
                                                                    <asp:RadioButton ID="RadioButton55" runat="server" Text="Yes" GroupName="YesNo" />
                                                                    <asp:RadioButton ID="RadioButton56" runat="server" Text="No" GroupName="YesNo" />
                                                                </td>--%>
                                                            </tr>
                                                            <tr>
                                                                <td>5</td>
                                                                <td>Feasibility study of the project showcasing expected benefits after execution

                                                                </td>
                                                                <td>
                                                                    <asp:RadioButton ID="RadioButton7" runat="server" Text="Yes" GroupName="YesNo" />
                                                                    <asp:RadioButton ID="RadioButton8" runat="server" Text="No" GroupName="YesNo" />
                                                                </td>

                                                                <td>18</td>
                                                                <td>Statutory clearances

                                                                </td>
                                                                <td>
                                                                    <asp:RadioButton ID="RadioButton33" runat="server" Text="Yes" GroupName="YesNo" />
                                                                    <asp:RadioButton ID="RadioButton34" runat="server" Text="No" GroupName="YesNo" />
                                                                </td>

                                                                <td>31</td>
                                                                <td>Proposed Finishing schedule to be attached as separate annexure


                                                                </td>
                                                                <td>
                                                                    <asp:RadioButton ID="RadioButton57" runat="server" Text="Yes" GroupName="YesNo" />
                                                                    <asp:RadioButton ID="RadioButton58" runat="server" Text="No" GroupName="YesNo" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>6</td>
                                                                <td>Location of site with GPS coordinates mentioned in the DPR

                                                                </td>
                                                                <td>
                                                                    <asp:RadioButton ID="RadioButton9" runat="server" Text="Yes" GroupName="YesNo" />
                                                                    <asp:RadioButton ID="RadioButton10" runat="server" Text="No" GroupName="YesNo" />
                                                                </td>

                                                                <td>19</td>
                                                                <td>Quality management plan *

                                                                </td>
                                                                <td>
                                                                    <asp:RadioButton ID="RadioButton35" runat="server" Text="Yes" GroupName="YesNo" />
                                                                    <asp:RadioButton ID="RadioButton36" runat="server" Text="No" GroupName="YesNo" />
                                                                </td>

                                                                <td>32</td>
                                                                <td>DPR should be signed by Executive Engineer, Chief Engineer, Municipal Commissioner


                                                                </td>
                                                                <td>
                                                                    <asp:RadioButton ID="RadioButton59" runat="server" Text="Yes" GroupName="YesNo" />
                                                                    <asp:RadioButton ID="RadioButton60" runat="server" Text="No" GroupName="YesNo" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>7</td>
                                                                <td>Requirement and demand analysis of project (Electrical, Water, drainage and other services)

                                                                </td>
                                                                <td>
                                                                    <asp:RadioButton ID="RadioButton11" runat="server" Text="Yes" GroupName="YesNo" />
                                                                    <asp:RadioButton ID="RadioButton12" runat="server" Text="No" GroupName="YesNo" />
                                                                </td>

                                                                <td>20</td>
                                                                <td>Annexures

                                                                </td>
                                                                <td>
                                                                    <asp:RadioButton ID="RadioButton37" runat="server" Text="Yes" GroupName="YesNo" />
                                                                    <asp:RadioButton ID="RadioButton38" runat="server" Text="No" GroupName="YesNo" />
                                                                </td>
                                                                <td>33</td>
                                                                <td>Letter from Municipal Commissioner for submission of DPR with project cost to be included in the DPR

                                                                </td>
                                                                <td>
                                                                    <asp:RadioButton ID="RadioButton61" runat="server" Text="Yes" GroupName="YesNo" />
                                                                    <asp:RadioButton ID="RadioButton62" runat="server" Text="No" GroupName="YesNo" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>8</td>
                                                                <td>Existing site condition with photographs attached in the DPR

                                                                </td>
                                                                <td>
                                                                    <asp:RadioButton ID="RadioButton13" runat="server" Text="Yes" GroupName="YesNo" />
                                                                    <asp:RadioButton ID="RadioButton14" runat="server" Text="No" GroupName="YesNo" />
                                                                </td>

                                                                <td>21</td>
                                                                <td>Implementation schedule

                                                                </td>
                                                                <td>
                                                                    <asp:RadioButton ID="RadioButton39" runat="server" Text="Yes" GroupName="YesNo" />
                                                                    <asp:RadioButton ID="RadioButton40" runat="server" Text="No" GroupName="YesNo" />
                                                                </td>

                                                                <td>34</td>
                                                                <td>Project Estimate should be on item rate basis
                                                                </td>
                                                                <td>
                                                                    <asp:RadioButton ID="RadioButton63" runat="server" Text="Yes" GroupName="YesNo" />
                                                                    <asp:RadioButton ID="RadioButton64" runat="server" Text="No" GroupName="YesNo" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>9</td>
                                                                <td>Engineering survey and investigation

                                                                </td>
                                                                <td>
                                                                    <asp:RadioButton ID="RadioButton15" runat="server" Text="Yes" GroupName="YesNo" />
                                                                    <asp:RadioButton ID="RadioButton16" runat="server" Text="No" GroupName="YesNo" />
                                                                </td>

                                                                <td style="background: #f8763d; text-align: center; color: white">22</td>
                                                                <td colspan="2" style="background: #f8763d; text-align: center; color: white">Following drawings to be attached

                                                                </td>
                                                                <%-- <td>
                                                                    <asp:RadioButton ID="RadioButton41" runat="server" Text="Yes" GroupName="YesNo" />
                                                                    <asp:RadioButton ID="RadioButton42" runat="server" Text="No" GroupName="YesNo" />
                                                                </td>--%>
                                                                <td>35</td>
                                                                <td>Land ownership to be confirmed and supporting document to be attached in annexure

                                                                </td>
                                                                <td>
                                                                    <asp:RadioButton ID="RadioButton65" runat="server" Text="Yes" GroupName="YesNo" />
                                                                    <asp:RadioButton ID="RadioButton66" runat="server" Text="No" GroupName="YesNo" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>10</td>
                                                                <td>Architecture design 

                                                                </td>
                                                                <td>
                                                                    <asp:RadioButton ID="RadioButton17" runat="server" Text="Yes" GroupName="YesNo" />
                                                                    <asp:RadioButton ID="RadioButton18" runat="server" Text="No" GroupName="YesNo" />
                                                                </td>

                                                                <td>23</td>
                                                                <td>Proposed Site plan

                                                                </td>
                                                                <td>
                                                                    <asp:RadioButton ID="RadioButton43" runat="server" Text="Yes" GroupName="YesNo" />
                                                                    <asp:RadioButton ID="RadioButton44" runat="server" Text="No" GroupName="YesNo" />
                                                                </td>
                                                                <td style="background: #f8763d; text-align: center; color: white">36</td>
                                                                <td colspan="2" style="background: #f8763d; text-align: center; color: white">Detailing to be considered in the proposal


                                                                </td>
                                                                <%-- <td>
                                                                    <asp:RadioButton ID="RadioButton67" runat="server" Text="Yes" GroupName="YesNo" />
                                                                    <asp:RadioButton ID="RadioButton68" runat="server" Text="No" GroupName="YesNo" />
                                                                </td>--%>
                                                            </tr>
                                                            <tr>
                                                                <td>11</td>
                                                                <td>Structure Design
                                                                </td>
                                                                <td>
                                                                    <asp:RadioButton ID="RadioButton19" runat="server" Text="Yes" GroupName="YesNo" />
                                                                    <asp:RadioButton ID="RadioButton20" runat="server" Text="No" GroupName="YesNo" />
                                                                </td>

                                                                <td>24</td>
                                                                <td>Proposed Floor plan
                                                                </td>
                                                                <td>
                                                                    <asp:RadioButton ID="RadioButton45" runat="server" Text="Yes" GroupName="YesNo" />
                                                                    <asp:RadioButton ID="RadioButton46" runat="server" Text="No" GroupName="YesNo" />
                                                                </td>
                                                                <td>37</td>
                                                                <td>In case of any non-schedule items 3 quotations to be added

                                                                </td>
                                                                <td>
                                                                    <asp:RadioButton ID="RadioButton71" runat="server" Text="Yes" GroupName="YesNo" />
                                                                    <asp:RadioButton ID="RadioButton72" runat="server" Text="No" GroupName="YesNo" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>12</td>
                                                                <td>Similar case study if added as separate case study *



                                                                </td>
                                                                <td>
                                                                    <asp:RadioButton ID="RadioButton21" runat="server" Text="Yes" GroupName="YesNo" />
                                                                    <asp:RadioButton ID="RadioButton22" runat="server" Text="No" GroupName="YesNo" />
                                                                </td>
                                                                <td>25</td>
                                                                <td>Proposed Sections 2 no.

                                                                </td>
                                                                <td>
                                                                    <asp:RadioButton ID="RadioButton47" runat="server" Text="Yes" GroupName="YesNo" />
                                                                    <asp:RadioButton ID="RadioButton48" runat="server" Text="No" GroupName="YesNo" />
                                                                </td>
                                                                <td>38</td>
                                                                <td>Green building rating system (GRIHA, LEED etc) to be considered


                                                                </td>
                                                                <td>
                                                                    <asp:RadioButton ID="RadioButton69" runat="server" Text="Yes" GroupName="YesNo" />
                                                                    <asp:RadioButton ID="RadioButton70" runat="server" Text="No" GroupName="YesNo" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>13</td>
                                                                <td>Financial estimates & cost projections on item rate basis

                                                                </td>
                                                                <td>
                                                                    <asp:RadioButton ID="RadioButton23" runat="server" Text="Yes" GroupName="YesNo" />
                                                                    <asp:RadioButton ID="RadioButton24" runat="server" Text="No" GroupName="YesNo" />
                                                                </td>
                                                                <td>26</td>
                                                                <td>Proposed Elevation of at least 2 side

                                                                </td>
                                                                <td>
                                                                    <asp:RadioButton ID="RadioButton49" runat="server" Text="Yes" GroupName="YesNo" />
                                                                    <asp:RadioButton ID="RadioButton50" runat="server" Text="No" GroupName="YesNo" />
                                                                </td>
                                                                <td>39</td>
                                                                <td>Proposed 3D view of proposed built form


                                                                </td>
                                                                <td>
                                                                    <asp:RadioButton ID="RadioButton73" runat="server" Text="Yes" GroupName="YesNo" />
                                                                    <asp:RadioButton ID="RadioButton74" runat="server" Text="No" GroupName="YesNo" />
                                                                </td>
                                                            </tr>

                                                        </tbody>
                                                    </table>

                                                    <%--<asp:GridView ID="gvRecords" runat="server" CssClass="table table-bordered mt-4" AutoGenerateColumns="false" >
                                                        <Columns>
                                                            <asp:BoundField DataField="ID" HeaderText="Fund Sanctioned Id">
                                                                    <HeaderStyle CssClass="displayStyle" />
                                                                    <ItemStyle CssClass="displayStyle" />
                                                                    <FooterStyle CssClass="displayStyle" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Sr. No.">
                                                                    <ItemTemplate>
                                                                        <%# Container.DataItemIndex + 1 %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                       
                                                            <asp:BoundField HeaderText="District" DataField="DistName" />
                                                            <asp:BoundField HeaderText="ULB Type" DataField="ULBType" />
                                                            <asp:BoundField HeaderText="ULB Name" DataField="ULBName" />
                                                            <asp:BoundField HeaderText="Parliament Name" DataField="ParliamentaryConstName" />
                                                            <asp:BoundField HeaderText="Assembly Name" DataField="AssemblyConstName" />
                                                            <asp:BoundField HeaderText="Scheme Name" DataField="SchemeName" />
                                                            <asp:BoundField HeaderText="Session" DataField="SessionYear" />
                                                            <asp:BoundField HeaderText="Amount (In Lakhs)" DataField="AmtInLac" />
                                                        </Columns>
                                                    </asp:GridView>--%>

                                                     <asp:Button ID="btnSave" Text="Submit" style="float:right" OnClick="btnSave_Click" runat="server" CssClass="btn bg-success text-white"></asp:Button>
                                                       

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
                    <%--<asp:PostBackTrigger ControlID="btnSave" />--%>
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
