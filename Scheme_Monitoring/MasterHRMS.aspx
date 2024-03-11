<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin2.master" AutoEventWireup="true" CodeFile="MasterHRMS.aspx.cs" Inherits="MasterHRMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="main-content">
        <div class="main-content-inner">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
            </cc1:ToolkitScriptManager>
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <style>
                        .control-label {
                            text-transform: uppercase;
                            color: #081748;
                            font-weight: 500;
                            font-family: Verdana, Geneva, Tahoma, sans-serif;
                            transition: 0.7s;
                        }

                            .control-label:hover {
                                color: blueviolet;
                                letter-spacing: 0.8px;
                            }
                    </style>
                    <div class="page-content">
                        <div class="page-header">
                            <div class="row">
                                <h1>Register New Employee Information *</h1>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="tabbable">
                                    <ul class="nav nav-tabs" id="myTab2">
                                        <li class="active" id="w_1" onclick="setTabPageActive('w_1', 'wt_1', 'doc1', 7)">
                                            <a data-toggle="tab" href="#doc1" aria-expanded="true" id="wt_1">
                                                <span style="font-weight: bolder; font-size: 15px">Section-1:</span>
                                                <i class="blue ace-icon fa fa-file-pdf-o"></i>
                                                Employee Basic Details
                                            </a>
                                        </li>
                                        <li class="" id="w_2" onclick="setTabPageActive('w_2', 'wt_2', 'doc2', 7)">
                                            <a data-toggle="tab" href="#doc2" aria-expanded="false" id="wt_2">
                                                <span style="font-weight: bolder; font-size: 15px">Section-2:</span>
                                                <i class="blue ace-icon fa fa-file-pdf-o"></i>
                                                Employee Current Posting Details
                                            </a>
                                        </li>
                                        <li class="" id="w_3" onclick="setTabPageActive('w_3', 'wt_3', 'doc3', 7)">
                                            <a data-toggle="tab" href="#doc3" aria-expanded="false" id="wt_3">
                                                <span style="font-weight: bolder; font-size: 15px">Section-3:</span>
                                                <i class="blue ace-icon fa fa-file-pdf-o"></i>
                                                Salary Breakup Info
                                            </a>
                                        </li>

                                        <li class="" id="w_4" onclick="setTabPageActive('w_4', 'wt_4', 'doc4', 7)">
                                            <a data-toggle="tab" href="#doc4" aria-expanded="false" id="wt_4">
                                                <span style="font-weight: bolder; font-size: 15px">Section-4:</span>
                                                <i class="blue ace-icon fa fa-file-pdf-o"></i>
                                                Bank Details
                                            </a>
                                        </li>
                                    </ul>
                                    <div class="tab-content">

                                        <div id="doc1" class="tab-pane fade active in">
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Employee Name * </label>
                                                        <asp:TextBox ID="txtEmployeeName" runat="server" CssClass="form-control" placeholder="Employee Name" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Spouse Name * </label>
                                                        <asp:TextBox ID="txtSpouseName" runat="server" CssClass="form-control" Placeholder="Husband/Wife Name" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Special Category * </label>
                                                        <asp:DropDownList ID="ddlSpecialCategory" runat="server" CssClass="form-control">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                            </div>

                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Father's Name * </label>
                                                        <asp:TextBox ID="txtEmployeeFather" runat="server" CssClass="form-control" Placeholder="father Name" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Gender *</label><br />
                                                        <asp:RadioButtonList ID="rbtGender" runat="server" AutoPostBack="true" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="Male" Text="&nbsp; Male &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"></asp:ListItem>
                                                            <asp:ListItem Value="Female" Text="&nbsp; Female &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"></asp:ListItem>
                                                            <asp:ListItem Value="Trans Gender" Text="&nbsp;Other&nbsp;&nbsp;&nbsp;&nbsp;"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Marital Status *</label>
                                                        <asp:DropDownList ID="ddlMaritalStatus" runat="server" CssClass="form-control">
                                                            <asp:ListItem Text=" -- Select --" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="Married" Value="Married"></asp:ListItem>
                                                            <asp:ListItem Text="Unmarried" Value="Unmarried"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Date Of Birth * </label>
                                                        <asp:TextBox ID="txtDOB" runat="server" CssClass="form-control date-picker" placeholder="DOB" data-date-format="dd/mm/yyyy" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Appointment date * </label>
                                                        <asp:TextBox ID="txtAppointmentDate" runat="server" CssClass="form-control date-picker" placeholder="Appointment Date" data-date-format="dd/mm/yyyy" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Date Of Joining in Service * </label>
                                                        <asp:TextBox ID="txtJoiningDate" runat="server" CssClass="form-control date-picker" Placeholder="Joining Date" data-date-format="dd/mm/yyyy" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>

                                            </div>

                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Departmental Employee code * </label>
                                                        <asp:TextBox ID="txtDepartmentealEmployeeCode" runat="server" CssClass="form-control" autocomplete="off" placeholder="Employee Code"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Date Of Marriage * </label>
                                                        <asp:TextBox ID="txtMarriageDate" runat="server" CssClass="form-control date-picker" placeholder="Marriage Date" data-date-format="dd/mm/yyyy" autocomplete="off">
                                                        </asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Employee Type * </label>
                                                        <asp:DropDownList ID="ddlEmployeeType" runat="server" CssClass="form-control">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Husband/Wife e-HRMS Code {If In Government} *</label>
                                                        <asp:TextBox ID="txtSpouseCode" runat="server" CssClass="form-control" placeholder="Spouse eHRMS Code" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Catagory *</label>
                                                        <asp:DropDownList ID="ddlCaste" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Religion *</label>
                                                        <asp:DropDownList ID="ddlReligion" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="table-header">Permanent Address Details *</div>
                                            <div class="row">
                                                <br />
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Full Address * </label>
                                                        <br />
                                                        <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <br />
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Home State * </label>
                                                        <br />
                                                        <asp:DropDownList ID="ddlHomeState" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlHomeState_SelectedIndexChanged" AutoPostBack="true">
                                                            <%-- <asp:ListItem Text="Uttar Pradesh" Value=""></asp:ListItem>--%>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Home District * </label>
                                                        <br />
                                                        <asp:DropDownList ID="ddlHomeDistrict" CssClass="form-control" runat="server" AutoPostBack="true">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Pin Code Of Area * </label>
                                                        <br />
                                                        <asp:TextBox ID="txtPinCode" CssClass="form-control" runat="server" Placeholder="220330" autocomplete="off" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <br />
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Email Address * </label>
                                                        <br />
                                                        <asp:TextBox ID="txtEmployeeEmail" CssClass="form-control" runat="server" Placeholder="example123@gmail.com" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Mobile Number *</label>
                                                        <br />
                                                        <asp:TextBox ID="txtMobile" CssClass="form-control" runat="server" Placeholder="contact" autocomplete="off" MaxLength="10"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">CUG No.*</label>
                                                        <br />
                                                        <asp:TextBox ID="txtCUGno" CssClass="form-control" runat="server" Placeholder="Closed User Group" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <br />
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">AADHAR* </label>
                                                        <br />
                                                        <asp:TextBox ID="txtAadharNo" CssClass="form-control" runat="server" Placeholder="123434567" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">PAN Number *</label>
                                                        <br />
                                                        <asp:TextBox ID="txtPanNo" CssClass="form-control" runat="server" Placeholder="VGTUS1586D" autocomplete="off" MaxLength="10"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">HRMS / Manav Sampada Code*</label>
                                                        <br />
                                                        <asp:TextBox ID="txtManavSampadaCode" CssClass="form-control" runat="server" Placeholder="Manav Sampada Code" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div id="doc2" class="tab-pane fade">
                                            <div class="row">
                                                <br />
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblZoneH" runat="server" Text="Zone" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlZone" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblCircleH" runat="server" Text="Circle" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlCircle" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlCircle_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblDivisionH" CssClass="control-label no-padding-right" Text="Division" runat="server"></asp:Label>
                                                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <br />
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Designation * </label>
                                                        <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Cadre * </label>
                                                        <asp:DropDownList ID="ddlCadre" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCadre_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">PRAN No* </label>
                                                        <br />
                                                        <asp:TextBox ID="txtPraanNo" CssClass="form-control" runat="server" autocomplete="off">
                                                        </asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <br />
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Date Of Retirement * </label>
                                                        <asp:TextBox ID="txtRetirmentDate" runat="server" CssClass="form-control date-picker" placeholder="Retirement Date" data-date-format="dd/mm/yyyy" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Joining date in current office  * </label>
                                                        <br />
                                                        <asp:TextBox ID="txtJoiningDateCurrentOffice" CssClass="form-control date-picker" runat="server" placeholder="Joining date" data-date-format="dd/mm/yyyy" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">G.P.F No.* </label>
                                                        <asp:TextBox ID="txtGPFNo" runat="server" CssClass="form-control " placeholder="GPF No"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div id="doc3" class="tab-pane fade">
                                            <div class="row">
                                                <br />
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">pay Band * </label>
                                                        <br />
                                                        <asp:DropDownList ID="ddlPayBand" CssClass="form-control " runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPayBand_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Pay Scale * </label>
                                                        <br />
                                                        <asp:DropDownList ID="ddlPayScale" CssClass="form-control" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlPayScale_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Grade Pay</label>
                                                        <br />
                                                        <asp:DropDownList ID="ddlGradePay" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlGradePay_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <br />
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Gov. Quarter?</label>
                                                        &nbsp;&nbsp;
                                                        <asp:CheckBox ID="chkbxQuaterAlot" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Cug No?</label>&nbsp;
                                                        <asp:CheckBox ID="chkbxCUGno" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Vehicle?</label>
                                                        <asp:CheckBox ID="chkbxVehicle" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <div class="col-md-4">
                                                                <div class="form-group">
                                                                    <label class="control-label no-padding-right">GPF/NPS:</label>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-8">
                                                                <asp:RadioButtonList ID="rbtGPFNPS" runat="server" AutoPostBack="true" RepeatDirection="Vertical" OnSelectedIndexChanged="rbtGPFNPS_SelectedIndexChanged">
                                                                    <asp:ListItem Value="NPS" Text="&nbsp;NPS &nbsp;" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Value="GPF" Text="&nbsp;GPF &nbsp;"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <div class="col-md-4">
                                                                <div class="form-group">
                                                                    <label class="control-label no-padding-right">City Type :</label>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-8">
                                                                <asp:RadioButtonList ID="rbtCityType" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Value="Rural" Text="&nbsp;Rural &nbsp;"></asp:ListItem>
                                                                    <asp:ListItem Value="Urban" Text="&nbsp;Urban &nbsp;"></asp:ListItem>
                                                                    <asp:ListItem Value="Semi-Urban" Text="Semi-Urban &nbsp;"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="table-header">
                                                    Salary Component Wise Details: 
                                                </div>
                                                <div class="col-md-12">
                                                    <table class="table table-striped table-bordered table-hover no-margin-bottom no-border-top">
                                                        <thead>
                                                            <tr>
                                                                <th>Sr No</th>
                                                                <th>Component</th>
                                                                <th>Manual Calculation</th>
                                                                <th>Addition Value</th>
                                                                <th>Deduction Value</th>
                                                                <th>Total Salary
                                                                    <asp:TextBox ID="txtDays" CssClass="form-control" runat="server" placeholder="Salary Days" autocomplete="off" AutoPostBack="true" OnTextChanged="CalculateSalary" Text="30" Width="40px"></asp:TextBox> / <asp:TextBox ID="txtTotalDays" CssClass="form-control" runat="server" placeholder="Days In Month" autocomplete="off" AutoPostBack="true" OnTextChanged="CalculateSalary" Text="30" Width="40px"></asp:TextBox> 
                                                                </th>
                                                            </tr>
                                                        </thead>

                                                        <tbody>
                                                            <tr>
                                                                <td>1</td>
                                                                <td>Basic Salary</td>
                                                                <td>&nbsp;</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtBasicSalary" CssClass="form-control" runat="server" placeholder="Basic salary" autocomplete="off" AutoPostBack="true" OnTextChanged="CalculateSalary"></asp:TextBox>
                                                                </td>
                                                                <td>&nbsp;</td>
                                                                <td>
                                                                    <asp:Label class="control-label no-padding-right" runat="server" ID="lblBasicSal" Text="0"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>2</td>
                                                                <td>Grade Pay</td>
                                                                <td>&nbsp;</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtGradePay" CssClass="form-control" runat="server" placeholder="Grade Pay" autocomplete="off" AutoPostBack="true" OnTextChanged="CalculateSalary"></asp:TextBox>
                                                                </td>
                                                                <td>&nbsp;</td>
                                                                <td>
                                                                    <asp:Label class="control-label no-padding-right" runat="server" ID="lblGradePay" Text="0"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>3</td>
                                                                <td>DA</td>
                                                                <td>
                                                                    <asp:CheckBox runat="server" ID="chkDA" AutoPostBack="true" OnCheckedChanged="chkDA_CheckedChanged" />
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtDA" CssClass="form-control" runat="server" placeholder="DA" autocomplete="off" AutoPostBack="true" OnTextChanged="CalculateSalary" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td>(3 = (1 + 2) * 189%)</td>
                                                                <td>
                                                                    <asp:Label class="control-label no-padding-right" runat="server" ID="lblDA" Text="0"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>4</td>
                                                                <td>HRA</td>
                                                                <td>&nbsp;</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtHRA" CssClass="form-control" runat="server" placeholder="HRA" autocomplete="off" AutoPostBack="true" OnTextChanged="CalculateSalary"></asp:TextBox>
                                                                </td>
                                                                <td>&nbsp;</td>
                                                                <td>
                                                                    <asp:Label class="control-label no-padding-right" runat="server" ID="lblHRA" Text="0"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>5</td>
                                                                <td>MA</td>
                                                                <td>&nbsp;</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtMA" CssClass="form-control" runat="server" placeholder="MA" autocomplete="off" AutoPostBack="true" OnTextChanged="CalculateSalary"></asp:TextBox>
                                                                </td>
                                                                <td>Flat Value</td>
                                                                <td>
                                                                    <asp:Label class="control-label no-padding-right" runat="server" ID="lblMA" Text="0"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>6</td>
                                                                <td>Personal Pay</td>
                                                                <td>&nbsp;</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtPersonalPay" CssClass="form-control" runat="server" placeholder="Personal Pay" autocomplete="off" AutoPostBack="true" OnTextChanged="CalculateSalary"></asp:TextBox>
                                                                </td>
                                                                <td>&nbsp;</td>
                                                                <td>
                                                                    <asp:Label class="control-label no-padding-right" runat="server" ID="lblPersonalPay" Text="0"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>7</td>
                                                                <td>Special Pay</td>
                                                                <td>&nbsp;</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtSpecialPay" CssClass="form-control" runat="server" placeholder="Special Pay" autocomplete="off" AutoPostBack="true" OnTextChanged="CalculateSalary"></asp:TextBox>
                                                                </td>
                                                                <td>&nbsp;</td>
                                                                <td>
                                                                    <asp:Label class="control-label no-padding-right" runat="server" ID="lblSpecialPay" Text="0"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>8</td>
                                                                <td>Other Allowance</td>
                                                                <td>&nbsp;</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtOtherAll" CssClass="form-control" runat="server" placeholder="Other Allowance" autocomplete="off" AutoPostBack="true" OnTextChanged="CalculateSalary"></asp:TextBox>
                                                                </td>
                                                                <td>&nbsp;</td>
                                                                <td>
                                                                    <asp:Label class="control-label no-padding-right" runat="server" ID="lblOtherAll" Text="0"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>9</td>
                                                                <td>Gross Salary</td>
                                                                <td>&nbsp;</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtGrossSal" CssClass="form-control" runat="server" placeholder="Gross Salary" autocomplete="off" AutoPostBack="true" OnTextChanged="CalculateSalary" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td>(9 = 1 + 2 + 3 + 4 + 5 + 6 + 7 + 8)</td>
                                                                <td>
                                                                    <asp:Label class="control-label no-padding-right" runat="server" ID="lblGrossSal" Text="0"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>10</td>
                                                                <td>Employer's NPS Contributon (Current)</td>
                                                                <td>
                                                                    <asp:CheckBox runat="server" ID="chkNPSContCurrentEmployer" AutoPostBack="true" OnCheckedChanged="chkNPSContCurrentEmployer_CheckedChanged" />
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtNPSContCurrentEmployer" CssClass="form-control" runat="server" placeholder="Employer's NPS Contributon Current" autocomplete="off" AutoPostBack="true" OnTextChanged="CalculateSalary" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td>(10 = (1 + 2 + 3) * 14%)</td>
                                                                <td>
                                                                    <asp:Label class="control-label no-padding-right" runat="server" ID="lblNPSContCurrentEmployer" Text="0"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>11</td>
                                                                <td>Employer's NPS Contributon (Arrear)</td>
                                                                <td>&nbsp;
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtNPSContArrearEmployer" CssClass="form-control" runat="server" placeholder="Employer's NPS Contributon Arrear" autocomplete="off" AutoPostBack="true" OnTextChanged="CalculateSalary"></asp:TextBox>
                                                                </td>
                                                                <td>&nbsp</td>
                                                                <td>
                                                                    <asp:Label class="control-label no-padding-right" runat="server" ID="lblNPSContArrearEmployer" Text="0"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>12</td>
                                                                <td>Gross Salary Including NPS Contribution</td>
                                                                <td>&nbsp;</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtGrossSalInclNPS" CssClass="form-control" runat="server" placeholder="Gross Salary Including NPS Contribution" autocomplete="off" AutoPostBack="true" OnTextChanged="CalculateSalary" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td>(12 = 9 + 10 + 11)</td>
                                                                <td>
                                                                    <asp:Label class="control-label no-padding-right" runat="server" ID="lblGrossSalInclNPS" Text="0"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>13</td>
                                                                <td>GPF</td>
                                                                <td>
                                                                    <asp:CheckBox runat="server" ID="chkGPF" AutoPostBack="true" OnCheckedChanged="chkGPF_CheckedChanged" />
                                                                </td>
                                                                <td>(13 = (1 + 2) * 10%)</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtGPF" CssClass="form-control" runat="server" placeholder="GPF" autocomplete="off" AutoPostBack="true" OnTextChanged="CalculateSalary" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:Label class="control-label no-padding-right" runat="server" ID="lblGPF" Text="0"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>14</td>
                                                                <td>GPF Advance</td>
                                                                <td>&nbsp;</td>
                                                                <td>&nbsp;</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtGPFAdvance" CssClass="form-control" runat="server" placeholder="GPF Advance" autocomplete="off" AutoPostBack="true" OnTextChanged="CalculateSalary"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:Label class="control-label no-padding-right" runat="server" ID="lblGPFAdvance" Text="0"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>15</td>
                                                                <td>GIS</td>
                                                                <td>
                                                                    <asp:CheckBox runat="server" ID="chkGIS" AutoPostBack="true" OnCheckedChanged="chkGIS_CheckedChanged" />
                                                                </td>
                                                                <td>Based On Selection Of Cadre / Class</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtGIS" CssClass="form-control" runat="server" placeholder="GIS" autocomplete="off" AutoPostBack="true" OnTextChanged="CalculateSalary" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:Label class="control-label no-padding-right" runat="server" ID="lblGIS" Text="0"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>16</td>
                                                                <td>Total Deduction to be Invested at HQ level</td>
                                                                <td>&nbsp;</td>
                                                                <td>(16 = 13 + 14 + 15)</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtDeductionInvestedHQ" CssClass="form-control" runat="server" placeholder="Deduction to be Invested at HQ level" autocomplete="off" AutoPostBack="true" OnTextChanged="CalculateSalary" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:Label class="control-label no-padding-right" runat="server" ID="lblDeductionInvestedHQ" Text="0"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>17</td>
                                                                <td>Income Tax</td>
                                                                <td>&nbsp;</td>
                                                                <td>&nbsp;</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtIncomeTax" CssClass="form-control" runat="server" placeholder="Income Tax" autocomplete="off" AutoPostBack="true" OnTextChanged="CalculateSalary"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:Label class="control-label no-padding-right" runat="server" ID="lblIncomeTax" Text="0"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>18</td>
                                                                <td>Emploee's NPS Contributon (Current)</td>
                                                                <td>
                                                                    <asp:CheckBox runat="server" ID="chkNPSContCurrentEmployee" AutoPostBack="true" OnCheckedChanged="chkNPSContCurrentEmployee_CheckedChanged" />
                                                                </td>
                                                                <td>(18 = (1 + 2 + 3) * 10%)</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtNPSContCurrentEmployee" CssClass="form-control" runat="server" placeholder="Employee's NPS Contributon Current" autocomplete="off" AutoPostBack="true" OnTextChanged="CalculateSalary" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:Label class="control-label no-padding-right" runat="server" ID="lblNPSContCurrentEmployee" Text="0"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>19</td>
                                                                <td>Employee's NPS Contributon (Arrear)</td>
                                                                <td>&nbsp;
                                                                </td>
                                                                <td>&nbsp;</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtNPSContArrearEmployee" CssClass="form-control" runat="server" placeholder="Employee's NPS Contributon Arrear" autocomplete="off" AutoPostBack="true" OnTextChanged="CalculateSalary"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:Label class="control-label no-padding-right" runat="server" ID="lblNPSContArrearEmployee" Text="0"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>20</td>
                                                                <td>Total Deduction to be Paid</td>
                                                                <td>&nbsp;</td>
                                                                <td>(20 = 17 + 18 + 19)</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtDeductionPaid" CssClass="form-control" runat="server" placeholder="Deduction to be Paid" autocomplete="off" AutoPostBack="true" OnTextChanged="CalculateSalary" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:Label class="control-label no-padding-right" runat="server" ID="lblDeductionPaid" Text="0"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>21</td>
                                                                <td>HRA For Jal Nigam Colony Employee</td>
                                                                <td>&nbsp;</td>
                                                                <td>&nbsp;</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtHRAColony" CssClass="form-control" runat="server" placeholder="HRA For Jal Nigam Colony Employee" autocomplete="off" AutoPostBack="true" OnTextChanged="CalculateSalary"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:Label class="control-label no-padding-right" runat="server" ID="lblHRAColony" Text="0"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>22</td>
                                                                <td>Colony Maintance</td>
                                                                <td>&nbsp;</td>
                                                                <td>&nbsp;</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtColonyMaintance" CssClass="form-control" runat="server" placeholder="Colony Maintance" autocomplete="off" AutoPostBack="true" OnTextChanged="CalculateSalary"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:Label class="control-label no-padding-right" runat="server" ID="lblColonyMaintance" Text="0"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>23</td>
                                                                <td>Motor Vehicle Deduction</td>
                                                                <td>&nbsp;</td>
                                                                <td>&nbsp;</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtMotorVehicleDed" CssClass="form-control" runat="server" placeholder="Motor Vehicle Deduction" autocomplete="off" AutoPostBack="true" OnTextChanged="CalculateSalary"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:Label class="control-label no-padding-right" runat="server" ID="lblMotorVehicleDed" Text="0"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>24</td>
                                                                <td>Other Deduction</td>
                                                                <td>&nbsp;</td>
                                                                <td>&nbsp;</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtOtherDeduction" CssClass="form-control" runat="server" placeholder="Other Deduction" autocomplete="off" AutoPostBack="true" OnTextChanged="CalculateSalary"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:Label class="control-label no-padding-right" runat="server" ID="lblOtherDeduction" Text="0"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>25</td>
                                                                <td>Total Deduction Not to be Paid</td>
                                                                <td>&nbsp;</td>
                                                                <td>(25 = 21 + 22 + 23 + 24)</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtDeductionNotPaid" CssClass="form-control" runat="server" placeholder="Deduction Not to be Paid" autocomplete="off" AutoPostBack="true" OnTextChanged="CalculateSalary" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:Label class="control-label no-padding-right" runat="server" ID="lblDeductionNotPaid" Text="0"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>26</td>
                                                                <td>Total Deduction</td>
                                                                <td>&nbsp;</td>
                                                                <td>(26 = 10 + 11 + 16 + 20 + 25)</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtDeductionTotal" CssClass="form-control" runat="server" placeholder="Total Deduction" autocomplete="off" AutoPostBack="true" OnTextChanged="CalculateSalary" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:Label class="control-label no-padding-right" runat="server" ID="lblDeductionTotal" Text="0"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>27</td>
                                                                <td>Net Salary Payble To Division</td>
                                                                <td>&nbsp;</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtNetSalaryPaybleToDivision" CssClass="form-control" runat="server" placeholder="Net Salary Payble To Division" autocomplete="off" AutoPostBack="true" OnTextChanged="CalculateSalary" Enabled="false" Font-Bold="true"></asp:TextBox>
                                                                </td>
                                                                <td>(27 = 12 - 16 - 25)</td>
                                                                <td>
                                                                    <asp:Label class="control-label no-padding-right" runat="server" ID="lblNetSalaryPaybleToDivision" Text="0"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>28</td>
                                                                <td>Net Salary Payble To Employee</td>
                                                                <td>&nbsp;</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtNetSalaryPaybleToEmployee" CssClass="form-control" runat="server" placeholder="Net Salary Payble To Employee" autocomplete="off" AutoPostBack="true" OnTextChanged="CalculateSalary" Enabled="false" Font-Bold="true"></asp:TextBox>
                                                                </td>
                                                                <td>(28 = 27 - 10 - 11 - 20)</td>
                                                                <td>
                                                                    <asp:Label class="control-label no-padding-right" runat="server" ID="lblNetSalaryPaybleToEmployee" Text="0"></asp:Label></td>
                                                            </tr>
                                                        </tbody>

                                                        <tfoot>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td><asp:Button ID="btnFreeze" runat="server" CssClass="btn btn-pink" Text="Freeze" OnClick="btnFreeze_Click" /></td>
                                                        </tfoot>
                                                    </table>
                                                </div>

                                                <div class="col-md-12">
                                                    <div class="col-md-6">
                                                        <br />
                                                        <asp:CheckBox runat="server" ID="chkUpdateLastGeneratedSal" Text="Update Last Generated Salary Data Also." ForeColor="Red" Font-Bold="true" AutoPostBack="true" OnCheckedChanged="chkUpdateLastGeneratedSal_CheckedChanged" />
                                                    </div>
                                                    <div class="col-md-6" runat="server" visible="false" id="divLastGeneratedSal">
                                                        <br />
                                                        <asp:ListBox ID="ddlLastGeneratedSal" runat="server" SelectionMode="Multiple" class="chosen-select form-control"
                                                            data-placeholder="Choose a Month..."></asp:ListBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div id="doc4" class="tab-pane fade">
                                            <div class="row">
                                                <br />

                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Bank Name* </label>
                                                        <br />
                                                        <asp:DropDownList ID="ddlBankName" CssClass="form-control" runat="server" AutoPostBack="true">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Branch name* </label>
                                                        <br />
                                                        <asp:TextBox ID="txtBranchName" CssClass="form-control" runat="server" Placeholder="Branch Name" autocomplete="off">
                                                        </asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Account Number * </label>
                                                        <br />
                                                        <asp:TextBox ID="txtAccountNo" CssClass="form-control" runat="server" Placeholder="Account Number" autocomplete="off" onkeyup="isNumericVal(this);" MaxLength="18"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <br />
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">IFSC code * </label>
                                                        <br />
                                                        <asp:TextBox ID="txtIFSCcode" CssClass="form-control " runat="server" placeholder="BOB123456" autocomplete="off" MaxLength="11">
                                                        </asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <br />
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <br />
                                    <span style="margin-left: 50px">
                                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-info" Text="Save Employee Details" OnClick="btnSave_Click" />
                                    </span>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <br />
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="hf_FreezeMode" runat="server" Value="0" />
                        <asp:HiddenField ID="hf_Employee_Id" runat="server" Value="0" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdateProgress ID="UpdateProgress1" DynamicLayout="true" runat="server" AssociatedUpdatePanelID="up">
                <ProgressTemplate>
                    <div style="position: fixed; z-index: 999; height: 100%; width: 100%; top: 0; background-color: Black; filter: alpha(opacity=60); opacity: 0.6; -moz-opacity: 0.8; cursor: not-allowed;">
                        <div style="z-index: 1000; margin: 300px auto; padding: 10px; width: 130px; background-color: transparent; border-radius: 1px; filter: alpha(opacity=100); opacity: 1; -moz-opacity: 1;">
                            <img src="assets/images/mb/mbloader.gif" style="height: 150px; width: 150px;" />
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
    </div>
    <script type="text/javascript">

        function setTabPageActive(mainMenuId, subMenuId, contentPageId, totalCount) {
            debugger;
            for (var i = 0; i < totalCount; i++) {
                $("#w_" + (i + 1)).removeClass('active');
                $("#wt_" + (i + 1)).attr('aria-expanded', 'false');
                $("#doc" + (i + 1)).removeClass('active in');
            }
            //$('#nav nav-tabs').find('li[class^="active"]').removeClass('active');
            //$('#nav nav-tabs').find('a').removeAttr('aria-expanded');

            $("#" + mainMenuId + "").addClass('active');
            $("#" + subMenuId + "").attr('aria-expanded', 'true');
            $("#" + contentPageId + "").addClass('active in');

            sessionStorage["_activeMainTabMenu"] = mainMenuId;
            sessionStorage["_activeSubTabMenu"] = subMenuId;
            sessionStorage["_activecontentPageId"] = contentPageId;
            sessionStorage["_activetotalCount"] = totalCount;
        }


        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            $(document).ready(function () {
                debugger;
                if (sessionStorage["_activeMainTabMenu"] == "" || sessionStorage["_activeSubTabMenu"] == undefined || sessionStorage["_activetotalCount"] == undefined) { }
                else {
                    //$('#nav nav-tabs').find('li').removeAttr('class');
                    var totalTabs = sessionStorage["_activetotalCount"];
                    for (var i = 0; i < totalTabs; i++) {
                        $("#w_" + (i + 1)).removeClass('active');
                        $("#wt_" + (i + 1)).attr('aria-expanded', 'false');
                        $("#doc" + (i + 1)).removeClass('active in');
                    }

                    $("#" + sessionStorage["_activeMainTabMenu"] + "").addClass('active');
                    $("#" + sessionStorage["_activecontentPageId"] + "").addClass('active in');
                    $("#" + sessionStorage["_activeSubTabMenu"] + "").attr('aria-expanded', 'true');
                }
            });
        });

    </script>

</asp:Content>
