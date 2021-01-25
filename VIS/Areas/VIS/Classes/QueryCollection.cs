﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using VAdvantage.Model;
using VAdvantage.Utility;

namespace VIS.Classes
{
    public class QueryCollection
    {
        static dynamic queryList = new ExpandoObject();
        static bool isLoad = false;

        private static void AddQueries(Ctx ctx)
        {
            queryList.VIS_1 = "SELECT dc.DocSubTypeSO FROM VAB_DocTypes dc INNER JOIN VAB_MasterDocType db on (dc.DocBaseType = db.DocBaseType)"
                            + " WHERE VAB_DocTypes_ID = @Param AND db.DocBaseType = 'SOO' AND dc.DocSubTypeSO IN ('WR','WI')";

            queryList.VIS_2 = "SELECT PayAmt FROM C_Payment_v WHERE C_Payment_ID=@C_Payment_ID";

            queryList.VIS_3 = "SELECT COUNT(*) FROM M_ProductPrice WHERE Isactive='Y' AND M_Product_ID =@param1 "
                                         + " AND M_PriceList_Version_ID =@param2 "
                                         + " AND  M_AttributeSetInstance_ID =@param3 "
                                         + "  AND C_UOM_ID=@param 4";

            queryList.VIS_4 = "SELECT COUNT(*) FROM M_ProductPrice WHERE Isactive='Y' AND M_Product_ID = @param1 "
                                       + " AND M_PriceList_Version_ID = @param2 "
                                       + " AND  ( M_AttributeSetInstance_ID = 0 OR M_AttributeSetInstance_ID IS NULL ) "
                                       + "  AND C_UOM_ID= @param3";

            queryList.VIS_5 = "SELECT PriceStd , PriceList FROM M_ProductPrice WHERE Isactive='Y' AND M_Product_ID = @param1 "
                                        + " AND M_PriceList_Version_ID = @param2 "
                                        + " AND  M_AttributeSetInstance_ID = @param3 "
                                        + "  AND C_UOM_ID= @param4 ";

            queryList.VIS_6 = "SELECT PriceStd , PriceList FROM M_ProductPrice WHERE Isactive='Y' AND M_Product_ID =@param1 "
                                      + " AND M_PriceList_Version_ID =@param2 "
                                      + " AND  ( M_AttributeSetInstance_ID = 0 OR M_AttributeSetInstance_ID IS NULL ) "
                                      + "  AND C_UOM_ID= @param3 ";

            queryList.VIS_7 = "SELECT PriceStd , PriceList FROM M_ProductPrice WHERE Isactive='Y' AND M_Product_ID =@param1 "
                                  + " AND M_PriceList_Version_ID = @param2 "
                                  + " AND  ( M_AttributeSetInstance_ID = 0 OR M_AttributeSetInstance_ID IS NULL ) "
                                  + "  AND C_UOM_ID= @param3 ";

            queryList.VIS_8 = "SELECT PriceStd , PriceList FROM M_ProductPrice WHERE Isactive='Y' AND M_Product_ID = @param1 "
                                        + " AND M_PriceList_Version_ID = @param2 "
                                        + " AND  M_AttributeSetInstance_ID = @param3 "
                                        + "  AND C_UOM_ID= @param4 ";

            queryList.VIS_9 = "SELECT PriceStd , PriceList FROM M_ProductPrice WHERE Isactive='Y' AND M_Product_ID =  @param1 "
                                      + " AND M_PriceList_Version_ID = @param2 "
                                      + " AND  ( M_AttributeSetInstance_ID = 0 OR M_AttributeSetInstance_ID IS NULL ) "
                                      + "  AND C_UOM_ID= @param3 ";
            queryList.VIS_10 = "SELECT con.DivideRate FROM C_UOM_Conversion con INNER JOIN C_UOM uom ON con.C_UOM_ID = uom.C_UOM_ID WHERE con.IsActive = 'Y' "
                                      + " AND con.M_Product_ID = @param1  AND con.C_UOM_ID = @param2  AND con.C_UOM_To_ID = @param3 ";

            queryList.VIS_11 = "SELECT con.DivideRate FROM C_UOM_Conversion con INNER JOIN C_UOM uom ON con.C_UOM_ID = uom.C_UOM_ID WHERE con.IsActive = 'Y'" +
                                  " AND con.C_UOM_ID = @param1  AND con.C_UOM_To_ID = @param2 ";

            queryList.VIS_12 = "SELECT COUNT(*) FROM M_ProductPrice WHERE Isactive='Y' AND M_Product_ID = @param1 "
                                             + " AND M_PriceList_Version_ID = @param2 "
                                             + " AND  M_AttributeSetInstance_ID = @param3 "
                                             + "  AND C_UOM_ID= @param4 ";

            queryList.VIS_13 = "SELECT COUNT(*) FROM M_ProductPrice WHERE Isactive='Y' AND M_Product_ID = @param1 "
                                           + " AND M_PriceList_Version_ID =  @param2 "
                                           + " AND  ( M_AttributeSetInstance_ID = 0 OR M_AttributeSetInstance_ID IS NULL ) "
                                           + "  AND C_UOM_ID= @param3 ";

            queryList.VIS_14 = "SELECT PriceStd , PriceList FROM M_ProductPrice WHERE Isactive='Y' AND M_Product_ID = @param1 "
                                            + " AND M_PriceList_Version_ID =  @param2 "
                                            + " AND  M_AttributeSetInstance_ID = @param3 "
                                            + "  AND C_UOM_ID= @param4 ";

            queryList.VIS_15 = "SELECT PriceStd , PriceList FROM M_ProductPrice WHERE Isactive='Y' AND M_Product_ID = @param1 "
                                          + " AND M_PriceList_Version_ID = @param2 "
                                          + " AND  ( M_AttributeSetInstance_ID = 0 OR M_AttributeSetInstance_ID IS NULL ) "
                                          + "  AND C_UOM_ID= @param3 ";
            queryList.VIS_16 = "SELECT PriceStd , PriceList FROM M_ProductPrice WHERE Isactive='Y' AND M_Product_ID = @param1 "
                                      + " AND M_PriceList_Version_ID = @param2 "
                                      + " AND  ( M_AttributeSetInstance_ID = 0 OR M_AttributeSetInstance_ID IS NULL ) "
                                      + "  AND C_UOM_ID= @param3 ";
            queryList.VIS_17 = "SELECT PriceStd , PriceList FROM M_ProductPrice WHERE Isactive='Y' AND M_Product_ID = @param1 "
                                            + " AND M_PriceList_Version_ID =  @param2 "
                                            + " AND  M_AttributeSetInstance_ID = @param3 "
                                            + "  AND C_UOM_ID= @param4 ";
            queryList.VIS_18 = "SELECT PriceStd , PriceList FROM M_ProductPrice WHERE Isactive='Y' AND M_Product_ID = @param1 "
                                          + " AND M_PriceList_Version_ID = @param2 "
                                          + " AND  ( M_AttributeSetInstance_ID = 0 OR M_AttributeSetInstance_ID IS NULL ) "
                                          + "  AND C_UOM_ID= @param3 ";
            queryList.VIS_19 = "SELECT con.DivideRate FROM C_UOM_Conversion con INNER JOIN C_UOM uom ON con.C_UOM_ID = uom.C_UOM_ID WHERE con.IsActive = 'Y' "
                                          + " AND con.M_Product_ID = @param1  AND con.C_UOM_ID =  @param2 AND con.C_UOM_To_ID = @param3 ";

            queryList.VIS_20 = "SELECT con.DivideRate FROM C_UOM_Conversion con INNER JOIN C_UOM uom ON con.C_UOM_ID = uom.C_UOM_ID WHERE con.IsActive = 'Y'" +
                                      " AND con.C_UOM_ID = @param1 AND con.C_UOM_To_ID = @param2 ";

            queryList.VIS_21 = "SELECT EnforcePriceLimit FROM M_PriceList WHERE IsActive = 'Y' AND M_PriceList_ID = @param1 ";

            queryList.VIS_22 = "SELECT OverwritePriceLimit FROM VAF_Role WHERE IsActive = 'Y' AND VAF_Role_ID = @param1 ";

            queryList.VIS_23 = "SELECT PriceList , PriceStd , PriceLimit FROM M_ProductPrice WHERE Isactive='Y' AND M_Product_ID = @param1 "
                                        + " AND M_PriceList_Version_ID = @param2 "
                                        + " AND  ( M_AttributeSetInstance_ID = 0 OR M_AttributeSetInstance_ID IS NULL ) "
                                        + "  AND C_UOM_ID= @param3 ";

            queryList.VIS_24 = "SELECT PriceList FROM M_ProductPrice WHERE Isactive='Y' AND M_Product_ID = @param1 "
                                       + " AND M_PriceList_Version_ID = @param2 "
                                       + " AND ( M_AttributeSetInstance_ID = 0 OR M_AttributeSetInstance_ID IS NULL ) AND C_UOM_ID= @param3 ";

            queryList.VIS_25 = "SELECT PriceStd FROM M_ProductPrice WHERE Isactive='Y' AND M_Product_ID = @param1 "
                                     + " AND M_PriceList_Version_ID = @param2 "
                                     + " AND ( M_AttributeSetInstance_ID = 0 OR M_AttributeSetInstance_ID IS NULL ) AND C_UOM_ID= @param3 ";
            queryList.VIS_26 = "SELECT con.DivideRate FROM C_UOM_Conversion con INNER JOIN C_UOM uom ON con.C_UOM_ID = uom.C_UOM_ID WHERE con.IsActive = 'Y' "
                                     + " AND con.M_Product_ID = @param1 "
                           + " AND con.C_UOM_ID = @param2  AND con.C_UOM_To_ID = @param3 ";

            queryList.VIS_27 = "SELECT con.DivideRate FROM C_UOM_Conversion con INNER JOIN C_UOM uom ON con.C_UOM_ID = uom.C_UOM_ID WHERE con.IsActive = 'Y'" +
                                      " AND con.C_UOM_ID = @param1  AND con.C_UOM_To_ID = @param2 ";

            queryList.VIS_28 = "SELECT C_UOM_ID FROM M_Product_PO WHERE IsActive = 'Y' AND  VAB_BusinessPartner_ID = @param1 "
                                     + " AND M_Product_ID = @param2 ";

            queryList.VIS_29 = "SELECT C_UOM_ID FROM M_Product WHERE IsActive = 'Y' AND M_Product_ID = @param1 ";

            queryList.VIS_30 = "Select NoOfMonths from VAB_Frequency where VAB_Frequency_ID=@param1 ";

            queryList.VIS_31 = "select rate from c_tax WHERE c_tax_id= @param1 ";

            queryList.VIS_32 = "SELECT C_OrderLine_ID FROM C_OrderLine WHERE C_Order_ID = (SELECT C_Order_ID FROM C_Order "
                             + "WHERE DocumentNo = (SELECT DocumentNo FROM M_Requisition WHERE M_Requisition.M_Requisition_id = @Param1) "
                             + "AND VAF_Client_ID = @Param2) AND M_Product_ID = @Param3";

            queryList.VIS_33 = "SELECT VAB_Currency_ID FROM M_PriceList where M_PriceList_ID = @Param";

            queryList.VIS_34 = "SELECT COALESCE(MAX(VAB_BatchInvoiceLine_ID),0) FROM VAB_BatchInvoiceLine WHERE VAB_BatchInvoice_ID = @Param";

            queryList.VIS_35 = "SELECT p.M_Product_ID, ra.Name, ra.Description, ra.Qty FROM S_ResourceAssignment ra"
                             + " INNER JOIN M_Product p ON (p.S_Resource_ID=ra.S_Resource_ID) WHERE ra.S_ResourceAssignment_ID = @Param";
            queryList.VIS_36 = "SELECT bomPriceStd(p.M_Product_ID,pv.M_PriceList_Version_ID) AS PriceStd,"
                             + "bomPriceList(p.M_Product_ID,pv.M_PriceList_Version_ID) AS PriceList,"
                             + "bomPriceLimit(p.M_Product_ID,pv.M_PriceList_Version_ID) AS PriceLimit,"
                             + "p.C_UOM_ID,pv.ValidFrom,pl.VAB_Currency_ID "
                             + "FROM M_Product p, M_ProductPrice pp, M_PriceList pl, M_PriceList_Version pv "
                             + "WHERE p.M_Product_ID=pp.M_Product_ID"
                             + " AND pp.M_PriceList_Version_ID=pv.M_PriceList_Version_ID"
                             + " AND pv.M_PriceList_ID=pl.M_PriceList_ID"
                             + " AND pv.IsActive='Y' AND p.M_Product_ID=@param1"
                             + " AND pl.M_PriceList_ID=@param2 ORDER BY pv.ValidFrom DESC";

            queryList.VIS_37 = "SELECT bomPriceStd(p.M_Product_ID,pv.M_PriceList_Version_ID) AS PriceStd,"
                             + "bomPriceList(p.M_Product_ID,pv.M_PriceList_Version_ID) AS PriceList,"
                             + "bomPriceLimit(p.M_Product_ID,pv.M_PriceList_Version_ID) AS PriceLimit,"
                             + "p.C_UOM_ID,pv.ValidFrom,pl.VAB_Currency_ID "
                             + "FROM M_Product p, M_ProductPrice pp, M_PriceList pl, M_PriceList bpl, M_PriceList_Version pv "
                             + "WHERE p.M_Product_ID=pp.M_Product_ID"
                             + " AND pp.M_PriceList_Version_ID = pv.M_PriceList_Version_ID"
                             + " AND pv.M_PriceList_ID = bpl.M_PriceList_ID"
                             + " AND pv.IsActive = 'Y' AND bpl.M_PriceList_ID = pl.BasePriceList_ID"	//	Base
                             + " AND p.M_Product_ID = @param1 AND pl.M_PriceList_ID = @param2"	//	2
                             + " ORDER BY pv.ValidFrom DESC";

            queryList.VIS_38 = "SELECT C_Period_ID FROM C_Period WHERE C_Year_ID IN "
                             + " (SELECT C_Year_ID FROM C_Year WHERE VAB_Calender_ID = "
                             + " (SELECT VAB_Calender_ID FROM VAF_ClientDetail WHERE VAF_Client_ID=@param1))"
                             + " AND @param2 BETWEEN StartDate AND EndDate AND PeriodType='S'";

            queryList.VIS_39 = "SELECT PeriodType, StartDate, EndDate FROM C_Period WHERE C_Period_ID=@param";

            queryList.VIS_40 = "SELECT ProfitBeforeTax,C_Year_ID,C_ProfitAndLoss_ID FROM C_ProfitLoss WHERE C_ProfitLoss_ID=@Param";

            queryList.VIS_41 = "Select M_PriceList_Version_ID from M_ProductPrice where M_Product_id=@param1"
                             + " and M_PriceList_Version_ID in (select m_pricelist_version_id from m_pricelist_version"
                             + " where m_pricelist_id = @param2 and isactive='Y')";

            queryList.VIS_42 = "Select PriceStd from M_ProductPrice where M_PriceList_Version_ID=@param1 and M_Product_id=@param2";

            queryList.VIS_43 = "SELECT loc.M_Warehouse_ID FROM M_Product p INNER JOIN M_Locator loc ON p.M_Locator_ID= loc.M_Locator_ID WHERE p.M_Product_ID=@Param";

            queryList.VIS_44 = "SELECT Count(*) FROM M_Manufacturer WHERE IsActive = 'Y' AND UPC = '@Param'";

            queryList.VIS_45 = "UPDATE RC_ViewColumn SET IsGroupBy='N' WHERE RC_View_ID=@Param1 AND RC_ViewColumn_ID NOT IN(@Param2)";

            queryList.VIS_46 = "SELECT VAF_TableView_ID FROM VAF_TableView WHERE IsActive='Y' AND TableName= 'VADMS_MetaData'";

            queryList.VIS_47 = "SELECT ColumnName FROM VAF_Column WHERE VAF_Column_ID=@Param";

            queryList.VIS_48 = "SELECT DISTINCT M_Product_Category_ID FROM M_Product WHERE IsActive='Y' AND M_Product_ID=@Param";

            queryList.VIS_49 = "SELECT  DiscountType FROM M_DiscountSchema WHERE M_DiscountSchema_ID=@Param1 AND IsActive='Y' AND VAF_Client_ID=@Param2";

            queryList.VIS_50 = "SELECT M_Product_Category_ID, M_Product_ID, BreakValue, IsBPartnerFlatDiscount, BreakDiscount FROM M_DiscountSchemaBreak WHERE "
                             + "M_DiscountSchema_ID = @Param1 AND M_Product_ID = @Param2 AND IsActive='Y' AND VAF_Client_ID= @Param3 Order BY BreakValue DESC";

            queryList.VIS_51 = "SELECT M_Product_Category_ID, M_Product_ID, BreakValue, IsBPartnerFlatDiscount, BreakDiscount FROM M_DiscountSchemaBreak WHERE "
                             + " M_DiscountSchema_ID = @Param1 AND M_Product_Category_ID = @Param2 AND IsActive='Y' AND VAF_Client_ID = @Param3 Order BY BreakValue DESC";

            queryList.VIS_52 = "SELECT M_Product_Category_ID, M_Product_ID, BreakValue, IsBPartnerFlatDiscount, BreakDiscount FROM M_DiscountSchemaBreak WHERE "
                             + " M_DiscountSchema_ID = @Param1 AND M_Product_Category_ID IS NULL AND M_Product_ID IS NULL AND IsActive='Y' AND VAF_Client_ID = @Param2 Order BY BreakValue DESC";

            queryList.VIS_53 = "SELECT VATAX_TaxRule FROM VAF_OrgDetail WHERE VAF_Org_ID = @Param1 AND IsActive ='Y' AND VAF_Client_ID = @Param2";

            queryList.VIS_54 = "SELECT Count(*) FROM VAF_Column WHERE ColumnName = 'C_Tax_ID' AND VAF_TableView_ID = (SELECT VAF_TableView_ID FROM VAF_TableView WHERE TableName = 'C_TaxCategory')";

            queryList.VIS_55 = "SELECT VATAX_TaxType_ID FROM VAB_BPart_Location WHERE VAB_BusinessPartner_ID = @Param1 AND IsActive = 'Y' AND VAB_BPart_Location_ID = @Param2";

            queryList.VIS_56 = "SELECT VATAX_TaxType_ID FROM VAB_BusinessPartner WHERE VAB_BusinessPartner_ID = @Param1 AND IsActive = 'Y'";

            queryList.VIS_57 = "SELECT C_Tax_ID FROM VATAX_TaxCatRate WHERE C_TaxCategory_ID = @Param1 AND IsActive ='Y' and VATAX_TaxType_ID =@Param2";

            queryList.VIS_58 = "SELECT IsTaxIncluded FROM M_PriceList WHERE M_PriceList_ID=@Param";

            queryList.VIS_59 = "select FO_RES_ACCOMODATION_ID,DATE_FROM,TILL_DATE from FO_RES_ACCOMODATION where FO_OBJECT_DATA_ID=@FO_OBJ_DATA_ID";

            queryList.VIS_60 = "SELECT DAYSPAYABLE2 FROM FO_SETTINGS WHERE VAF_ORG_ID=@Param";

            queryList.VIS_61 = "SELECT max(OFFERNO)FROM FO_OFFER";

            queryList.VIS_62 = "SELECT FO_PRICE_LIST_ID FROM FO_ADDRESS_PRICE WHERE FO_ADDRESS_ID=@Param";

            queryList.VIS_63 = "SELECT NoOfDays FROM VAB_Frequency WHERE VAB_Frequency_ID=@Param";

            queryList.VIS_64 = "SELECT ProfileType FROM S_Resource WHERE VAF_UserContact_ID=@Param";

            queryList.VIS_65 = "SELECT M_Product_ID, MovementQty, M_AttributeSetInstance_ID FROM M_InOutLine WHERE M_InOutLine_ID=@Param";

            queryList.VIS_66 = "SELECT SUM(ConfirmedQty + ScrappedQty) FROM M_PackageLine WHERE M_Package_ID=@Param1 AND M_InOutLine_ID=@Param2";

            queryList.VIS_67 = "SELECT EndingBalance FROM VAB_CashJRNL WHERE VAB_CashBook_ID=@Param1 AND VAF_Client_ID=@Param2 AND VAF_Org_ID=@Param3 AND " +
                            "VAB_CashJRNL_ID IN (SELECT Max(VAB_CashJRNL_ID) FROM VAB_CashJRNL WHERE VAB_CashBook_ID=@Param4 AND VAF_Client_ID=@Param5 AND VAF_Org_ID=@Param6) AND Processed='Y'";

            queryList.VIS_68 = "SELET Count(*) FROM M_Transaction WHERE M_Product_ID = @Param";

            queryList.VIS_69 = "SELET uom.FO_HOTEL_UOM_ID,s.DESCRIPTION FROM FO_HOTEL_UOM uom INNER JOIN FO_SERVICE s ON(uom.FO_HOTEL_UOM_ID = s.FO_HOTEL_UOM_ID) "
                             + "WHERE s.FO_SERVICE_ID=@FO_SERVICE_ID ";

            queryList.VIS_70 = "SELET PRICE,CHILDGROUP1,CHILDGROUP2,UOM1,UOM2 FROM FO_SERVICE_PRICE_PRICELINE WHERE CREATED=(SELECT max(CREATED) FROM FO_SERVICE_PRICE_PRICELINE "
                             + "WHERE FO_SERVICE_ID=@FO_SERVICE_ID)";

            queryList.VIS_71 = "SELECT SUM(t.CurrentQty) keep (dense_rank last ORDER BY t.MovementDate, t.M_Transaction_ID) AS CurrentQty FROM m_transaction t" +
                            " INNER JOIN M_Locator l ON t.M_Locator_ID = l.M_Locator_ID WHERE t.MovementDate <= @Param1 AND l.VAF_Org_ID = @Param2" +
                            " AND t.M_Locator_ID = @Param3 AND t.M_Product_ID = @Param4 AND NVL(t.M_AttributeSetInstance_ID,0) = @Param5";


            queryList.VIS_72 = "SELECT COUNT(*) FROM VAF_TableView WHERE TableName='R_Request'";

            queryList.VIS_73 = "SELECT Name, PO_Name FROM VAF_ColumnDic WHERE UPPER(ColumnName)=@ColumnName";
            queryList.VIS_74 = "SELECT t.Name, t.PO_Name FROM VAF_ColumnDic_TL t, VAF_ColumnDic e "
                + "WHERE t.VAF_ColumnDic_ID=e.VAF_ColumnDic_ID AND UPPER(e.ColumnName)=@ColumnName "
                + "AND t.VAF_Language=@VAF_Language";

            queryList.VIS_75 = "SELECT TableName FROM VAF_TableView WHERE VAF_TableView_ID=@tableID";

            queryList.VIS_76 = "UPDATE VAF_Print_Rpt_Layout SET IsDefault='N' WHERE IsDefault='Y' AND VAF_TableView_ID=@tableID AND VAF_Tab_ID=@tabID";

            queryList.VIS_77 = "UPDATE VAF_Print_Rpt_Layout SET IsDefault='Y' WHERE VAF_Print_Rpt_Layout_ID=@printForamt";

            queryList.VIS_78 = "SELECT DISTINCT VAF_Screen_ID, PO_Window_ID FROM VAF_TableView t WHERE TableName = @targetTableName";

            queryList.VIS_79 = " SELECT p.IsSOTrx FROM @ParentTable p, @targetTableName c  WHERE @targetWhereClause AND p.@ParentTable1_ID = c.@ParentTable2_ID";


            queryList.VIS_80 = "SELECT VAF_Screen_ID, Name FROM VAF_Screen WHERE Name LIKE 'Work Center%' OR NAME LIKE 'Production Resource'";

            queryList.VIS_81 = "SELECT ISREPORT FROM VAF_Job WHERE VAF_Job_ID=@VAF_Job_ID";


            queryList.VIS_82 = "SELECT Value, Name FROM VAF_CtrlRef_List WHERE VAF_Control_Ref_ID=@VAF_Control_Ref_ID AND IsActive='Y'";


            queryList.VIS_83 = "SELECT kc.ColumnName"
                            + " FROM VAF_CtrlRef_Table rt"
                            + " INNER JOIN VAF_Column kc ON (rt.Column_Key_ID=kc.VAF_Column_ID)"
                            + "WHERE rt.VAF_Control_Ref_ID=@VAF_Control_Ref_ID";

            queryList.VIS_84 = "SELECT Columnname, tbl.TableName FROM VAF_Column clm INNER JOIN VAF_TableView tbl ON (tbl.VAF_TableView_ID = clm.VAF_TableView_ID) WHERE VAF_Column_ID = "
                      + " (SELECT Column_Key_ID FROM VAF_CtrlRef_Table WHERE VAF_Control_Ref_ID = @refid)";

            queryList.VIS_85 = "SELECT  tbl.tablename, clm.Columnname FROM ( "
                        + " SELECT kc.ColumnName, dc.ColumnName as ColName1, t.TableName,  t.VAF_TableView_ID "
                        + " FROM VAF_CtrlRef_Table rt"
                        + " INNER JOIN VAF_Column kc ON (rt.Column_Key_ID=kc.VAF_Column_ID)"
                        + " INNER JOIN VAF_Column dc ON (rt.Column_Display_ID=dc.VAF_Column_ID)"
                        + " INNER JOIN VAF_TableView t ON (rt.VAF_TableView_ID=t.VAF_TableView_ID) "
                        + "WHERE rt.VAF_Control_Ref_ID=@refid ) rr "
                    + " INNER JOIN VAF_TableView tbl "
                    + " ON (tbl.VAF_TableView_ID = rr.VAF_TableView_ID) "
                    + " INNER JOIN VAF_Column clm "
                    + " ON (clm.VAF_TableView_ID      = tbl.VAF_TableView_ID) "
                    + " WHERE (clm.ColumnName   IN ('DocumentNo', 'Value', 'Name') "
                    + " OR clm.IsIdentifier      ='Y') "
                    + " AND clm.VAF_Control_Ref_ID IN (10,14) "
                    + " AND EXISTS "
                      + " (SELECT *  FROM VAF_Column cc WHERE cc.VAF_TableView_ID=tbl.VAF_TableView_ID  AND cc.IsKey ='Y' AND cc.ColumnName = @colname)";


            queryList.VIS_86 = "SELECT t.TableName, c.ColumnName "
                + "FROM VAF_Column c "
                + " INNER JOIN VAF_TableView t ON (c.VAF_TableView_ID=t.VAF_TableView_ID AND t.IsView='N') "
                + "WHERE (c.ColumnName IN ('DocumentNo', 'Value', 'Name') OR c.IsIdentifier='Y')"
                + " AND c.VAF_Control_Ref_ID IN (10,14)"
                + " AND EXISTS (SELECT * FROM VAF_Column cc WHERE cc.VAF_TableView_ID=t.VAF_TableView_ID"
                    + " AND cc.IsKey='Y' AND cc.ColumnName=@colname)";

            queryList.VIS_87 = "SELECT kc.ColumnName, dc.ColumnName, t.TableName "
                        + "FROM VAF_CtrlRef_Table rt"
                        + " INNER JOIN VAF_Column kc ON (rt.Column_Key_ID=kc.VAF_Column_ID)"
                        + " INNER JOIN VAF_Column dc ON (rt.Column_Display_ID=dc.VAF_Column_ID)"
                        + " INNER JOIN VAF_TableView t ON (rt.VAF_TableView_ID=t.VAF_TableView_ID) "
                        + "WHERE rt.VAF_Control_Ref_ID=@VAF_Control_Ref_ID";
            queryList.VIS_88 = "SELECT VAF_Screen_ID FROM VAF_Tab WHERE VAF_Tab_ID =@VAF_Tab_ID";

            queryList.VIS_89 = "SELECT VAF_QuickSearchWindow_ID FROM VAF_QuickSearchWindow WHERE VAF_TableView_ID = (SELECT VAF_TableView_ID FROM VAF_TableView WHERE TableName=@tableName) AND IsActive='Y'"
                    + " ORDER BY ISCUSTOMDEFAULT DESC , VAF_QuickSearchWindow_ID ASC ";


            queryList.VIS_90 = "SELECT kc.ColumnName"
                            + " FROM VAF_CtrlRef_Table rt"
                            + " INNER JOIN VAF_Column kc ON (rt.Column_Key_ID=kc.VAF_Column_ID)"
                            + "WHERE rt.VAF_Control_Ref_ID=@VAF_Control_Ref_ID";
            queryList.VIS_91 = "SELECT kc.ColumnName"
                            + " FROM VAF_CtrlRef_Table rt"
                            + " INNER JOIN VAF_Column kc ON (rt.Column_Key_ID=kc.VAF_Column_ID)"
                            + "WHERE rt.VAF_Control_Ref_ID=@VAF_Control_Ref_ID";

            queryList.VIS_92 = "SELECT Value FROM M_Locator WHERE IsActive='Y' and M_Locator_ID=@keyValue";

            queryList.VIS_93 = "SELECT ColumnName FROM VAF_Column WHERE VAF_TableView_ID = 207 AND IsIdentifier  = 'Y' AND SeqNo  IS NOT NULL ORDER BY SeqNo";

            queryList.VIS_94 = "SELECT C_ValidCombination_ID, Combination, Description FROM C_ValidCombination WHERE C_ValidCombination_ID=@ID";

            queryList.VIS_95 = "SELECT Description FROM M_AttributeSetInstance WHERE M_AttributeSetInstance_ID=@M_AttributeSetInstance_ID";

            queryList.VIS_96 = "SELECT Description FROM VAB_GenFeatureSetInstance WHERE VAB_GenFeatureSetInstance_ID=@C_GenttributeSetInstance_ID";

            queryList.VIS_97 = "SELECT c.ColumnName, c.VAF_Control_Ref_Value_ID, c.IsParent, vr.Code FROM VAF_Column c LEFT OUTER JOIN VAF_DataVal_Rule vr ON (c.VAF_DataVal_Rule_ID=vr.VAF_DataVal_Rule_ID) WHERE c.VAF_Column_ID=@Column_ID";

            queryList.VIS_98 = "SELECT c.ColumnName,c.IsTranslated,c.VAF_Control_Ref_ID,c.VAF_Control_Ref_Value_ID "
              + "FROM VAF_TableView t INNER JOIN VAF_Column c ON (t.VAF_TableView_ID=c.VAF_TableView_ID) "
              + "WHERE TableName=@tableName"
              + " AND c.IsIdentifier='Y' "
              + "ORDER BY c.SeqNo";

            queryList.VIS_99 = "SELECT Amount "
                    + "FROM VAB_DimAmt "
                    + "WHERE VAB_DimAmt_ID=@VAB_DimAmt_ID";

            queryList.VIS_100 = "SELECT VAF_Job_ID,name,CLASSNAME,ENTITYTYPE FROM VAF_Job WHERE value=@processName AND ISACTIVE='Y'";

            queryList.VIS_101 = "SELECT count(*) FROM VAF_TableView t "
        + "INNER JOIN VAF_Column c ON (t.VAF_TableView_ID=c.VAF_TableView_ID) "
        + "WHERE t.TableName=@TableName AND c.ColumnName='VAB_BusinessPartner_ID' ";

            queryList.VIS_102 = "UPDATE VAF_Print_Rpt_Layout SET IsDefault='N' WHERE IsDefault='Y' AND VAF_TableView_ID=@vaf_tableview_ID AND VAF_Tab_ID=@VAF_Tab_ID";

            queryList.VIS_103 = "UPDATE VAF_Print_Rpt_Layout SET IsDefault='Y' WHERE VAF_Print_Rpt_Layout_ID=@id";

            queryList.VIS_104 = "SELECT cc.ColumnName "
            + "FROM VAF_Column c"
            + " INNER JOIN VAF_CtrlRef_Table r ON (c.VAF_Control_Ref_Value_ID=r.VAF_Control_Ref_ID)"
            + " INNER JOIN VAF_Column cc ON (r.Column_Key_ID=cc.VAF_Column_ID) "
            + "WHERE c.VAF_Control_Ref_ID IN (18,30)" 	//	Table/Search
            + " AND c.ColumnName=@colName";

            queryList.VIS_105 = "SELECT t.TableName "
            + "FROM VAF_Column c"
            + " INNER JOIN VAF_TableView t ON (c.VAF_TableView_ID=t.VAF_TableView_ID) "
            + "WHERE c.ColumnName=@colName AND IsKey='Y'"		//	#1 Link Column
            + " AND EXISTS (SELECT * FROM VAF_Column cc"
            + " WHERE cc.VAF_TableView_ID=t.VAF_TableView_ID AND cc.ColumnName=@tabKeyColumn)";	//	#2 Tab Key Column

            queryList.VIS_106 = "SELECT VAF_Control_Ref_ID FROM VAF_Column WHERE ColumnName=@colName";	//	#2 Tab Key Column

            queryList.VIS_107 = "SELECT  VAF_UserSearch.Code,VAF_DefaultUserQuery.VAF_UserContact_id,VAF_DefaultUserQuery.vaf_tab_id FROM VAF_UserSearch VAF_UserSearch JOIN VAF_DefaultUserQuery VAF_DefaultUserQuery ON VAF_UserSearch.VAF_UserSearch_ID=VAF_DefaultUserQuery.VAF_UserSearch_ID WHERE VAF_UserSearch.IsActive                 ='Y'" +
             "AND VAF_DefaultUserQuery.VAF_UserContact_ID=@VAF_UserContact_ID AND VAF_UserSearch.VAF_Client_ID =@VAF_Client_ID AND (VAF_UserSearch.VAF_Tab_ID =@VAF_Tab_ID OR VAF_UserSearch.VAF_TableView_ID                 = @VAF_TableView_ID)";

            queryList.VIS_108 = "SELECT Record_ID "
            + "FROM VAF_Private_Rights "
            + "WHERE VAF_UserContact_ID=@VAF_UserContact_ID AND VAF_TableView_ID=@VAF_TableView_ID AND IsActive='Y' "
            + "ORDER BY Record_ID";

            queryList.VIS_109 = "SELECT VACM_Subscribe_ID, Record_ID FROM VACM_Subscribe WHERE VAF_UserContact_ID=@VAF_UserContact_ID AND VAF_TableView_ID=@VAF_TableView_ID";

            queryList.VIS_110 = "SELECT vadms_windowdoclink_id,record_id FROM vadms_windowdoclink wdl JOIN vadms_document doc "
                 + " ON wdl.VADMS_Document_ID  =doc.VADMS_Document_ID  WHERE doc.vadms_docstatus!='DD' AND vaf_tableview_id=@vaf_tableview_id";

            queryList.VIS_111 = "SELECT VACM_Chat_ID, Record_ID FROM VACM_Chat WHERE VAF_TableView_ID=@VAF_TableView_ID";

            queryList.VIS_112 = "SELECT distinct att.VAF_Attachment_ID, att.Record_ID FROM VAF_Attachment att"
               + " INNER JOIN VAF_Attachmentline al ON (al.VAF_Attachment_id=att.VAF_Attachment_id)"
               + " WHERE att.VAF_TableView_ID=@VAF_TableView_ID";

            queryList.VIS_113 = "SELECT VAF_ExportData_ID, Record_ID FROM VAF_ExportData WHERE VAF_TableView_ID=@VAF_TableView_ID";

            queryList.VIS_114 = "SELECT  CASE WHEN length(VAF_UserSearch.Name)>25 THEN substr(VAF_UserSearch.name ,0,25)||'...' ELSE VAF_UserSearch.Name END AS Name,VAF_UserSearch.Name as title, VAF_UserSearch.Code, VAF_UserSearch.VAF_UserSearch_ID, VAF_UserSearch.VAF_UserContact_ID, VAF_UserSearch.VAF_Tab_ID, "
+ " case  WHEN VAF_UserSearch.VAF_UserSearch_ID IN (Select VAF_UserSearch_ID FROM VAF_DefaultUserQuery WHERE VAF_DefaultUserQuery.VAF_Tab_ID=@VAF_Tab_ID AND VAF_DefaultUserQuery.VAF_UserContact_ID=@VAF_UserContact_ID  )  "
+ "then (Select VAF_DefaultUserQuery_ID FROM VAF_DefaultUserQuery  WHERE VAF_DefaultUserQuery.VAF_Tab_ID=@VAF_Tab_ID1 AND VAF_DefaultUserQuery.VAF_UserContact_ID=@VAF_UserContact_ID1 )   ELSE null End as VAF_DefaultUserQuery_ID"
       + " FROM VAF_UserSearch VAF_UserSearch WHERE VAF_UserSearch.VAF_Client_ID       =@VAF_Client_ID AND VAF_UserSearch.IsActive             ='Y' "
       + " AND VAF_UserSearch.VAF_Tab_ID           =@VAF_Tab_ID2 AND VAF_UserSearch.VAF_TableView_ID           =@VAF_TableView_ID"
       + " AND lower(VAF_UserSearch.Name) like lower('%'||@queryData||'%')"
       + " ORDER BY Upper(VAF_UserSearch.NAME), VAF_UserSearch.VAF_UserSearch_ID";


            queryList.VIS_115 = "SELECT count(*) "
            + " FROM VAF_UserSearch VAF_UserSearch LEFT OUTER JOIN VAF_DefaultUserQuery VAF_DefaultUserQuery ON VAF_DefaultUserQuery.VAF_UserSearch_ID=VAF_UserSearch.VAF_UserSearch_ID WHERE"
                               + " VAF_UserSearch.VAF_Client_ID=@VAF_Client_ID AND VAF_UserSearch.IsActive='Y'"
                               + " AND (VAF_UserSearch.VAF_Tab_ID=@VAF_Tab_ID AND VAF_UserSearch.VAF_TableView_ID=@VAF_TableView_ID)"
                               + " ORDER BY VAF_UserSearch.VAF_UserSearch_ID";



            queryList.VIS_116 = "SELECT  CASE WHEN length(VAF_UserSearch.Name)>25 THEN substr(VAF_UserSearch.name ,0,25)||'...' ELSE VAF_UserSearch.Name END AS Name,VAF_UserSearch.Name as title, VAF_UserSearch.Code, VAF_UserSearch.VAF_UserSearch_ID, VAF_UserSearch.VAF_UserContact_ID, VAF_UserSearch.VAF_Tab_ID, "
+ " case  WHEN VAF_UserSearch.VAF_UserSearch_ID IN (Select VAF_UserSearch_ID FROM VAF_DefaultUserQuery WHERE VAF_DefaultUserQuery.VAF_Tab_ID=@VAF_Tab_ID AND VAF_DefaultUserQuery.VAF_UserContact_ID=@VAF_UserContact_ID  )  "
+ "then (Select VAF_DefaultUserQuery_ID FROM VAF_DefaultUserQuery  WHERE VAF_DefaultUserQuery.VAF_Tab_ID=@VAF_Tab_ID1 AND VAF_DefaultUserQuery.VAF_UserContact_ID=@VAF_UserContact_ID1 )   ELSE null End as VAF_DefaultUserQuery_ID"
       + " FROM VAF_UserSearch VAF_UserSearch WHERE VAF_UserSearch.VAF_Client_ID       =@VAF_Client_ID AND VAF_UserSearch.IsActive             ='Y' "
       + " AND VAF_UserSearch.VAF_Tab_ID           =@VAF_Tab_ID2 AND VAF_UserSearch.VAF_TableView_ID           =@VAF_TableView_ID"
       + " ORDER BY Upper(VAF_UserSearch.NAME), VAF_UserSearch.VAF_UserSearch_ID";


            queryList.VIS_117 = "SELECT  CASE WHEN length(VAF_UserSearch.Name)>25 THEN substr(VAF_UserSearch.name ,0,25)||'...' ELSE VAF_UserSearch.Name END AS Name,VAF_UserSearch.Name as title, VAF_UserSearch.Code, VAF_UserSearch.VAF_UserSearch_ID, VAF_UserSearch.VAF_UserContact_ID, VAF_UserSearch.VAF_Tab_ID, "
+ " case  WHEN VAF_UserSearch.VAF_UserSearch_ID IN (Select VAF_UserSearch_ID FROM VAF_DefaultUserQuery WHERE VAF_DefaultUserQuery.VAF_Tab_ID=@VAF_Tab_ID AND VAF_DefaultUserQuery.VAF_UserContact_ID=@VAF_UserContact_ID  )  "
+ "then (Select VAF_DefaultUserQuery_ID FROM VAF_DefaultUserQuery  WHERE VAF_DefaultUserQuery.VAF_Tab_ID=@VAF_Tab_ID1 AND VAF_DefaultUserQuery.VAF_UserContact_ID=@VAF_UserContact_ID1 )   ELSE null End as VAF_DefaultUserQuery_ID"
       + " FROM VAF_UserSearch VAF_UserSearch WHERE VAF_UserSearch.VAF_Client_ID       =@VAF_Client_ID AND VAF_UserSearch.IsActive             ='Y' "
       + " AND VAF_UserSearch.VAF_Tab_ID           =@VAF_Tab_ID2 AND VAF_UserSearch.VAF_TableView_ID           =@VAF_TableView_ID"
       + " ORDER BY Upper(VAF_UserSearch.NAME), VAF_UserSearch.VAF_UserSearch_ID";


            queryList.VIS_118 = "select VAF_ReportLayout_id FROM VAF_Job WHERE VAF_Job_ID=@VAF_Job_ID";

            queryList.VIS_119 = "select versionno FROM VAF_ModuleInfo where Prefix='VAREPH_'";

            queryList.VIS_120 = "SELECT VAF_TreeInfo_ID FROM VAF_TreeInfo "
            + "WHERE VAF_Client_ID=@VAF_Client_ID AND VAF_TableView_ID=@VAF_TableView_ID AND IsActive='Y' AND IsAllNodes='Y' "
                        + "ORDER BY IsDefault DESC, VAF_TreeInfo_ID";

            queryList.VIS_121 = "SELECT VAF_TreeInfo_ID, Name FROM VAF_TreeInfo "
                    + "WHERE VAF_Client_ID=@VAF_Client_ID AND VAF_TableView_ID=@VAF_TableView_ID AND IsActive='Y' AND IsAllNodes='Y' "
                    + "ORDER BY IsDefault DESC, VAF_TreeInfo_ID";


            queryList.VIS_122 = "SELECT t.TableName, c.VAF_Column_ID, c.ColumnName, e.Name,"	//	1..4
            + "c.IsParent, c.IsKey, c.IsIdentifier, c.IsTranslated "				//	4..8
            + "FROM VAF_TableView t, VAF_Column c, VAF_ColumnDic e "
            + "WHERE t.VAF_TableView_ID=@VAF_TableView_ID"
            + " AND t.VAF_TableView_ID=c.VAF_TableView_ID"
            + " AND (c.VAF_Column_ID=@VAF_ColumnSortOrder_ID OR VAF_Column_ID=@VAF_ColumnSortYesNo_ID"  	//	#2..3
            + " OR c.IsParent='Y' OR c.IsKey='Y' OR c.IsIdentifier='Y')"
            + " AND c.VAF_ColumnDic_ID=e.VAF_ColumnDic_ID";

            queryList.VIS_123 = "SELECT t.TableName, c.VAF_Column_ID, c.ColumnName, et.Name,"	//	1..4
                + "c.IsParent, c.IsKey, c.IsIdentifier, c.IsTranslated "		//	4..8
                + "FROM VAF_TableView t, VAF_Column c, VAF_ColumnDic_TL et "
                + "WHERE t.VAF_TableView_ID=@VAF_TableView_ID" //	#1
                + " AND t.VAF_TableView_ID=c.VAF_TableView_ID"
                + " AND (c.VAF_Column_ID=@VAF_ColumnSortOrder_ID OR VAF_Column_ID=@VAF_ColumnSortYesNo_ID" //	#2..3
                + "	OR c.IsParent='Y' OR c.IsKey='Y' OR c.IsIdentifier='Y')"
                + " AND c.VAF_ColumnDic_ID=et.VAF_ColumnDic_ID"
                + " AND et.VAF_Language=@VAF_Language";


            queryList.VIS_124 = "SELECT * FROM C_ValidCombination WHERE C_ValidCombination_ID=@C_ValidCombination_ID AND VAB_AccountBook_ID=@VAB_AccountBook_ID";

            queryList.VIS_125 = "SELECT ColumnName FROM VAF_Column WHERE VAF_Column_ID = @VAF_Column_ID";

            queryList.VIS_126 = "SELECT TableName FROM VAF_TableView WHERE VAF_TableView_ID=@tblID_s";

            queryList.VIS_127 = "UPDATE C_ValidCombination SET Alias=NULL WHERE C_ValidCombination_ID=@IDvalue";

            queryList.VIS_128 = "UPDATE C_ValidCombination SET Alias=@f_alies WHERE C_ValidCombination_ID=@IDvalue";

            queryList.VIS_129 = "SELECT VAF_Screen_ID FROM VAF_Screen WHERE Name='All Requests'";

            queryList.VIS_130 = "select VAF_RecrodType_id, entitytype, name from VAF_RecrodType";

            queryList.VIS_131 = "SELECT COUNT(VAF_MODULEINFO_ID) FROM VAF_MODULEINFO WHERE PREFIX='VA009_'  AND IsActive = 'Y'";


            queryList.VIS_132 = "SELECT M_AttributeSet_ID FROM M_Product WHERE M_Product_ID =@M_Product_ID";

            queryList.VIS_133 = "SELECT "	//	Entered UOM
                //+ "l.QtyInvoiced-SUM(NVL(mi.Qty,0)),round(l.QtyEntered/l.QtyInvoiced,6),"
            + "round((l.QtyInvoiced-SUM(COALESCE(mi.Qty,0))) * "					//	1               
            + "(CASE WHEN l.QtyInvoiced=0 THEN 0 ELSE l.QtyEntered/l.QtyInvoiced END ),2) as QUANTITY,"	//	2
            + "round((l.QtyInvoiced-SUM(COALESCE(mi.Qty,0))) * "					//	1               
            + "(CASE WHEN l.QtyInvoiced=0 THEN 0 ELSE l.QtyEntered/l.QtyInvoiced END ),2) as QTYENTER,"	//	2
            + " l.C_UOM_ID,COALESCE(uom.UOMSymbol,uom.Name),"			//  3..4
            + " l.M_Product_ID,p.Name, l.VAB_InvoiceLine_ID,l.Line,"      //  5..8
            + " l.C_OrderLine_ID "                					//  9
            + " FROM C_UOM uom  INNER JOIN VAB_InvoiceLine l ON (l.C_UOM_ID=uom.C_UOM_ID) "
            + " INNER JOIN M_Product p ON (l.M_Product_ID=p.M_Product_ID) LEFT OUTER JOIN M_MatchInv mi ON (l.VAB_InvoiceLine_ID=mi.VAB_InvoiceLine_ID) "
              + " WHERE l.VAB_Invoice_ID=@VAB_Invoice_ID GROUP BY l.QtyInvoiced,l.QtyEntered, l.C_UOM_ID,COALESCE(uom.UOMSymbol,uom.Name),"
                    + " l.M_Product_ID,p.Name, l.VAB_InvoiceLine_ID,l.Line,l.C_OrderLine_ID ORDER BY l.Line";

            queryList.VIS_134 = "SELECT "	//	Entered UOM
                //+ "l.QtyInvoiced-SUM(NVL(mi.Qty,0)),round(l.QtyEntered/l.QtyInvoiced,6),"
            + "round((l.QtyInvoiced-SUM(COALESCE(mi.Qty,0))) * "					//	1               
            + "(CASE WHEN l.QtyInvoiced=0 THEN 0 ELSE l.QtyEntered/l.QtyInvoiced END ),2) as QUANTITY,"	//	2
            + "round((l.QtyInvoiced-SUM(COALESCE(mi.Qty,0))) * "					//	1               
            + "(CASE WHEN l.QtyInvoiced=0 THEN 0 ELSE l.QtyEntered/l.QtyInvoiced END ),2) as QTYENTER,"	//	2
            + " l.C_UOM_ID,COALESCE(uom.UOMSymbol,uom.Name),"			//  3..4
            + " l.M_Product_ID,p.Name, l.VAB_InvoiceLine_ID,l.Line,"      //  5..8
            + " l.C_OrderLine_ID FROM C_UOM_Trl uom INNER JOIN VAB_InvoiceLine l ON (l.C_UOM_ID=uom.C_UOM_ID AND uom.VAF_Language=@VAF_Language) INNER JOIN M_Product p ON (l.M_Product_ID=p.M_Product_ID) "
           + " LEFT OUTER JOIN M_MatchInv mi ON (l.VAB_InvoiceLine_ID=mi.VAB_InvoiceLine_ID) WHERE l.VAB_Invoice_ID=@VAB_Invoice_ID GROUP BY l.QtyInvoiced,l.QtyEntered,"
            + " l.C_UOM_ID,COALESCE(uom.UOMSymbol,uom.Name),"
                + " l.M_Product_ID,p.Name, l.VAB_InvoiceLine_ID,l.Line,l.C_OrderLine_ID  ORDER BY l.Line";


            queryList.VIS_135 = "SELECT PaymentRule FROM C_PaySelectionCheck WHERE C_PaySelection_ID = @pSelectID";

            queryList.VIS_136 = "select VAF_Job_id from VAF_Job where VAF_Print_Rpt_Layout_id = (select check_printformat_id from VAB_Bank_Acctdoc where VAB_Bank_Acct_id = (select VAB_Bank_Acct_id from c_payment where c_payment_id = (select c_payment_id from c_payselectioncheck where c_payselectioncheck_id = @check_ID)) and VAB_Bank_Acctdoc.isactive = 'Y' AND rownum =1)";

            queryList.VIS_137 = "select vaf_tableview_id from vaf_tableview where tablename = 'C_PaySelectionCheck'";

            queryList.VIS_138 = "SELECT VAF_Job_ID  FROM VAF_Tab WHERE VAF_Tab_ID = 330";

            queryList.VIS_139 = "select vaf_tableview_id from vaf_tableview where tablename = 'C_Payment'";

            queryList.VIS_140 = "SELECT M_InOut_ID FROM M_InOutLine WHERE M_InOutLine_ID=@lineID";

            queryList.VIS_141 = "SELECT M_Inventory_ID FROM M_InventoryLine WHERE M_InventoryLine_ID=@lineID";

            queryList.VIS_142 = "SELECT M_Movement_ID FROM M_MovementLine WHERE M_MovementLine_ID=@lineID";

            queryList.VIS_143 = "SELECT M_Production_ID FROM M_ProductionLine WHERE M_ProductionLine_ID=@lineID";


            queryList.VIS_144 = "SELECT Log_ID, P_ID, P_Date, P_Number, P_Msg "
                + "FROM VAF_JInstance_Log "
                + "WHERE VAF_JInstance_ID=@VAF_JInstance_ID "
                + " ORDER BY Log_ID";

            queryList.VIS_145 = "SELECT Count(VAF_ModuleInfo_ID) FROM VAF_ModuleInfo WHERE PREFIX='VA034_' AND IsActive = 'Y'";

            queryList.VIS_146 = "SELECT adt.TableName, adt.VAF_Screen_ID, adt.PO_Window_ID, " +
            "case when adwfa.VAF_Screen_ID is null then (select VAF_SCREEN_ID from VAF_WFlow_Task where VAF_WFlow_Handler_ID = (select VAF_WFlow_Handler_ID from VAF_WFlow_Task where VAF_WFlow_Task_ID = adwfa.VAF_WFlow_Task_ID) and VAF_SCREEN_ID is not null AND rownum = 1) else adwfa.VAF_Screen_ID end as ActivityWindow " +
            "FROM VAF_TableView adt " +
            "LEFT JOIN VAF_WFlow_Task adwfa on adt.VAF_TableView_ID = adwfa.VAF_TableView_ID " +
            "WHERE adt.VAF_TableView_ID = @VAF_TableView_ID and adwfa.VAF_WFlow_Task_ID = @VAF_WFlow_Task_ID";

            queryList.VIS_147 = "DELETE FROM VAF_UserSearchLine WHERE VAF_UserSearchLine_ID=@VAF_UserSearch_ID";

            queryList.VIS_148 = "SELECT l.Value, t.Name FROM VAF_CtrlRef_List l, VAF_CtrlRef_TL t "
                   + "WHERE l.VAF_CtrlRef_List_ID=t.VAF_CtrlRef_List_ID"
                   + " AND t.VAF_Language='" + Env.GetVAF_Language(ctx) + "'"
                   + " AND l.VAF_Control_Ref_ID=@VAF_Control_Ref_ID AND l.IsActive='Y'";

            queryList.VIS_149 = "SELECT IsCrystalReport FROM VAF_Job WHERE VAF_Job_ID=@VAF_Job_ID";

            queryList.VIS_150 = "select vaf_tableview_id from vaf_tableview where tablename = 'C_PaySelection'";


            queryList.VIS_151 = "select VAF_Job_ID from VAF_Job where name='VARPT_RemittancePrint'";

            queryList.VIS_152 = " SELECT VAF_Job_ID from VAB_Bank_AcctDoc WHERE VAB_Bank_Acct_ID=@BankAcct_ID AND rownum=1";
        }

        public static string GetQuery(string code, Ctx ctx)
        {
            //if (!isLoad)
            //{
            AddQueries(ctx);
            isLoad = true;
            //}

            object result = ((IDictionary<string, object>)queryList)[code];
            if (result != null)
            {
                return result.ToString();
            }
            return code;
        }



    }
}