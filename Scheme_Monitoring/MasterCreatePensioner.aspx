<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin2.master" AutoEventWireup="true" CodeFile="MasterCreatePensioner.aspx.cs" Inherits="MasterCreatePensioner" %>

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
                            color: #383535;
                            font-family: sans-serif;
                            transition: 0.6s;
                        }

                            .control-label:hover {
                                color: red;
                                letter-spacing: 0.5px;
                            }
                    </style>
                    <div class="page-content">
                        <div class="page-header">
                            <div class="row">
                                <h1>Add New Pensioner Details</h1>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="tabbable">
                                    <ul class="nav nav-tabs" id="myTab2">
                                        <li class="active" id="w_1" onclick="setTabPageActive('w_1', 'wt_1', 'doc1', 7)">
                                            <a data-toggle="tab" href="#doc1" aria-expanded="true" id="wt_1">
                                                <span style="font-weight: bolder; font-size: 15px">Section-1:</span>
                                                <i class="green ace-icon fa fa-file-pdf-o"></i>
                                                Basic Details
                                            </a>
                                        </li>
                                        <li class="" id="w_2" onclick="setTabPageActive('w_2', 'wt_2', 'doc2', 7)">
                                            <a data-toggle="tab" href="#doc2" aria-expanded="false" id="wt_2">
                                                <span style="font-weight: bolder; font-size: 15px">Section-2:</span>
                                                <i class="green ace-icon fa fa-file-pdf-o"></i>
                                                Pension Details
                                            </a>
                                        </li>
                                        <li class="" id="w_3" onclick="setTabPageActive('w_3', 'wt_3', 'doc3', 7)">
                                            <a data-toggle="tab" href="#doc3" aria-expanded="false" id="wt_3">
                                                <span style="font-weight: bolder; font-size: 15px">Section-3:</span>
                                                <i class="green ace-icon fa fa-file-pdf-o"></i>
                                                Gratuity Details
                                            </a>
                                        </li>

                                        <li class="" id="w_4" onclick="setTabPageActive('w_4', 'wt_4', 'doc4', 7)">
                                            <a data-toggle="tab" href="#doc4" aria-expanded="false" id="wt_4">
                                                <span style="font-weight: bolder; font-size: 15px">Section-4:</span>
                                                <i class="green ace-icon fa fa-file-pdf-o"></i>
                                                Leave Encashment Details
                                            </a>
                                        </li>

                                        <li class="" id="w_5" onclick="setTabPageActive('w_5', 'wt_5', 'doc5', 7)">
                                            <a data-toggle="tab" href="#doc5" aria-expanded="false" id="wt_5">
                                                <span style="font-weight: bolder; font-size: 15px">Section-5:</span>
                                                <i class="green ace-icon fa fa-file-pdf-o"></i>
                                                Group Insurance Scheme (GIS) Details
                                            </a>
                                        </li>

                                        <li class="" id="w_6" onclick="setTabPageActive('w_6', 'wt_6', 'doc6', 7)">
                                            <a data-toggle="tab" href="#doc6" aria-expanded="false" id="wt_6">
                                                <span style="font-weight: bolder; font-size: 15px">Section-6:</span>
                                                <i class="green ace-icon fa fa-file-pdf-o"></i>
                                                Pension Arrear Details
                                            </a>
                                        </li>

                                        <li class="" id="w_7" onclick="setTabPageActive('w_7', 'wt_7', 'doc7', 7)">
                                            <a data-toggle="tab" href="#doc7" aria-expanded="false" id="wt_7">
                                                <span style="font-weight: bolder; font-size: 15px">Section-7:</span>
                                                <i class="green ace-icon fa fa-file-pdf-o"></i>
                                                Other Details
                                            </a>
                                        </li>
                                    </ul>
                                    <div class="tab-content">
                                        <div id="doc1" class="tab-pane fade active in">
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Retirement <%=Session["Default_Zone"].ToString() %>:</label>
                                                        <asp:DropDownList ID="ddlZoneRetirement" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlZoneRetirement_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Retirement <%=Session["Default_Division"].ToString() %>:</label>
                                                        <asp:DropDownList ID="ddlDivisionRetirement" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Pension <%=Session["Default_Zone"].ToString() %>:</label>
                                                        <asp:DropDownList ID="ddlZonePension" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlZonePension_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Pension <%=Session["Default_Division"].ToString() %>: </label>
                                                        <asp:DropDownList ID="ddlDivisionPension" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Name Of Pensioner:</label>
                                                        <asp:TextBox ID="txtPensionerName" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Father Name Or Husband Name:</label>
                                                        <asp:TextBox ID="txtFatherName" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Mobile Number:</label>
                                                        <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control" MaxLength="10" autocomplete="off" onkeyup="isNumericVal(this);"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Pan Card Number:</label>
                                                        <asp:TextBox ID="txtPanCardNo" runat="server" CssClass="form-control" MaxLength="10" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Aadhar Card Number:</label>
                                                        <asp:TextBox ID="txtAadharNo" runat="server" CssClass="form-control" MaxLength="12" onkeyup="isNumericVal(this);" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Permanent Address/स्थायी निवास स्थान:</label>
                                                        <asp:TextBox ID="txtPermanentAddress" runat="server" CssClass="form-control" TextMode="MultiLine" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Postal address/पत्र व्यवहार का पता:</label>
                                                        <asp:TextBox ID="txtPostalAddress" runat="server" CssClass="form-control" TextMode="MultiLine" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Gender *</label><br />
                                                        <asp:RadioButtonList ID="rbtGender" runat="server" AutoPostBack="true" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="M" Text="&nbsp; Male &nbsp;"></asp:ListItem>
                                                            <asp:ListItem Value="F" Text="&nbsp; Female &nbsp;"></asp:ListItem>
                                                            <asp:ListItem Value="Tg" Text="&nbsp; Other"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Retirement Year/सेवा निवृति का वर्ष   :</label>
                                                        <asp:TextBox ID="txtRetirementYear" runat="server" CssClass="form-control datepicker" MaxLength="4" onkeyup="isNumericVal(this);" AutoComplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Date of Birth : </label>
                                                        <asp:TextBox ID="txtDOB" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Date of Joining : </label>
                                                        <asp:TextBox ID="txtDOJ" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Date Of Retirement: </label>
                                                        <asp:TextBox ID="txtDOR" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Date Of Death: </label>
                                                        <asp:TextBox ID="txtDOD" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Date Of Regularization: </label>
                                                        <asp:TextBox ID="txtDORegularization" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Date Of Sending Pension Form By Division: </label>
                                                        <asp:TextBox ID="txtPensionFormSendingDate" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Upload Photo:</label>
                                                        <asp:FileUpload ID="flPhoto" runat="server"></asp:FileUpload>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Date Of Receiving Pension Form By HQ: </label>
                                                        <asp:TextBox ID="txtPensionFormreceivingDate" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">AAO Pension Name:</label>
                                                        <asp:TextBox ID="txtAAOPensionName" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">संकलित अदेय प्रमाण पत्र अपलोड करें :</label>
                                                        <asp:FileUpload ID="flCompiledOrder" runat="server"></asp:FileUpload>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">पेंशन प्रपत्र अपलोड करें :</label>
                                                        <asp:FileUpload ID="flPensionOrder" runat="server"></asp:FileUpload>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Designation/पदनाम  : </label>
                                                        <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Alphabatically आवंटन के हिसाब से लेखाकार का नाम:</label>
                                                        <asp:TextBox ID="txtAccountantName" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Pension Code: </label>
                                                        <asp:TextBox ID="txtPensionCode" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Upload Live Certificate: </label>
                                                        <asp:FileUpload ID="flLiveCertificate" runat="server"></asp:FileUpload>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Pay Band : </label>
                                                        <asp:DropDownList ID="ddlPayBand" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPayBand_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Pay Scale : </label>
                                                        <asp:DropDownList ID="ddlPayScale" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPayScale_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                 <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Grade Pay : </label>
                                                        <asp:DropDownList ID="ddlGradePay" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Employee Code : </label>
                                                        <asp:TextBox ID="txtEmployeeCode" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">CR Number : </label>
                                                        <asp:TextBox ID="txtCrNo" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-12">
                                                    <div class="table-header">
                                                        Family Member Details 
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:GridView ID="grdFamilyMember" runat="server" AutoGenerateColumns="False" CssClass="display table table-bordered"
                                                        EmptyDataText="No Records Found" OnPreRender="grdFamilyMember_PreRender" OnRowDataBound="grdFamilyMember_RowDataBound">
                                                        <Columns>
                                                            <asp:BoundField DataField="PersonFamilyDtls_Id" HeaderText="PersonFamilyDtls_Id">
                                                                <HeaderStyle CssClass="displayStyle" />
                                                                <ItemStyle CssClass="displayStyle" />
                                                                <FooterStyle CssClass="displayStyle" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="PersonFamilyDtls_Is_GovtServant" HeaderText="PersonFamilyDtls_Is_GovtServant">
                                                                <HeaderStyle CssClass="displayStyle" />
                                                                <ItemStyle CssClass="displayStyle" />
                                                                <FooterStyle CssClass="displayStyle" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="PersonFamilyDtls_Relation" HeaderText="PersonFamilyDtls_Relation">
                                                                <HeaderStyle CssClass="displayStyle" />
                                                                <ItemStyle CssClass="displayStyle" />
                                                                <FooterStyle CssClass="displayStyle" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="PersonFamilyDtls_MaritalStatus" HeaderText="PersonFamilyDtls_MaritalStatus">
                                                                <HeaderStyle CssClass="displayStyle" />
                                                                <ItemStyle CssClass="displayStyle" />
                                                                <FooterStyle CssClass="displayStyle" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="S No.">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Name of Family Member">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" Text='<%# Eval("PersonFamilyDtls_MemberName") %>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Date Of Birth">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtDateOfBirth" runat="server" Text='<%# Eval("PersonFamilyDtls_DOB") %>' CssClass="form-control date-picker" Width="120px" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Relation with the allottee">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlRelation" runat="server" CssClass="form-control" Width="120px" OnSelectedIndexChanged="ddlRelation_SelectedIndexChanged" AutoPostBack="true">
                                                                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                                        <asp:ListItem Value="Self" Text="Self"></asp:ListItem>
                                                                        <asp:ListItem Value="Father" Text="Father"></asp:ListItem>
                                                                        <asp:ListItem Value="Mother" Text="Mother"></asp:ListItem>
                                                                        <asp:ListItem Value="Husband" Text="Husband"></asp:ListItem>
                                                                        <asp:ListItem Value="Wife" Text="Wife"></asp:ListItem>
                                                                        <asp:ListItem Value="Brother" Text="Brother"></asp:ListItem>
                                                                        <asp:ListItem Value="Sister" Text="Sister"></asp:ListItem>
                                                                        <asp:ListItem Value="Son" Text="Son"></asp:ListItem>
                                                                        <asp:ListItem Value="Daughter" Text="Daughter"></asp:ListItem>
                                                                        <asp:ListItem Value="Step Children" Text="Step Children"></asp:ListItem>
                                                                        <asp:ListItem Value="Other" Text="Other"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Age">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtAge" runat="server" CssClass="form-control" Text='<%# Eval("PersonFamilyDtls_Age") %>' Width="60px" onkeyup="checknum(this);" MaxLength="2"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Marital Status">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlMaritalStatus" runat="server" CssClass="form-control" Width="120px">
                                                                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                                        <asp:ListItem Value="Married" Text=" Married"></asp:ListItem>
                                                                        <asp:ListItem Value="Unmarried" Text=" Unmarried"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="whether Govt. Servant or not">
                                                                <ItemTemplate>
                                                                    <asp:RadioButtonList ID="rbtkGovtServ" runat="server" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                                                                        <asp:ListItem Value="No" Text="No"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-12">
                                                    <label class="control-label no-padding-right"><b>मृत्यु होने की दशा में नामिनी का नाम तथा पता जिसे जीवनकालीन अवशेष का भुगतान किया जाए।</b></label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Nominee Name: </label>
                                                        <asp:TextBox ID="txtNomineeName" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Nominee Address: </label>
                                                        <asp:TextBox ID="txtNomineeAddress" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-12">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Pensioner And Family Pensioner: </label>
                                                        <asp:RadioButtonList ID="rblFamilyPensioner" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="Continue_Pension" Text="&nbsp; Continue Pension &nbsp;" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Value="Stop_Pension" Text="&nbsp; Stop Pension &nbsp;"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-12">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Pensioner And Family Pensioner: </label>
                                                        <asp:RadioButtonList ID="rblPensioner" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rblPensioner_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="YES" Text="Yes"></asp:ListItem>
                                                            <asp:ListItem Value="NO" Text="No" Selected="True"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div runat="server" id="rblPensionerYes">
                                                <div class="row">
                                                    <div class="col-xs-3">
                                                        <div class="form-group">
                                                            <label class="control-label no-padding-right">Family Pensioner Name: </label>
                                                            <asp:TextBox ID="txtFamilyPensionerName" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-xs-3">
                                                        <div class="form-group">
                                                            <label class="control-label no-padding-right">Relation: </label>
                                                            <asp:DropDownList ID="ddlFamilyRelation" runat="server" CssClass="form-control">
                                                                <asp:ListItem Value="0">---Select---</asp:ListItem>
                                                                <asp:ListItem Value="Wife">Wife </asp:ListItem>
                                                                <asp:ListItem Value="Husband">Husband</asp:ListItem>
                                                                <asp:ListItem Value="Daughter">Daughter</asp:ListItem>
                                                                <asp:ListItem Value="Son">Son</asp:ListItem>
                                                                <asp:ListItem Value="Other">Other</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-xs-3">
                                                        <div class="form-group">
                                                            <label class="control-label no-padding-right">Date Of Birth:</label>
                                                            <asp:TextBox ID="txtFamilyDOB" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-xs-3">
                                                        <div class="form-group">
                                                            <label class="control-label no-padding-right">Mobile Number: </label>
                                                            <asp:TextBox ID="txtFamilyMobileNumber" runat="server" CssClass="form-control" MaxLength="10" autocomplete="off" onkeyup="isNumericVal(this);"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-xs-3">
                                                        <div class="form-group">
                                                            <label class="control-label no-padding-right">Pan Card Number: </label>
                                                            <asp:TextBox ID="txtFamilyPanNumber" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-xs-3">
                                                        <div class="form-group">
                                                            <label class="control-label no-padding-right">Aadhar Card Number: </label>
                                                            <asp:TextBox ID="txtFamilyAadharNumber" runat="server" CssClass="form-control" MaxLength="12" onkeyup="isNumericVal(this);"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <br />
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <br />
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <br />
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <br />
                                                        <asp:Button ID="btnSaveBasicDetails" runat="server" CssClass="btn btn-pink" Text="Save Basic Details" OnClick="btnSaveBasicDetails_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div id="doc2" class="tab-pane fade">
                                            <div class="row">
                                                <div class="col-xs-12">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Pension: </label>
                                                        <asp:RadioButtonList ID="rbtPensionApplicable" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbtPensionApplicable_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="YES" Text="Yes" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Value="NO" Text="No"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div runat="server" id="divPensionYes">
                                                <div class="row">
                                                    <div class="col-xs-3">
                                                        <div class="form-group">
                                                            <label class="control-label no-padding-right">Basic Pension Amount: </label>
                                                            <asp:TextBox ID="txtBasicPension" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-xs-3">
                                                        <div class="form-group">
                                                            <label class="control-label no-padding-right">Family Pension: </label>
                                                            <asp:TextBox ID="txtFamilyPension" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-xs-3">
                                                        <div class="form-group">
                                                            <label class="control-label no-padding-right">PPO No:</label>
                                                            <asp:TextBox ID="txtPensionPPONo" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-xs-3">
                                                        <div class="form-group">
                                                            <label class="control-label no-padding-right">PPO Date: </label>
                                                            <asp:TextBox ID="txtPensionPPODate" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-xs-3">
                                                        <div class="form-group">
                                                            <label class="control-label no-padding-right">Upload PPO: </label>
                                                            <asp:FileUpload ID="flPPO" runat="server"></asp:FileUpload>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div runat="server" id="divPensionNo">
                                                <div class="row">
                                                    <div class="col-xs-6">
                                                        <div class="form-group">
                                                            <label class="control-label no-padding-right">Reason: </label>
                                                            <asp:DropDownList ID="ddlReasonNoPension" runat="server" CssClass="form-control"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-xs-6">
                                                        <div class="form-group">
                                                            <label class="control-label no-padding-right">Any Comments: </label>
                                                            <asp:TextBox ID="txtPensionNoComments" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <br />
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <br />
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <br />
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <br />
                                                        <asp:Button ID="btnSavePensionDetails" runat="server" CssClass="btn btn-pink" Text="Save Pension Details" OnClick="btnSavePensionDetails_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div id="doc3" class="tab-pane fade">
                                            <div class="row">
                                                <div class="col-xs-12">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Gratuity: </label>
                                                        <asp:RadioButtonList ID="rbtGratuityApplicable" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rbtGratuityApplicable_SelectedIndexChanged">
                                                            <asp:ListItem Value="YES" Text="Yes" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Value="NO" Text="No"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div runat="server" id="divGratuityYes">
                                                <div class="row">
                                                    <div class="col-xs-3">
                                                        <div class="form-group">
                                                            <label class="control-label no-padding-right">GPO Amount: </label>
                                                            <asp:TextBox ID="txtGratuityAmount" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-xs-3">
                                                        <div class="form-group">
                                                            <label class="control-label no-padding-right">GPO Number: </label>
                                                            <asp:TextBox ID="txtGratuityNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-xs-3">
                                                        <div class="form-group">
                                                            <label class="control-label no-padding-right">GPO Date: </label>
                                                            <asp:TextBox ID="txtGratuityDate" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-xs-3">
                                                        <div class="form-group">
                                                            <label class="control-label no-padding-right">Upload GPO: </label>
                                                            <asp:FileUpload ID="flGratuity" runat="server"></asp:FileUpload>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="table-header">Field By Budget</div>
                                                <br />

                                                <div class="row">
                                                    <div class="col-xs-3">
                                                        <div class="form-group">
                                                            <label class="control-label no-padding-right">Gratuity Payment Order Date (Done By HQ): </label>
                                                            <asp:TextBox ID="txtGratuityPOHQDate" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-xs-3">
                                                        <div class="form-group">
                                                            <label class="control-label no-padding-right">Gratuity Payment Order No (Done By HQ): </label>
                                                            <asp:TextBox ID="txtGratuityPOHQNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-xs-3">
                                                        <div class="form-group">
                                                            <label class="control-label no-padding-right">Upload Order (HQ): </label>
                                                            <asp:FileUpload ID="flGratuityPO" runat="server"></asp:FileUpload>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div runat="server" id="divGratuityNo">
                                                <div class="row">
                                                    <div class="col-xs-6">
                                                        <div class="form-group">
                                                            <label class="control-label no-padding-right">Reason: </label>
                                                            <asp:TextBox ID="txtGratuityReason" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-12">
                                                    <div class="table-header">
                                                        Details of Any Deduction From Gratuity                                                   
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Gratuity Deduction Amount: </label>
                                                        <asp:TextBox ID="txtGratuityDeductionAmount" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Reason: </label>
                                                        <asp:TextBox ID="txtGratuityDeductionReason" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Upload Gratuity Deduction Reason Order: </label>
                                                        <asp:FileUpload ID="flReasonOrder" runat="server"></asp:FileUpload>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <br />
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <br />
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <br />
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <br />
                                                        <asp:Button ID="btnSaveGratutityDetails" runat="server" CssClass="btn btn-pink" Text="Save Gratutity Details" OnClick="btnSaveGratutityDetails_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div id="doc4" class="tab-pane fade">
                                            <div class="row">
                                                <div class="col-xs-12">
                                                    <div class="form-group">
                                                        <div class="table-header">Field By Division</div>
                                                        <br />
                                                        <label class="control-label no-padding-right">Leave Encashment: </label>

                                                        <asp:RadioButtonList ID="rbtLeaveApplicable" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rbtLeaveApplicable_SelectedIndexChanged">
                                                            <asp:ListItem Value="YES" Text="Yes" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Value="NO" Text="No"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div runat="server" id="divLeaveEncashmentYes">
                                                <div class="row">
                                                    <div class="col-xs-3">
                                                        <div class="form-group">
                                                            <label class="control-label no-padding-right">LE Amount: </label>
                                                            <asp:TextBox ID="txtLeaveEncashmentAmount" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-xs-3">
                                                        <div class="form-group">
                                                            <label class="control-label no-padding-right">LE Order No: </label>
                                                            <asp:TextBox ID="txtLeaveEncashmentNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-xs-3">
                                                        <div class="form-group">
                                                            <label class="control-label no-padding-right">LE Date: </label>
                                                            <asp:TextBox ID="txtLeaveEncashmentDate" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-xs-3">
                                                        <div class="form-group">
                                                            <label class="control-label no-padding-right">Upload LE: </label>
                                                            <asp:FileUpload ID="flLeaveEncashmentDoc" runat="server"></asp:FileUpload>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="table-header">Field By Budget</div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-xs-3">
                                                        <div class="form-group">
                                                            <label class="control-label no-padding-right">LE Payment Order Date (Done By HQ): </label>
                                                            <asp:TextBox ID="txtLeaveEncashmentOrderHQDate" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-xs-3">
                                                        <div class="form-group">
                                                            <label class="control-label no-padding-right">LE Payment Order No (Done By HQ): </label>
                                                            <asp:TextBox ID="txtLeaveEncashmentOrderHQNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-xs-3">
                                                        <div class="form-group">
                                                            <label class="control-label no-padding-right">Upload Leave Encashment Order (HQ): </label>
                                                            <asp:FileUpload ID="flLeaveEncashmentOrderHQ" runat="server"></asp:FileUpload>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div runat="server" id="divLeaveEncashmentNo">
                                                <div class="row">
                                                    <div class="col-xs-6">
                                                        <div class="form-group">
                                                            <label class="control-label no-padding-right">Reason: </label>
                                                            <asp:TextBox ID="txtLeaveEncashmentReason" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-xs-12">
                                                    <div class="table-header">
                                                        Details of Any Deduction From Leave Encashment                                                   
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Leave Encashment Deduction Amount: </label>
                                                        <asp:TextBox ID="txtLeaveEncashmentDeductionAmount" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Reason: </label>
                                                        <asp:TextBox ID="txtLeaveEncashmentDeductionReason" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Upload Leave Encashment Deduction Reason Order: </label>
                                                        <asp:FileUpload ID="flLeaveEncashmentDeductionReason" runat="server"></asp:FileUpload>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <br />
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <br />
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <br />
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <br />
                                                        <asp:Button ID="btnSaveLEDetails" runat="server" CssClass="btn btn-pink" Text="Save LE Details" OnClick="btnSaveLEDetails_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div id="doc5" class="tab-pane fade">
                                            <div class="row">
                                                <div class="col-xs-12">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">GIS: </label>
                                                        <asp:RadioButtonList ID="rdtgisApplicable" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdtgisApplicable_SelectedIndexChanged">
                                                            <asp:ListItem Value="YES" Text="Yes" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Value="NO" Text="No"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div runat="server" id="rdtgisApplicableYes" style="margin-left: 13px;">
                                                <div class="row">
                                                    <div class="col-xs-3">
                                                        <div class="form-group">
                                                            <label class="control-label no-padding-right">Reason: </label>
                                                            <asp:DropDownList ID="ddlDivisionReason" runat="server" CssClass="form-control">
                                                            </asp:DropDownList>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">LIC ID क्रमांक </label>
                                                        <asp:TextBox ID="txtLICId" runat="server" CssClass="form-control" autocomplete="off" onkeyup="isNumericVal(this);"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">दावा संख्या </label>
                                                        <asp:TextBox ID="txtClaimNum" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">दावा संख्या प्रेषण का दिनांक </label>
                                                        <asp:TextBox ID="txtClaimNumberDate" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">दावा अपलोड करें </label>
                                                        <asp:FileUpload ID="flClaimUploadPath" runat="server"></asp:FileUpload>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">GIS बीमा प्रकार: </label>
                                                        <asp:RadioButtonList ID="rbtGISType" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rbtGISType_SelectedIndexChanged">
                                                            <asp:ListItem Value="YES" Text="सामान्य बीमा" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Value="NO" Text="मृत बीमा"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div runat="server" id="rdtDeathCertificateYES">
                                                <div class="col-xs-12">
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <label class="control-label no-padding-right">मृतक प्रमाण पत्र अपलोड करें </label>
                                                            <asp:FileUpload ID="flDeathCeritificate" runat="server"></asp:FileUpload>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">दावा मुख्यालय को प्राप्त होने का दिनांक </label>
                                                        <asp:TextBox ID="txtClaimRecieveDate" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">दावा LIC प्रेषित किये जाने का क्रमांक  </label>
                                                        <asp:TextBox ID="txtLICSendNumber" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">दावा LIC प्रेषित किये जाने की दिनांक :  </label>
                                                        <asp:TextBox ID="txtLICSendDate" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">LIC से प्राप्त धनराशि  </label>
                                                        <asp:TextBox ID="txtLICRecieveAmount" runat="server" CssClass="form-control " autocomplete="off" onkeyup="isNumericVal(this);"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">LIC से धनराशि प्राप्त होने की दिनांक : </label>
                                                        <asp:TextBox ID="txtLICRecieveDate" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-xs-12">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">पेंशनर को भुगतान का विवरण : </label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">धनराशि  : </label>
                                                        <asp:TextBox ID="txtPensionerAmount" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Cheque Number : </label>
                                                        <asp:TextBox ID="txtPensionerChequeNumber" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);" autocomplete="off" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Account Number of Pensionar : </label>
                                                        <asp:TextBox ID="txtPensionerAccountNumber" runat="server" CssClass="form-control" autocomplete="off" onkeyup="isNumericVal(this);" MaxLength="18"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">IFSC Code : </label>
                                                        <asp:TextBox ID="txtPensionerIFSCCode" runat="server" CssClass="form-control" autocomplete="off" MaxLength="11"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-left">Date: </label>
                                                        <asp:TextBox ID="txtPensionPaidDate" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-xs-12">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">यदि पेंशनर मृत है तो डिवीज़न को प्रेषित धनराशि का विवरण  : </label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Amount  : </label>
                                                        <asp:TextBox ID="txtDivisionAmountNumber" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">cheque number  : </label>
                                                        <asp:TextBox ID="txtDivisionChequeNumber" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);" autocomplete="off" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">date  : </label>
                                                        <asp:TextBox ID="txtDivisionDateNumber" runat="server" CssClass="form-control date-picker" data-date-format="dd/mm/yyyy" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <br />
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <br />
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <br />
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <br />
                                                        <asp:Button ID="btnSaveGISDetails" runat="server" CssClass="btn btn-pink" Text="Save GIS Details" OnClick="btnSaveGISDetails_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div id="doc6" class="tab-pane fade">

                                            <div class="row">
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Nature Of Arrear: </label>
                                                        <asp:DropDownList ID="ddlNatureOfArrear" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Arrear Amount: </label>
                                                        <asp:TextBox ID="txtArrearAmount" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Arrear Order Date: </label>
                                                        <asp:TextBox ID="txtArrearOrderDate" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Arrear Order No: </label>
                                                        <asp:TextBox ID="txtArrearOrderNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Upload Arrear Order With Due Drawan: </label>
                                                        <asp:FileUpload ID="flArrearOrder" runat="server"></asp:FileUpload>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <br />
                                                        <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-purple" Text="Add" OnClick="btnAdd_Click" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-xs-12">
                                                    <div style="overflow: auto">
                                                        <asp:GridView ID="grdNatureOfArrear" runat="server" CssClass="display table table-bordered"
                                                            AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdNatureOfArrear_PreRender">
                                                            <Columns>
                                                                <asp:BoundField DataField="PensionMasterArrear_Id" HeaderText="PensionMasterArrear_Id">
                                                                    <HeaderStyle CssClass="displayStyle" />
                                                                    <ItemStyle CssClass="displayStyle" />
                                                                    <FooterStyle CssClass="displayStyle" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="PensionMasterArrear_NatureArreare_Id" HeaderText="PensionMasterArrear_NatureArreare_Id">
                                                                    <HeaderStyle CssClass="displayStyle" />
                                                                    <ItemStyle CssClass="displayStyle" />
                                                                    <FooterStyle CssClass="displayStyle" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="PensionMasterArrear_OrderPath" HeaderText="PensionMasterArrear_OrderPath">
                                                                    <HeaderStyle CssClass="displayStyle" />
                                                                    <ItemStyle CssClass="displayStyle" />
                                                                    <FooterStyle CssClass="displayStyle" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="S No.">
                                                                    <ItemTemplate>
                                                                        <%# Container.DataItemIndex + 1 %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="Amount" DataField="PensionMasterArrear_ArrearAmount" />
                                                                <asp:BoundField HeaderText="Order Date" DataField="PensionMasterArrear_OrderDate" />
                                                                <asp:BoundField HeaderText="Order No" DataField="PensionMasterArrear_OrderNo" />
                                                                <asp:TemplateField HeaderText="Remove">
                                                                    <ItemTemplate>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="row">
                                                <div class="col-xs-12">
                                                    <div class="table-header">
                                                        Details of Dearness Relief Arrear
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Basic Pension Rate: </label>
                                                        <asp:DropDownList ID="ddlBasicPensionRate" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Number Of Month: </label>
                                                        <asp:TextBox ID="txtNoOfMonthDA" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);" MaxLength="3"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <br />
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <br />
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <br />
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <br />
                                                        <asp:Button ID="btnSaveArrearDetails" runat="server" CssClass="btn btn-pink" Text="Save Pension Arrear Details" OnClick="btnSaveArrearDetails_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div id="doc7" class="tab-pane fade">
                                            <div class="row">
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Court Case No: </label>
                                                        <asp:TextBox ID="txtCourtCaseNo" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Court Case Year: </label>
                                                        <asp:TextBox ID="txtCourtCaseYear" runat="server" CssClass="form-control datepicker" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Contempt No: </label>
                                                        <asp:TextBox ID="txtContemptNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Contempt Year: </label>
                                                        <asp:TextBox ID="txtContemptYear" runat="server" CssClass="form-control datepicker" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Detail Of Court Case Order अपलोड करें </label>
                                                        <asp:FileUpload ID="flCourtCaseOrder" runat="server"></asp:FileUpload>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Detail Of Contempt Order अपलोड करें </label>
                                                        <asp:FileUpload ID="flContemptOrder" runat="server"></asp:FileUpload>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-12">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Regular Field Employee: </label>
                                                        <asp:RadioButtonList ID="rbtRegularFieldEmployee" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rbtRegularFieldEmployee_SelectedIndexChanged">
                                                            <asp:ListItem Value="YES" Text="Yes" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Value="NO" Text="No"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div runat="server" id="divRegularFieldEmployeeYes">
                                                <div class="row">
                                                    <div class="col-xs-3">
                                                        <div class="form-group">
                                                            <label class="control-label no-padding-right">Reason Of No Payment Of Pension: </label>
                                                            <asp:TextBox ID="txtRegularizationReason" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <br />
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <br />
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <br />
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <br />
                                                        <asp:Button ID="btnSaveOtherDetails" runat="server" CssClass="btn btn-pink" Text="Save Other Details" OnClick="btnSaveOtherDetails_Click" />
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
                                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-info" Text="Save Pensioner Details" OnClick="btnSave_Click" />
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <br />
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="hf_CompiledOrder" runat="server" Value="0" />
                        <asp:HiddenField ID="hf_PensionOrder" runat="server" Value="0" />
                        <asp:HiddenField ID="hf_PPO" runat="server" Value="0" />
                        <asp:HiddenField ID="hf_Gratuity" runat="server" Value="0" />
                        <asp:HiddenField ID="hf_GratuityPO" runat="server" Value="0" />
                        <asp:HiddenField ID="hf_ReasonOrder" runat="server" Value="0" />
                        <asp:HiddenField ID="hf_LeaveEncashmentDoc" runat="server" Value="0" />
                        <asp:HiddenField ID="hf_LeaveEncashmentOrderHQ" runat="server" Value="0" />
                        <asp:HiddenField ID="hf_LeaveEncashmentDeductionReason" runat="server" Value="0" />
                        <asp:HiddenField ID="hf_LiveCertificate" runat="server" Value="0" />
                        <asp:HiddenField ID="hf_ClaimUploadPath" runat="server" Value="0" />
                        <asp:HiddenField ID="hf_DeathCeritificate" runat="server" Value="0" />
                        <asp:HiddenField ID="hf_CourtCaseOrder" runat="server" Value="0" />
                        <asp:HiddenField ID="hf_ContemptOrder" runat="server" Value="0" />
                        <asp:HiddenField ID="hf_Photo" runat="server" Value="0" />


                        <asp:HiddenField ID="hf_PensionMaster_Id" runat="server" Value="0" />
                    </div>

                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSave" />
                    <asp:PostBackTrigger ControlID="btnSaveBasicDetails" />
                    <asp:PostBackTrigger ControlID="btnSavePensionDetails" />
                    <asp:PostBackTrigger ControlID="btnSaveGratutityDetails" />
                    <asp:PostBackTrigger ControlID="btnSaveLEDetails" />
                    <asp:PostBackTrigger ControlID="btnSaveGISDetails" />
                    <asp:PostBackTrigger ControlID="btnSaveArrearDetails" />
                    <asp:PostBackTrigger ControlID="btnSaveOtherDetails" />
                    <asp:PostBackTrigger ControlID="btnAdd" />
                </Triggers>
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

