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
            lengthMenu: [[10, 25, 50, 100, -1], [10, 25, 50, 100, "All"]],
            searching: true,
        dom: "Blfrtip",
        buttons: ["copy", "csv", "excel", "print", "pdf"], 
        fixedHeader: !0
    }),
    new DataTable("#ajax-datatables", {
        ajax: "assets/json/datatable.json"
    });
}
document.addEventListener("DOMContentLoaded", function () {
    initializeTables()
});