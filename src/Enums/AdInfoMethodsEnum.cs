namespace Bizy.OuinneBiseSharp.Enums
{
    using System.ComponentModel;

    public enum AdInfoMethodsEnum
    {
        /// <summary>
        ///     debtor balance
        /// </summary>
        [Description("customerbalance")]
        CustomerBalance,

        /// <summary>
        ///     list of open invoice numbers for a given customer
        /// </summary>
        [Description("customerdocumentsopen")]
        CustomerDocumentsOpen,

        /// <summary>
        ///     beginning debtor balance
        /// </summary>
        [Description("customerinitialbalance")]
        CustomerInitialBalance,

        /// <summary>
        ///     open debtor's invoices total amount
        /// </summary>
        [Description("customerinvoicesopen")]
        CustomerInvoicesOpen,

        /// <summary>
        ///     total amount of sales for an item or group of items
        /// </summary>
        [Description("customersalesitem")]
        CustomerSalesItem,

        /// <summary>
        ///     total amount of sales [VAT Excluded] [VAT Included] [TTC] [HT]
        /// </summary>
        [Description("customersalestotal")]
        CustomerSalesTotal,

        /// <summary>
        ///     total amount of sales, VAT Exclude (no need to pass a parameter)
        /// </summary>
        [Description("customersalestotalvatexcluded")]
        CustomerSalesTotalVATExcluded,

        /// <summary>
        ///     total amount of open debtor's credit notes
        /// </summary>
        [Description("customercreditnoteopen")]
        CustomerCreditNoteOpen,

        /// <summary>
        ///     Last sale
        /// </summary>
        [Description("customerlastsale")]
        CustomerLastSale,

        /// <summary>
        ///     debtor advances
        /// </summary>
        [Description("customeradvances")]
        CustomerAdvances,

        /// <summary>
        ///     total number of sales
        /// </summary>
        [Description("salescount")]
        SalesCount,

        /// <summary>
        ///     creditor balance
        /// </summary>
        [Description("supplierbalance")]
        SupplierBalance,

        /// <summary>
        ///     beginning creditor balance
        /// </summary>
        [Description("supplierinitialbalance")]
        SupplierInitialBalance,

        /// <summary>
        ///     total amount of purchases
        /// </summary>
        [Description("supplierpurchasestotal")]
        SupplierPurchasesTotal,

        /// <summary>
        ///     total amount of open creditor's invoices
        /// </summary>
        [Description("supplierinvoicesopen")]
        SupplierInvoicesOpen,

        /// <summary>
        ///     total amount of creditor's credit notes
        /// </summary>
        [Description("suppliercreditnoteopen")]
        SupplierCreditNoteOpen,

        /// <summary>
        ///     last purchase
        /// </summary>
        [Description("supplierlastpurchase")]
        SupplierLastPurchase,

        /// <summary>
        ///     total amount of purchases by item
        /// </summary>
        [Description("supplierpurchaseitem")]
        SupplierPurchaseItem,

        /// <summary>
        ///     total number of purchases
        /// </summary>
        [Description("supplierpurchasescount")]
        SupplierPurchasesCount,

        /// <summary>
        ///     total amount of sales for the vendor
        /// </summary>
        [Description("salesmansalestotal")]
        SalesmanSalesTotal
    }
}
