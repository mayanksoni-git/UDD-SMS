function initializeTables() {
    new DataTable("#scroll-vertical", {
        scrollY: "210px",
        scrollCollapse: !0,
        paging: !1
    }),
    new DataTable("#scroll-horizontal", {
        scrollX: !0
    }),
    new DataTable("#alternative-pagination", {
        pagingType: "full_numbers"
    }),
    new DataTable("#ctl00_ContentPlaceHolder1_grdPost", {
        paging: true,
        destroy: true,
        lengthMenu: [[10, 25, 50, 100, -1], [10, 25, 50, 100, "All"]],
        searching: true,
    dom: "Blfrtip",
    buttons: ["csv", "excel", "print"], 
    fixedHeader: !0
    }),
    new DataTable("#ajax-datatables", {
        ajax: "assets/json/datatable.json"
    }),
        
    $('#ctl00_ContentPlaceHolder1_grdFinancialFull').DataTable({
        destroy: true, // Ensures re-initialization on each AJAX load
        dom: 'Blfrtip', // Adds button container at the top

        buttons: ["csv", "excel", "print"]
    });
    $('#ctl00_ContentPlaceHolder1_grdRevert').DataTable({
        destroy: true, // Ensures re-initialization on each AJAX load
        dom: 'Blfrtip', // Adds button container at the top

        buttons: ["csv", "excel", "print"]
    });
}
document.addEventListener("DOMContentLoaded", function () {
    initializeTables()
});