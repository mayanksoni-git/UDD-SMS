<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true" CodeFile="MasterProjectDPR.aspx.cs" Inherits="MasterProjectDPR" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="main-content">
        <div class="page-content">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
            </cc1:ToolkitScriptManager>
            <%--<asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>--%>
                    <div class="container-fluid">
                        <div id="divCreate" runat="server">

                            <div class="row">
                                <div class="col-12 mb-0">
                                    <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                                        <h4 class="mb-sm-0">Create DPR</h4>
                                        <div class="page-title-right">
                                            <ol class="breadcrumb m-0">
                                                <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                                <li class="breadcrumb-item">DPR</li>
                                                <li class="breadcrumb-item active">Create Project DPR</li>
                                            </ol>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="card">
                                        <div class="card-header align-items-center d-flex">
                                            <h4 class="card-title mb-0 flex-grow-1">Create DPR </h4>
                                        </div>

                                        <div class="card-body">
                                            <div class="live-preview">
                                                <div class="row gy-4">
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <%--<label class="control-label no-padding-right">Scheme* </label>--%>
                                                            <asp:Label ID="lblScheme" runat="server" Text="Scheme*" CssClass="control-label no-padding-right"></asp:Label>
                                                            <asp:DropDownList ID="ddlProjectMaster" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlProjectMaster_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <asp:Label ID="lblZoneH" runat="server" Text="Zone" CssClass="control-label no-padding-right"></asp:Label>
                                                            <asp:DropDownList ID="ddlZone" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div id="divCircle" runat="server">
                                                            <asp:Label ID="lblCircleH" runat="server" Text="Circle" CssClass="control-label no-padding-right"></asp:Label>
                                                            <asp:DropDownList ID="ddlCircle" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCircle_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div id="divDivision" runat="server">
                                                            <asp:Label ID="lblDivisionH" runat="server" Text="Division" CssClass="control-label no-padding-right"></asp:Label>
                                                            <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-select"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <%--<label class="control-label no-padding-right">Project Name* </label>--%>
                                                            <asp:Label ID="lblProjectName" runat="server" Text="Project Name* " CssClass="control-label no-padding-right"></asp:Label>
                                                            <asp:TextBox ID="txtProjectWorkName" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <asp:Label ID="Label3" runat="server" Text="Total Capital Expenditures Cost (In Lakhs)" CssClass="control-label no-padding-right"></asp:Label>
                                                            <asp:TextBox ID="txtCapexCost" runat="server" CssClass="form-control" onkeyup="TotalAmount()"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <asp:Label ID="Label6" runat="server" Text="Total Operational & Maintenance Cost (In Lakhs)" CssClass="control-label no-padding-right"></asp:Label>
                                                            <asp:TextBox ID="txtOMCost" runat="server" CssClass="form-control" onkeyup="TotalAmount()"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                  
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <asp:Label ID="Label2" runat="server" Text="Total Project Cost (In Lakhs)" CssClass="control-label no-padding-right"></asp:Label>
                                                            <asp:TextBox ID="txtProjectCost" runat="server" ReadOnly="true" Enabled="false" CssClass="form-control" onkeyup="isNumericVal(this);"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                      <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <%--<label class="control-label no-padding-right">Project Type* </label>--%>
                                                            <asp:Label ID="lblProjectType" runat="server" Text="Project Type*" CssClass="control-label no-padding-right"></asp:Label>
                                                            <asp:DropDownList ID="ddlProjectType" runat="server" CssClass="form-select"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <asp:Label ID="Label5" runat="server" Text="Tentitive Date of DPR Submission" CssClass="control-label no-padding-right"></asp:Label>
                                                            <asp:TextBox ID="txtTentitiveDate" runat="server" CssClass="form-control date-picker" autocomplete="off"
                                                                TextMode="Date"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                   
                                                     <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <asp:Label ID="Label4" runat="server" Text="Enter Ward" CssClass="control-label no-padding-right"></asp:Label>
                                                            <asp:TextBox ID="TxtWard" runat="server" CssClass="form-control"  ></asp:TextBox>
                                                        </div>
                                                    </div>
                                                   
                                                     <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <asp:Label ID="Label7" runat="server" Text="Enter Zone" CssClass="control-label no-padding-right"></asp:Label>
                                                            <asp:TextBox ID="TxtZone" runat="server" CssClass="form-control"  ></asp:TextBox>
                                                        </div>
                                                    </div>
                                                     <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <asp:Label ID="Label8" runat="server" Text="Land Status: " CssClass="control-label no-padding-right"></asp:Label>
                                                            <asp:CheckBoxList ID="chkLandStatus" runat="server" RepeatDirection="Horizontal">
                                                                <asp:ListItem Text="Land Identified" Value="I" CssClass="checkboxdpr"></asp:ListItem>
                                                                <asp:ListItem Text="Land Transffered" Value="T" CssClass="checkboxdpr"></asp:ListItem>
                                                               
                                                            </asp:CheckBoxList>

                                                            


                                                        </div>
                                                    </div>

                                                     <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <asp:Label ID="Label1" runat="server" Text="Project DPR Land Status Remark" CssClass="control-label no-padding-right"></asp:Label>
                                                            <asp:TextBox ID="ORemark" runat="server" CssClass="form-control" TextMode="MultiLine" ></asp:TextBox>
                                                        </div>
                                                    </div>

                                                      <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <br />
                                                            <asp:CheckBox ID="chkSkip" runat="server" Text="Create DPR and Skip To Bid Process Management Module"></asp:CheckBox>
                                                        </div>
                                                    </div>
                                                   

                                                      <div class="col-xxl-11 col-md-11 flex-grow-1"></div>
                                                    <div class="col-xxl-1 col-md-6 flex-grow-1">
                                                        <div>
                                                            <br />
                                                            <asp:Button ID="btnSave" Text="Save" OnClick="btnSave_Click" runat="server"  CssClass="btn bg-success text-white"></asp:Button>
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

                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">DPR List</h4>
                                    </div>
                                    <!-- end card header -->
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-12">
                                                <!-- div.table-responsive -->
                                                <div class="clearfix" id="Div2" runat="server">
                                                    <div class="pull-right tableTools-container"></div>
                                                </div>
                                                <!-- div.dataTables_borderWrap -->
                                                <div style="overflow: auto">
                                                    <asp:GridView ID="grdPost" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPost_PreRender" OnRowDataBound="grdPost_RowDataBound">
                                                        <Columns>
                                                            <asp:BoundField DataField="ProjectDPR_Id" HeaderText="ProjectDPR_Id">
                                                                <HeaderStyle CssClass="displayStyle" />
                                                                <ItemStyle CssClass="displayStyle" />
                                                                <FooterStyle CssClass="displayStyle" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ProjectDPR_ProjectTypeId" HeaderText="ProjectDPR_ProjectTypeId">
                                                                <HeaderStyle CssClass="displayStyle" />
                                                                <ItemStyle CssClass="displayStyle" />
                                                                <FooterStyle CssClass="displayStyle" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ProjectDPR_Project_Id" HeaderText="ProjectDPR_Project_Id">
                                                                <HeaderStyle CssClass="displayStyle" />
                                                                <ItemStyle CssClass="displayStyle" />
                                                                <FooterStyle CssClass="displayStyle" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ProjectDPR_DistrictId" HeaderText="ProjectDPR_DistrictId">
                                                                <HeaderStyle CssClass="displayStyle" />
                                                                <ItemStyle CssClass="displayStyle" />
                                                                <FooterStyle CssClass="displayStyle" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ProjectDPR_ULBId" HeaderText="ProjectDPR_ULBId">
                                                                <HeaderStyle CssClass="displayStyle" />
                                                                <ItemStyle CssClass="displayStyle" />
                                                                <FooterStyle CssClass="displayStyle" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ProjectDPR_DivisionId" HeaderText="ProjectDPR_DivisionId">
                                                                <HeaderStyle CssClass="displayStyle" />
                                                                <ItemStyle CssClass="displayStyle" />
                                                                <FooterStyle CssClass="displayStyle" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="S No.">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Select">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnEdit" Width="20px" Height="20px" OnClick="btnEdit_Click" ImageUrl="~/assets/images/edit.png" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Zone" DataField="Zone_Name" />
                                                            <asp:BoundField HeaderText="Circle" DataField="Circle_Name" />
                                                            <asp:BoundField HeaderText="Division" DataField="Division_Name" />
                                                            <asp:BoundField HeaderText="Project Type" DataField="ProjectType_Name" />
                                                            <asp:BoundField HeaderText="Work" DataField="ProjectDPR_Name" />
                                                            <asp:BoundField HeaderText="Capex Cost (In Lakhs)" DataField="ProjectDPR_CapexCost" />
                                                            <asp:BoundField HeaderText="O & M  (In Lakhs)" DataField="ProjectDPR_OandM_Cost" />
                                                            <asp:BoundField HeaderText="ACA Cost  (In Lakhs)" DataField="ProjectDPR_ACA_Cost" />
                                                            <asp:BoundField HeaderText="Project Cost  (In Lakhs)" DataField="ProjectDPR_Project_Cost" />
                                                            <asp:BoundField HeaderText="Project Code" DataField="ProjectDPR_Code" />
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnDelete" Width="20px" OnClick="btnDelete_Click" ImageUrl="~/assets/images/delete.png" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
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
                    <asp:HiddenField ID="hf_ProjectDPR_Id" runat="server" Value="0" />
               
                <%--<Triggers>
                    <asp:PostBackTrigger ControlID="btnSave" />
                </Triggers>
            </asp:UpdatePanel>--%>
            <%--<asp:UpdateProgress ID="UpdateProgress1" DynamicLayout="true" runat="server" AssociatedUpdatePanelID="up">
                <ProgressTemplate>
                    <div style="position: fixed; z-index: 999; height: 100%; width: 100%; top: 0; background-color: Black; filter: alpha(opacity=60); opacity: 0.6; -moz-opacity: 0.8; cursor: not-allowed;">
                        <div style="z-index: 1000; margin: 300px auto; padding: 10px; width: 130px; background-color: transparent; border-radius: 1px; filter: alpha(opacity=100); opacity: 1; -moz-opacity: 1;">
                            <img src="assets/images/mb/mbloader.gif" style="height: 150px; width: 150px;" />
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>--%>
        </div>
    </div>

    <script>

        function TotalAmount() {
            var cap = document.getElementById("ctl00_ContentPlaceHolder1_txtOMCost").value;
            var om = document.getElementById("ctl00_ContentPlaceHolder1_txtCapexCost").value;
            
            cap = cap ? cap : 0;
            om = om ? om : 0;
            document.getElementById("ctl00_ContentPlaceHolder1_txtProjectCost").value = (Number(om) + Number(cap));
            
        }

        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            jQuery(function ($) {
                $('.modalBackground1').click(function () {
                    var id = $(this).attr('id').replace('_backgroundElement', '');
                    $find(id).hide();
                });
            })
        });
    </script>
</asp:Content>



