namespace Bizy.OuinneBiseSharp.Enums
{
    using System.ComponentModel;

    public enum StockMethodsEnum
    {
        /// <summary>
        ///     Stock physically available
        /// </summary>
        [Description("available")]
        Available,

        /// <summary>
        ///     On suppliers orders or in production
        /// </summary>
        [Description("suppliersorders")]
        SuppliersOrders,

        /// <summary>
        ///     On customers orders confirmations (reserved)
        /// </summary>
        [Description("customersorders")]
        CustomersOrders,

        /// <summary>
        ///     Delivered to customers (delivery slips
        /// </summary>
        [Description("customersdeliveries")]
        CustomersDeliveries,

        /// <summary>
        ///     On customers worksheets
        /// </summary>
        [Description("customersworksheets")]
        CustomersWorksheets,

        /// <summary>
        ///     Total sales amount
        /// </summary>
        [Description("salesamount")]
        SalesAmount,

        /// <summary>
        ///     total quantity sold;
        /// </summary>
        [Description("salesquantity")]
        SalesQuantity,

        /// <summary>
        ///     total quantity returned (debtors credit notes)
        /// </summary>
        [Description("salesquantityreturned")]
        SalesQuantityReturned,

        /// <summary>
        ///     date of the last sale
        /// </summary>
        [Description("saleslastdate")]
        SalesLastDate
    }
}