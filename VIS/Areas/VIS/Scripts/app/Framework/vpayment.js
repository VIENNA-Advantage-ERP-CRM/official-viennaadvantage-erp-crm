﻿; (function (VIS, $) {

    function VPayment(windowNo, mTab, button) {


        //Private Variables       
        var self = this;
        var ctx = VIS.Env.getCtx();
        var _initOK = false;
        //this.Title =VIS.Msg.getMsg("Payment");
        var _isSOTrx = VIS.context.isSOTrx(windowNo);
        //	Data from Order/Invoice
        var _DocStatus = null;
        // Start Payment Rule       
        var _PaymentRule = "";
        // Invoice Currency              
        var _VAB_Currency_ID = 0;
        // Start Acct Date          
        var _DateAcct = null;
        // Start Payment Term       
        var _C_PaymentTerm_ID = 0;
        // Start Payment            
        var _C_Payment_ID = 0;
        var _VAB_CashJRNLLine_ID = 0;
        // Start CashBook           
        var _VAB_CashBook_ID = 0;
        // Start CreditCard         
        var _CCType = "";
        //log
        this.log = VIS.Logging.VLogger.getVLogger("vPayment");
        var _VAF_Client_ID = 0;
        var _VAF_Org_ID = 0;
        var _VAB_BusinessPartner_ID = 0;

        var inputProperties = {};


        /** Cash = B */
        var PAYMENTRULE_Cash = "B";
        /** Direct Debit = D */
        var PAYMENTRULE_DirectDebit = "D";
        /** Credit Card = K */
        var PAYMENTRULE_CreditCard = "K";
        /** On Credit = P */
        var PAYMENTRULE_OnCredit = "P";
        /** Check = S */
        var PAYMENTRULE_Check = "S";
        /** Direct Deposit = T */
        var PAYMENTRULE_DirectDeposit = "T";



        /** Direct Deposit = A */
        var TENDERTYPE_DirectDeposit = "A";
        /** Credit Card = C */
        var TENDERTYPE_CreditCard = "C";
        /** Direct Debit = D */
        var TENDERTYPE_DirectDebit = "D";
        /** Check = K */
        var TENDERTYPE_Check = "K";


        // Only allow changing Rule        
        var _onlyRule = false;
        // this.onClose = null;


        //Variables for From Design             
        var $maindiv;
        var ch = null;
        var bPanel;
        var kPanel;
        var tPanel;
        var sPanel;
        var pPanel;
        this.onClose = null;
        var lblKStatus;
        var lblTStatus;
        var lblSStatus;
        var lblBAmountLabel;
        var lblBAmountField;
        var lblSAmountLabel;
        var lblSAmountField;
        var lblPaymentLabel;
        var lblKTypeLabel;
        var lblKNumnerLabel;
        var lblKExpLabel;
        var lblTAccountLabel;
        var lblKApprovalLabel;
        var lblSNumberLabel;
        var lblSRoutingLabel;
        var lblSCurrencyLabel;
        var lblBCurrencyLabel;
        var lblPTermLabel;
        var lblBDateLabel;
        var lblSCheckLabel;
        var lblSBankAccountLabel;
        var lblBCashBookLabel;


        var cmbPayment;
        var cmbKType;
        var cmbTAccount;
        var cmbSCurrency;
        var cmbBCurrency;
        var cmbPTerm;
        var cmbSBankAccount;
        var cmbBCashBook;

        var btnKOnline;
        var btnSOnline;

        var txtKNumber;
        var txtKExp;
        var txtKApproval;
        var txtSNumber;
        var txtSRouting;//"",true,false,false,70, 
        var txtSCheck;

        //private VDate bDateField;
        var bDateField;
        var centerPanel;
        var bPanelLayout;// TableLayoutPanel();
        var pPanelLayout;
        var centerLayout;
        var kLayout;
        var tPanelLayout;
        var sPanelLayout;
        var tabmenubusy = $('<div class="vis-busyindicatorouterwrap"><div class="vis-busyindicatorinnerwrap"><i class="vis-busyindicatordiv"></i><label>' + VIS.Msg.getMsg("Loading") + '</label></div></div>');
        //var loadLabel = $('<label>' + VIS.Msg.getMsg("Loading") + '</label>');




        this.init = function () {

            initDesign();
            _initOK = dynInit(button.values);
            invokeDynInit(_initOK);
        };
        //init();
        function initDesign() {

            var mainUpperDiv = $('<div class="input-group vis-input-wrap"></div>');
            var mainInnerCtrlDiv = $('<div class="vis-control-wrap"></div>');

            var $mainSpan = null;

            //if (VIS.Application.isRTL) {
            //    $mainSpan = $('<p class="vis-payment-spanPayment-RTL"> ' + VIS.Msg.translate(ctx, 'PaymentMethod') + ' </p>');
            //}
            //else {
             $mainSpan=   $('<label class="vis-payment-spanPayment"> ' + VIS.Msg.translate(ctx, 'PaymentMethod') + ' </label>');
            //}
            mainUpperDiv.append(mainInnerCtrlDiv);

            cmbPayment = $('<select class="vis-payment-cmbPayment"></select>');
            cmbPayment.appendTo(mainInnerCtrlDiv);
            $mainSpan.appendTo(mainInnerCtrlDiv);

            mainUpperDiv.appendTo($maindiv);


            centerPanel = $('<table class="vis-payment-table"></table>');
            var centerPanelTR = $('<tr></tr>');
            centerPanel.append(centerPanelTR);
            /**************CreditCard************************/
            // CreditCard               

            lblKTypeLabel = $('<label class="vis-payment-span"> ' + VIS.Msg.translate(ctx, 'CreditCardType') + '</label>');
            lblKNumnerLabel = $('<label class="vis-payment-span">' + VIS.Msg.translate(ctx, 'CreditCardNumber') + '</label>');
            lblKExpLabel = $('<label class="vis-payment-span">' + VIS.Msg.translate(ctx, 'Expires') + '</label>');
            lblKApprovalLabel = $('<label class="vis-payment-span">' + VIS.Msg.translate(ctx, 'VoiceAuthCode') + '</label>');
            btnKOnline = $('<label class="vis-payment-span">' + VIS.Msg.translate(ctx, 'Online') + '</label>');
            lblKStatus = $('<label class="vis-payment-span"></label>');
            kPanel = $('<table class="vis-payment-table"></table>');
            var trkpanel = $('<tr></tr>');
            kPanel.append(trkpanel);

            kLayout = $('<table class="vis-payment-table"></table>');
            trkpanel.append(kLayout);


            centerPanelTR.append(kPanel);

            cmbKType = $('<select  class="vis-payment-cmb"></select>');
            var kltr1 = $('<tr></tr>');
            //var kltr1td1 = $('<td ></td>');
            var kltr1td2 = $('<td class="vis-payment-td2"></td>');
            var $DivInputWrap = $('<div class="input-group vis-input-wrap">');
            var $DivControlWrap = $('<div class="vis-control-wrap">');
            $DivControlWrap.append(cmbKType);
            $DivControlWrap.append(lblKTypeLabel);
            kltr1.append(kltr1td2);
            kltr1td2.append($DivInputWrap);
            $DivInputWrap.append($DivControlWrap);
            //kltr1.append(kltr1td1).append(kltr1td2);
            kLayout.append(kltr1);


            txtKNumber = $('<input class="vis-payment-inputs" type="text" placeholder=" " data-placeholder="" >');
            var kltr2 = $('<tr></tr>');
            //var kltr2td1 = $('<td class="vis-payment-td1"></td>');
            var kltr2td2 = $('<td class="vis-payment-td2"></td>');
            var $DivInputWrap = $('<div class="input-group vis-input-wrap">');
            var $DivControlWrap = $('<div class="vis-control-wrap">');
            $DivControlWrap.append(txtKNumber);
            $DivControlWrap.append(lblKNumnerLabel);
            //kltr2.append(kltr2td1).append(kltr2td2);
            kltr2.append(kltr2td2);
            kltr2td2.append($DivInputWrap);
            $DivInputWrap.append($DivControlWrap);
            kLayout.append(kltr2);


            txtKExp = $('<input class="vis-payment-inputs"  type="text" placeholder=" " data-placeholder="" >');
            var kltr3 = $('<tr></tr>');
            //var kltr3td1 = $('<td class="vis-payment-td1"></td>');
            var kltr3td2 = $('<td class="vis-payment-td2"></td>');
            var $DivInputWrap = $('<div class="input-group vis-input-wrap">');
            var $DivControlWrap = $('<div class="vis-control-wrap">');
            $DivControlWrap.append(txtKExp);
            $DivControlWrap.append(lblKExpLabel);
            //kltr3.append(kltr3td1).append(kltr3td2);
            kltr3.append(kltr3td2);
            kltr3td2.append($DivInputWrap);
            $DivInputWrap.append($DivControlWrap);
            kLayout.append(kltr3);


            txtKApproval = $('<input class="vis-payment-inputs"  type="text" placeholder=" " data-placeholder="" >');
            var kltr4 = $('<tr></tr>');
            //var kltr4td1 = $('<td class="vis-payment-td1"></td>');
            var kltr4td2 = $('<td class="vis-payment-td2"></td>');
            var $DivInputWrap = $('<div class="input-group vis-input-wrap">');
            var $DivControlWrap = $('<div class="vis-control-wrap">');
            $DivControlWrap.append(txtKApproval);
            $DivControlWrap.append(lblKApprovalLabel);
            //kltr4.append(kltr4td1).append(kltr4td2);
            kltr4.append(kltr4td2);
            kltr4td2.append($DivInputWrap);
            $DivInputWrap.append($DivControlWrap);
            kLayout.append(kltr4);


            btnKOnline = $('<button class="vis-payment-inputs-buttons"  type="button">' + VIS.Msg.translate(ctx, 'Online') + '</button>');
            var kltr5 = $('<tr></tr>');
            var kltr5td1 = $('<td></td>');
            kltr5td1.append(btnKOnline);
            kltr5.append(kltr5td1);
            kLayout.append(kltr5);


            //**************DircetDebit/Credit************************/
            // DirectDebit/Credit           
            lblTAccountLabel = $('<label class="vis-payment-span"> ' + VIS.Msg.translate(ctx, 'VAB_BPart_Bank_Acct_ID') + '</label>');
            lblTStatus = $('<p class="vis-payment-span"> </p>');

            tPanel = $('<table class="vis-payment-table"></table>');
            var tptr = $('<tr></tr>');
            tPanelLayout = $('<table class="vis-payment-table"></table>');
            tPanel.append(tptr);
            tptr.append(tPanelLayout);
            centerPanelTR.append(tPanel);

            cmbTAccount = $('<select class="vis-payment-cmb"></select>');
            var tptr1 = $('<tr></tr>');
            //var tptr1td1 = $('<td class="vis-payment-td1"></td>');
            var tptr1td2 = $('<td class="vis-payment-td2"></td>');
            var $DivInputWrap = $('<div class="input-group vis-input-wrap">');
            var $DivControlWrap = $('<div class="vis-control-wrap">');
            $DivControlWrap.append(cmbTAccount);
            $DivControlWrap.append(lblTAccountLabel);
            //tptr1.append(tptr1td1).append(tptr1td2);
            tptr1.append(tptr1td2);
            tptr1td2.append($DivInputWrap);
            $DivInputWrap.append($DivControlWrap);
            tPanelLayout.append(tptr1);


            //**************Check************************/
            /// Check     
            lblSBankAccountLabel = $('<label class="vis-payment-span"> ' + VIS.Msg.translate(ctx, 'VAB_Bank_Acct_ID') + '</label>');
            lblSAmountLabel = $('<label class="vis-payment-span"> ' + VIS.Msg.translate(ctx, 'Amount') + '</label>');
            lblSAmountField = $('<label class="vis-payment-span vis-payment-AmountField"> </label>');
            lblSRoutingLabel = $('<label class="vis-payment-span"> ' + VIS.Msg.translate(ctx, 'RoutingNo') + '</label>');
            lblSNumberLabel = $('<label class="vis-payment-span"> ' + VIS.Msg.translate(ctx, 'AccountNo') + '</label>');
            lblSCheckLabel = $('<label class="vis-payment-span"> ' + VIS.Msg.translate(ctx, 'CheckNo') + '</label>');
            lblSCurrencyLabel = $('<label class="vis-payment-span"> ' + VIS.Msg.translate(ctx, 'VAB_Currency_ID') + '</label>');
            //txtSNumber.Width = 180;
            lblSStatus = $('<p class="vis-payment-span"> </p>');
            btnSOnline = $('<button class="vis-payment-inputs-buttons"  type="button">' + VIS.Msg.translate(ctx, 'Online') + '</button>');

            sPanel = $('<table class="vis-payment-table"></table>');
            var sPanelTr = $('<tr></tr>');
            sPanelLayout = $('<table class="vis-payment-table"></table>');
            sPanelTr.append(sPanelLayout);
            sPanel.append(sPanelTr);
            centerPanelTR.append(sPanel);

            cmbSBankAccount = $('<select class="vis-payment-cmb"></select>');
            var spstr1 = $('<tr></tr>');
            //var spstr1td1 = $('<td class="vis-payment-td1"></td>');
            var spstr1td2 = $('<td class="vis-payment-td2"></td>');
            var $DivInputWrap = $('<div class="input-group vis-input-wrap">');
            var $DivControlWrap = $('<div class="vis-control-wrap">');
            $DivControlWrap.append(cmbSBankAccount);
            $DivControlWrap.append(lblSBankAccountLabel);
            //spstr1.append(spstr1td1).append(spstr1td2);
            spstr1.append(spstr1td2);
            spstr1td2.append($DivInputWrap);
            $DivInputWrap.append($DivControlWrap);
            sPanelLayout.append(spstr1);


            lblSAmountField = $('<input type="text" readonly class="vis-payment-span vis-payment-AmountField">');
            var spstr2 = $('<tr></tr>');
            //var spstr2td1 = $('<td class="vis-payment-td1"></td>');
            var spstr2td2 = $('<td class="vis-payment-td2"></td>');
            var $DivInputWrap = $('<div class="input-group vis-input-wrap">');
            var $DivControlWrap = $('<div class="vis-control-wrap">');
            $DivControlWrap.append(lblSAmountField);
            $DivControlWrap.append(lblSAmountLabel);
            //spstr2.append(spstr2td1).append(spstr2td2);
            spstr2.append(spstr2td2);
            spstr2td2.append($DivInputWrap);
            $DivInputWrap.append($DivControlWrap);
            sPanelLayout.append(spstr2);


            txtSRouting = $('<input class="vis-payment-inputs" type="text" placeholder=" " data-placeholder="" >');
            var spstr3 = $('<tr></tr>');
            //var spstr3td1 = $('<td class="vis-payment-td1"></td>');
            var spstr3td2 = $('<td class="vis-payment-td2"></td>');
            var $DivInputWrap = $('<div class="input-group vis-input-wrap">');
            var $DivControlWrap = $('<div class="vis-control-wrap">');
            $DivControlWrap.append(txtSRouting);
            $DivControlWrap.append(lblSRoutingLabel);
            //spstr3.append(spstr3td1).append(spstr3td2);
            spstr3.append(spstr3td2);
            spstr3td2.append($DivInputWrap);
            $DivInputWrap.append($DivControlWrap);
            sPanelLayout.append(spstr3);


            txtSNumber = $('<input class="vis-payment-inputs" type="text" placeholder=" " data-placeholder="" >');
            var spstr4 = $('<tr></tr>');
            //var spstr4td1 = $('<td class="vis-payment-td1"></td>');
            var spstr4td2 = $('<td class="vis-payment-td2"></td>');
            var $DivInputWrap = $('<div class="input-group vis-input-wrap">');
            var $DivControlWrap = $('<div class="vis-control-wrap">');
            $DivControlWrap.append(txtSNumber);
            $DivControlWrap.append(lblSNumberLabel);
            //spstr4.append(spstr4td1).append(spstr4td2);
            spstr4.append(spstr4td2);
            spstr4td2.append($DivInputWrap);
            $DivInputWrap.append($DivControlWrap);
            sPanelLayout.append(spstr4);


            txtSCheck = $('<input class="vis-payment-inputs"  type="text" placeholder=" " data-placeholder="" >');
            var spstr5 = $('<tr></tr>');
            //var spstr5td1 = $('<td class="vis-payment-td1"></td>');
            var spstr5td2 = $('<td class="vis-payment-td2"></td>');
            var $DivInputWrap = $('<div class="input-group vis-input-wrap">');
            var $DivControlWrap = $('<div class="vis-control-wrap">');
            $DivControlWrap.append(txtSCheck);
            $DivControlWrap.append(lblSCheckLabel);
            //spstr5.append(spstr5td1).append(spstr5td2);
            spstr5.append(spstr5td2);
            spstr5td2.append($DivInputWrap);
            $DivInputWrap.append($DivControlWrap);
            sPanelLayout.append(spstr5);


            //not know where it will appear in the form
            cmbSCurrency = $('<select class="vis-payment-cmb"></select>');
            var spstr6 = $('<tr></tr>');
            //var spstr6td1 = $('<td class="vis-payment-td1"></td>');
            var spstr6td2 = $('<td class="vis-payment-td2"></td>');
            var $DivInputWrap = $('<div class="input-group vis-input-wrap">');
            var $DivControlWrap = $('<div class="vis-control-wrap">');
            $DivControlWrap.append(cmbSCurrency);
            $DivControlWrap.append(lblSCurrencyLabel);
            //spstr6.append(spstr6td1).append(spstr6td2);
            spstr6.append(spstr6td2);
            spstr6td2.append($DivInputWrap);
            $DivInputWrap.append($DivControlWrap);
            sPanelLayout.append(spstr6);




            btnSOnline = $('<button class="vis-payment-inputs-buttons"  type="button">' + VIS.Msg.translate(ctx, 'Online') + '</button>');
            var spstr7 = $('<tr></tr>');
            var spstr7td1 = $('<td colspan="2"></td>');
            spstr7td1.append(btnSOnline);
            spstr7.append(spstr7td1);
            sPanelLayout.append(spstr7);

            //**************On Credit************************/
            // On Credit     
            lblPTermLabel = $('<label class="vis-payment-span"> ' + VIS.Msg.translate(ctx, 'C_PaymentTerm_ID') + '</label>');

            pPanel = $('<table  class="vis-payment-table"></table>');
            var pPanelTr = $('<tr></tr>');
            pPanelLayout = $('<table  class="vis-payment-table"></table>');
            pPanelTr.append(pPanelLayout);
            pPanel.append(pPanelTr);
            centerPanelTR.append(pPanel);

            cmbPTerm = $('<select class="vis-payment-cmb"></select>');
            var ppltr1 = $('<tr></tr>');
            //var ppltr1td1 = $('<td class="vis-payment-td1"></td>');
            var ppltr1td2 = $('<td class="vis-payment-td2"></td>');
            var $DivInputWrap = $('<div class="input-group vis-input-wrap">');
            var $DivControlWrap = $('<div class="vis-control-wrap">');
            $DivControlWrap.append(cmbPTerm);
            $DivControlWrap.append(lblPTermLabel);
            //ppltr1.append(ppltr1td1).append(ppltr1td2);
            ppltr1.append(ppltr1td2);
            ppltr1td2.append($DivInputWrap);
            $DivInputWrap.append($DivControlWrap);
            pPanelLayout.append(ppltr1);

            //**************Cash************************/
            // Cash            
            lblBCashBookLabel = $('<label class="vis-payment-span"> ' + VIS.Msg.translate(ctx, 'VAB_CashBook_ID') + '</label>');


            lblBCurrencyLabel = $('<label class="vis-payment-span"> ' + VIS.Msg.translate(ctx, 'VAB_Currency_ID') + '</label>');


            lblBAmountLabel = $('<label class="vis-payment-span"> ' + VIS.Msg.translate(ctx, 'Amount') + '</label>');


            lblBAmountField = $('<label class="vis-payment-span vis-payment-AmountField"> </label>');


            lblBDateLabel = $('<label class="vis-payment-span"> ' + VIS.Msg.translate(ctx, 'DateAcct') + '</label>');


            bPanel = $('<table class="vis-payment-table"></table>');
            var bPanelTr = $('<tr></tr>');
            bPanelLayout = $('<table class="vis-payment-table"></table>');
            bPanelTr.append(bPanelLayout);
            bPanel.append(bPanelTr);
            centerPanelTR.append(bPanel);


            cmbBCurrency = $('<select class="vis-payment-cmb"></select>');
            var bPltr1 = $('<tr></tr>');
            //var bPltr1td1 = $('<td class="vis-payment-td1"></td>');
            var bPltr1td2 = $('<td class="vis-payment-td2"></td>');
            var $DivInputWrap = $('<div class="input-group vis-input-wrap">');
            var $DivControlWrap = $('<div class="vis-control-wrap">');
            $DivControlWrap.append(cmbBCurrency);
            $DivControlWrap.append(lblBCurrencyLabel);
            //bPltr1.append(bPltr1td1).append(bPltr1td2);
            bPltr1.append(bPltr1td2);
            bPltr1td2.append($DivInputWrap);
            $DivInputWrap.append($DivControlWrap);
            bPanelLayout.append(bPltr1);


            cmbBCashBook = $('<select class="vis-payment-cmb"></select>');
            var bPltr2 = $('<tr></tr>');
            //var bPltr2td1 = $('<td class="vis-payment-td1"></td>');
            var bPltr2td2 = $('<td class="vis-payment-td2"></td>');
            var $DivInputWrap = $('<div class="input-group vis-input-wrap">');
            var $DivControlWrap = $('<div class="vis-control-wrap">');
            $DivControlWrap.append(cmbBCashBook);
            $DivControlWrap.append(lblBCashBookLabel);
            //bPltr2.append(bPltr2td1).append(bPltr2td2);
            bPltr2.append(bPltr2td2);
            bPltr2td2.append($DivInputWrap);
            $DivInputWrap.append($DivControlWrap);
            bPanelLayout.append(bPltr2);


            bDateField = $('<input class="vis-payment-inputs vis-payment-date" type="date" placeholder=" " data-placeholder="" >');
            var bPltr3 = $('<tr></tr>');
            //var bPltr3td1 = $('<td class="vis-payment-td1"></td>');
            var bPltr3td2 = $('<td class="vis-payment-td2"></td>');
            var $DivInputWrap = $('<div class="input-group vis-input-wrap">');
            var $DivControlWrap = $('<div class="vis-control-wrap">');
            $DivControlWrap.append(bDateField);
            $DivControlWrap.append(lblBDateLabel);
            //bPltr3.append(bPltr3td1).append(bPltr3td2);
            bPltr3.append(bPltr3td2);
            bPltr3td2.append($DivInputWrap);
            $DivInputWrap.append($DivControlWrap);
            bPanelLayout.append(bPltr3);


            //lblBAmountField = $('<p class="vis-payment-span vis-payment-AmountField"> </p> ');
            lblBAmountField = $('<input type="text" readonly class="vis-payment-span vis-payment-AmountField"> ');
            var bPltr4 = $('<tr></tr>');
            //var bPltr4td1 = $('<td class="vis-payment-td1"></td>');
            var bPltr4td2 = $('<td class="vis-payment-td2"></td>');
            var $DivInputWrap = $('<div class="input-group vis-input-wrap">');
            var $DivControlWrap = $('<div class="vis-control-wrap">');
            $DivControlWrap.append(lblBAmountField);
            $DivControlWrap.append(lblBAmountLabel);
            //bPltr4.append(bPltr4td1).append(bPltr4td2);
            bPltr4.append(bPltr4td2);
            bPltr4td2.append($DivInputWrap);
            $DivInputWrap.append($DivControlWrap);
            bPanelLayout.append(bPltr4);

            $maindiv.append(centerPanel);


        };

        function dynInit(values) {

            var retval = false;
            _DocStatus = mTab.getValue("DocStatus");
            //   this.log.config(_DocStatus);

            if (mTab.getValue("VAB_BusinessPartner_ID") == null) {
                VIS.ADialog.error("0", true, VIS.Msg.getMsg("SaveErrorRowNotFound"));
                retval = false;
                return retval;
            }

            //  DocStatus
            _DocStatus = mTab.getValue("DocStatus");
            if (_DocStatus == null) {
                _DocStatus = "";
            }
            //	Is the Trx closed?		Reversed / Voided / Cloased
            if (_DocStatus.equals("RE") || _DocStatus.equals("VO") || _DocStatus.equals("CL")) {
                retval = false;
                return retval;
            }
            //  Document is not complete - allow to change the Payment Rule only
            if (_DocStatus.equals("CO") || _DocStatus.equals("WP")) {
                _onlyRule = false;
            }
            else {
                _onlyRule = true;
            }
            //	PO only  Rule
            if (!_onlyRule		//	Only order has Warehouse
                && !_isSOTrx && mTab.getValue("M_Warehouse_ID") != null) {
                _onlyRule = true;
            }

            if (_onlyRule) {
                centerPanel.hide();
            }
            else {
                centerPanel.show();
            }


            //  Amount
            _Amount = mTab.getValue("GrandTotal");
            if (!_onlyRule && _Amount == 0) {
                //ADialog.error(m_WindowNo, this, "PaymentZero");
                VIS.ADialog.error("", true, VIS.Msg.getMsg("PaymentZero"));
                retval = false;
                return retval;
            }

            //Get Data from Grid
            _VAF_Client_ID = parseInt(mTab.getValue("VAF_Client_ID"));
            _VAF_Org_ID = parseInt(mTab.getValue("VAF_Org_ID"));//.intValue();
            _VAB_BusinessPartner_ID = parseInt(mTab.getValue("VAB_BusinessPartner_ID"));//.intValue();
            _PaymentRule = mTab.getValue("PaymentRule");
            _VAB_Currency_ID = parseInt(mTab.getValue("VAB_Currency_ID"));//.intValue();
            _DateAcct = mTab.getValue("DateAcct");

            if (mTab.getValue("C_PaymentTerm_ID") != null || mTab.getValue("C_PaymentTerm_ID") != {}) {
                _C_PaymentTerm_ID = parseInt(mTab.getValue("C_PaymentTerm_ID"));//.intValue();
            }
            if (mTab.getValue("C_Payment_ID") != null && mTab.getValue("C_Payment_ID") != {}) {
                _C_Payment_ID = parseInt(mTab.getValue("C_Payment_ID"));//.intValue();
            }
            if (mTab.getValue("VAB_CashJRNLLine_ID") != null && mTab.getValue("VAB_CashJRNLLine_ID") != {}) {
                _VAB_CashJRNLLine_ID = parseInt(mTab.getValue("VAB_CashJRNLLine_ID"));//.intValue();
            }

            inputProperties = {
                _DocStatus: _DocStatus, _isSOTrx: _isSOTrx, _VAF_Client_ID: _VAF_Client_ID, _VAF_Org_ID: _VAF_Org_ID, _VAB_BusinessPartner_ID: _VAB_BusinessPartner_ID, _PaymentRule: _PaymentRule, _VAB_Currency_ID: _VAB_Currency_ID,
                _DateAcct: _DateAcct, _C_PaymentTerm_ID: _C_PaymentTerm_ID, _C_Payment_ID: _C_Payment_ID, _VAB_CashJRNLLine_ID: _VAB_CashJRNLLine_ID, values: JSON.stringify(values), _Amount: _Amount
            };

            $.ajax({
                url: VIS.Application.contextUrl + 'PaymentRule/LoadPaymentMethodDetails',
                type: 'POST',
                async: false,
                dataType: 'Json',
                data: inputProperties,
                success: function (data) {
                    setVisibility(false);
                    var result = JSON.parse(data);

                    _CCType = result.CCType;


                    //centerPanel.Visibility = !_onlyRule;
                    if (_onlyRule) {
                        centerPanel.hide();
                    }
                    else {
                        centerPanel.show();
                    }

                    lblBAmountField.val(_Amount);
                    lblSAmountField.val(_Amount);


                    txtKNumber.val(result.strKNumber);
                    txtKExp.val(result.strKExp);
                    txtKApproval.val(result.strKApproval);
                    lblKStatus.val(result.strKStatus);

                    if (result.canChange) {
                        cmbKType.attr('disable', false);
                        txtKNumber.attr('disable', false);
                        txtKExp.attr('disable', false);
                        txtKApproval.attr('disable', false);
                        btnKOnline.attr('disable', false);
                    }

                    txtSRouting.val(result.StrSRouting);
                    txtSNumber.val(result.StrSNumber);
                    txtSCheck.val(result.StrSCheck);
                    lblSStatus.text(result.StrSStatus);
                    //  Transfer
                    lblTStatus.text(result.StrTStatus);

                    //	Accounting Date
                    
                    if (_DateAcct != null) {
                        bDateField.Value = _DateAcct;
                    }
                    //	Is the currency an EMU currency?
                    var VAB_Currency_ID = _VAB_Currency_ID;
                    if (result._Currencies != null && result._Currencies != undefined && result._Currencies[_VAB_Currency_ID]) {
                        //IEnumerator en = (IEnumerator)_Currencies.Keys.GetEnumerator();
                        //while (en.MoveNext())
                        //{
                        //    Object key = en.Current;
                        //    cmbBCurrency.Items.Add(_Currencies[(int)key]);
                        //    cmbSCurrency.Items.Add(_Currencies[(int)key]);
                        //}

                        $.each(result._Currencies, function (key, name) {
                            cmbSCurrency
                                .append($("<option></option>")
                                .attr("value", name.Key)
                                .text(name.Name));
                        });


                        $.each(result._Currencies, function (key, name) {
                            cmbBCurrency
                                .append($("<option></option>")
                                .attr("value", name.Key)
                                .text(name.Name));
                        });

                        //cmbSCurrency.SelectedItem = _Currencies[VAB_Currency_ID];
                        //cmbBCurrency.SelectedItem = _Currencies[VAB_Currency_ID];
                    }
                    else	//	No EMU Currency
                    {
                        lblBCurrencyLabel.hide();	//	Cash
                        cmbBCurrency.hide();
                        lblSCurrencyLabel.hide();	//	Check
                        cmbSCurrency.hide();
                    }


                    //Payment Combo
                    if (_PaymentRule == null) {
                        _PaymentRule = "";
                    }



                    for (var a in button.values) {
                        var PaymentRule = a;
                        if (PAYMENTRULE_DirectDebit.equals(PaymentRule)			//	SO
                            && !_isSOTrx) {
                            continue;
                        }
                        else if (PAYMENTRULE_DirectDeposit.equals(PaymentRule)	//	PO 
                            && _isSOTrx) {
                            continue;
                        }

                        cmbPayment.append($('<option value="' + PaymentRule + '">' + button.values[a] + '</option>'));
                        if (PaymentRule.equals(_PaymentRule))	//	to select
                        {
                            cmbPayment.val(PaymentRule);//  = pp;
                        }
                    }


                    if (result.loadPaymentTerms != null && result.loadPaymentTerms != undefined) {
                        //Load Payment Terms
                        $.each(result.loadPaymentTerms, function (key, name) {
                            cmbPTerm
                                .append($("<option></option>")
                                .attr("value", name.Key)
                                .text(name.Name));
                        });

                        //	Set Selection
                        if (_C_PaymentTerm_ID != null && _C_PaymentTerm_ID != undefined) {
                            cmbPTerm.val(_C_PaymentTerm_ID);
                        }
                    }


                    if (result.loadAccounts != null && result.loadAccounts != undefined) {
                        //Load Accounts
                        $.each(result.loadAccounts, function (key, name) {
                            cmbTAccount.append($('<option></option>').attr("value", name.Key).text(name.Name));
                        });
                        if (result.kp != null) {
                            cmbTAccount.val(result.kp);
                        }
                    }
                    if (result.ccs != null && result.ccs != undefined) {
                        //Load Credit Cards
                        $.each(result.ccs, function (key, name) {
                            cmbKType.append($('<option></option>').attr("value", name.Key).text(name.Name));
                        });
                        //	Set Selection
                        if (result.CCType != null) {
                            cmbKType.val(result.CCType);
                        }

                    }



                    if (result.loadBankAccounts != null && result.loadBankAccounts != undefined) {
                        //Load Bank Accounts
                        $.each(result.loadBankAccounts, function (key, name) {
                            cmbSBankAccount.append($('<option></option>').attr("value", name.Key).text(name.Name));
                        });
                        if (result.kp != null) {
                            cmbSBankAccount.val(result.kp);
                        }
                    }
                    if (result.Checkbook_ID != null) {
                        cmbSBankAccount.val(result.Checkbook_ID);
                    }

                    if (result.loadCashBooks != null && result.loadCashBooks != undefined) {
                        //Load Cash Books
                        $.each(result.loadCashBooks, function (key, name) {
                            cmbBCashBook.append($('<option></option>').attr("value", name.Key).text(name.Name));
                        });
                    }


                    //	Set Selection
                    if (result.Checkbook_ID != null) {
                        cmbBCashBook.val(result.Checkbook_ID);
                        if (_VAB_CashBook_ID == 0) {
                            _VAB_CashBook_ID = result.Checkbook_ID;  //  set to default to avoid 'cashbook changed' message
                        }
                    }



                    if (_PaymentRule == "B") {
                        kPanel.hide();
                        pPanel.hide();
                        bPanel.show()
                        tPanel.hide();
                        sPanel.hide();
                    }
                    else if (_PaymentRule == "K") {
                        kPanel.show();
                        pPanel.hide();
                        bPanel.hide();
                        tPanel.hide();
                        sPanel.hide();
                    }
                    else if (_PaymentRule == "D") {
                        kPanel.hide();
                        pPanel.hide();
                        bPanel.hide();
                        tPanel.show();
                        sPanel.hide();
                    }
                    else if (_PaymentRule == "P") {
                        kPanel.hide();
                        pPanel.show();
                        bPanel.hide();
                        tPanel.hide();
                        sPanel.hide();
                    }
                    else if (_PaymentRule == "S") {
                        kPanel.hide();
                        pPanel.hide();
                        bPanel.hide();
                        tPanel.hide();
                        sPanel.show();
                    }



                    retval = true;
                    return retval;
                }
            });
            return retval;
        };

        function invokeDynInit(_initOK) {
            if (_initOK != undefined && !_initOK) {
                ch.close();
                return;
            }
            //else {
            //    SetBusy(false);
            //}
            cmbPayment.on("change", cmbpaymentChanged);
            cmbSCurrency.on("change", cmbSCurrencyChanged);
            cmbBCurrency.on("change", cmbBCurrencyChanged);
            btnKOnline.on("click", btnKOnlineClick);

            //Ok button click
            ch.onOkClick = function () {
                setVisibility(true);

                var savea = save();
                if (savea) {
                    self.isOKPressed = true;

                    if (self.onClose) {
                        self.onClose();
                    }
                }
                else {
                    

                    //setVisibility(false);
                    //return false;
                }
            };

            //Cancel button click
            ch.onCancelClick = function () {
                if (self.onClose) {
                    self.onClose();
                }
            };

            ch.onClose = function () { self.dispose(); };

        };

        function cmbpaymentChanged(cmb) {

            //	get selection
            var pp = cmbPayment.val();
            if (pp != null) {
                var s = pp.toLower();
                //if (X_C_Order.PAYMENTRULE_DirectDebit.equalsIgnoreCase(s))
                if (PAYMENTRULE_DirectDebit == s.toLower() || PAYMENTRULE_DirectDebit == s.toUpper()) {
                    s = PAYMENTRULE_DirectDeposit.toLower();
                }
                s += "Panel";
                //centerLayout.Show(centerPanel, s);	//	switch to panel
                // centerPanel.Show();

                if (pp.equals("K")) {
                    kPanel.show();
                    pPanel.hide();
                    bPanel.hide();
                    tPanel.hide();
                    sPanel.hide();
                }
                else if (pp.equals("B")) {
                    kPanel.hide();
                    pPanel.hide();
                    bPanel.show();
                    tPanel.hide();
                    sPanel.hide();
                }
                else if (pp.equals("D")) {
                    kPanel.hide();
                    pPanel.hide();
                    bPanel.hide();
                    tPanel.show();
                    sPanel.hide();
                }
                else if (pp.equals("P")) {
                    kPanel.hide();
                    pPanel.show();
                    bPanel.hide();
                    tPanel.hide();
                    sPanel.hide();
                }
                else if (pp.equals("S")) {
                    kPanel.hide();
                    pPanel.hide();
                    bPanel.hide();
                    tPanel.hide();
                    sPanel.show();
                }
                else if (pp.equals("T")) {
                    kPanel.hide();
                    pPanel.hide();
                    bPanel.hide();
                    tPanel.hide();
                    sPanel.hide();
                }

            }





        };

        function cmbSCurrencyChanged(cmb) {
        };

        function cmbBCurrencyChanged(cmb) {
        };

        function btnKOnlineClick(btn) {

        };

        function save() {

            var returnval = true;

            var dataOK = checkMandatory();
            if (!dataOK) {
                returnval = false;
                return returnval;
            }


            var newPaymentRule = cmbPayment.val();
            if (newPaymentRule == null)
                return;

            if (_onlyRule) {
                if (newPaymentRule) {
                    if (!newPaymentRule.equals(_PaymentRule)) {
                        mTab.setValue("PaymentRule", newPaymentRule);
                    }
                    returnval = true;
                    return returnval;
                }
            }

            inputProperties['cmbPayment'] = cmbPayment.val();

            if (newPaymentRule.equals(PAYMENTRULE_Cash)) {
                inputProperties['cmbBCurrency'] = cmbBCurrency.val();
                inputProperties['cmbBCashBook'] = cmbBCashBook.val();
                inputProperties['bDateField'] = bDateField.val();
                ///saveValues = { cmbBCurrency: cmbBCurrency.val(), cmbBCashBook: cmbBCashBook.val(), bDateField: bDateField.val() };

            }

                //	K (CreditCard)  Type, Number, Exp, Approval
            else if (newPaymentRule.equals(PAYMENTRULE_CreditCard)) {
                inputProperties['cmbKType'] = cmbKType.val();
                inputProperties['txtKNumber'] = txtKNumber.val();
                inputProperties['txtKExp'] = txtKExp.val();
                inputProperties['txtKApproval'] = txtKApproval.val();
                //saveValues = {cmbKType:cmbKType.val(), txtKNumber: txtKNumber.val(), txtKExp: txtKExp.val(), txtKApproval: txtKApproval.val() };

            }
            else if (newPaymentRule.equals(PAYMENTRULE_DirectDeposit)) {

                inputProperties['cmbTAccount'] = cmbTAccount.val();
                //saveValues = { cmbTAccount: cmbTAccount.val() };
            }
            else if (newPaymentRule.equals(PAYMENTRULE_OnCredit)) {

                inputProperties['cmbPTerm'] = cmbPTerm.val();

                //  saveValues = { cmbPTerm: cmbPTerm.val() };
            }

            else if (newPaymentRule.equals(PAYMENTRULE_Check)) {
                inputProperties['cmbSBankAccount'] = cmbSBankAccount.val();
                inputProperties['txtSRouting'] = txtSRouting.val();
                inputProperties['txtSNumber'] = txtSNumber.val();
                inputProperties['txtSCheck'] = txtSCheck.val();
                inputProperties['cmbSCurrency'] = cmbSCurrency.val();
                //  saveValues = { cmbSBankAccount: cmbSBankAccount.val(), txtSRouting: txtSRouting.val(), txtSNumber: txtSNumber.val(), txtSCheck: txtSCheck.val(), cmbSCurrency: cmbSCurrency.val() };
            }


            inputProperties['C_Order_ID'] = ctx.getContextAsInt(windowNo, "C_Order_ID");
            inputProperties['VAB_Invoice_ID'] = ctx.getContextAsInt(windowNo, "VAB_Invoice_ID");


            $.ajax({
                url: VIS.Application.contextUrl + 'PaymentRule/SaveChanges',
                type: 'POST',
                async: false,
                dataType: 'Json',
                data: inputProperties,
                success: function (data) {

                    var result = JSON.parse(data);


                    if (result.ErrorMsg != null && result.ErrorMsg != "" && result.ErrorMsg != undefined) {
                        alert(result.ErrorMsg);
                    }

                    if (result.SucessMsg != null && result.SucessMsg != "" && result.SucessMsg != undefined) {
                        alert(result.SucessMsg);
                    }
                    /**********************
                *	Save Values to mTab
                */
                    //log.config("Saving changes");

                    //
                    if (result.newPaymentRule != null && result.newPaymentRule != undefined && !result.newPaymentRule.equals(_PaymentRule)) {
                        mTab.setValue("PaymentRule", result.newPaymentRule);
                    }
                    //

                    if (result.newDateAcct != null && result.newDateAcct != undefined && !result.newDateAcct.equals(_DateAcct)) {
                        mTab.setValue("DateAcct", result.newDateAcct);
                    }
                    //
                    if (result.newC_PaymentTerm_ID != _C_PaymentTerm_ID && result.newC_PaymentTerm_ID > 0) {
                        mTab.setValue("C_PaymentTerm_ID", result.newC_PaymentTerm_ID);
                    }
                    //	Set Payment
                    if (result._C_Payment_ID != _C_Payment_ID) {
                        if (result._C_Payment_ID == 0) {
                            mTab.setValue("C_Payment_ID", null);
                        }
                        else {
                            mTab.setValue("C_Payment_ID", result._C_Payment_ID);
                        }
                    }
                    //	Set Cash
                    if (result._VAB_CashJRNLLine_ID != _VAB_CashJRNLLine_ID) {
                        if (result._VAB_CashJRNLLine_ID == 0) {
                            mTab.setValue("VAB_CashJRNLLine_ID", null);
                        }
                        else {
                            mTab.setValue("VAB_CashJRNLLine_ID", result._VAB_CashJRNLLine_ID);
                        }
                    }

                    returnval = true;
                    return returnval;

                },
                error: function () {
                    returnval = false;
                    return returnval;
                }
            });
            return returnval;
        };



        /// <summary>
        /// Check Mandatory
        /// </summary>
        /// <returns>true if all mandatory items are OK</returns>
        function checkMandatory() {
            var dataOK = true;
            try {
                //	log.config( "VPayment.checkMandatory");

                var vp = cmbPayment.val();
                if (vp == null || vp == undefined) {
                    return false;
                }
                var PaymentRule = vp;
                //  only Payment Rule
                if (_onlyRule) {
                    return true;
                }

                var DateAcct = _DateAcct;
                var C_PaymentTerm_ID = _C_PaymentTerm_ID;
                var VAB_CashBook_ID = _VAB_CashBook_ID;
                var CCType = _CCType;
                //
                var VAB_Bank_Acct_ID = 0;

                /***********************
                 *	Mandatory Data Check
                 */

                //	B (Cash)		(Currency)
                if (PaymentRule.equals(PAYMENTRULE_Cash)) {
                    VAB_CashBook_ID = cmbBCashBook.val();

                    DateAcct = bDateField.val();
                }

                    //	K (CreditCard)  Type, Number, Exp, Approval
                else if (PaymentRule.equals(PAYMENTRULE_CreditCard)) {
                    CCType = cmbKType.val();


                    if (txtKNumber.val() == null || txtKNumber.val() == "" || txtKNumber.val() == undefined) {
                        VIS.ADialog.error("", true, VIS.Msg.getMsg("CreditCardNumberError"));
                        dataOK = false;
                    }
                    //if (vp != null)
                    //{
                    //    CCType = vp.GetValue();
                    //}
                    ////
                    //String error = MPaymentValidate.ValidateCreditCardNumber(txtKNumber.Text, CCType);
                    //if (error.Length != 0)
                    //{
                    //    txtKNumber.BackColor = GlobalVariable.MANDATORY_TEXT_BACK_COLOR;
                    //    //MessageBox.Show("getFieldBackground_Error()");
                    //    if (error.IndexOf("?") == -1)
                    //    {
                    //        //ADialog.error(_WindowNo, this, error);
                    //        ShowMessage.Error("", true, Msg.GetMsg(GetCtx(),error, true).ToString());
                    //        dataOK = false;
                    //    }
                    //    else    //  warning
                    //    {
                    //        //if (!ADialog.ask(_WindowNo, this, error))
                    //        if (!ShowMessage.Ask("", true, error))
                    //        {
                    //            dataOK = false;
                    //        }
                    //    }
                    //}
                    //error = MPaymentValidate.ValidateCreditCardExp(txtKExp.Text);
                    //if (error.Length != 0)
                    //{
                    //    txtKExp.BackColor = GlobalVariable.MANDATORY_TEXT_BACK_COLOR;
                    //    //ADialog.error(_WindowNo, this, error);
                    //    ShowMessage.Error("", true, Msg.GetMsg(GetCtx(),error, true).ToString());
                    //    //Msg.GetMsg(GetCtx(),error, true);
                    //    dataOK = false;
                    //}
                }

                    //	T (Transfer)	BPartner_Bank
                else if (PaymentRule.equals(PAYMENTRULE_DirectDeposit)
                    || PaymentRule.equals(PAYMENTRULE_DirectDebit)) {
                    var bpba = cmbTAccount.val();
                    if (bpba == null) {
                        //ADialog.error(_WindowNo, this, "PaymentBPBankNotFound");
                        // cmbTAccount.ccs('background-color', 'Red');
                        VIS.ADialog.error("", true, VIS.Msg.getMsg("PaymentBPBankNotFound"));
                        dataOK = false;
                    }
                }	//	Direct

                    //	P (PaymentTerm)	PaymentTerm
                else if (PaymentRule.equals(PAYMENTRULE_OnCredit)) {
                    C_PaymentTerm_ID = cmbPTerm.val();
                    if (kp != null) {
                        C_PaymentTerm_ID = kp;
                    }
                }

                    //	S (Check)		(Currency) CheckNo, Routing
                else if (PaymentRule.equals(PAYMENTRULE_Check)) {
                    //	cmbSCurrency.SelectedItem;
                    var kp = cmbSBankAccount.val();
                    if (kp != null) {
                        VAB_Bank_Acct_ID = kp;
                    }
                    //String error = MPaymentValidate.ValidateRoutingNo(txtSRouting.Text);
                    if (txtSRouting.val() == null || txtSRouting.val() == "" || txtSRouting.val() == undefined) {
                        //txtSRouting.BackColor = GlobalVariable.MANDATORY_TEXT_BACK_COLOR;
                        //ADialog.error(_WindowNo, this, error);
                        VIS.ADialog.error("", true, VIS.Msg.getMsg("PaymentBankRoutingNotValid"));
                        //ShowMessage.Error(error, true, error);
                        dataOK = false;
                    }
                    //error = MPaymentValidate.ValidateAccountNo(txtSNumber.Text);
                    if (txtSNumber.val() == null || txtSNumber.val() == "" || txtSNumber.val() == undefined) {
                        // txtSNumber.BackColor = GlobalVariable.MANDATORY_TEXT_BACK_COLOR;
                        //ADialog.error(_WindowNo, this, error);
                        VIS.ADialog.error("", true, VIS.Msg.getMsg("PaymentBankAccountNotValid"));
                        //ShowMessage.Error("", true, error);
                        dataOK = false;
                    }
                    //error = MPaymentValidate.ValidateCheckNo(txtSCheck.Text);
                    if (txtSCheck.val() == null || txtSCheck.val() == "" || txtSCheck.val() == undefined) {
                        //txtSCheck.BackColor = GlobalVariable.MANDATORY_TEXT_BACK_COLOR;
                        //ADialog.error(_WindowNo, this, error);
                        VIS.ADialog.error("", true, VIS.Msg.getMsg("PaymentBankCheckNotValid"));
                        //ShowMessage.Error("", true, error);
                        dataOK = false;
                    }
                }
                else {
                    //log.log(Level.SEVERE, "Unknown PaymentRule " + PaymentRule);
                    return false;
                }

                //  find Bank Account if not qualified yet
                if ("KTSD".indexOf(PaymentRule) != -1 && VAB_Bank_Acct_ID == 0) {
                    var tender = TENDERTYPE_CreditCard;
                    if (PaymentRule.equals(PAYMENTRULE_DirectDeposit)) {
                        tender = TENDERTYPE_DirectDeposit;
                    }
                    else if (PaymentRule.equals(PAYMENTRULE_DirectDebit)) {
                        tender = TENDERTYPE_DirectDebit;
                    }
                    else if (PaymentRule.equals(PAYMENTRULE_Check)) {
                        tender = TENDERTYPE_Check;
                    }
                    //	Check must have a bank account
                    if (VAB_Bank_Acct_ID == 0 && "S".equals(PaymentRule)) {
                        //ADialog.error(_WindowNo, this, "PaymentNoProcessor");
                        VIS.ADialog.error("", true, VIS.Msg.getMsg("PaymentNoProcessor"));
                        dataOK = false;
                    }
                }
            }
            catch (ex) {
                log.Severe(ex.ToString());
                //MessageBox.Show("VPayment--CheckMandatory");
            }
            //log.config("OK=" + dataOK);
            return dataOK;
        }

        function setVisibility(tvisible) {
            if (tvisible) {
                tabmenubusy.show(); /*loadLabel.show();*/
                $maindiv.find("button").prop("disabled", true);
            }
            else {
                tabmenubusy.hide();
                //loadLabel.hide();
                $maindiv.find("button").prop("disabled", false);
            }
        }


        /// <summary>
        /// Init OK to be able to make changes?
        /// </summary>
        /// <returns>true if init OK</returns>
        this.isInitOK = function () {
            return _initOK;
        }

        this.show = function () {
            ch = new VIS.ChildDialog();
            $maindiv = $('<div class="vis-formouterwrpdiv"></div>');
            $maindiv.append(tabmenubusy)/*.append(loadLabel)*/;
            setVisibility(true);
            ch.setContent($maindiv);
            ch.setWidth(360);
            ch.setTitle(VIS.Msg.getMsg("Payment"));
            ch.setModal(true);
            ch.show();
        };

        this.onClose = null;

        this.dispose = function () {
            if ($maindiv != null) {
                $maindiv = null;
            }
            ch = null;
            bPanel = null;
            kPanel = null;
            tPanel = null;
            sPanel = null;
            pPanel = null;
            this.onClose = null;
            lblKStatus = null;
            lblTStatus = null;
            lblSStatus = null;
            lblBAmountLabel = null;
            lblBAmountField = null;
            lblSAmountLabel = null;
            lblSAmountField = null;
            lblPaymentLabel = null;
            lblKTypeLabel = null;
            lblKNumnerLabel = null;
            lblKExpLabel = null;
            lblTAccountLabel = null;
            lblKApprovalLabel = null;
            lblSNumberLabel = null;
            lblSRoutingLabel = null;
            lblSCurrencyLabel = null;
            lblBCurrencyLabel = null;
            lblPTermLabel = null;
            lblBDateLabel = null;
            lblSCheckLabel = null;
            lblSBankAccountLabel = null;
            lblBCashBookLabel = null;


            cmbPayment = null;
            cmbKType = null;
            cmbTAccount = null;
            cmbSCurrency = null;
            cmbBCurrency = null;
            cmbPTerm = null;
            cmbSBankAccount = null;
            cmbBCashBook = null;

            btnKOnline = null;
            btnSOnline = null;

            txtKNumber = null;
            txtKExp = null;
            txtKApproval = null;
            txtSNumber = null;
            txtSRouting = null;//"",true,false,false,70, 
            txtSCheck = null;

            //private VDate bDateField;
            bDateField = null;

            bPanelLayout = null;// TableLayoutPanel();
            pPanelLayout = null;
            centerLayout = null;
            kLayout = null;
            tPanelLayout = null;
            sPanelLayout = null;
        };

    };

    VIS.VPayment = VPayment;



})(VIS, jQuery);
