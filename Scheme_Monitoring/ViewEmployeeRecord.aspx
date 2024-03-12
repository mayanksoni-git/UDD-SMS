<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin2.master" AutoEventWireup="true" CodeFile="ViewEmployeeRecord.aspx.cs" Inherits="ViewEmployeeRecord" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-content">
        <div class="main-content-inner">
            <style>
                .control-label {
                    text-transform: uppercase;
                    color: #383535;
                    font-family: sans-serif;
                    font-weight: bold;
                    transition: 0.5s;
                }

                    .control-label:hover {
                        color: #dd0909;
                        font-weight: bolder;
                        letter-spacing: 0.5px;
                    }

                .boxcontent {
                    padding-top: 10px;
                }

                .boxborder {
                    border: 1px solid lightgrey;
                    margin-top: 10px;
                }
            </style>
            <div class="page-content">
                <div class="page-header">
                    <div class="col-md-12">
                        <div class="col-md-12">
                            <h1>View Employee Details
                            </h1>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-12">
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="row boxborder">
                                    <div class="col-xs-12">
                                        <div class="table-header">
                                            <span style="color: cornsilk; font-weight: bold">Section-1 :</span> Employee Basic Details *
                                        </div>
                                    </div>
                                    <div class="col-xs-12 boxcontent">
                                        <div class="row">
                                            <div class="col-xs-4">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Employee Name *</label>
                                                    <asp:TextBox ID="txtEmployeeName" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-4">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Spouse Name *</label>
                                                    <asp:TextBox ID="txtSpouseName" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-4">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Special Category * </label>
                                                    <asp:TextBox ID="ddlSpecialCategory" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div clas="row">
                                            <div class="col-xs-4">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Father's Name *  </label>
                                                    <asp:TextBox ID="txtEmployeeFather" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-4">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Gender *</label>
                                                    <asp:RadioButtonList ID="rbtGender" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" Enabled="false">
                                                        <asp:ListItem Value="Male" Text="&nbsp; Male &nbsp;"></asp:ListItem>
                                                        <asp:ListItem Value="Female" Text="&nbsp; Female &nbsp;"></asp:ListItem>
                                                        <asp:ListItem Value="Trans Gender" Text="&nbsp; Trans Gender"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                            <div class="col-xs-4">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Marital Status *</label>
                                                    <asp:TextBox ID="ddlMaritalStatus" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-xs-4">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Date Of Birth * </label>
                                                    <asp:TextBox ID="txtDOB" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-4">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Appointment date * </label>
                                                    <asp:TextBox ID="txtAppointmentDate" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-4">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Date Of Joining in Service * </label>
                                                    <asp:TextBox ID="txtJoiningDate" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-xs-4">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Departmental Employee code * </label>
                                                    <asp:TextBox ID="txtDepartmentealEmployeeCode" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-4">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Date Of Marriage * </label>
                                                    <asp:TextBox ID="txtMarriageDate" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-4">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Employee Type * </label>
                                                    <asp:TextBox ID="ddlEmployeeType" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-xs-4">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Husband/Wife e-HRMS Code {If In Government} *</label>
                                                    <asp:TextBox ID="txtSpouseCode" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Caste/Catagory *</label>
                                                    <asp:DropDownList ID="ddlCaste" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Religion *</label>
                                                    <asp:DropDownList ID="ddlReligion" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-xs-12">
                                                <div class="table-header">
                                                    Permanent Address Details * 
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row boxcontent">
                                            <div class="col-xs-4">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Home State * </label>
                                                    <asp:TextBox ID="ddlHomeState" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-4">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Home District * </label>
                                                    <asp:TextBox ID="ddlHomeDistrict" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-4">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Pin Code Of Area * </label>
                                                    <asp:TextBox ID="txtPinCode" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-xs-4">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Email Address * </label>
                                                    <asp:TextBox ID="txtEmployeeEmail" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-4">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Mobile Number *</label>
                                                    <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-4">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">CUG No.* </label>
                                                    <asp:TextBox ID="txtCUGno" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row boxborder">
                                    <div class="col-xs-12">
                                        <div class="table-header">
                                            <span style="color: cornsilk; font-weight: bold">Section-2 :</span>Employee Current Posting Details *
                                        </div>
                                    </div>
                                    <div class="col-xs-12 boxcontent">
                                        <div class="row">
                                            <div class="col-xs-4">
                                                <div class="form-group">
                                                    <asp:Label ID="lblZoneH" runat="server" Text="Zone" CssClass="control-label no-padding-right"></asp:Label>
                                                    <asp:TextBox ID="ddlZone" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-4">
                                                <div class="form-group">
                                                    <asp:Label ID="lblCircleH" runat="server" Text="Circle" CssClass="control-label no-padding-right"></asp:Label>
                                                    <asp:TextBox ID="ddlCircle" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-4">
                                                <div class="form-group">
                                                    <asp:Label ID="lblDivisionH" CssClass="control-label no-padding-right" Text="Division" runat="server"></asp:Label>
                                                    <asp:TextBox ID="ddlDivision" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-xs-4">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Designation * </label>
                                                    <asp:TextBox ID="ddlDesignation" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-4">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Cadre * </label>
                                                    <asp:TextBox ID="ddlCadre" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-4">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Order Date *  </label>
                                                    <asp:TextBox ID="txtOrderDate" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-xs-4">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Date Of Retirement * </label>
                                                    <asp:TextBox ID="txtRetirmentDate" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-4">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Joining date in current office  * </label>
                                                    <asp:TextBox ID="txtJoiningDateCurrentOffice" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">G.P.F No.* </label>
                                                    <asp:TextBox ID="txtGPF" runat="server" CssClass="form-control " placeholder="g.p.f no." Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row boxborder">
                                    <div class="col-xs-12">
                                        <div class="table-header">
                                            <span style="color: cornsilk; font-weight: bold">Section-3 :</span>
                                            Salary Breakup Info *
                                        </div>
                                    </div>
                                    <div class="col-xs-12 boxcontent">
                                        <div class="row">
                                            <div class="col-xs-4">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">pay Band * </label>
                                                    <asp:TextBox ID="ddlPayBand" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-4">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">pay scale * </label>
                                                    <asp:TextBox ID="ddlPayScale" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-4">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Basic Salary *  </label>
                                                    <asp:TextBox ID="txtBasicSalary" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-xs-4">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Gov. Quarter alloted :</label>
                                                    <asp:TextBox ID="chkGovQuaterAlloted" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-4">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Cug No Alloted:</label>
                                                    <asp:TextBox ID="chkCugNoAlloted" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-4">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Vehicle Alloted:</label>
                                                    <asp:TextBox ID="chkVehicleAlloted" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-xs-4">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">City Type :</label>

                                                    <asp:RadioButtonList ID="rbtCityType" runat="server" AutoPostBack="true" Enabled="false" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="Rural" Text="&nbsp;Rural &nbsp;"></asp:ListItem>
                                                        <asp:ListItem Value="Urban" Text="&nbsp;Urban &nbsp;"></asp:ListItem>
                                                        <asp:ListItem Value="Semi-Urban" Text="Semi-Urban &nbsp;"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="table-header">
                                                    Additive Component
                                                </div>
                                                <asp:GridView ID="grdPostAddition" Enabled="false" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPostAddition_PreRender" OnRowDataBound="grdPostAddition_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="PayComponent_Id" HeaderText="PayComponent_Id">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PayComponent_ParentComponent_Id" HeaderText="PayComponent_ParentComponent_Id">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Rural" DataField="PayComponent_Rate_Rural">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Urban" DataField="PayComponent_Rate_Urban">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Semi-Urban" DataField="PayComponent_Rate_SemiUrban">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Formula Applicable" DataField="PayComponent_FormulaApplicable">
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
                                                                <asp:CheckBox ID="chkComponentPlus" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="Component Name" DataField="PayComponent_Name" />
                                                        <asp:TemplateField HeaderText="Value">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtSalaryCompPlus" CssClass="form-control" runat="server" placeholder="Salary" Text='<%# Eval("HRMSEmployeeSalaryComponent_PayComponent_Value") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="table-header">
                                                    Substractive Component
                                                </div>
                                                <asp:GridView ID="grdPostSubstractive" Enabled="false" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPostSubstractive_PreRender" OnRowDataBound="grdPostSubstractive_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="PayComponent_Id" HeaderText="PayComponent_Id">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PayComponent_ParentComponent_Id" HeaderText="PayComponent_ParentComponent_Id">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Rural" DataField="PayComponent_Rate_Rural">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Urban" DataField="PayComponent_Rate_Urban">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Semi-Urban" DataField="PayComponent_Rate_SemiUrban">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Formula Applicable" DataField="PayComponent_FormulaApplicable">
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
                                                                <asp:CheckBox ID="chkComponentMinus" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="Component Name" DataField="PayComponent_Name" />
                                                        <asp:TemplateField HeaderText="Value">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtSalaryCompMinus" CssClass="form-control" runat="server" placeholder="Salary" autocomplete="off" Text='<%# Eval("HRMSEmployeeSalaryComponent_PayComponent_Value") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row boxborder">
                                    <div class="table-header">
                                        <span style="color: cornsilk; font-weight: bold">Section -4:</span>
                                        Bank Details  *
                                    </div>
                                    <div class="col-xs-12 boxcontent">
                                        <div class="row">
                                            <div class="col-xs-4">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Bank Name * </label>
                                                    <asp:TextBox ID="ddlBankName" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-4">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Branch name* </label>
                                                    <asp:TextBox ID="txtBranchName" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-4">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Account Number *  </label>
                                                    <asp:TextBox ID="txtAccountNo" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-xs-4">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">IFSC code *  </label>
                                                    <asp:TextBox ID="txtIFSCcode" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
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


            <asp:HiddenField ID="hf_HRMSEmployee_Id" runat="server" Value="0" />
            <asp:HiddenField ID="hf_HRMSEmployeeJuridiction_Id" runat="server" Value="0" />
            <asp:HiddenField ID="hf_HRMSEmployeeSalaryInfo_Id" runat="server" Value="0" />
            <asp:HiddenField ID="hf_HRMSEmployeeBankDetails_Id" runat="server" Value="0" />
        </div>

    </div>
</asp:Content>

