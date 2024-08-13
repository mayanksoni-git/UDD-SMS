<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/TemplateMasterAdmin_PMS.master" CodeFile="CreateVisionPlan.aspx.cs" Inherits="CreateVisionPlan" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

     <asp:HiddenField ID="ULBFundId" runat="server" />
    <asp:HiddenField ID="ULBID" runat="server" />
    <asp:HiddenField ID="FYID" runat="server" />
   
   

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
                                    <h4 class="mb-sm-0" id="HeadingSec" runat="server">Create Vision Plan</h4>
                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                            <li class="breadcrumb-item">Annual Action Plan Report</li>
                                            <li class="breadcrumb-item active">Create Vision Plan</li>
                                        </ol>
                                    </div>
                                </div>
                            </div>
                            <!--end col-->
                        </div>

                          <div class="row">
                            <div class="col-lg-12">
                                <div class="card" id="sectionFilter" runat="server">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Create Vision Plan</h4>
                                        
                                    </div>
                                    <!-- end card header -->
                                     <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-4">
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divZone" runat="server">
                                                        <asp:Label ID="lblZoneH" runat="server" Text="State*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlZone" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divCircle" runat="server">
                                                        <asp:Label ID="lblCircleH" runat="server" Text="District*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlCircle" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCircle_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divDivision" runat="server">
                                                        <asp:Label ID="lblDivisionH" runat="server" Text="ULB*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-select"></asp:DropDownList>
                                                        <%--<asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"></asp:DropDownList>--%>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divFY" runat="server">
                                                        <asp:Label ID="lblFY" runat="server" Text="Select Financial Year*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlFY" runat="server" CssClass="form-select" AutoPostBack="true" ></asp:DropDownList>
                                                    </div>
                                                </div>
                                                 <div class="col-xxl-3 col-md-6">
                                                    <div id="divProj" runat="server">
                                                        <asp:Label ID="lblProj" runat="server" Text="CMVNY Project*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="DDLProj" runat="server" CssClass="form-select" ></asp:DropDownList>
                                                    </div>
                                                </div>
                                              
                                                <div class="col-xxl-3 col-md-6">
                                                    <div  runat="server" id="NewConstruction">
                                                        <asp:Label ID="Label1" runat="server" Text="Is Constructed ?*" CssClass="form-label"></asp:Label>
                                                        <asp:RadioButton ID="RadioButton3" runat="server" GroupName="IsConstructed" Text="Constructed " Value="1" />
                                                        <asp:RadioButton ID="RadioButton1" runat="server" GroupName="IsConstructed" Text="Under Construction " Value="2" /><br />
                                                        <asp:RadioButton ID="RadioButton2" runat="server" GroupName="IsConstructed" Text="Under Sanction " Value="3" />
                                                    </div>
                                                </div>
                                                <div class="col-xxl-3 col-md-6" id="sectionyear" style="display:none">
                                                    <div id="div2" runat="server">
                                                        <asp:Label ID="Label2" runat="server" Text="Enter year*" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="TxtYear" runat="server" CssClass="form-control" ></asp:TextBox>
                                                    </div>
                                                </div>
                                              <div class="col-xxl-3 col-md-6">
                                                    <div id="condition" runat="server">
                                                        <asp:Label ID="Label3" runat="server" Text="Condition *" CssClass="form-label"></asp:Label>
                                                        <asp:RadioButton ID="RadioButton4" runat="server" GroupName="IsConstructed" Text="Good " Value="1" />
                                                        <asp:RadioButton ID="RadioButton5" runat="server" GroupName="IsConstructed" Text="Need Renovation" Value="2" /><br />
                                                        <asp:RadioButton ID="RadioButton6" runat="server" GroupName="IsConstructed" Text="Need Redevelopement" Value="3" />
                                                    </div>
                                                </div>

                                                <div class="col-xxl-12  col-md-12">
                                                    <div>
                                                        <asp:Button ID="BtnSearch" Text="Search" OnClick="BtnSearch_Click" runat="server" Style="float: right" CssClass="btn bg-success text-white"></asp:Button>

                                                    </div>
                                                </div>
                                              
                                            </div>
                                            <!--end row-->
                                        </div>
                                    </div>
                                </div>

                                <div class="card" id="sectionData" runat="server">
                                            <table id="sample-table-2" class="mt-5 table table-striped table-bordered table-hover">

                                                <thead >
                                                    <tr class="table-success">
                                                        <th style="text-align: center;font-size:26px"  colspan="4" rowspan="1"> Annual Income :
                                                            <label id="AnnualULB" runat="server"></label>
                                                        </th>
                                                        </tr>
                                                    <tr>
                                                        <th style="font-size:18px" colspan="2">District :
                                                             <label id="District" runat="server"></label>
                                                        </th>
                                                        <th style="font-size:18px" colspan="1" >ULB Type (Category) :
                                                            
                                                        </th>
                                                        </tr>
                                                    <tr>
                                                        <th style="font-size:18px" colspan="2">Year :
                                                             <label id="year" runat="server"></label>
                                                        </th>
                                                       <th style="font-size:18px"> <label id="ULBType" runat="server"></label></th>
                                                        </tr>
                                                    <tr class="table-primary">
                                                        <th align="center" >Sr No.  
                                                                              
                                                        </th>
                                                        <th rowspan="2">Name Of Head
                                                        </th>
                                                         <th style="text-align: right" rowspan="2">Amount
                                                        </th>
                                                         <th style="text-align: right" rowspan="2">
                                                        </th>
                                                    </tr>
                                                   
                                                </thead>
                                       
                                     <asp:Repeater ID="rptSearchResult" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                 
                                                     <%#DataBinder.Eval(Container,"DataItem.SrNo")%>
                                                </td>
                                                <td align="left">
                                                 
                                                    <%#DataBinder.Eval(Container,"DataItem.ULBIncomeType_Name")%>
                                                </td>
                                                <td style="text-align: right">
                                                    <%#DataBinder.Eval(Container,"DataItem.Amount")%>
                                                </td>
                                                
                                              




                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                           
                                        </FooterTemplate>
                                    </asp:Repeater>
                                     </table>
                                </div>

                            </div>
                        </div>


                       
                    </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="BtnSearch" />
                </Triggers>

                </asp:UpdatePanel>
          

            </div>
        </div>
     <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>
   <script type="text/javascript">
       $(document).ready(function () {
           $('#NewConstruction input[type=checkbox]').click(function () {
               debugger
               var selectedValue = $(this).val();
               if (selectedValue == 1) {
                   $("#sectionyear").slideDown();
               }
               else {
                   $("#sectionyear").slideUp();

               }
             alert("You selected: " + selectedValue);
           });
       });
    </script>
        <style>
        .filter-btn {
    display: flex;
    padding: 8px 10px;
    border-radius: 5px;
    font-size: 14px;
    font-weight: 500;
    color: #ffffff;
    background-color: #f85420;
    width: 137px;
    border: 0;
    background: -webkit-gradient(linear, left top, right top, from(#FF5722), to(#D84315));
    background: linear-gradient(88deg, #FF5722 0%, #D84315);
    text-align: center;
    align-items: center;
    justify-content: center;
    height: 39px;
    line-height: 22px;
    transition: all .35s ease-Out;
}
    </style>
    </asp:Content>