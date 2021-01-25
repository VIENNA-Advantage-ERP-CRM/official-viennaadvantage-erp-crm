namespace VAdvantage.Model
{

/** Generated Model - DO NOT CHANGE */
using System;
using System.Text;
using VAdvantage.DataBase;
using VAdvantage.Common;
using VAdvantage.Classes;
using VAdvantage.Process;
using VAdvantage.Model;
using VAdvantage.Utility;
using System.Data;
/** Generated Model for VAB_LCostDistribution
 *  @author Jagmohan Bhatt (generated) 
 *  @version Vienna Framework 1.1.1 - $Id$ */
public class X_VAB_LCostDistribution : PO
{
public X_VAB_LCostDistribution (Context ctx, int VAB_LCostDistribution_ID, Trx trxName) : base (ctx, VAB_LCostDistribution_ID, trxName)
{
/** if (VAB_LCostDistribution_ID == 0)
{
SetAmt (0.0);
SetBase (0.0);
SetVAB_InvoiceLine_ID (0);
SetVAB_LCostDistribution_ID (0);
SetM_CostElement_ID (0);
SetM_Product_ID (0);
SetQty (0.0);
}
 */
}
public X_VAB_LCostDistribution (Ctx ctx, int VAB_LCostDistribution_ID, Trx trxName) : base (ctx, VAB_LCostDistribution_ID, trxName)
{
/** if (VAB_LCostDistribution_ID == 0)
{
SetAmt (0.0);
SetBase (0.0);
SetVAB_InvoiceLine_ID (0);
SetVAB_LCostDistribution_ID (0);
SetM_CostElement_ID (0);
SetM_Product_ID (0);
SetQty (0.0);
}
 */
}
/** Load Constructor 
@param ctx context
@param rs result set 
@param trxName transaction
*/
public X_VAB_LCostDistribution (Context ctx, DataRow rs, Trx trxName) : base(ctx, rs, trxName)
{
}
/** Load Constructor 
@param ctx context
@param rs result set 
@param trxName transaction
*/
public X_VAB_LCostDistribution (Ctx ctx, DataRow rs, Trx trxName) : base(ctx, rs, trxName)
{
}
/** Load Constructor 
@param ctx context
@param rs result set 
@param trxName transaction
*/
public X_VAB_LCostDistribution (Ctx ctx, IDataReader dr, Trx trxName) : base(ctx, dr, trxName)
{
}
/** Static Constructor 
 Set Table ID By Table Name
 added by ->Harwinder */
static X_VAB_LCostDistribution()
{
 Table_ID = Get_Table_ID(Table_Name);
 model = new KeyNamePair(Table_ID,Table_Name);
}
/** Serial Version No */
//static long serialVersionUID 27562514372955L;
/** Last Updated Timestamp 7/29/2010 1:07:36 PM */
public static long updatedMS = 1280389056166L;
/** VAF_TableView_ID=760 */
public static int Table_ID;
 // =760;

/** TableName=VAB_LCostDistribution */
public static String Table_Name="VAB_LCostDistribution";

protected static KeyNamePair model;
protected Decimal accessLevel = new Decimal(1);
/** AccessLevel
@return 1 - Org 
*/
protected override int Get_AccessLevel()
{
return Convert.ToInt32(accessLevel.ToString());
}
/** Load Meta Data
@param ctx context
@return PO Info
*/
protected override POInfo InitPO (Ctx ctx)
{
POInfo poi = POInfo.GetPOInfo (ctx, Table_ID);
return poi;
}
/** Load Meta Data
@param ctx context
@return PO Info
*/
protected override POInfo InitPO(Context ctx)
{
POInfo poi = POInfo.GetPOInfo (ctx, Table_ID);
return poi;
}
/** Info
@return info
*/
public override String ToString()
{
StringBuilder sb = new StringBuilder ("X_VAB_LCostDistribution[").Append(Get_ID()).Append("]");
return sb.ToString();
}
/** Set Amount.
@param Amt Amount */
public void SetAmt (Decimal? Amt)
{
if (Amt == null) throw new ArgumentException ("Amt is mandatory.");
Set_Value ("Amt", (Decimal?)Amt);
}
/** Get Amount.
@return Amount */
public Decimal GetAmt() 
{
Object bd =Get_Value("Amt");
if (bd == null) return Env.ZERO;
return  Convert.ToDecimal(bd);
}
/** Set Base.
@param Base Calculation Base */
public void SetBase (Decimal? Base)
{
if (Base == null) throw new ArgumentException ("Base is mandatory.");
Set_Value ("Base", (Decimal?)Base);
}
/** Get Base.
@return Calculation Base */
public Decimal GetBase() 
{
Object bd =Get_Value("Base");
if (bd == null) return Env.ZERO;
return  Convert.ToDecimal(bd);
}
/** Set Invoice Line.
@param VAB_InvoiceLine_ID Invoice Detail Line */
public void SetVAB_InvoiceLine_ID (int VAB_InvoiceLine_ID)
{
if (VAB_InvoiceLine_ID < 1) throw new ArgumentException ("VAB_InvoiceLine_ID is mandatory.");
Set_ValueNoCheck ("VAB_InvoiceLine_ID", VAB_InvoiceLine_ID);
}
/** Get Invoice Line.
@return Invoice Detail Line */
public int GetVAB_InvoiceLine_ID() 
{
Object ii = Get_Value("VAB_InvoiceLine_ID");
if (ii == null) return 0;
return Convert.ToInt32(ii);
}
/** Get Record ID/ColumnName
@return ID/ColumnName pair */
public KeyNamePair GetKeyNamePair() 
{
return new KeyNamePair(Get_ID(), GetVAB_InvoiceLine_ID().ToString());
}
/** Set Landed Cost Allocation.
@param VAB_LCostDistribution_ID Allocation for Land Costs */
public void SetVAB_LCostDistribution_ID (int VAB_LCostDistribution_ID)
{
if (VAB_LCostDistribution_ID < 1) throw new ArgumentException ("VAB_LCostDistribution_ID is mandatory.");
Set_ValueNoCheck ("VAB_LCostDistribution_ID", VAB_LCostDistribution_ID);
}
/** Get Landed Cost Allocation.
@return Allocation for Land Costs */
public int GetVAB_LCostDistribution_ID() 
{
Object ii = Get_Value("VAB_LCostDistribution_ID");
if (ii == null) return 0;
return Convert.ToInt32(ii);
}
/** Set Attribute Set Instance.
@param M_AttributeSetInstance_ID Product Attribute Set Instance */
public void SetM_AttributeSetInstance_ID (int M_AttributeSetInstance_ID)
{
if (M_AttributeSetInstance_ID <= 0) Set_ValueNoCheck ("M_AttributeSetInstance_ID", null);
else
Set_ValueNoCheck ("M_AttributeSetInstance_ID", M_AttributeSetInstance_ID);
}
/** Get Attribute Set Instance.
@return Product Attribute Set Instance */
public int GetM_AttributeSetInstance_ID() 
{
Object ii = Get_Value("M_AttributeSetInstance_ID");
if (ii == null) return 0;
return Convert.ToInt32(ii);
}
/** Set Cost Element.
@param M_CostElement_ID Product Cost Element */
public void SetM_CostElement_ID (int M_CostElement_ID)
{
if (M_CostElement_ID < 1) throw new ArgumentException ("M_CostElement_ID is mandatory.");
Set_Value ("M_CostElement_ID", M_CostElement_ID);
}
/** Get Cost Element.
@return Product Cost Element */
public int GetM_CostElement_ID() 
{
Object ii = Get_Value("M_CostElement_ID");
if (ii == null) return 0;
return Convert.ToInt32(ii);
}
/** Set Product.
@param M_Product_ID Product, Service, Item */
public void SetM_Product_ID (int M_Product_ID)
{
if (M_Product_ID < 1) throw new ArgumentException ("M_Product_ID is mandatory.");
Set_ValueNoCheck ("M_Product_ID", M_Product_ID);
}
/** Get Product.
@return Product, Service, Item */
public int GetM_Product_ID() 
{
Object ii = Get_Value("M_Product_ID");
if (ii == null) return 0;
return Convert.ToInt32(ii);
}
/** Set Quantity.
@param Qty Quantity */
public void SetQty (Decimal? Qty)
{
if (Qty == null) throw new ArgumentException ("Qty is mandatory.");
Set_Value ("Qty", (Decimal?)Qty);
}
/** Get Quantity.
@return Quantity */
public Decimal GetQty() 
{
Object bd =Get_Value("Qty");
if (bd == null) return Env.ZERO;
return  Convert.ToDecimal(bd);
}
/** Set Landed Cost.
@param VAB_LCost_ID Landed cost to be allocated to material receipts */
public void SetVAB_LCost_ID(int VAB_LCost_ID)
{
    if (VAB_LCost_ID <= 0) 
        Set_Value("VAB_LCost_ID", null);
    else
        Set_Value("VAB_LCost_ID", VAB_LCost_ID);
}/** Get Landed Cost.
@return Landed cost to be allocated to material receipts */
public int GetVAB_LCost_ID() 
{ 
    Object ii = Get_Value("VAB_LCost_ID"); 
    if (ii == null) 
        return 0; 
    return Convert.ToInt32(ii); 
}
/** Set Warehouse.
@param M_Warehouse_ID Storage Warehouse and Service Point */
public void SetM_Warehouse_ID(int M_Warehouse_ID)
{
    if (M_Warehouse_ID <= 0) Set_Value("M_Warehouse_ID", null);
    else
        Set_Value("M_Warehouse_ID", M_Warehouse_ID);
}/** Get Warehouse.
@return Storage Warehouse and Service Point */
public int GetM_Warehouse_ID() 
{ Object ii = Get_Value("M_Warehouse_ID"); 
    if (ii == null) 
        return 0; 
    return Convert.ToInt32(ii); 
}

        /// <summary>
        /// Set Difference.
        /// </summary>
        /// <param name="DifferenceAmt">Difference Amount</param>
        public void SetDifferenceAmt(Decimal? DifferenceAmt) { Set_Value("DifferenceAmt", (Decimal?)DifferenceAmt); }
        /// <summary>
        /// Get Difference.
        /// </summary>
        /// <returns>Difference Amount</returns>
        public Decimal GetDifferenceAmt() { Object bd = Get_Value("DifferenceAmt"); if (bd == null) return Env.ZERO; return Convert.ToDecimal(bd); }


        /// <summary>
        ///  Set Shipment/Receipt Line.
        /// </summary>
        /// <param name="M_InOutLine_ID">Line on Shipment or Receipt document</param>
        public void SetM_InOutLine_ID(int M_InOutLine_ID)
        {
            if (M_InOutLine_ID <= 0) Set_Value("M_InOutLine_ID", null);
            else
                Set_Value("M_InOutLine_ID", M_InOutLine_ID);
        }
        /// <summary>
        /// Get Shipment/Receipt Line.
        /// </summary>
        /// <returns>Line on Shipment or Receipt document</returns>
        public int GetM_InOutLine_ID() { Object ii = Get_Value("M_InOutLine_ID"); if (ii == null) return 0; return Convert.ToInt32(ii); }

        /// <summary>
        /// Set Expected Cost Calculated.
        /// </summary>
        /// <param name="IsExpectedCostCalculated">Expected Cost Calculated</param>
        public void SetIsExpectedCostCalculated(Boolean IsExpectedCostCalculated) { Set_Value("IsExpectedCostCalculated", IsExpectedCostCalculated); }
        /// <summary>
        ///  Get Expected Cost Calculated.
        /// </summary>
        /// <returns>Expected Cost Calculated </returns>
        public Boolean IsExpectedCostCalculated() { Object oo = Get_Value("IsExpectedCostCalculated"); if (oo != null) { if (oo.GetType() == typeof(bool)) return Convert.ToBoolean(oo); return "Y".Equals(oo); } return false; }

    }

}
