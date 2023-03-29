$(function () {
    var l = abp.localization.getResource('spider');

    var dataTable = $('#ProductsTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: true,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(spider.products.product.getInvoices),
            columnDefs: [
                {
                    title: 'CounterpartyName',
                    data: "CounterpartyName"
                },
                {
                    title: 'CounterpartyAddress',
                    data: "counterpartyAddress"
                },
                
            ]
        })
    );
});
