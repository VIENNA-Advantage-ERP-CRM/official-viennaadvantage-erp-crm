﻿/********************************************************
 * Module Name    : 
 * Purpose        : 
 * Class Used     : X_M_Cost_ForeignCurrency
 * Chronological Development
 * Amit Bansal     08-Dec-2016
 ******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using VAdvantage.Classes;
using VAdvantage.Utility;
using VAdvantage.DataBase;
using VAdvantage.Process;
using VAdvantage.Logging;

namespace VAdvantage.Model
{
    public class MCostForeignCurrency : X_M_Cost_ForeignCurrency
    {

        private static VLogger _log = VLogger.GetVLogger(typeof(MCostForeignCurrency).FullName);


        public MCostForeignCurrency(Ctx ctx, int M_Cost_ForeignCurrency_ID, Trx trxName)
            : base(ctx, M_Cost_ForeignCurrency_ID, trxName)
        {

        }

        public MCostForeignCurrency(Ctx ctx, DataRow dr, Trx trxName)
            : base(ctx, dr, trxName)
        {

        }

        protected override bool BeforeSave(bool newRecord)
        {
            return true;
        }

        protected override bool AfterSave(bool newRecord, bool success)
        {
            return true;
        }

        public MCostForeignCurrency(int VAF_Org_ID, MProduct product, int M_AttributeSetInstance_ID,
              int M_CostElement_ID, int VAB_BusinessPartner_ID, int VAB_Currency_ID)
            : this(product.GetCtx(), 0, product.Get_TrxName())
        {
            SetClientOrg(product.GetVAF_Client_ID(), VAF_Org_ID);
            SetM_Product_ID(product.GetM_Product_ID());
            SetM_AttributeSetInstance_ID(M_AttributeSetInstance_ID);
            SetM_CostElement_ID(M_CostElement_ID);
            SetVAB_BusinessPartner_ID(VAB_BusinessPartner_ID);
            SetVAB_Currency_ID(VAB_Currency_ID);
        }

        public static MCostForeignCurrency Get(MProduct product, int M_AttributeSetInstance_ID, int VAF_Org_ID, int M_CostElement_ID,
            int VAB_BusinessPartner_ID, int VAB_Currency_ID)
        {
            MCostForeignCurrency foreignCurrency = null;
            String sql = "SELECT * "
                + "FROM M_Cost_ForeignCurrency c "
                + "WHERE VAF_Client_ID=" + product.GetVAF_Client_ID() + " AND VAF_Org_ID=" + VAF_Org_ID
                + " AND M_Product_ID=" + product.GetM_Product_ID()
                + " AND NVL(M_AttributeSetInstance_ID , 0) =" + M_AttributeSetInstance_ID
                + " AND M_CostElement_ID=" + M_CostElement_ID
                + " AND VAB_BusinessPartner_ID = " + VAB_BusinessPartner_ID
                + " AND VAB_Currency_ID = " + VAB_Currency_ID;
            DataTable dt = null;
            IDataReader idr = null;
            try
            {
                idr = DB.ExecuteReader(sql, null, product.Get_TrxName());
                dt = new DataTable();
                dt.Load(idr);
                idr.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    foreignCurrency = new MCostForeignCurrency(product.GetCtx(), dr, product.Get_TrxName());
                }
            }
            catch (Exception e)
            {
                if (idr != null)
                {
                    idr.Close();
                }
                _log.Log(Level.SEVERE, sql, e);
            }
            finally { dt = null; }

            //	New
            if (foreignCurrency == null)
                foreignCurrency = new MCostForeignCurrency(VAF_Org_ID, product, M_AttributeSetInstance_ID,
                     M_CostElement_ID, VAB_BusinessPartner_ID, VAB_Currency_ID);
            return foreignCurrency;
        }

        public static bool InsertForeignCostAverageInvoice(Ctx ctx, MInvoice invoice, MInvoiceLine invoiceLine, Trx trx)
        {
            int acctSchema_ID = 0;
            int M_CostElement_ID = 0;
            int VAF_Org_ID = 0;
            int M_ASI_ID = 0;
            MProduct product = null;
            MAcctSchema acctSchema = null;
            MCostForeignCurrency foreignCost = null;
            dynamic pc = null;
            String cl = null;
            try
            {
                // if cost is calculated then not to calculate again
                if (invoiceLine.IsFutureCostCalculated())
                    return true;

                acctSchema_ID = Util.GetValueOfInt(DB.ExecuteScalar(@"SELECT asch.VAB_AccountBook_id FROM VAB_AccountBook asch INNER JOIN VAF_ClientDetail ci
                                    ON ci.VAB_AccountBook1_id = asch.VAB_AccountBook_id WHERE ci.vaf_client_id  = " + invoice.GetVAF_Client_ID()));
                acctSchema = new MAcctSchema(ctx, acctSchema_ID, trx);

                if (acctSchema.GetVAB_Currency_ID() != invoice.GetVAB_Currency_ID())
                {
                    // Get Costing Element of Av. Invoice
                    M_CostElement_ID = Util.GetValueOfInt(DB.ExecuteScalar(@"SELECT M_CostElement_ID FROM M_CostElement WHERE VAF_Client_ID = "
                                       + invoice.GetVAF_Client_ID() + " AND IsActive = 'Y' AND CostingMethod = 'I'"));

                    product = new MProduct(ctx, invoiceLine.GetM_Product_ID(), trx);

                    if (product != null && product.GetProductType() == "I" && product.GetM_Product_ID() > 0) // for Item Type product
                    {
                        pc = MProductCategory.Get(product.GetCtx(), product.GetM_Product_Category_ID());

                        // Get Costing Level
                        if (pc != null)
                        {
                            cl = pc.GetCostingLevel();
                        }
                        if (cl == null)
                            cl = acctSchema.GetCostingLevel();

                        if (cl == "C" || cl == "B")
                        {
                            VAF_Org_ID = 0;
                        }
                        else
                        {
                            VAF_Org_ID = invoice.GetVAF_Org_ID();
                        }
                        if (cl != "B")
                        {
                            M_ASI_ID = 0;
                        }
                        else
                        {
                            M_ASI_ID = invoiceLine.GetM_AttributeSetInstance_ID();
                        }

                        foreignCost = MCostForeignCurrency.Get(product, M_ASI_ID, VAF_Org_ID, M_CostElement_ID, invoice.GetVAB_BusinessPartner_ID(), invoice.GetVAB_Currency_ID());
                        foreignCost.SetVAB_Invoice_ID(invoice.GetVAB_Invoice_ID());
                        foreignCost.SetCumulatedQty(Decimal.Add(foreignCost.GetCumulatedQty(), invoiceLine.GetQtyInvoiced()));
                        foreignCost.SetCumulatedAmt(Decimal.Add(foreignCost.GetCumulatedAmt(), invoiceLine.GetLineNetAmt()));
                        if (foreignCost.GetCumulatedQty() != 0)
                        {
                            foreignCost.SetCostPerUnit(Decimal.Round(Decimal.Divide(foreignCost.GetCumulatedAmt(), foreignCost.GetCumulatedQty()), acctSchema.GetCostingPrecision()));
                        }
                        else
                        {
                            foreignCost.SetCostPerUnit(0);
                        }
                        if (!foreignCost.Save(trx))
                        {
                            ValueNamePair pp = VLogger.RetrieveError();
                            _log.Severe("Error occured during updating M_Cost_ForeignCurrency. Error name : " + pp.GetName() +
                                " AND Error Value : " + pp.GetValue() + " , For Invoice line : " + invoiceLine.GetVAB_InvoiceLine_ID() +
                                " , AND vaf_client_ID : " + invoiceLine.GetVAF_Client_ID());
                            return false;
                        }
                        else
                        {
                            invoiceLine.SetIsFutureCostCalculated(true);
                            if (!invoiceLine.Save(trx))
                            {
                                ValueNamePair pp = VLogger.RetrieveError();
                                _log.Severe("Error occured during updating Is Foreign Cost On VAB_Invoice. Error name : " + pp.GetName() +
                                    " AND Error Value : " + pp.GetValue() + " , For Invoice line : " + invoiceLine.GetVAB_InvoiceLine_ID() +
                                    " , AND vaf_client_ID : " + invoiceLine.GetVAF_Client_ID());
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Log(Level.SEVERE, "", ex);
                return false;
            }
            return true;
        }

        public static bool InsertForeignCostAveragePO(Ctx ctx, MOrder order, MOrderLine orderLine, MInOutLine inoutLine, Trx trx)
        {
            int acctSchema_ID = 0;
            int M_CostElement_ID = 0;
            int VAF_Org_ID = 0;
            int M_ASI_ID = 0;
            MProduct product = null;
            MAcctSchema acctSchema = null;
            MCostForeignCurrency foreignCost = null;
            dynamic pc = null;
            String cl = null;
            try
            {
                // if cost is calculated then not to calculate again
                if (inoutLine.IsFutureCostCalculated())
                    return true;

                acctSchema_ID = Util.GetValueOfInt(DB.ExecuteScalar(@"SELECT asch.VAB_AccountBook_id FROM VAB_AccountBook asch INNER JOIN VAF_ClientDetail ci
                                    ON ci.VAB_AccountBook1_id = asch.VAB_AccountBook_id WHERE ci.vaf_client_id  = " + order.GetVAF_Client_ID()));
                acctSchema = new MAcctSchema(ctx, acctSchema_ID, trx);

                if (acctSchema.GetVAB_Currency_ID() != order.GetVAB_Currency_ID())
                {
                    // Get Costing Element of Average PO
                    M_CostElement_ID = Util.GetValueOfInt(DB.ExecuteScalar(@"SELECT M_CostElement_ID FROM M_CostElement WHERE VAF_Client_ID = "
                                       + order.GetVAF_Client_ID() + " AND IsActive = 'Y' AND CostingMethod = 'A'"));

                    product = new MProduct(ctx, orderLine.GetM_Product_ID(), trx);

                    if (product != null && product.GetProductType() == "I" && product.GetM_Product_ID() > 0) // for Item Type product
                    {
                        pc = MProductCategory.Get(product.GetCtx(), product.GetM_Product_Category_ID());

                        // Get Costing Level
                        if (pc != null)
                        {
                            cl = pc.GetCostingLevel();
                        }
                        if (cl == null)
                            cl = acctSchema.GetCostingLevel();

                        if (cl == "C" || cl == "B")
                        {
                            VAF_Org_ID = 0;
                        }
                        else
                        {
                            VAF_Org_ID = order.GetVAF_Org_ID();
                        }
                        if (cl != "B")
                        {
                            M_ASI_ID = 0;
                        }
                        else
                        {
                            M_ASI_ID = orderLine.GetM_AttributeSetInstance_ID();
                        }

                        foreignCost = MCostForeignCurrency.Get(product, M_ASI_ID, VAF_Org_ID, M_CostElement_ID, order.GetVAB_BusinessPartner_ID(), order.GetVAB_Currency_ID());
                        foreignCost.SetC_Order_ID(order.GetC_Order_ID());
                        foreignCost.SetCumulatedQty(Decimal.Add(foreignCost.GetCumulatedQty(), inoutLine.GetMovementQty()));
                        foreignCost.SetCumulatedAmt(Decimal.Add(foreignCost.GetCumulatedAmt(),
                                         Decimal.Multiply(Decimal.Divide(orderLine.GetLineNetAmt(), orderLine.GetQtyOrdered()), inoutLine.GetMovementQty())));
                        if (foreignCost.GetCumulatedQty() != 0)
                        {
                            foreignCost.SetCostPerUnit(Decimal.Round(Decimal.Divide(foreignCost.GetCumulatedAmt(), foreignCost.GetCumulatedQty()), acctSchema.GetCostingPrecision()));
                        }
                        else
                        {
                            foreignCost.SetCostPerUnit(0);
                        }
                        if (!foreignCost.Save(trx))
                        {
                            ValueNamePair pp = VLogger.RetrieveError();
                            _log.Severe("Error occured during updating M_Cost_ForeignCurrency. Error name : " + pp.GetName() +
                                " AND Error Value : " + pp.GetValue() + " , For Invoice line : " + orderLine.GetC_OrderLine_ID() +
                                " , AND vaf_client_ID : " + orderLine.GetVAF_Client_ID());
                            return false;
                        }
                        else
                        {
                            inoutLine.SetIsFutureCostCalculated(true);
                            if (!inoutLine.Save(trx))
                            {
                                ValueNamePair pp = VLogger.RetrieveError();
                                _log.Severe("Error occured during updating Is Foreign Cost On VAB_Invoice. Error name : " + pp.GetName() +
                                    " AND Error Value : " + pp.GetValue() + " , For Material Receipt : " + inoutLine.GetM_InOutLine_ID() +
                                    " , AND vaf_client_ID : " + inoutLine.GetVAF_Client_ID());
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Log(Level.SEVERE, "", ex);
                return false;
            }
            return true;
        }

        public static bool InsertForeignCostMatchInvoice(Ctx ctx, MInvoiceLine invoiceLine, decimal matchQty, int ASI, Trx trx)
        {
            int acctSchema_ID = 0;
            int M_CostElement_ID = 0;
            int VAF_Org_ID = 0;
            int M_ASI_ID = 0;
            MProduct product = null;
            MAcctSchema acctSchema = null;
            MCostForeignCurrency foreignCost = null;
            dynamic pc = null;
            String cl = null;
            MInvoice invoice = null;
            try
            {
                // if cost is calculated then not to calculate again
                if (invoiceLine.IsFutureCostCalculated())
                    return true;

                invoice = new MInvoice(ctx, invoiceLine.GetVAB_Invoice_ID(), trx);

                if (!invoice.IsSOTrx() && !invoice.IsReturnTrx())
                {
                    acctSchema_ID = Util.GetValueOfInt(DB.ExecuteScalar(@"SELECT asch.VAB_AccountBook_id FROM VAB_AccountBook asch INNER JOIN VAF_ClientDetail ci
                                    ON ci.VAB_AccountBook1_id = asch.VAB_AccountBook_id WHERE ci.vaf_client_id  = " + invoice.GetVAF_Client_ID()));
                    acctSchema = new MAcctSchema(ctx, acctSchema_ID, trx);

                    if (acctSchema.GetVAB_Currency_ID() != invoice.GetVAB_Currency_ID())
                    {
                        // Get Costing Element of Av. Invoice
                        M_CostElement_ID = Util.GetValueOfInt(DB.ExecuteScalar(@"SELECT M_CostElement_ID FROM M_CostElement WHERE VAF_Client_ID = "
                                           + invoice.GetVAF_Client_ID() + " AND IsActive = 'Y' AND CostingMethod = 'I'"));

                        product = new MProduct(ctx, invoiceLine.GetM_Product_ID(), trx);

                        if (product != null && product.GetProductType() == "I" && product.GetM_Product_ID() > 0) // for Item Type product
                        {
                            pc = MProductCategory.Get(product.GetCtx(), product.GetM_Product_Category_ID());

                            // Get Costing Level
                            if (pc != null)
                            {
                                cl = pc.GetCostingLevel();
                            }
                            if (cl == null)
                                cl = acctSchema.GetCostingLevel();

                            if (cl == "C" || cl == "B")
                            {
                                VAF_Org_ID = 0;
                            }
                            else
                            {
                                VAF_Org_ID = invoice.GetVAF_Org_ID();
                            }
                            if (cl != "B")
                            {
                                M_ASI_ID = 0;
                            }
                            else
                            {
                                M_ASI_ID = ASI;
                            }

                            foreignCost = MCostForeignCurrency.Get(product, M_ASI_ID, VAF_Org_ID, M_CostElement_ID, invoice.GetVAB_BusinessPartner_ID(), invoice.GetVAB_Currency_ID());
                            foreignCost.SetVAB_Invoice_ID(invoice.GetVAB_Invoice_ID());
                            foreignCost.SetCumulatedQty(Decimal.Add(foreignCost.GetCumulatedQty(), matchQty));
                            foreignCost.SetCumulatedAmt(Decimal.Add(foreignCost.GetCumulatedAmt(), Decimal.Multiply(invoiceLine.GetPriceActual(), matchQty)));
                            if (foreignCost.GetCumulatedQty() != 0)
                            {
                                foreignCost.SetCostPerUnit(Decimal.Round(Decimal.Divide(foreignCost.GetCumulatedAmt(), foreignCost.GetCumulatedQty()), acctSchema.GetCostingPrecision()));
                            }
                            else
                            {
                                foreignCost.SetCostPerUnit(0);
                            }
                            if (!foreignCost.Save(trx))
                            {
                                ValueNamePair pp = VLogger.RetrieveError();
                                _log.Severe("Error occured during updating M_Cost_ForeignCurrency. Error name : " + pp.GetName() +
                                    " AND Error Value : " + pp.GetValue() + " , For Invoice line : " + invoiceLine.GetVAB_InvoiceLine_ID() +
                                    " , AND vaf_client_ID : " + invoiceLine.GetVAF_Client_ID());
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Log(Level.SEVERE, "", ex);
                return false;
            }
            return true;
        }

        public static bool InsertForeignCostMatchOrder(Ctx ctx, MOrderLine orderLine, decimal matchQty, int ASI, Trx trx)
        {
            int acctSchema_ID = 0;
            int M_CostElement_ID = 0;
            int VAF_Org_ID = 0;
            int M_ASI_ID = 0;
            MProduct product = null;
            MAcctSchema acctSchema = null;
            MCostForeignCurrency foreignCost = null;
            dynamic pc = null;
            String cl = null;
            MOrder order = null;
            try
            {
                order = new MOrder(ctx, orderLine.GetC_Order_ID(), trx);

                if (!order.IsSOTrx() && !order.IsReturnTrx())
                {
                    acctSchema_ID = Util.GetValueOfInt(DB.ExecuteScalar(@"SELECT asch.VAB_AccountBook_id FROM VAB_AccountBook asch INNER JOIN VAF_ClientDetail ci
                                    ON ci.VAB_AccountBook1_id = asch.VAB_AccountBook_id WHERE ci.vaf_client_id  = " + order.GetVAF_Client_ID()));
                    acctSchema = new MAcctSchema(ctx, acctSchema_ID, trx);

                    if (acctSchema.GetVAB_Currency_ID() != order.GetVAB_Currency_ID())
                    {
                        // Get Costing Element of Av. PO
                        M_CostElement_ID = Util.GetValueOfInt(DB.ExecuteScalar(@"SELECT M_CostElement_ID FROM M_CostElement WHERE VAF_Client_ID = "
                                           + order.GetVAF_Client_ID() + " AND IsActive = 'Y' AND CostingMethod = 'A'"));

                        product = new MProduct(ctx, orderLine.GetM_Product_ID(), trx);

                        if (product != null && product.GetProductType() == "I" && product.GetM_Product_ID() > 0) // for Item Type product
                        {
                            pc = MProductCategory.Get(product.GetCtx(), product.GetM_Product_Category_ID());

                            // Get Costing Level
                            if (pc != null)
                            {
                                cl = pc.GetCostingLevel();
                            }
                            if (cl == null)
                                cl = acctSchema.GetCostingLevel();

                            if (cl == "C" || cl == "B")
                            {
                                VAF_Org_ID = 0;
                            }
                            else
                            {
                                VAF_Org_ID = order.GetVAF_Org_ID();
                            }
                            if (cl != "B")
                            {
                                M_ASI_ID = 0;
                            }
                            else
                            {
                                M_ASI_ID = ASI;
                            }

                            foreignCost = MCostForeignCurrency.Get(product, M_ASI_ID, VAF_Org_ID, M_CostElement_ID, order.GetVAB_BusinessPartner_ID(), order.GetVAB_Currency_ID());
                            foreignCost.SetC_Order_ID(order.GetC_Order_ID());
                            foreignCost.SetCumulatedQty(Decimal.Add(foreignCost.GetCumulatedQty(), matchQty));
                            foreignCost.SetCumulatedAmt(Decimal.Add(foreignCost.GetCumulatedAmt(), Decimal.Multiply(orderLine.GetPriceActual(), matchQty)));
                            if (foreignCost.GetCumulatedQty() != 0)
                            {
                                foreignCost.SetCostPerUnit(Decimal.Round(Decimal.Divide(foreignCost.GetCumulatedAmt(), foreignCost.GetCumulatedQty()), acctSchema.GetCostingPrecision()));
                            }
                            else
                            {
                                foreignCost.SetCostPerUnit(0);
                            }
                            if (!foreignCost.Save(trx))
                            {
                                ValueNamePair pp = VLogger.RetrieveError();
                                _log.Severe("Error occured during updating M_Cost_ForeignCurrency. Error name : " + pp.GetName() +
                                    " AND Error Value : " + pp.GetValue() + " , For Invoice line : " + orderLine.GetC_OrderLine_ID() +
                                    " , AND vaf_client_ID : " + orderLine.GetVAF_Client_ID());
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Log(Level.SEVERE, "", ex);
                return false;
            }
            return true;
        }
    }
}
