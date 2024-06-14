namespace SFPOS.Entities.MasterDataClasses
{
    class CommonModel
    {
    }
    public class CommonModelCont
    {
        public const string EmptyString = "";
        public const string AddDollorSign = "$";
        public const string DefaultUPC  = "0000000000000";

        #region Validation
        public const string NumericOnetoNine_Validation = "^[0-9]+$";

       //public const string NumericOnetoNine_Validation_withDot = @"(?<=^| )\d+(\.\d{1,6})?(?=$| )";
        public const string NumericOnetoNine_Validation_withDot = @"^(?!0*\.0+$)\d*(?:\.\d+)?$";

        public const string Email_Validation = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";

        public const string Name_Validation = @"^[a-zA-Z ]*$";

        public const string phone_Validation = @"^\+?[0-9][0-9\s-]{6,12}$";  //can't add + sign in middle and - sign at first position but visa-versa is possible

        public const string phone_Validation_Customer = @"^\+?[0-9][0-9\s-]{3,12}$";

        public const string ZipCode_Validation = @"^[0-9]{5,6}$";

        public const string UserID_Validation = @"^[0-9]{4}$";

        public const string Fax_Validation = "^[0-9]{1,3}-[0-9]{3}-[0-9]{4}$";  //@"^[0-9]+$";

        public const string PassWord_Validation = "[^\\s]+";

        //public const string OnlyTwoDecimal_Validation = @"\.\d\d";
        public const string OnlyTwoDecimal_Validation = @"^\d+(\.\d{2,2})?$"; // @"\.\d\d";

        public const string positive_decimal = " ^[-.0 - 9] + $";
        //@"^.*(?=.{3,12})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$";
        //  "^.*(?=.{10,})(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).*$"
        #endregion
    }
    public class CommonTextBoxs
    {
        #region FrmCheckCoupon
        public const string FrmCheckCoupon = "FrmCheckCoupon-";
        #endregion
        #region FrmCoupon
        public const string FrmCoupon = "FrmCoupon-";
        #endregion
        #region FrmTaxReport
        public const string FrmTaxReport = "FrmTaxReport-";
        #endregion
        #region frmProduct_
        public const string frmProduct_ = "frmProduct_-";
        #endregion
        #region frmCurrentUserPwd
        public const string frmCurrentUserPwd = "frmCurrentUserPwd-";
        #endregion
        #region FrmSectionWiseSaleHistory
        public const string FrmSectionWiseSaleHistory = "FrmSectionWiseSaleHistory-";
        #endregion
        #region FrmSectionWiseTaxHistory
        public const string FrmSectionWiseTaxHistory = "FrmSectionWiseTaxHistory-";
        #endregion
        #region FrmSectionWiseDailySale
        public const string FrmSectionWiseDailySale = "FrmSectionWiseDailySale-";
        #endregion
        #region FrmProductWiseSaleHistory
        public const string FrmProductWiseSaleHistory = "FrmProductWiseSaleHistory-";
        #endregion
        #region FrmProductWiseDailySale
        public const string FrmProductWiseDailySale = "FrmProductWiseDailySale-";
        #endregion
        #region FrmEmployeeWiseSaleHistory
        public const string FrmEmployeeWiseSaleHistory = "FrmEmployeeWiseSaleHistory-";
        #endregion
        #region FrmEmployeeWiseDailySale
        public const string FrmEmployeeWiseDailySale = "FrmEmployeeWiseDailySale-";
        #endregion
        #region FrmEmployeeInvoiceWiseSaleHistory
        public const string FrmEmployeeInvoiceWiseSaleHistory = "FrmEmployeeInvoiceWiseSaleHistory-";
        #endregion
        #region FrmDepartmentWiseSaleHistory
        public const string FrmDepartmentWiseSaleHistory = "FrmDepartmentWiseSaleHistory-";
        #endregion
        #region FrmDepartmentWiseSale
        public const string FrmDepartmentWiseSale = "FrmDepartmentWiseSale-";
        #endregion
        #region FrmCounterWiseSaleHistory
        public const string FrmCounterWiseSaleHistory = "FrmCounterWiseSaleHistory-";
        #endregion
        #region FrmCounterWisePaymentHistory
        public const string FrmCounterWisePaymentHistory = "FrmCounterWisePaymentHistory-";
        #endregion

        #region FrmCounterWiseDailySale
        public const string FrmCounterWiseDailySale = "FrmCounterWiseDailySale-";
        #endregion

        #region FrmCounterWiseDailyPaymentHistory
        public const string FrmCounterWiseDailyPaymentHistory = "FrmCounterWiseDailyPaymentHistory-";
        #endregion

        #region FrmCounterInvoiceWiseSaleHistory
        public const string FrmCounterInvoiceWiseSaleHistory = "FrmCounterInvoiceWiseSaleHistory-";
        #endregion
        #region FrmRegisterSaleStatusByTrans
        public const string FrmRegisterSaleStatusByTrans = "FrmRegisterSaleStatusByTrans-";
        #endregion
        #region FrmCounterInvoiceWiseDailyPaymentHistory
        public const string FrmCounterInvoiceWiseDailyPaymentHistory = "FrmCounterInvoiceWiseDailyPaymentHistory-";
        #endregion
        #region FrmCounterInvoiceWiseDailySale
        public const string FrmCounterInvoiceWiseDailySale = "FrmCounterInvoiceWiseDailySale-";
        #endregion
        #region frmSettings_BE
        public const string frmSettings_BE = "frmSettings_BE-";
        #endregion
        #region FrmMetroMaster
        public const string FrmMetroMaster = "FrmMetroMaster-";
        #endregion
        #region FrmEmployeeWiseDailyPaymentHistory
        public const string FrmEmployeeWiseDailyPaymentHistory = "FrmEmployeeWiseDailyPaymentHistory-";
        #endregion
        #region FrmEmployeeWisePaymentHistory
        public const string FrmEmployeeWisePaymentHistory = "FrmEmployeeWisePaymentHistory-";
        #endregion
        #region FrmDepartmentWiseDailyPaymentHistory
        public const string FrmDepartmentWiseDailyPaymentHistory = "FrmDepartmentWiseDailyPaymentHistory-";
        #endregion

        #region FrmDepartmentWisePaymentHistory
        public const string FrmDepartmentWisePaymentHistory = "FrmDepartmentWisePaymentHistory-";
        #endregion

        #region FrmEmployeeInvoiceWisePaymentHistory
        public const string FrmEmployeeInvoiceWisePaymentHistory = "FrmEmployeeInvoiceWisePaymentHistory-";
        #endregion

        #region frmEmployeeInvoiceWiseDailySale
        public const string frmEmployeeInvoiceWiseDailySale = "frmEmployeeInvoiceWiseDailySale-";
        #endregion

        #region frmEmployeeInvoiceWiseDailyPayment
        public const string frmEmployeeInvoiceWiseDailyPayment = "frmEmployeeInvoiceWiseDailyPayment-";
        #endregion

        #region FrmLogin
        public const string FrmLogin = "FrmLogin-";
        public const string FrmSplash = "FrmSplash-";
        #endregion

        #region FrmResume
        public const string FrmResume = "FrmResume-";
        #endregion

        #region FrmManagerPassWord
        public const string FrmManagerPassWord = "FrmManagerPassWord-";
        #endregion

        #region FrmCancelTransaction
        public const string FrmCancelTransaction = "FrmCancelTransaction-";
        #endregion

        #region frmDepartment
        public const string frmDepartment = "frmDepartment-";
        public const string txtDepartmentName = "txtDepartmentName";
        #endregion

        #region frmEmployee
        public const string frmEmployee = "frmEmployee-";
        public const string txtFirstName = "txtFirstName";
        public const string txtLastName = "txtLastName";
        
        public const string txtPwd = "txtPwd";
        public const string cmbStoreName = "cmbStoreName";
        public const string cmbRoleName = "cmbRoleName";

        #endregion

        #region frmRole
        public const string frmRole = "frmRole-";
        public const string txtRoleType = "txtRoleType";
        
        #endregion

        #region frmPurchaseOrders
        public const string frmPurchaseOrders = "frmPurchaseOrders-";
        public const string txtVendorInvoiceNo = "txtVendorInvoiceNo";
        public const string datePickerOrderDate = "datePickerOrderDate";
        #endregion

        #region frmProduct
        public const string frmProduct = "frmProduct-";
        public const string txtProductName = "txtProductName";
        public const string txtUPCCode = "txtUPCCode";
        public const string cmbDepartment = "cmbDepartment";
        public const string cmbSection = "cmbSection";
        public const string cmbTaxGroup = "cmbTaxGroup";
        public const string cmbUoM = "cmbUoM";
        public const string txtPrice = "txtPrice";
        public const string txtCertificateCode = "txtCertificateCode"; 
        #endregion

        #region frmStore
        public const string frmStore = "frmStore-";
        public const string frmdemoStore = "frmdemoStore-";
        public const string txtPhone = "txtPhone";
        public const string txtZipcode = "txtZipcode";
        public const string txtFax = "txtFax";
        public const string txtStoreName = "txtStoreName";
        public const string cmbCountry = "cmbCountry";
        public const string cmbState = "cmbState";
        public const string cmbCity = "cmbCity";
        public const string txtState = "txtState";
        public const string txtCity = "txtCity";
        public const string txtpersonName = "txtpersonName";
        #endregion

        #region frmUoM
        public const string frmUoM = "frmUoM-";
        public const string txtUoMName = "txtUoMName";
        #endregion  

        #region frmProductVendor
        public const string frmProductVendor = "frmProductVendor-";
        public const string cmbVendorName = "cmbVendorName";
        public const string txtVendorUPCCode = "txtVendorUPCCode";
        #endregion

        #region frmProductSaleDiscount
        public const string frmProductSaleDiscount = "frmProductSaleDiscount-";
        public const string txtSalesDiscount = "txtSalesDiscount";
        public const string datePickerStartDate = "datePickerStartDate";
        public const string datePickerEndDate = "datePickerEndDate";

        #endregion

        #region frmProductSalePrice
        public const string frmProductSalePrice = "frmProductSalePrice-";
        public const string txtSalesPrice = "txtSalesPrice";
        #endregion

        #region frmSection
        public const string frmSection = "frmSection-";
        public const string txtSectionName = "txtSectionName";
        #endregion

        #region frmProductUoM
        public const string frmProductUoM = "frmProductUoM-";
        public const string txtDiscription = "txtDiscription";
        public const string txtQtyPerUoM = "txtQtyPerUoM";
        #endregion

        #region frmTaxGroup
        public const string frmTaxGroup = "frmTaxGroup-";
        public const string txtTaxGroupCode = "txtTaxGroupCode";
        #endregion

        #region frmTaxRateDetail
        public const string frmTaxRateDetail = "frmTaxRateDetail-";
        public const string txtTax = "txtTax";
        public const string cmbTaxGroupCode = "cmbTaxGroupCode";
        #endregion

        #region frmVendor
        public const string frmVendor = "frmVendor-";
        public const string txtVendorName = "txtVendorName";
        public const string txtContactPerson = "txtContactPerson";
        public const string txtEmail = "txtEmail";

        #endregion

        #region frmAddEditPurchaseHeader/Line
        public const string frmAddEditPurchaseHeaderLine = "frmAddEditPurchaseHeader/Line-";
        public const string txtQty = "txtQty";
        public const string txtUnitCost = "txtUnitCost";
        public const string txtGroupPrice = "txtGroupPrice";
        public const string cmbProdName = "cmbProdName";
        public const string UPC_Code = "UPC Code";
        public const string Item_Code = "Item Code";
        public const string txtUpcItem = "txtUpcItem";
        public const string txtTareWeight = "txtTareWeight";
        public const string txtFSEligibleAmount = "txtFSEligibleAmount";
        #endregion

        #region FrmPurchaseHeaderLine
        public const string FrmPurchaseHeaderLine = "FrmPurchaseHeaderLine-";
        public const string datePickerDate = "datePickerDate";
        public const string txtShippedBy = "txtShippedBy";
        public const string txtAdjustment = "txtAdjustment";
        public const string txtInvoiceNo = "txtInvoiceNo";
        public const string txtReceivedBy = "txtReceivedBy";
        public const string txtTotalAmt = "txtTotalAmt";
        #endregion

        #region frmPORdlcReport
        public const string frmPORdlcReport = "frmPORdlcReport-";
        #endregion

        #region frmProductDetail
        public const string frmProductDetail = "frmProductDetail-";
        #endregion
        
        #region MenuDashBoard
        public const string MenuDashBoard = "MenuDashBoard-";
        #endregion

        #region FrmInventoryReport
        public const string FrmInventoryReport = "FrmInventoryReport-";
        #endregion

        #region FrmShortcutKey
        public const string FrmShortcutKey = "FrmShortcutKey-";
        #endregion

        #region FrmOrderHistory
        public const string FrmOrderHistory = "FrmOrderHistory-";
        #endregion

        #region frmOrderScanner
        public const string frmOrderScanner = "frmOrderScanner-";
        #endregion

        #region FrmFoodStamp
        public const string FrmFoodStamp = "FrmFoodStamp-";
        #endregion

        #region FrmAgeVerification
        public const string FrmAgeVerification = "FrmAgeVerification-";
        #endregion

        #region FrmTotal
        public const string FrmTotal = "FrmTotal-";
        #endregion
        
        #region MenuLiveCounter
        public const string MenuLiveCounter = "MenuLiveCounter-";
        #endregion

        #region FrmProductSales
        public const string FrmProductSales = "FrmProductSales-";
        #endregion
        #region FrmLotto
        public const string FrmLotto = "FrmLotto-";
        #endregion
        #region FrmLottoTrans
        public const string FrmLottoTrans = "FrmLottoTrans-";
        #endregion

        public const string FrmMetro_AddProduct = "FrmMetro_AddProduct";
        public const string FrmSalesOrders = "FrmSalesOrders";
        public const string frmPriceCheck = "frmPriceCheck";

    }
}
